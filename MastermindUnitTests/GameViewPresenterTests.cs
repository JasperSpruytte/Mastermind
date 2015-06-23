using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperSpruytte.MastermindWindows.Game;
using FakeItEasy;
using JasperSpruytte.MastermindWindows.Views;
using JasperSpruytte.MastermindWindows.Presenters;

namespace MastermindUnitTests
{
    [TestClass]
    public class GameViewPresenterTests
    {
        private ColorSequence secretCode;
        private IGameView view;
        private GameViewPresenter presenter;
        private IMastermindSettings mastermindSettings;

        [TestInitialize]
        public void Initialize()
        {
            secretCode = new ColorSequence("1234");
            mastermindSettings = A.Fake<IMastermindSettings>();
            mastermindSettings.NumberOfTurns = 2;
            mastermindSettings.NumberOfColors = secretCode.Length;
            view = A.Fake<IGameView>();
            presenter = new GameViewPresenter(view, mastermindSettings, secretCode);
            ColorSequence guess = new ColorSequence("1235");
            A.CallTo(() => view.GetGuess(A<int>.Ignored, A<int>.Ignored)).Returns(guess);
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DisablesSavingWhenGameIsOver()
        {
            ColorSequence guess = secretCode;
            A.CallTo(() => view.GetGuess(A<int>.Ignored, A<int>.Ignored)).Returns(guess);

            presenter.AdvanceTurn();

            A.CallTo(() => view.DisableSaving()).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DoesNotDisableSavingIfGameIsNotOver()
        {
            presenter.AdvanceTurn();

            A.CallTo(() => view.DisableSaving()).MustNotHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_StartNewGame_SavingEnabledWhenStartingNewIfUserIsGuessing()
        {
            ColorSequence guess = secretCode;
            A.CallTo(() => view.GetGuess(A<int>.Ignored, A<int>.Ignored)).Returns(guess);

            presenter.AdvanceTurn();
            presenter.StartNewGame();

            A.CallTo(() => view.EnableSaving()).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_RevealsSecretCodeWhenGameIsOver()
        {
            A.CallTo(() => view.GetGuess(A<int>.Ignored, A<int>.Ignored)).Returns(secretCode);
            presenter.AdvanceTurn();

            A.CallTo(() => view.ShowSecretCode()).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_StartNewGame_InitializesUserGuessingMode()
        {
            presenter.StartNewGame();

            A.CallTo(() => view.InitializeUserGuessingMode(mastermindSettings.NumberOfTurns, mastermindSettings.NumberOfColors, mastermindSettings.LengthOfSecretCode)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_ViewShowsErrorMessageWhenExceptionIsThrown()
        {
            A.CallTo(() => view.GetGuess(A<int>.Ignored, A<int>.Ignored)).Returns(null);
            presenter.AdvanceTurn();

            A.CallTo(() => view.ShowErrorMessage(A<string>.That.Not.IsNullOrEmpty())).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_ViewShowsFeedbackOfMostRecentGuess()
        {
            Mastermind game = new Mastermind(mastermindSettings, secretCode);
            presenter = new GameViewPresenter(view, game);

            presenter.AdvanceTurn();

            A.CallTo(() => view.ShowFeedback(game.AllFeedback)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_GetsMostRecentGuessFromView()
        {
            presenter.AdvanceTurn();

            A.CallTo(() => view.GetGuess(secretCode.Length, 0)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DisablesPreviousGuessingRowAndEnablesNewGuessingRown()
        {
            presenter.AdvanceTurn();

            A.CallTo(() => view.DisableGuessing()).MustHaveHappened();
            A.CallTo(() => view.EnableGuessing(1)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DoesNotEnableGuessingIfGameIsOver()
        {
            A.CallTo(() => view.GetGuess(secretCode.Length, 0)).Returns(secretCode);

            presenter.AdvanceTurn();

            A.CallTo(() => view.EnableGuessing(A<int>.Ignored)).MustNotHaveHappened();
        }
    }
}
