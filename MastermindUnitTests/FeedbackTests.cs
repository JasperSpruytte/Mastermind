using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperSpruytte.MastermindWindows.Game;

namespace MastermindUnitTests
{
    [TestClass]
    public class FeedbackTests
    {
        [TestMethod]
        public void Feedback_Equals_FeedbackEqualsAnotherFeedbackWithTheSameValues()
        {
            int pegsInTheCorrectPlace = 0;
            int pegsInTheWrongPlace = 2;
            Feedback feedback1 = new Feedback(pegsInTheCorrectPlace, pegsInTheWrongPlace);
            Feedback feedback2 = new Feedback(pegsInTheCorrectPlace, pegsInTheWrongPlace);

            bool areEqual = feedback1.Equals(feedback2);

            Assert.IsTrue(areEqual);
        }
    }
}
