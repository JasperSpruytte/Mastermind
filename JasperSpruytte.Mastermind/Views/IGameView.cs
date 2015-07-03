using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.SavingLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Views
{
    public interface IGameView
    {
        void StartNewGame(IMastermindSettings mastermindSettings);
        void EnableSaving();
        void DisableSaving();
        void ShowSecretCode(ColorSequence secretCode);
        void ShowMessage(string message);
        void ShowFeedback(List<Feedback> feedback);
        ColorSequence GetGuess(int turn);
        void DisableGuessing();
        void EnableGuessing(int turn);
        void ShowSavedGames(IReadOnlyCollection<MastermindMemento> mementos);
    }
}
