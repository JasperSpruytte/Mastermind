using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Presenters
{
    public class GameViewPresenter
    {
        private Views.IGameView view;
        private Game.Mastermind game;
        private IMastermindSettings settings;

        public GameViewPresenter(IGameView view, IMastermindSettings mastermindSettings, ColorSequence secretCode = null)
        {
            this.view = view;
            this.settings = mastermindSettings;
            if (secretCode != null)
                game = new Mastermind(mastermindSettings, secretCode);
            else
                game = new Mastermind(mastermindSettings);
        }

        public GameViewPresenter(IGameView view, Mastermind game)
        {
            this.view = view;
            this.game = game;
        }

        public void StartNewGame()
        {
            view.EnableSaving();
            view.InitializeUserGuessingMode(settings.NumberOfTurns, settings.NumberOfColors, settings.LengthOfSecretCode);
        }

        public void AdvanceTurn()
        {
            try
            {
                ColorSequence guess = view.GetGuess(game.CurrentTurn);
                game.Guess(guess);
            }
            catch (Exception e)
            {
                view.ShowMessage(e.Message);
            }

            view.ShowFeedback(game.AllFeedback);
            view.DisableGuessing();

            if (game.GameOver)
            {
                view.DisableSaving();
                view.ShowSecretCode(game.GetSecretCode());
                string gameOverMessage = (game.PlayerWon) ? "You win!" : "You lose!";
                view.ShowMessage(gameOverMessage);
            }
            else
            {
                view.EnableGuessing(game.CurrentTurn);
            }
        }
    }
}
