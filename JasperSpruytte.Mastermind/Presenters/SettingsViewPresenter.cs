using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Presenters
{
    public class SettingsViewPresenter
    {
        private ISettingsView view;
        private IMastermindSettings mastermindSettings;
        private GameViewPresenter gameViewPresenter;

        public SettingsViewPresenter(ISettingsView view, GameViewPresenter gameViewPresenter)
        {
            this.view = view;
            this.mastermindSettings = gameViewPresenter.MastermindSettings;
            this.gameViewPresenter = gameViewPresenter;
            view.Presenter = this;
            view.MastermindSettings = mastermindSettings;
        }

        public void ShowView()
        {
            view.ShowToUser();
        }

        public void Cancel()
        {
            view.Close();
        }

        public void Save()
        {
            mastermindSettings = view.MastermindSettings;
            mastermindSettings.Save();
            view.Close();
            gameViewPresenter.StartNewGame(mastermindSettings);
        }
    }
}
