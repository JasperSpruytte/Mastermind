using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JasperSpruytte.MastermindWindows.Game;

namespace JasperSpruytte.MastermindWindows.Game
{
    public interface ISecretCodeGenerator
    {
        ColorSequence GenerateSecretCode(int numberOfColors, int lengthOfSecretCode);
    }
}
