using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Game
{
    public class Feedback
    {
        public Feedback(int pegsInTheRightPlace, int pegsWithTheRightColor)
        {
            PegsInTheRightPlace = pegsInTheRightPlace;
            PegsWithCorrectColorInTheWrongPlace = pegsWithTheRightColor;
        }

        public Feedback(int[] feedbackAsArray)
        {
            PegsInTheRightPlace = feedbackAsArray[0];
            PegsWithCorrectColorInTheWrongPlace = feedbackAsArray[1];
        }

        public int PegsInTheRightPlace { get; private set; }
        public int PegsWithCorrectColorInTheWrongPlace { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Feedback))
            {
                return false;
            }

            Feedback objFeedback = obj as Feedback;
            return PegsInTheRightPlace == objFeedback.PegsInTheRightPlace && PegsWithCorrectColorInTheWrongPlace == objFeedback.PegsWithCorrectColorInTheWrongPlace;
        }

        public override int GetHashCode()
        {
            return PegsInTheRightPlace ^ PegsWithCorrectColorInTheWrongPlace;
        }

        public int[] ToArray()
        {
            return new int[] {PegsInTheRightPlace, PegsWithCorrectColorInTheWrongPlace};
        }
    }
}
