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
        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DisablesSavingWhenGameIsOver()
        {
            ColorSequence secretCode = new ColorSequence("1234");
            Mastermind game = new Mastermind(1, secretCode.Length, secretCode);
            IGameView view = A.Fake<IGameView>();
            GameViewPresenter presenter = new GameViewPresenter(view, game);
            ColorSequence guess = secretCode;

            presenter.AdvanceTurn(guess);

            A.CallTo(() => view.DisableSaving()).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DoesNotDisableSavingIfGameIsNotOver()
        {
            ColorSequence secretCode = new ColorSequence("1234");
            Mastermind game = new Mastermind(2, secretCode.Length, secretCode);
            IGameView view = A.Fake<IGameView>();
            GameViewPresenter presenter = new GameViewPresenter(view, game);
            ColorSequence guess = new ColorSequence("1235");

            presenter.AdvanceTurn(guess);

            A.CallTo(() => view.DisableSaving()).MustNotHaveHappened();
        }
    }
}
