using System;
using JasperSpruytte.MastermindWindows.Game;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace MastermindUnitTests
{
    [TestClass]
    public class PegTests
    {
        [TestMethod]
        public void Peg_Constructor_EmptyPegIsGray()
        {
            ColorPeg peg = new ColorPeg(0);

            Assert.AreEqual(peg.Color, Color.Gray);
        }

        [TestMethod]
        public void Peg_Equals_PegsWithSameNumberAreEqual()
        {
            ColorPeg peg1 = new ColorPeg(2);
            ColorPeg peg2 = new ColorPeg(2);

            bool areEqual = peg1.Equals(peg2);

            Assert.IsTrue(areEqual);
        }
    } 
}
