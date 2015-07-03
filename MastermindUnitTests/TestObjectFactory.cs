using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JasperSpruytte.MastermindWindows.Game;
using FakeItEasy;

namespace MastermindUnitTests
{
    class TestObjectFactory
    {
        public Mastermind CreateMastermind(int numberOfTurns = 10, int numberOfColors = 6, int lengthOfSecretCode = 4, ColorSequence secretCode = null)
        {
            if (secretCode == null)
                secretCode = CreateFilledInPattern(lengthOfSecretCode);
            IMastermindSettings settings = CreateMastermindSettings();
            settings.NumberOfTurns = numberOfTurns;
            settings.NumberOfColors = numberOfColors;
            settings.LengthOfSecretCode = lengthOfSecretCode;
            return new Mastermind(settings, secretCode);
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

        public IMastermindSettings CreateMastermindSettings()
        {
            IMastermindSettings settings = A.Fake<IMastermindSettings>();
            settings.NumberOfTurns = 12;
            settings.NumberOfColors = 6;
            settings.LengthOfSecretCode = 4;
            A.CallTo(() => settings.MinimumNumberOfTurns).Returns(1);
            A.CallTo(() => settings.MaximumNumberOfTurns).Returns(15);
            A.CallTo(() => settings.MinimumNumberOfColors).Returns(1);
            A.CallTo(() => settings.MaximumNumberOfColors).Returns(10);
            A.CallTo(() => settings.MinimumLengthOfSecretCode).Returns(1);
            A.CallTo(() => settings.MaximumLengthOfSecretCode).Returns(10);
            return settings;
        }
    
    }
}
