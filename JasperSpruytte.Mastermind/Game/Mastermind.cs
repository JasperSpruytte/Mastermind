using JasperSpruytte.MastermindWindows.SavingLoading;
using System;
using System.Collections.Generic;

namespace JasperSpruytte.MastermindWindows.Game
{
    public class Mastermind
    {
        #region Constructors

        public Mastermind(IMastermindSettings settings, ColorSequence secretCode = null)
        {
            Settings = settings;
            CheckIfSettingsWithinRange();
            CurrentTurn = 0;
            if (secretCode != null)
            {
                SecretCode = secretCode;
                if (SecretCode.Contains(0))
                    throw new ArgumentException("The secret code is not completely filled in.");    
            }
            else
            {
                ISecretCodeGenerator secretCodeGenerator = new RandomPatternGenerator();
                SecretCode = secretCodeGenerator.GenerateSecretCode(NumberOfColors, LengthOfSecretCode);
            }
            
            AllFeedback = new List<Feedback>(NumberOfTurns);
            Guesses = new List<ColorSequence>(NumberOfTurns);
        }

        public Mastermind(MastermindMemento memento)
        {
            Settings = new MastermindSettings();
            Settings.NumberOfTurns = memento.NumberOfTurns;
            Settings.NumberOfColors = memento.NumberOfColors;
            Settings.LengthOfSecretCode = memento.LengthOfSecretCode;
            CurrentTurn = memento.CurrentTurn;
            SecretCode = new ColorSequence(memento.SecretCode);
            AllFeedback = new List<Feedback>();
            for (int feedbackIndex = 0; feedbackIndex < memento.Feedback.Count; feedbackIndex++)
            {
                AllFeedback.Add(new Feedback(memento.Feedback[feedbackIndex]));
            }
            Guesses = new List<ColorSequence>();
            for (int i = 0; i < memento.Guesses.Count; i++)
            {
                Guesses.Add(new ColorSequence(memento.Guesses[i]));
            }
        }

        #endregion Constructors

        #region Fields
        
        private DateTime _lastMementoCreatedOn = DateTime.MinValue;

        #endregion Fields

        #region Properties

        public int NumberOfTurns
        {
            get { return Settings.NumberOfTurns; }
        }

        public int NumberOfColors
        {
            get { return Settings.NumberOfColors; }
        }

        public int LengthOfSecretCode
        {
            get { return Settings.LengthOfSecretCode; }
        }

        public int CurrentTurn
        {
            get;
            private set;
        }

        private ColorSequence SecretCode { get; set; }
        public bool GameOver { get; private set; }
        public bool PlayerWon { get; private set; }
        public List<Feedback> AllFeedback { get; private set; }
        public List<ColorSequence> Guesses { get; private set; }
        public IMastermindSettings Settings { get; private set; }

        #endregion Properties

        #region Behavior

        public Feedback Guess(ColorSequence guess)
        {
            if (GameOver)
                throw new InvalidOperationException("You cannot make a guess after the end of the game.");

            CheckIfGuessIsValid(guess);
            Feedback feedback = SecretCode.Evaluate(guess);
            AllFeedback.Add(feedback);
            Guesses.Add(guess);
            CurrentTurn++;
            CheckIfGameIsOver(feedback);
            return feedback;
        }

        public ColorSequence GetSecretCode()
        {
            if (!GameOver)
                throw new InvalidOperationException("You cannot access the secret game while the game is still going on.");

            return SecretCode;
        }

        private void CheckIfGameIsOver(Feedback latestFeedback)
        {
            if (CurrentTurn == NumberOfTurns)
                GameOver = true;
            if (latestFeedback.PegsInTheRightPlace == LengthOfSecretCode)
            {
                PlayerWon = true;
                GameOver = true;
            }
        }

        private void CheckIfSettingsWithinRange()
        {
            if (NumberOfTurns < Settings.MinimumNumberOfTurns || NumberOfTurns > Settings.MaximumNumberOfTurns)
            {
                throw new ArgumentOutOfRangeException("The number of colors has to be between " + Settings.MinimumNumberOfTurns + " and " + Settings.MaximumNumberOfTurns + ".");
            }
            if (NumberOfColors < Settings.MinimumNumberOfColors || NumberOfColors > Settings.MaximumNumberOfColors)
            {
                throw new ArgumentOutOfRangeException("The number of colors has to be between " + Settings.MinimumNumberOfColors + " and " + Settings.MaximumNumberOfColors + ".");
            }
            if (LengthOfSecretCode < Settings.MinimumLengthOfSecretCode || LengthOfSecretCode > Settings.MaximumLengthOfSecretCode)
            {
                throw new ArgumentOutOfRangeException("The length of the secret code has to be between " + Settings.MinimumLengthOfSecretCode + " and " + Settings.MaximumLengthOfSecretCode + ".");
            }
        }

        private void CheckIfGuessIsValid(ColorSequence guess)
        {
            if (guess.Contains(0))
            {
                throw new ArgumentException("Please fill in the entire color pattern.");
            }
            if (guess.Length != LengthOfSecretCode)
            {
                throw new ArgumentException("The guess needs to be of length " + LengthOfSecretCode + ".");
            }
        }

        public MastermindMemento CreateMemento(bool userIsGuessing)
        {
            List<string> guessesAsString = new List<string>();
            List<int[]> feedbackAsArrays = new List<int[]>();

            for (int pegIndex = 0; pegIndex < Guesses.Count; pegIndex++)
            {
                guessesAsString.Add(Guesses[pegIndex].ToString());
                feedbackAsArrays.Add(AllFeedback[pegIndex].ToArray());
            }

            return new MastermindMemento()
            {
                NumberOfTurns = NumberOfTurns,
                NumberOfColors = NumberOfColors,
                LengthOfSecretCode = LengthOfSecretCode,
                CurrentTurn = CurrentTurn,
                SecretCode = SecretCode.ToString(),
                Guesses = guessesAsString,
                Feedback = feedbackAsArrays,
                PreviousMementoCreatedOn = _lastMementoCreatedOn,
                CreatedOn = _lastMementoCreatedOn = DateTime.Now,
                UserIsGuessing = userIsGuessing
            };
        }

        public void ResetGame()
        {
            ResetGame(false);
        }

        public void ResetGame(bool generateNewSecretCode)
        {
            CurrentTurn = 0;
            Guesses = new List<ColorSequence>();
            AllFeedback = new List<Feedback>();
            if (generateNewSecretCode)
            {
                ISecretCodeGenerator codeGenerator = new RandomPatternGenerator();
                SecretCode = codeGenerator.GenerateSecretCode(NumberOfColors, LengthOfSecretCode);
            }
        }

        #endregion Behavior
    }
}
