using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JasperSpruytte.MastermindWindows.Game;

namespace JasperSpruytte.MastermindWindows.SavingLoading
{
    [Serializable]
    public class MastermindMemento
    {
        public int NumberOfTurns { get; set; }
        public int NumberOfColors { get; set; }
        public int LengthOfSecretCode { get; set; }
        public bool UserIsGuessing { get; set; }
        public int CurrentTurn { get; set; }
        public string SecretCode { get; set; }
        public List<string> Guesses { get; set; }
        public List<int[]> Feedback { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime PreviousMementoCreatedOn { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || (obj is MastermindMemento))
            {
                return false;
            }

            MastermindMemento objMemento = obj as MastermindMemento;

            return NumberOfTurns == objMemento.NumberOfTurns &&
                NumberOfColors == objMemento.NumberOfColors &&
                LengthOfSecretCode == objMemento.LengthOfSecretCode &&
                UserIsGuessing == objMemento.UserIsGuessing &&
                CurrentTurn == objMemento.CurrentTurn &&
                SecretCode == objMemento.SecretCode &&
                Guesses.Equals(objMemento.Guesses) &&
                Feedback.Equals(objMemento.Feedback) &&
                CreatedOn == objMemento.CreatedOn &&
                PreviousMementoCreatedOn == objMemento.CreatedOn;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
