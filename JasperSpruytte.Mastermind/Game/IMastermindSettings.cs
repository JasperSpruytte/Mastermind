using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Game
{
    public interface IMastermindSettings
    {
        int NumberOfTurns { get; set; }
        int NumberOfColors { get; set; }
        int LengthOfSecretCode { get; set; }
        bool UserIsGuessing { get; set; }
        int MinimumNumberOfTurns { get; }
        int MaximumNumberOfTurns { get; }
        int MinimumNumberOfColors { get; }
        int MaximumNumberOfColors { get; }
        int MinimumLengthOfSecretCode { get; }
        int MaximumLengthOfSecretCode { get; }
        void Save();
    }
}
