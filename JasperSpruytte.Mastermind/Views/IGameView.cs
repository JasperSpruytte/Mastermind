using JasperSpruytte.MastermindWindows.Game;
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
        void ShowSecretCode(ColorSequence secretCode);
        void InitializeUserGuessingMode(int numberOfTurns, int numberOfColors, int lengthOfSecretCode);
        void ShowMessage(string message);
        void ShowFeedback(List<Feedback> feedback);
        ColorSequence GetGuess(int turn);
        void DisableGuessing();
        void EnableGuessing(int turn);
    }
}
