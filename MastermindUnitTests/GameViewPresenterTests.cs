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
        private Mastermind game;
        private IGameView view;
        private GameViewPresenter presenter;

        [TestInitialize]
        public void Initialize()
        {
            secretCode = new ColorSequence("1234");
            game = new Mastermind(2, secretCode.Length, secretCode);
            view = A.Fake<IGameView>();
            presenter = new GameViewPresenter(view, game);
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
    }
}
