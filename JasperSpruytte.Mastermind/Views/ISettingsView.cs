using JasperSpruytte.MastermindWindows.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Views
{
    public interface ISettingsView
    {
        SettingsViewPresenter Presenter { get; set; }
        void SetSettings(int numberOfTurns, int numberOfColors, int lengthOfSecretCode, bool userIsGuessing);
        void ShowToUser();
    }
}
