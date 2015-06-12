using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JasperSpruytte.MastermindWindows.Game;

namespace MastermindUnitTests
{
    class FixedCodeGenerator: ISecretCodeGenerator
    {
        public FixedCodeGenerator(ColorSequence secretCode)
        {
            SecretCode = secretCode;
        }

        private ColorSequence SecretCode
        {
            get;
            set;
        }

        public ColorSequence GenerateSecretCode(int numberOfColors, int lengthOfSecretCode)
        {
            return SecretCode;
        }
    }
}
