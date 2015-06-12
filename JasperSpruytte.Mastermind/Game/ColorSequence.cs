using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Diagnostics;
using JasperSpruytte.MastermindWindows.Game;

namespace JasperSpruytte.MastermindWindows.Game
{
    public class ColorSequence
    {
        #region Constructors

        public ColorSequence(int numberOfPegs)
        {
            Pegs = new ColorPeg[numberOfPegs];
            for (int i = 0; i < numberOfPegs; i++)
            {
                Pegs[i] = new ColorPeg(0);
            }
        }

        public ColorSequence(params int[] pegs)
        {
            Pegs = new ColorPeg[pegs.Length];
            for (byte pegIndex = 0; pegIndex < Length; pegIndex++)
            {
                Pegs[pegIndex] = new ColorPeg(pegs[pegIndex]);
            }
        }

        public ColorSequence(string colorSequenceAsString)
        {
            Pegs = new ColorPeg[colorSequenceAsString.Length];
            int pegIndex = 0;
            foreach (char numberAsChar in colorSequenceAsString)
            {
                int number = (int)Char.GetNumericValue(numberAsChar);
                Pegs[pegIndex] = new ColorPeg(number);
                pegIndex++;
            }
        }

        #endregion Constructors

        #region Properties

        public byte Length { get { return (byte)Pegs.Length; } }

        public ColorPeg this[int index]
        {
            get { return Pegs[index]; }
            set { Pegs[index] = value; }
        }

        private ColorPeg[] Pegs { get; set; }

        #endregion Properties

        #region Methods

        public bool Contains(ColorPeg peg, int startIndex)
        {
            bool containsPeg = false;

            for (int pegIndex = startIndex; pegIndex < Length; pegIndex++)
            {
                if (Pegs[pegIndex].Number == peg.Number)
                {
                    containsPeg = true;
                    break;
                }
            }

            return containsPeg;
        }

        public bool Contains(int pegNumber)
        {
            return Contains(pegNumber, 0);
        }

        public bool Contains(ColorPeg peg)
        {
            return Contains(peg.Number);
        }

        public bool Contains(int pegNumber, int startIndex)
        {
            return Contains(new ColorPeg(pegNumber), startIndex);
        }

        public Feedback Evaluate(ColorSequence code)
        {
            int pegsInTheCorrectPlace = 0;
            int pegsWithTheRightColor = 0;
            bool[] guessUsedPegs = new bool[Length];
            bool[] secretCodeUsedPegs = new bool[Length];

            for (int pegIndex = 0; pegIndex < Length; pegIndex++)
            {
                if (this[pegIndex].Equals(code[pegIndex]))
                {
                    pegsInTheCorrectPlace++;
                    guessUsedPegs[pegIndex] = secretCodeUsedPegs[pegIndex] = true;
                }
            }

            for (int secretCodeIndex = 0; secretCodeIndex < Length; secretCodeIndex++)
            {
                for (int guessIndex = 0; guessIndex < Length; guessIndex++)
                {
                    if (this[secretCodeIndex].Equals(code[guessIndex]) && !guessUsedPegs[guessIndex] && !secretCodeUsedPegs[secretCodeIndex])
                    {
                        pegsWithTheRightColor++;
                        guessUsedPegs[guessIndex] = secretCodeUsedPegs[secretCodeIndex] = true;
                    }
                }
            }

            return new Feedback(pegsInTheCorrectPlace, pegsWithTheRightColor);
        }

        public override string ToString()
        {
            string colorSequenceString = "";

            for (int pegIndex = 0; pegIndex < Length; pegIndex++)
            {
                colorSequenceString += Pegs[pegIndex].Number;
            }

            return colorSequenceString;
        }

        #endregion Methods
    }
}
