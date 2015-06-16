using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Views
{
    public interface IGameView
    {
        void EnableSaving();
        void DisableSaving();
        void ShowSecretCode();
    }
}
