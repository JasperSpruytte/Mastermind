using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.Views;
using JasperSpruytte.MastermindWindows.Presenters;
using JasperSpruytte.MastermindWindows.SavingLoading;

namespace MastermindUnitTests.Presenters
{
    [TestClass]
    public class SettingsViewPresenterTests
    {
        private ISettingsView view;
        private IMastermindSettings mastermindSettings;
        private SettingsViewPresenter presenter;
        private GameViewPresenter gameViewPresenter;

        [TestInitialize]
        public void InitializeSettingsViewPresenter()
        {
            view = A.Fake<ISettingsView>();
            TestObjectFactory factory = new TestObjectFactory();
            mastermindSettings = factory.CreateMastermindSettings();
            IMastermindRepository mastermindRepository = A.Fake<IMastermindRepository>();
            mastermindSettings.NumberOfTurns = 12;
            mastermindSettings.NumberOfColors = 6;
            mastermindSettings.LengthOfSecretCode = 4;
            mastermindSettings.UserIsGuessing = true;
            IGameView gameView = A.Fake<IGameView>();
            gameViewPresenter = new GameViewPresenter(gameView, mastermindSettings, mastermindRepository);

            presenter = new SettingsViewPresenter(view, gameViewPresenter);
        }

        [TestMethod]
        public void SettingsViewPresenter_Constructor_SendsCorrectInitialValuesToView()
        {
            Assert.AreEqual(view.MastermindSettings, mastermindSettings);
        }

        [TestMethod]
        public void SettingsViewPresenter_Constructor_SetsItselfAsPresenterOfView()
        {
            Assert.AreEqual(presenter, view.Presenter);
        }

        [TestMethod]
        public void SettingsViewPresenter_ShowView_ShowsView()
        {
            presenter.ShowView();

            A.CallTo(() => view.ShowToUser()).MustHaveHappened();
        }

        [TestMethod]
        public void SettingsViewPresenter_Cancel_ClosesView()
        {
            presenter.Cancel();

            A.CallTo(() => view.Close()).MustHaveHappened();
        }

        [TestMethod]
        public void SettingsViewPresenter_Save_GetsMastermindSettingsAndSavesThemAndStartsNewGame()
        {
            
            presenter.Save();

            A.CallTo(() => view.MastermindSettings).MustHaveHappened();
            A.CallTo(() => mastermindSettings.Save()).MustHaveHappened();
            A.CallTo(() => view.Close()).MustHaveHappened();
        }
    }
}
