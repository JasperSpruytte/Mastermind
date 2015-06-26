using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.Views;
using JasperSpruytte.MastermindWindows.Presenters;

namespace MastermindUnitTests.Presenters
{
    [TestClass]
    public class SettingsViewPresenterTests
    {
        private ISettingsView view;
        private IMastermindSettings mastermindSettings;
        private SettingsViewPresenter presenter;

        [TestInitialize]
        public void InitializeSettingsViewPresenter()
        {
            view = A.Fake<ISettingsView>();
            mastermindSettings = A.Fake<IMastermindSettings>();
            mastermindSettings.NumberOfTurns = 12;
            mastermindSettings.NumberOfColors = 6;
            mastermindSettings.LengthOfSecretCode = 4;
            mastermindSettings.UserIsGuessing = true;

            presenter = new SettingsViewPresenter(view, mastermindSettings);
        }

        [TestMethod]
        public void SettingsViewPresenter_Constructor_SendsCorrectInitialValuesToView()
        {
            A.CallTo(() => view.SetSettings(mastermindSettings.NumberOfTurns, mastermindSettings.NumberOfColors, mastermindSettings.LengthOfSecretCode, mastermindSettings.UserIsGuessing)).MustHaveHappened();
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
        public void SettingsViewPresenter_Constructor_SetsSettingsLimits()
        {

        }
    }
}
