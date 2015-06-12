using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JasperSpruytte.MastermindWindows.Game;

namespace MastermindUnitTests
{
    class TestObjectFactory
    {
        public Mastermind CreateMastermind(int numberOfTurns = 10, int numberOfColors = 6, int lengthOfSecretCode = 4, ISecretCodeGenerator codeGenerator = null)
        {
            if (codeGenerator == null)
                codeGenerator = new FixedCodeGenerator(new ColorSequence(lengthOfSecretCode));
            return new Mastermind(numberOfTurns, numberOfColors, lengthOfSecretCode, codeGenerator);
        }

        public ColorSequence CreateFilledInPattern(int patternLength = 3)
        {
            ColorSequence pattern = new ColorSequence(patternLength);

            for (int i = 0; i < pattern.Length; i++)
            {
                pattern[i] = new ColorPeg(1);
            }

            return pattern;
        }

        public ISecretCodeGenerator CreateSecretCodeGeneratorMockup(ColorSequence secretCode)
        {
            return new FixedCodeGenerator(secretCode);
        }
    
    }
}
