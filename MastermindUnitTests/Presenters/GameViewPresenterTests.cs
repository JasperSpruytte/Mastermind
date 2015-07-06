using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperSpruytte.MastermindWindows.Game;
using FakeItEasy;
using JasperSpruytte.MastermindWindows.Views;
using JasperSpruytte.MastermindWindows.Presenters;
using JasperSpruytte.MastermindWindows.SavingLoading;
using System.Collections.Generic;

namespace MastermindUnitTests.Presenters
{
    [TestClass]
    public class GameViewPresenterTests
    {
        private ColorSequence secretCode;
        private IGameView view;
        private GameViewPresenter presenter;
        private IMastermindSettings mastermindSettings;
        private IMastermindRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            secretCode = new ColorSequence("1234");
            TestObjectFactory factory = new TestObjectFactory();
            mastermindSettings = factory.CreateMastermindSettings();
            mastermindSettings.NumberOfTurns = 2;
            mastermindSettings.NumberOfColors = secretCode.Length;
            view = A.Fake<IGameView>();
            repository = A.Fake<IMastermindRepository>();
            presenter = new GameViewPresenter(view, mastermindSettings, repository, secretCode);
            ColorSequence guess = new ColorSequence("1235");
            A.CallTo(() => view.GetGuess(A<int>.Ignored)).Returns(guess);
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_DisablesSavingWhenGameIsOver()
        {
            ColorSequence guess = secretCode;
            A.CallTo(() => view.GetGuess(A<int>.Ignored)).Returns(guess);

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
            A.CallTo(() => view.GetGuess(A<int>.Ignored)).Returns(guess);

            presenter.AdvanceTurn();
            presenter.StartNewGame();

            A.CallTo(() => view.EnableSaving()).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_RevealsSecretCodeWhenGameIsOver()
        {
            A.CallTo(() => view.GetGuess(A<int>.Ignored)).Returns(secretCode);
            presenter.AdvanceTurn();

            A.CallTo(() => view.ShowSecretCode(secretCode)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_ViewShowsErrorMessageWhenExceptionIsThrown()
        {
            A.CallTo(() => view.GetGuess(A<int>.Ignored)).Returns(null);
            presenter.AdvanceTurn();

            A.CallTo(() => view.ShowMessage(A<string>.That.Not.IsNullOrEmpty())).MustHaveHappened();
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

            A.CallTo(() => view.GetGuess(0)).MustHaveHappened();
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
            A.CallTo(() => view.GetGuess(0)).Returns(secretCode);

            presenter.AdvanceTurn();

            A.CallTo(() => view.EnableGuessing(A<int>.Ignored)).MustNotHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_ShowsCorrectMessageWhenUserLoses()
        {
            mastermindSettings.NumberOfTurns = 1;
            presenter = new GameViewPresenter(view, mastermindSettings, repository, secretCode);

            presenter.AdvanceTurn();

            A.CallTo(() => view.ShowMessage("You lose!")).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_AdvanceTurn_ShowsCorrectMessageWhenUserWins()
        {
            A.CallTo(() => view.GetGuess(0)).Returns(secretCode);

            presenter.AdvanceTurn();

            A.CallTo(() => view.ShowMessage("You win!")).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_StartNewGame_StartsNewGame()
        {
            presenter.StartNewGame();

            A.CallTo(() => view.StartNewGame(mastermindSettings)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_Constructor_GetsListOfAllSavedGamesAndSendsItToTheView()
        {
            presenter = new GameViewPresenter(view, mastermindSettings, repository, secretCode);

            A.CallTo(() => view.ShowSavedGames(repository.Mementos)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_Save_CallsSaveMementoOnRepository()
        {
            presenter.SaveGame();

            A.CallTo(() => repository.Save(A<MastermindMemento>.Ignored)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_SaveGame_ReloadsSavedGames()
        {
            A.CallTo(() => repository.Mementos).Returns(A.Dummy<IReadOnlyCollection<MastermindMemento>>());

            presenter.SaveGame();

            A.CallTo(() => view.ShowSavedGames(repository.Mementos)).MustHaveHappened();
        }

        [TestMethod]
        public void GameViewPresenter_DeleteSavedGame_DelegatesToRepositoryAndReloadsListOfSavedGames()
        {
            MastermindMemento memento = new MastermindMemento();

            presenter.DeleteSavedGame(memento);

            A.CallTo(() => repository.Delete(memento)).MustHaveHappened();
            A.CallTo(() => view.ShowSavedGames(repository.Mementos)).MustHaveHappened();
        }
    }
}
