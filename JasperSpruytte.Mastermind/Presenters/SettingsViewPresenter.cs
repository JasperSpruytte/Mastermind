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

        public SettingsViewPresenter(ISettingsView view, IMastermindSettings mastermindSettings)
        {
            this.view = view;
            this.mastermindSettings = mastermindSettings;
            view.Presenter = this;
            view.SetSettings(mastermindSettings.NumberOfTurns, mastermindSettings.NumberOfColors, mastermindSettings.LengthOfSecretCode, mastermindSettings.UserIsGuessing);
        }

        public void ShowView()
        {
            view.ShowToUser();
        }
    }
}
