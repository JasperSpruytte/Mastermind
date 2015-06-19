using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.Presenters;
using JasperSpruytte.MastermindWindows.Properties;
using JasperSpruytte.MastermindWindows.SavingLoading;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace JasperSpruytte.MastermindWindows.Views
{
    public partial class GameView : Form, IGameView
    {
        private Mastermind _mastermind;
        private MastermindAIPlayer _computerPlayer;
        private IMastermindSettings mastermindSettings;
        private MastermindDAL _mastermindDAL;
        private const int ColorDimension = 25;
        private const int FeedbackDimension = 5;
        private const int SpaceBetweenColors = 6;
        private const int SpaceBetweenFeedback = 26;
        private const int HorizontalSpaceBetweenLabels = 6;
        private const int SpaceFromTopForColorLabels = 15;
        private const int SpaceBetweenGroupboxes = 5;
        private GameViewPresenter presenter;
        
        public GameView()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _mastermindDAL = new MastermindDAL();
            ShowSavedGamesMenu();
            StartNewGame();
        }

        public void StartNewGame()
        {
            mastermindSettings = new MastermindSettings();
            _computerPlayer = null;
            SetLocationOfBtnAdvance(0);
            SetupGame(mastermindSettings.NumberOfTurns, mastermindSettings.NumberOfColors, mastermindSettings.LengthOfSecretCode, mastermindSettings.UserIsGuessing, 0, false);
            if (mastermindSettings.UserIsGuessing)
            {
                SetUpBtnGuess();
                _mastermind = new Mastermind(mastermindSettings.NumberOfTurns, mastermindSettings.NumberOfColors, mastermindSettings.LengthOfSecretCode);
            }
            else
            {
                btnAdvance.Text = "Start";
                btnAdvance.Click += btnStart_Click;
                _mastermind = null;
                _computerPlayer = null;
            }
            presenter = new GameViewPresenter(this, mastermindSettings);
            presenter.StartNewGame();
            
            btnAdvance.Enabled = true;
            this.Refresh();
        }

        private void SetupGame(int numberOfTurns, int numberOfColors, int lengthOfSecretCode, bool userIsGuessing, int turn, bool gameOVer)
        {
            InitializeAvailableColorsGroupbox(numberOfColors);
            InitializeGameboardGroupbox(numberOfTurns, lengthOfSecretCode, userIsGuessing);
            InitializeFeedbackGroupbox(numberOfTurns, lengthOfSecretCode);
            InitializeSecretCodeGroupbox(lengthOfSecretCode, userIsGuessing);
            SetTurn(lengthOfSecretCode, userIsGuessing, turn, gameOVer);
            this.Width = ((btnAdvance.Right > grbAvailableColors.Right) ? btnAdvance.Right : grbAvailableColors.Right) + 40;
            this.Height = grbSecretCode.Bottom + 50;
        }

        private void InitializeFeedbackGroupbox(int numberOfTurns, int lengthOfSecretCode)
        {
            InitializeLabelGroupBox(grbFeedback, lengthOfSecretCode, numberOfTurns, grbGameboard.Right + SpaceBetweenGroupboxes, grbGameboard.Top, SpaceFromTopForColorLabels + ColorDimension / 3, FeedbackDimension, SpaceBetweenFeedback);

            foreach (Control control in grbFeedback.Controls)
            {
                Label lblFeedback = control as Label;
                lblFeedback.Enabled = false;
                lblFeedback.MouseDown += lblFeedback_MouseDown;
            }
            
        }

        private void InitializeAvailableColorsGroupbox(int numberOfColors)
        {
            InitializeLabelGroupBox(grbAvailableColors, numberOfColors, 1, grbAvailableColors.Left, grbAvailableColors.Top, SpaceFromTopForColorLabels, ColorDimension, SpaceBetweenColors);
            Label[,] availableColorsArray = grbAvailableColors.Tag as Label[,];
            for (int pegIndex = 0; pegIndex < numberOfColors; pegIndex++)
            {
                ColorPeg peg = new ColorPeg(pegIndex + 1);
                ChangeLabelColor(availableColorsArray[pegIndex, 0], peg);
                availableColorsArray[pegIndex, 0].MouseDown += lblAvailableColor_MouseDown;
            }
        }

        private void InitializeGameboardGroupbox(int numberOfTurns, int lengthOfSecretCode, bool userIsGuessing)
        {
            InitializeLabelGroupBox(grbGameboard, lengthOfSecretCode, numberOfTurns, grbGameboard.Left, grbAvailableColors.Bottom + SpaceBetweenGroupboxes, SpaceFromTopForColorLabels, ColorDimension, SpaceBetweenColors);

            if (userIsGuessing)
            {
                foreach (object control in grbGameboard.Controls)
                {
                    Label lblColor = control as Label;
                    lblColor.Enabled = false;
                    lblColor.AllowDrop = true;
                    lblColor.MouseDown += lblColor_MouseDown;
                    lblColor.DragEnter += lblColor_DragEnter;
                    lblColor.DragDrop += lblColor_DragDrop;
                }
            }
        }

        private void InitializeSecretCodeGroupbox(int lengthOfSecretCode, bool userIsGuessing)
        {
            InitializeLabelGroupBox(grbSecretCode, lengthOfSecretCode, 1, grbGameboard.Left, grbGameboard.Bottom + SpaceBetweenGroupboxes, SpaceFromTopForColorLabels, ColorDimension, SpaceBetweenColors);
            if (!userIsGuessing)
            {
                foreach (object control in grbSecretCode.Controls)
                {
                    Label lblColor = control as Label;
                    lblColor.AllowDrop = true;
                    lblColor.MouseDown += lblColor_MouseDown;
                    lblColor.DragEnter += lblColor_DragEnter;
                    lblColor.DragDrop += lblColor_DragDrop;
                    EnableColorLabel(lblColor);
                }
            }
        }



        private void InitializeLabelGroupBox(GroupBox groupbox, int numberOfColumns, int numberOfRows, int x, int y, int spaceFromTop, int labelDimension, int verticalSpaceBetweenLabels)
        {

            groupbox.Controls.Clear();
            Label[,] groupBoxArray = new Label[numberOfColumns, numberOfRows];
            groupbox.Width = CalculateDimension(HorizontalSpaceBetweenLabels, numberOfColumns, labelDimension, HorizontalSpaceBetweenLabels);
            groupbox.Height = CalculateDimension(spaceFromTop, numberOfRows, labelDimension, verticalSpaceBetweenLabels);
            groupbox.Location = new Point(x, y);

            for (byte columnNr = 0; columnNr < numberOfColumns; columnNr++)
            {
                for (byte rowNr = 0; rowNr < numberOfRows; rowNr++)
                {
                    Label lblColor = CreateLabel(columnNr, rowNr, labelDimension, HorizontalSpaceBetweenLabels, verticalSpaceBetweenLabels, spaceFromTop);
                    groupbox.Controls.Add(lblColor);
                    groupBoxArray[columnNr, rowNr] = lblColor;
                }
            }

            groupbox.Tag = groupBoxArray;
        }

        private Label CreateLabel(int columnNumber, int rowNumber, int labelDimension, int horizontalSpaceBetweenLabels, int verticalSpaceBetweenLabels, int spaceFromTop)
        {
            Label label = new Label();
            ColorPeg peg = new ColorPeg(0);
            label.Width = labelDimension;
            label.Height = labelDimension;
            ChangeLabelColor(label, peg);
            int labelX = CalculateDimension(horizontalSpaceBetweenLabels, columnNumber, labelDimension, horizontalSpaceBetweenLabels);
            int labelY = CalculateDimension(spaceFromTop, rowNumber, labelDimension, verticalSpaceBetweenLabels);
            label.Location = new Point(labelX, labelY);

            return label;
        }

        private int CalculateDimension(int initialSpace, int numberOfLabels, int labelDimension, int spaceBetweenLabels)
        {
            return initialSpace + numberOfLabels * (labelDimension + spaceBetweenLabels);
        }

        private void lblColor_MouseDown(object sender, MouseEventArgs e)
        {
            Label lblColor = sender as Label;
            ColorPeg peg =  new ColorPeg((int)lblColor.Tag);
            
            int modifier = 0;
            if (e.Button == MouseButtons.Left)
                modifier = 1;
            else if (e.Button == MouseButtons.Right)
                modifier = -1;

            int newPegNumber = peg.Number + modifier;
            if (newPegNumber > mastermindSettings.NumberOfColors)
                newPegNumber = 1;
            else if (newPegNumber < 1)
                newPegNumber = mastermindSettings.NumberOfColors;

            ChangeLabelColor(lblColor, new ColorPeg(newPegNumber));
        }

        private void lblAvailableColor_MouseDown(object sender, MouseEventArgs e)
        {
            Label lblAvailableColor = sender as Label;
            lblAvailableColor.DoDragDrop(lblAvailableColor.Tag, DragDropEffects.Copy);
        }

        private void lblColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ColorPeg)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void lblColor_DragDrop(object sender, DragEventArgs e)
        {
            Label lblColor = sender as Label;
            ColorPeg peg = e.Data.GetData(typeof(ColorPeg)) as ColorPeg;
            ChangeLabelColor(lblColor, peg.Number, peg.Color);
        }

        private void ChangeLabelColor(Label label, int colorNumber, Color color)
        {
            label.BackColor = color;
            label.Tag = colorNumber;
        }

        private void ChangeLabelColor(Label label, ColorPeg peg)
        {
            ChangeLabelColor(label, peg.Number, peg.Color);
        }

        private void SetTurn(int lengthOfSecretCode, bool userIsGuessing, int turn, bool gameOver)
        {       
            Label[,] gameboard = grbGameboard.Tag as Label[,];
            Label[,] feedback = grbFeedback.Tag as Label[,];

            for (int column = 0; column < lengthOfSecretCode; column++)
            {
                
                if (!userIsGuessing)
                {
                    feedback[column, turn].Enabled = true;
                }
                if (!gameOver)
                {
                    if (userIsGuessing)
                    {
                        EnableColorLabel(gameboard[column, turn]);
                    }
                    if (turn != 0)
                    {
                        gameboard[column, turn - 1].Enabled = false;
                        feedback[column, turn - 1].Enabled = false;
                        if (!userIsGuessing)
                            feedback[column, turn + 1].Enabled = true;
                    }
                }
            }
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            
            try
            {
                ColorSequence guess = GetMostRecentGuess();
                presenter.AdvanceTurn(guess);
                int previousTurn = _mastermind.CurrentTurn - 1;
                ShowFeedback(_mastermind.AllFeedback[previousTurn], previousTurn);
                SetTurn(_mastermind.LengthOfSecretCode, mastermindSettings.UserIsGuessing, _mastermind.CurrentTurn, _mastermind.GameOver);
                
                if (_mastermind.GameOver) 
                    EndGame();
                else
                    SetLocationOfBtnAdvance(_mastermind.CurrentTurn);
                
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void ShowFeedback(Feedback feedback, int turn)
        {
            Label[,] feedbackArray = grbFeedback.Tag as Label[,];
            for (int correctPlaceIndex = 0; correctPlaceIndex < feedback.PegsInTheRightPlace; correctPlaceIndex++)
            {
                feedbackArray[correctPlaceIndex, turn].BackColor = Color.White;
            }
            for (int correctColorIndex = 0; correctColorIndex < feedback.PegsWithCorrectColorInTheWrongPlace; correctColorIndex++)
            {
                feedbackArray[correctColorIndex + feedback.PegsInTheRightPlace, turn].BackColor = Color.Black;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsView settings = new SettingsView(this);
            settings.ShowDialog();
        }

        private ColorSequence GetMostRecentGuess()
        {
            ColorSequence guess = new ColorSequence(_mastermind.LengthOfSecretCode);
            Label[,] gameboard = grbGameboard.Tag as Label[,];

            for (int colorIndex = 0; colorIndex < _mastermind.LengthOfSecretCode; colorIndex++)
            {
                guess[colorIndex] =  new ColorPeg((int)gameboard[colorIndex, _mastermind.CurrentTurn].Tag);
            }

            return guess;
        }

        private void ShowGuess(ColorSequence guess, int turn)
        {
            Label[,] gameboard = grbGameboard.Tag as Label[,];

            for (int pegIndex = 0; pegIndex < guess.Length; pegIndex++)
            {
                Label lblPeg = gameboard[pegIndex, turn];
                ChangeLabelColor(lblPeg, guess[pegIndex]);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Settings settings = Properties.Settings.Default;

            try
            {
                Label[,] secretCodeLabel = grbSecretCode.Tag as Label[,];
                ColorSequence secretCode = new ColorSequence(settings.LengthOfSecretCode);
                for (int pegIndex = 0; pegIndex < settings.LengthOfSecretCode; pegIndex++)
                {
                    ColorPeg peg = new ColorPeg((int)secretCodeLabel[pegIndex, 0].Tag);
                    secretCode[pegIndex] = peg;
                }

                _mastermind = new Mastermind(settings.NumberOfTurns, settings.NumberOfColors, secretCode);
                _computerPlayer = new MastermindAIPlayer(_mastermind);

                _computerPlayer.MakeGuess();
                ColorSequence guess = _mastermind.Guesses[0];
                ShowGuess(guess, 0);
                btnAdvance.Click -= btnStart_Click;
                SetUpBtnGiveFeedback();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private void btnGiveFeedback_Click(object sender, EventArgs e)
        {
            Feedback feedback = GetMostRecentFeedback();

            try
            {
                _computerPlayer.ReceiveFeedback(feedback);
                if (!_mastermind.GameOver)
                {
                    ColorSequence newGuess = _computerPlayer.MakeGuess();
                    ShowGuess(newGuess, _mastermind.CurrentTurn - 1);
                }
                else
                {
                    EndGame();
                }
                SetTurn(mastermindSettings.LengthOfSecretCode, mastermindSettings.UserIsGuessing, _mastermind.CurrentTurn - 1, _mastermind.GameOver);
                SetLocationOfBtnAdvance(_mastermind.CurrentTurn - 1);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        private Feedback GetMostRecentFeedback()
        {
            Label[,] feedback = grbFeedback.Tag as Label[,];
            int correctPlace = 0;
            int wrongPlace = 0;

            for (int feedbackIndex = 0; feedbackIndex < _mastermind.LengthOfSecretCode; feedbackIndex++)
            {
                Label lblFeedbackPeg = feedback[feedbackIndex, _mastermind.CurrentTurn - 1];
                if (lblFeedbackPeg.BackColor == Color.White)
                    correctPlace++;
                if (lblFeedbackPeg.BackColor == Color.Black)
                    wrongPlace++;
            }

            return new Feedback(correctPlace, wrongPlace);
        }

        private void lblFeedback_MouseDown(object sender, MouseEventArgs e)
        {
            Label lblPeg = sender as Label;
            Color[] colors = { Color.Gray, Color.White, Color.Black };

            int modifier = 0;
            if (e.Button == MouseButtons.Left)
                modifier = 1;
            else if (e.Button == MouseButtons.Right)
                modifier = -1;

            int colorNumber = (int)lblPeg.Tag + modifier;
            if (colorNumber > colors.GetUpperBound(0))
                colorNumber = 0;
            else if (colorNumber < 0)
                colorNumber = colors.GetUpperBound(0);

            ChangeLabelColor(lblPeg, colorNumber, colors[colorNumber]);
        }

        private void RemoveClickEventFromBtnAdvance()
        {
            FieldInfo fieldInfo = typeof(Control).GetField("EventClick", BindingFlags.Static | BindingFlags.NonPublic);
            object obj = fieldInfo.GetValue(btnAdvance);
            PropertyInfo propertyInfo = btnAdvance.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            EventHandlerList list = (EventHandlerList)propertyInfo.GetValue(btnAdvance, null);
            list.RemoveHandler(obj, list[obj]);
        }

        private void SetLocationOfBtnAdvance(int turn)
        {
            btnAdvance.Location = new Point(grbFeedback.Right + 10, grbGameboard.Top + SpaceFromTopForColorLabels + turn * (ColorDimension + HorizontalSpaceBetweenLabels));
        }

        private void EndGame()
        {
            btnAdvance.Enabled = false;
            bool userWon = mastermindSettings.UserIsGuessing == _mastermind.PlayerWon;
            string message = (userWon) ? "You win!" : "You lose!";
            MessageBox.Show(message);
        }

        private void EnableColorLabel(Label lblColor)
        {
            lblColor.Enabled = true;
            lblColor.BackColor = Color.DarkGray;
        }

        private void tsmiSave_Click(object sender, EventArgs e)
        {
            _mastermindDAL.Save(_mastermind.CreateMemento(mastermindSettings.UserIsGuessing));
            ShowSavedGamesMenu();
        }

        private void ShowSavedGamesMenu()
        {
            ToolStripItemCollection loadGames = tsmiSavedGames.DropDownItems;
            loadGames.Clear();

            foreach (MastermindMemento memento in _mastermindDAL.Mementos)
            {
                
                string text = memento.CreatedOn + " Turn " + (memento.CurrentTurn + 1) + " out of " + memento.NumberOfTurns;
                ToolStripMenuItem tsmiMemento = new ToolStripMenuItem(text);

                ToolStripMenuItem tsmiLoad = new ToolStripMenuItem("Load");
                tsmiLoad.Tag = memento;
                tsmiLoad.Click += tsmiMemento_Click;
                tsmiMemento.DropDownItems.Add(tsmiLoad);

                ToolStripMenuItem tsmiDelete = new ToolStripMenuItem("Delete");
                tsmiDelete.Tag = memento;
                tsmiDelete.Click += tsmiDelete_Click;
                tsmiMemento.DropDownItems.Add(tsmiDelete);

                loadGames.Add(tsmiMemento);
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiDelete = sender as ToolStripMenuItem;
            MastermindMemento gameToDelete = tsmiDelete.Tag as MastermindMemento;
            _mastermindDAL.Delete(gameToDelete);
            ShowSavedGamesMenu();
        }

        private void tsmiMemento_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmiMemento = sender as ToolStripMenuItem;
            MastermindMemento memento = tsmiMemento.Tag as MastermindMemento;
            _mastermind = new Mastermind(memento);
            int turnBtnAdvance = (memento.UserIsGuessing) ? memento.CurrentTurn : memento.CurrentTurn - 1;
            SetLocationOfBtnAdvance(turnBtnAdvance);
            if (!memento.UserIsGuessing)
            {
                _computerPlayer = new MastermindAIPlayer(_mastermind);
                SetUpBtnGiveFeedback();
            }
            else
            {
                _computerPlayer = null;
                SetUpBtnGuess();
            }

            SetupGame(memento.NumberOfTurns, memento.NumberOfColors, memento.LengthOfSecretCode, memento.UserIsGuessing, memento.CurrentTurn, false);
            for (int turn = 0; turn < _mastermind.Guesses.Count; turn++)
            {
                ShowGuess(_mastermind.Guesses[turn], turn);
                ShowFeedback(_mastermind.AllFeedback[turn], turn);
            }
            
        }

        private void SetUpBtnGuess()
        {
            RemoveClickEventFromBtnAdvance();
            btnAdvance.Text = "Guess";
            btnAdvance.Click += btnGuess_Click;
        }

        private void SetUpBtnGiveFeedback()
        {
            btnAdvance.Text = "Give feedback";
            btnAdvance.Click += btnGiveFeedback_Click;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout about = new frmAbout();
            about.ShowDialog();
        }

        public void DisableSaving()
        {
            tsmiSave.Enabled = false;
        }

        public void EnableSaving()
        {
            tsmiSave.Enabled = true;
        }

        public void ShowSecretCode()
        {
            ColorSequence secretCode = _mastermind.GetSecretCode();
            Label[,] secretCodeLabel = grbSecretCode.Tag as Label[,];
            for (int colorNumber = 0; colorNumber < _mastermind.LengthOfSecretCode; colorNumber++)
            {
                secretCodeLabel[colorNumber, 0].BackColor = secretCode[colorNumber].Color;
            }
        }
    }
}
