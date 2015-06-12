using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JasperSpruytte.MastermindWindows.Game;

namespace JasperSpruytte.MastermindWindows.Game
{
    class RandomPatternGenerator: ISecretCodeGenerator
    {
        public ColorSequence GenerateSecretCode(int numberOfColors, int lengthOfSecretCode)
        {
            ColorSequence secretCode = new ColorSequence(lengthOfSecretCode);
            Random random = new Random();
            for (byte i = 0; i < lengthOfSecretCode; i++)
            {
                int pegNumber = random.Next(1, numberOfColors);
                secretCode[i] = new ColorPeg(pegNumber);
            }
            return secretCode;
        }
    }
}
