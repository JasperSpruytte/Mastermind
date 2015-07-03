using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperSpruytte.MastermindWindows.Game;
using System.Diagnostics;
using FakeItEasy;

namespace MastermindUnitTests
{
    [TestClass]
    public class MastermindUnitTests
    {
        private TestObjectFactory mf;

        [TestInitialize]
        public void Initialize()
        {
            mf = new TestObjectFactory();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The number of turns has to be between 1 and 15.")]
        public void Mastermind_Constructor_NumberOfTurnsCanNotBeZeroOrLess()
        {
            int numberOfTurns = 0;
            Mastermind mastermind = mf.CreateMastermind(numberOfTurns);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The number of turns has to be between 1 and 15.")]
        public void Mastermind_Constructor_NumberOfTurnsCanNotBeMoreThan15()
        {
            int numberOfTurns = 16;
            Mastermind mastermind = mf.CreateMastermind(numberOfTurns);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The number of colors has to be between 1 and 10.")]
        public void Mastermind_Constructor_NumberOfColorsCanNotBeZero()
        {
            int numberOfColors = 0;
            mf.CreateMastermind(numberOfColors: numberOfColors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The number of colors has to be between 1 and 10.")]
        public void Mastermind_Constructor_NumberOfColorsCanNotBeMoreThanTen()
        {
            int numberOfColors = 11;
            Mastermind mastermind = mf.CreateMastermind(numberOfColors: numberOfColors);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The length of the secret code has to be between 1 and 10.")]
        public void Mastermind_Constructor_secretCodeCannotHaveALengthOfZero()
        {
            int lengthOfSecretCode = 0;
            Mastermind mastermind = mf.CreateMastermind(lengthOfSecretCode: lengthOfSecretCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), "The length of the secret code has to be between 1 and 10.")]
        public void Mastermind_Constructor_secretCodeCannotHaveALengthOverTen()
        {
            int lengthOfSecretCode = 11;
            Mastermind mastermind = mf.CreateMastermind(lengthOfSecretCode: lengthOfSecretCode);
        }

        [TestMethod]
        public void Mastermind_Guess_AdvancesTurn()
        {
            Mastermind mastermind = mf.CreateMastermind();
            ColorSequence guess = mf.CreateFilledInPattern(mastermind.LengthOfSecretCode);

            int initialTurn = mastermind.CurrentTurn;
            mastermind.Guess(guess);
            int newTurn = mastermind.CurrentTurn;

            Assert.AreEqual(0, initialTurn);
            Assert.AreEqual(1, newTurn);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Please fill in the entire color pattern.")]
        public void Mastermind_Guess_DoesNotAcceptIncompletePatterns()
        {
            Mastermind mastermind = mf.CreateMastermind();
            ColorSequence pattern = new ColorSequence(mastermind.LengthOfSecretCode);

            mastermind.Guess(pattern);
        }

        [TestMethod]
        public void Mastermind_Guess_DoesNotAdvanceTurnIfGivenIncompletePattern()
        {
            Mastermind mastermind = mf.CreateMastermind();
            ColorSequence pattern = new ColorSequence(mastermind.LengthOfSecretCode);
            int initialTurn = mastermind.CurrentTurn;
            int newTurn = int.MaxValue;
            bool threwException = false;

            try
            {
                mastermind.Guess(pattern);
            }
            catch (Exception)
            {
                threwException = true;
                newTurn = mastermind.CurrentTurn;
                
            }

            if (threwException)
                Assert.AreEqual(initialTurn, newTurn);
            else
                Assert.Fail();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Mastermind_Guess_DoesNotAcceptPatternsThatAreNotAsLongAsTheSecretCode()
        {
            Mastermind mastermind = mf.CreateMastermind();
            int lengthOfPattern = (int)(mastermind.LengthOfSecretCode - 1);
            ColorSequence pattern = mf.CreateFilledInPattern(lengthOfPattern);

            mastermind.Guess(pattern);
        }

        [TestMethod]
        public void Mastermind_Guess_ReturnsCorrectFeedback()
        {
            ColorSequence secretCode = new ColorSequence(2, 3, 2);
            ColorSequence guess = new ColorSequence(1, 3, 2);
            Mastermind mastermind = mf.CreateMastermind(lengthOfSecretCode: secretCode.Length, secretCode: secretCode);
            
            int expectedPegsInCorrectPlace = 2;
            int expectedPegsWithCorrectColor = 0;

            Feedback feedback = mastermind.Guess(guess);

            Assert.AreEqual(expectedPegsInCorrectPlace, feedback.PegsInTheRightPlace);
            Assert.AreEqual(expectedPegsWithCorrectColor, feedback.PegsWithCorrectColorInTheWrongPlace);
        }

        [TestMethod]
        public void Mastermind_Guess_GameIsOverAfterWhenTurnsAreUp()
        {
            int numberOfTurns = 3;
            ColorSequence secretCode = new ColorSequence("1234");
            Mastermind mastermind = mf.CreateMastermind(numberOfTurns: numberOfTurns, secretCode: secretCode);
            ColorSequence guess = mf.CreateFilledInPattern(mastermind.LengthOfSecretCode);

            for (int turn = 0; turn < numberOfTurns; turn++)
            {
                mastermind.Guess(guess);
            }

            Assert.IsTrue(mastermind.GameOver);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Mastermind_Guess_YouCannotGuessWhenTheGameIsOver()
        {
            int numberOfTurns = 1;
            Mastermind mastermind = mf.CreateMastermind(numberOfTurns: numberOfTurns);
            ColorSequence guess = mf.CreateFilledInPattern(mastermind.LengthOfSecretCode);

            mastermind.Guess(guess);
            mastermind.Guess(guess);
        }

        [TestMethod]
        public void Mastermind_Guess_PlayerWinsWhenGuessingTheRightPattern()
        {
            ColorSequence secretCode = new ColorSequence(1, 2, 3, 4);
            Mastermind mastermind = mf.CreateMastermind(secretCode: secretCode);

            mastermind.Guess(secretCode);

            Assert.IsTrue(mastermind.GameOver);
            Assert.IsTrue(mastermind.PlayerWon);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Mastermind_Guess_CannotAccessSecretCodeBeforeGameIsOver()
        {
            Mastermind mastermind = mf.CreateMastermind();
            ColorSequence secretCode = mastermind.GetSecretCode();
        }

        [TestMethod]
        public void Mastermind_Guess_FeedbackCorrectWithTwoCorrectColorsThatAreInTheWrongPlace()
        {
            int colorNumber1 = 4;
            int colorNumber2 = 3;
            ColorSequence secretCode = new ColorSequence(colorNumber1, colorNumber1, colorNumber2, colorNumber2);
            ColorSequence guess = new ColorSequence(2, colorNumber2, colorNumber1, 1);
            Mastermind mastermind = mf.CreateMastermind(secretCode: secretCode);
            Feedback expectedFeedback = new Feedback(0, 2);

            Feedback actualFeedback = mastermind.Guess(guess);

            Assert.IsTrue(expectedFeedback.Equals(actualFeedback));
        }

        [TestMethod]
        public void Mastermind_Guess_FeedbackDoesNotDuplicateColors()
        {
            int colorNumber1 = 1;
            int colorNumber2 = 2;
            ColorSequence secretCode = new ColorSequence(colorNumber2, colorNumber2, colorNumber2, colorNumber1);
            ColorSequence guess = new ColorSequence(colorNumber1, colorNumber1, colorNumber1, colorNumber1);
            Mastermind mastermind = mf.CreateMastermind(secretCode: secretCode);
            Feedback expectedFeedback = new Feedback(1, 0);
            
            Feedback actualFeedback = mastermind.Guess(guess);
            System.Diagnostics.Debug.WriteLine(expectedFeedback.PegsInTheRightPlace + " " + actualFeedback.PegsWithCorrectColorInTheWrongPlace);
            Assert.IsTrue(expectedFeedback.Equals(actualFeedback));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Mastermind_Constructor_DoesNotAcceptSecretCodeThatIsNotFilledInCompletely()
        {
            ColorSequence secretCode = new ColorSequence("0000");
            IMastermindSettings settings = mf.CreateMastermindSettings();
            settings.NumberOfTurns = 5;
            settings.NumberOfColors = 5;
            settings.LengthOfSecretCode = secretCode.Length;
            Mastermind mastermind = new Mastermind(settings, secretCode);
        }

        [TestMethod]
        public void Mastermind_ResetGame_ResetsState()
        {
            int expectedTurn = 0;
            int expectedNumberOfGuesses = 0;
            int expectedNumberOfFeedback = 0;
            int colorNumber1 = 1;
            int colorNumber2 = 2;
            ColorSequence secretCode = new ColorSequence(colorNumber2, colorNumber2, colorNumber2, colorNumber1);
            ColorSequence guess = new ColorSequence(colorNumber1, colorNumber1, colorNumber1, colorNumber1);
            Mastermind mastermind = mf.CreateMastermind(secretCode: secretCode);

            mastermind.Guess(guess);
            mastermind.ResetGame();

            Assert.AreEqual(expectedTurn, mastermind.CurrentTurn);
            Assert.AreEqual(expectedNumberOfGuesses, mastermind.Guesses.Count);
            Assert.AreEqual(expectedNumberOfFeedback, mastermind.AllFeedback.Count);
            
        }
    }
}
