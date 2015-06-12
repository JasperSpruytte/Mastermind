using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;


namespace JasperSpruytte.MastermindWindows.Game
{

    public class ColorPeg
    {
        private readonly string[] colors = { "None", "Red", "Yellow", "Blue", "Green", "Pink", "Orange", "Purple", "Brown", "White", "Black" };

        public ColorPeg(int number)
        {
            Number = number;
            if (Number == 0)
            {
                Color = Color.Gray;
            }
            else
            {
                Color = Color.FromName(ToString());
            }
        }

        #region Properties

        public Color Color { get; private set; }
        public int Number { get; private set; }

        #endregion Properties

        #region Methods

        public override string ToString()
        {
            return colors[Number];
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ColorPeg))
            {
                return false;
            }

            ColorPeg objPeg = (ColorPeg)obj;

            return Number == objPeg.Number;
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }

        #endregion Methods
    }
}
