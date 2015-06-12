using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperSpruytte.MastermindWindows.Game;
using System.Drawing;

namespace MastermindUnitTests
{
    [TestClass]
    public class ColorSequenceUnitTests
    {
        [TestMethod]
        public void Pattern_Constructor_CreatesEmptyPegs()
        {
            int numberOfPegs = 4;
            ColorSequence pattern = new ColorSequence(numberOfPegs);

            byte numberOfEmptyPegs = 0;
            for (byte i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] != null  && pattern[i].Number == 0)
                {
                    numberOfEmptyPegs++;
                }
            }

            Assert.AreEqual(numberOfPegs, numberOfEmptyPegs);

        }

        [TestMethod]
        public void Pattern_Contains_ContainsAllPegsPutInConstructor()
        {
            ColorSequence pattern = new ColorSequence(0, 2, 3);

            bool contains1 = pattern.Contains(new ColorPeg(0));
            bool contains2 = pattern.Contains(new ColorPeg(2));
            bool contains3 = pattern.Contains(new ColorPeg(3));

            Assert.IsTrue(contains1);
            Assert.IsTrue(contains2);
            Assert.IsTrue(contains3);
        }

        [TestMethod]
        public void Pattern_Contains_ContainsWithStartIndexIgnoresPegsBeforeStartIndex()
        {
            byte pegNumberToSearchFor = 1;
            ColorSequence pattern = new ColorSequence(pegNumberToSearchFor, 2, 3, 4);
            byte startIndex = 2;

            bool containsPegAfterStartIndex = pattern.Contains(pegNumberToSearchFor, startIndex);

            Assert.IsFalse(containsPegAfterStartIndex);
        }

        [TestMethod]
        public void ColorSequence_Constructor_TakesNumericStringAndConvertsItToColorSequence()
        {
            string colorSequenceAsString = "1234";
            ColorSequence colorSequence = new ColorSequence(colorSequenceAsString);
            bool colorSequenceIsCorrect = false;

            for (int pegIndex = 0; pegIndex < colorSequenceAsString.Length; pegIndex++)
            {
                colorSequenceIsCorrect = colorSequence[pegIndex].Number == int.Parse(colorSequenceAsString.Substring(pegIndex, 1));
                if (!colorSequenceIsCorrect)
                    break;
            }

            Assert.IsTrue(colorSequenceIsCorrect);
        }
    }
}
