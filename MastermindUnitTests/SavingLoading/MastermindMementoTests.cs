using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JasperSpruytte.MastermindWindows.Game;
using FakeItEasy;
using JasperSpruytte.MastermindWindows.SavingLoading;

namespace MastermindUnitTests.SavingLoading
{
    [TestClass]
    public class MastermindMementoTests
    {
        //[TestMethod]
        //public void MastermindMemento_Equals_IsEqualToCloneOfItself()
        //{
        //    IMastermindSettings settings = A.Fake<IMastermindSettings>();
        //    settings.NumberOfTurns = 10;
        //    settings.NumberOfColors = 6;
        //    settings.LengthOfSecretCode = 4;
        //    A.CallTo(() => settings.MinimumNumberOfTurns).Returns(1);
        //    A.CallTo(() => settings.MinimumNumberOfColors).Returns(1);
        //    A.CallTo(() => settings.MinimumLengthOfSecretCode).Returns(1);
        //    A.CallTo(() => settings.MaximumNumberOfTurns).Returns(15);
        //    A.CallTo(() => settings.MaximumNumberOfColors).Returns(10);
        //    A.CallTo(() => settings.MaximumLengthOfSecretCode).Returns(10);
        //    ColorSequence secretCode = new ColorSequence("1234");
        //    Mastermind game = new Mastermind(settings, secretCode);
        //    //MastermindMemento originalMemento = game.CreateMemento()

        //}
    }
}
