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
            if (secretCode != null)
                game = new Mastermind(mastermindSettings, secretCode);
            else
                game = new Mastermind(mastermindSettings);
        }

        public void StartNewGame()
        {
            view.EnableSaving();
        }

        public void AdvanceTurn(ColorSequence guess)
        {
            game.Guess(guess);
            if (game.GameOver)
            {
                view.DisableSaving();
                view.ShowSecretCode();
            }
        }


        public IMastermindSettings MastermindSettings { get; private set; }
    }
}
