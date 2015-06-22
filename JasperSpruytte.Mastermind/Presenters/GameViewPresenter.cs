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

        public void StartNewGame()
        {
            view.EnableSaving();
            view.InitializeUserGuessingMode(settings.NumberOfTurns, settings.NumberOfColors, settings.LengthOfSecretCode);
        }

        public void AdvanceTurn(ColorSequence guess)
        {
            try
            {
                game.Guess(guess);
            }
            catch (Exception e)
            {
                view.ShowErrorMessage(e.Message);
            }

            if (game.GameOver)
            {
                view.DisableSaving();
                view.ShowSecretCode();
            }
        }


        public IMastermindSettings MastermindSettings { get; private set; }
    }
}
