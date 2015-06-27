using JasperSpruytte.MastermindWindows.Game;
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
        IMastermindSettings MastermindSettings { get; set; }
        void ShowToUser();
        void Close();
    }
}
