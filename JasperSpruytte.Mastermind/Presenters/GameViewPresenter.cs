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

        public GameViewPresenter(IGameView view, IMastermindSettings mastermindSettings, ColorSequence secretCode = null)
        {
            this.view = view;
            this.MastermindSettings = mastermindSettings;
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

        public IMastermindSettings MastermindSettings { get; private set; }

        public void StartNewGame()
        {
            view.EnableSaving();
            view.InitializeUserGuessingMode(MastermindSettings.NumberOfTurns, MastermindSettings.NumberOfColors, MastermindSettings.LengthOfSecretCode);
        }

        public void StartNewGame(IMastermindSettings mastermindSettings)
        {

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

        public void OpenSettingsView()
        {
            ISettingsView settingsView = new SettingsView();
            SettingsViewPresenter settingsViewPresenter = new SettingsViewPresenter(settingsView, this);
            settingsViewPresenter.ShowView();
        }
    }
}
