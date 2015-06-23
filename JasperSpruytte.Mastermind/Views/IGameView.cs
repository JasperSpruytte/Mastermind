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
        void ShowSecretCode();
        void InitializeUserGuessingMode(int numberOfTurns, int numberOfColors, int lengthOfSecretCode);
        void ShowErrorMessage(string errorMessage);
        void ShowFeedback(List<Feedback> feedback);
        ColorSequence GetGuess(int lengthOfSecretCode, int turn);
        void DisableGuessing();
        void EnableGuessing(int turn);
    }
}
