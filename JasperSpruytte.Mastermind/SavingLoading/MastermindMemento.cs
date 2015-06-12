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
    }
}
