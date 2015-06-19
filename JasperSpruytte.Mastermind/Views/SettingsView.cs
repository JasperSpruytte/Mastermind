using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.Properties;
using System;
using System.Windows.Forms;

namespace JasperSpruytte.MastermindWindows.Views
{
    public partial class SettingsView : Form
    {
        private GameView _frmMain;
        private Settings _settings;

        public SettingsView(GameView main)
        {
            InitializeComponent();
            _frmMain = main;
            _settings = Settings.Default;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            nudNumberOfTurns.Value = _settings.NumberOfTurns;
            nudNumberOfTurns.Minimum = Mastermind.MinimumNumberOfTurns;
            nudNumberOfTurns.Maximum = Mastermind.MaximumNumberOfTurns;
            nudNumberOfColors.Value = _settings.NumberOfColors;
            nudNumberOfColors.Minimum = Mastermind.MinimumNumberOfColors;
            nudNumberOfColors.Maximum = Mastermind.MaximumNumberOfColors;
            nudLengthOfSecretCode.Value = _settings.LengthOfSecretCode;
            nudLengthOfSecretCode.Minimum = Mastermind.MinimumLengthOfSecretCode;
            nudLengthOfSecretCode.Maximum = Mastermind.MaximumLengthOfSecretCode;
            if (_settings.UserIsGuessing)
                rbUser.Checked = true;
            else
                rbComputer.Checked = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.NumberOfTurns = (int)nudNumberOfTurns.Value;
            _settings.NumberOfColors = (int)nudNumberOfColors.Value;
            _settings.LengthOfSecretCode = (int)nudLengthOfSecretCode.Value;
            _settings.UserIsGuessing = rbUser.Checked;
            _settings.Save();
            _frmMain.StartNewGame();
            this.Close();
        }
    }
}
