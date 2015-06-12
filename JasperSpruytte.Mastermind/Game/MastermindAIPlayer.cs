using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Game
{
    public class MastermindAIPlayer
    {
        private Mastermind _mastermind;
        private bool _awaitingFeedback;
        //private List<ColorSequence> _possibleCodes;

        public MastermindAIPlayer(Mastermind mastermind)
        {
            _mastermind = mastermind;
            _awaitingFeedback = false;
            //_possibleCodes = GenerateAllPossibleCodes();
        }

        public ColorSequence MakeGuess()
        {
            if (_awaitingFeedback)
                throw new InvalidOperationException("You need to give me feedback before I can make the next guess.");

            ColorSequence guess = new ColorSequence(_mastermind.LengthOfSecretCode);
            if (_mastermind.CurrentTurn == 0)
            {
                guess = GenerateFirstGuess();
            }
            else
            {
                RandomPatternGenerator randomGenerator = new RandomPatternGenerator();
                ColorSequence lastGuess = _mastermind.Guesses[_mastermind.CurrentTurn - 1];
                Feedback lastFeedback = _mastermind.AllFeedback[_mastermind.CurrentTurn - 1];
                while (!lastGuess.Evaluate(guess).Equals(lastFeedback) || CheckIfGuessAlreadyUsed(guess) || guess.Contains(0))
                    guess = randomGenerator.GenerateSecretCode(_mastermind.NumberOfColors, _mastermind.LengthOfSecretCode);
            }

            _mastermind.Guess(guess);
            _awaitingFeedback = true;

            return guess;
        }

        private bool CheckIfGuessAlreadyUsed(ColorSequence code)
        {
            bool codeUsed = false;

            foreach (ColorSequence usedCode in _mastermind.Guesses)
            {
                codeUsed = usedCode.Equals(code);
                if (codeUsed)
                    break;
            }

            return codeUsed;
        }

        public void ReceiveFeedback(Feedback feedback)
        {
            if (!_awaitingFeedback)
                throw new InvalidOperationException("I need to make a guess first!");
            Feedback correctFeedback = _mastermind.AllFeedback[_mastermind.CurrentTurn - 1];

            if (!correctFeedback.Equals(feedback))
            {
                throw new ArgumentException("The feedback you gave me is incorrect, you cheater!");
            }

            _awaitingFeedback = false;
        }

        //private List<ColorSequence> GenerateAllPossibleCodes()
        //{
        //    List<ColorSequence> possibleCodes = new List<ColorSequence>();
        //    string code = "1111";
        //    possibleCodes.Add(new ColorSequence(code));
        //    int numberOfCodes = (int)Math.Pow(_mastermind.NumberOfColors, _mastermind.LengthOfSecretCode);
        //    string maxNumber = (_mastermind.NumberOfColors + 1).ToString();
        //    for (int i = 1; i < numberOfCodes; i++)
        //    {
        //        string nextCode = (int.Parse(code) + 1).ToString();
        //        if (nextCode.Contains(maxNumber))
        //        {
        //            int maxNumberPosition = nextCode.IndexOf(maxNumber);
        //            string increasedNumber = (int.Parse(nextCode.Substring(maxNumberPosition - 1)) + 1).ToString();
        //            nextCode = nextCode.Substring(0, maxNumberPosition) + increasedNumber + "1" + nextCode.Substring(maxNumberPosition + 1);
        //        }
        //        possibleCodes.Add(new ColorSequence(nextCode));
        //    }

        //    return possibleCodes;
        //}

        private ColorSequence GenerateFirstGuess()
        {
            ColorSequence guess = new ColorSequence(_mastermind.LengthOfSecretCode);
            int numberOfPegsWithFirstColor = (int)Math.Ceiling((double)(_mastermind.LengthOfSecretCode / 2));
            for (int pegIndex = 0; pegIndex < numberOfPegsWithFirstColor; pegIndex++)
            {
                guess[pegIndex] = new ColorPeg(1);
            }
            int pegNumberOfSecondColor = (_mastermind.NumberOfColors > 1) ? 2 : 1;
            for (int pegIndex = numberOfPegsWithFirstColor; pegIndex < _mastermind.LengthOfSecretCode; pegIndex++)
            {
                guess[pegIndex] = new ColorPeg(pegNumberOfSecondColor);
            }

            return guess;
        }

    }
}
