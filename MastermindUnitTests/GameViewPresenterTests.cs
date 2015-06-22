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
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DisablesSavingWhenGameIsOver()
        {
            ColorSequence guess = secretCode;

            presenter.AdvanceTurn(guess);

            A.CallTo(() => view.DisableSaving()).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DoesNotDisableSavingIfGameIsNotOver()
        {
            ColorSequence guess = new ColorSequence("1235");

            presenter.AdvanceTurn(guess);

            A.CallTo(() => view.DisableSaving()).MustNotHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_StartNewGame_SavingEnabledWhenStartingNewIfUserIsGuessing()
        {
            ColorSequence guess = new ColorSequence("1234");

            presenter.AdvanceTurn(guess);
            presenter.StartNewGame();

            A.CallTo(() => view.EnableSaving()).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_RevealsSecretCodeWhenGameIsOver()
        {
            presenter.AdvanceTurn(secretCode);

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
            presenter.AdvanceTurn(null);

            A.CallTo(() => view.ShowErrorMessage(A<string>.That.Not.IsNullOrEmpty())).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_ViewShowsFeedbackOfMostRecentGuess()
        {

        }
    }
}
