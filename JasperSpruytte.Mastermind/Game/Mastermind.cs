using JasperSpruytte.MastermindWindows.SavingLoading;
using System;
using System.Collections.Generic;

namespace JasperSpruytte.MastermindWindows.Game
{
    public class Mastermind
    {
        #region Constructors

        public Mastermind(int numberOfTurns, int numberOfColors, int lengthOfSecretCode)
            : this(numberOfTurns, numberOfColors, lengthOfSecretCode, new RandomPatternGenerator()) { }

        public Mastermind(int numberOfTurns, int numberOfColors, int lengthOfSecretCode, ISecretCodeGenerator secretCodeGenerator)
        {
            this._numberOfTurns = numberOfTurns;
            this._numberOfColors = numberOfColors;
            this._lengthOfSecretCode = lengthOfSecretCode;
            CheckIfSettingsWithinRange();
            CurrentTurn = 0;
            SecretCode = secretCodeGenerator.GenerateSecretCode(NumberOfColors, LengthOfSecretCode);
            AllFeedback = new List<Feedback>(NumberOfTurns);
            Guesses = new List<ColorSequence>(NumberOfTurns);
        }

        public Mastermind(int numberOfTurns, int numberOfColors, ColorSequence secretCode) : this(numberOfTurns, numberOfColors, secretCode.Length)
        {
            SecretCode = secretCode;
            if (SecretCode.Contains(0))
                throw new ArgumentException("The secret code is not completely filled in.");           
        }

        public Mastermind(MastermindMemento memento)
        {
            _numberOfTurns = memento.NumberOfTurns;
            _numberOfColors = memento.NumberOfColors;
            _lengthOfSecretCode = memento.LengthOfSecretCode;
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

        private readonly int _numberOfTurns;
        private readonly int _numberOfColors;
        private readonly int _lengthOfSecretCode;

        public const int MinimumNumberOfTurns = 1;
        public const int MaximumNumberOfTurns = 15;
        public const int MinimumNumberOfColors = 1;
        public const int MaximumNumberOfColors = 10;
        public const int MinimumLengthOfSecretCode = 1;
        public const int MaximumLengthOfSecretCode = 10;

        private DateTime _lastMementoCreatedOn = DateTime.MinValue;

        #endregion Fields

        #region Properties

        public int NumberOfTurns
        {
            get { return _numberOfTurns; }
        }

        public int NumberOfColors
        {
            get { return _numberOfColors; }
        }

        public int LengthOfSecretCode
        {
            get { return _lengthOfSecretCode; }
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
            if (NumberOfTurns < MinimumNumberOfTurns || NumberOfTurns > MaximumNumberOfTurns)
            {
                throw new ArgumentOutOfRangeException("The number of colors has to be between " + MinimumNumberOfTurns +" and " + MaximumNumberOfTurns + ".");
            }
            if (NumberOfColors < MinimumNumberOfColors || NumberOfColors > MaximumNumberOfColors)
            {
                throw new ArgumentOutOfRangeException("The number of colors has to be between " + MinimumNumberOfColors + " and " + MaximumNumberOfColors + ".");
            }
            if (LengthOfSecretCode < MinimumLengthOfSecretCode || LengthOfSecretCode > MaximumLengthOfSecretCode)
            {
                throw new ArgumentOutOfRangeException("The length of the secret code has to be between " + MinimumLengthOfSecretCode + " and " + MaximumLengthOfSecretCode + ".");
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
