using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperSpruytte.MastermindWindows.Game;
using FakeItEasy;

namespace MastermindUnitTests
{
    [TestClass]
    public class MastermindAIPlayerTests
    {
        private ISecretCodeGenerator _codeGenerator;
        private TestObjectFactory _objectFactory;
        private Mastermind _mastermind;
        private MastermindAIPlayer _player;

        [TestInitialize]
        public void Initialize()
        {
            ColorSequence secretCode = new ColorSequence(1, 2, 3, 4);
            _objectFactory = new TestObjectFactory();
            _mastermind = _objectFactory.CreateMastermind(lengthOfSecretCode: 4, secretCode: secretCode, numberOfTurns: 10);
            _player = new MastermindAIPlayer(_mastermind);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MastermindAIPlayer_GiveFeedback_RejectsWrongFeedback()
        {
            _player.MakeGuess();
            Feedback actualFeedback = _mastermind.AllFeedback[0];
            Feedback wrongFeedback = new Feedback(actualFeedback.PegsInTheRightPlace + 1, actualFeedback.PegsWithCorrectColorInTheWrongPlace + 1);
            _player.ReceiveFeedback(wrongFeedback);
        }

        [TestMethod]
        public void MastermindAIPlayer_GiveFeedback_AcceptsCorrectFeedback()
        {
            _player.MakeGuess();
            Feedback correctFeedback = _mastermind.AllFeedback[0];

            bool acceptedFeedback = true;
            try
            {
                _player.ReceiveFeedback(correctFeedback);
            }
            catch (ArgumentException)
            {
                acceptedFeedback = false;
            }

            Assert.IsTrue(acceptedFeedback);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MastermindAIPlayer_GiveFeedback_CannotBeCalledBeforeHavingMadeAGuess()
        {
            _player.ReceiveFeedback(new Feedback(0, 0));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MastermindAIPlayer_MakeGuess_MustBeFollowedByReceiveFeedback()
        {
            _player.MakeGuess();
            _player.MakeGuess();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void MastermindAIPlayer_MakeGuess_MustMakeGuessAfterGivingFeedback()
        {
            _player.MakeGuess();
            Feedback feedback = _mastermind.AllFeedback[0];
            _player.ReceiveFeedback(feedback);
            _player.ReceiveFeedback(feedback);
        }

        [TestMethod]
        public void MastermindAIPlayer_MakeGuess_CanGuessASimpleSecretCode()
        {
            ColorSequence secretCode = new ColorSequence(1, 1, 1);
            TestObjectFactory factory = new TestObjectFactory();
            IMastermindSettings settings = factory.CreateMastermindSettings();
            settings.NumberOfColors = 2;
            settings.NumberOfTurns = _mastermind.Settings.MaximumNumberOfTurns;
            settings.LengthOfSecretCode = secretCode.Length;
            Mastermind mastermind = new Mastermind(settings, secretCode);
            MastermindAIPlayer player = new MastermindAIPlayer(mastermind);

            while (!mastermind.GameOver)
            {
                player.MakeGuess();
                player.ReceiveFeedback(mastermind.AllFeedback[mastermind.CurrentTurn - 1]);
            }

            Assert.IsTrue(mastermind.PlayerWon);
        }
    }
}
