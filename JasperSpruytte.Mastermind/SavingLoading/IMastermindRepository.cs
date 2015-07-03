using System;
using System.Collections.Generic;
namespace JasperSpruytte.MastermindWindows.SavingLoading
{
    public interface IMastermindRepository
    {
        void Delete(MastermindMemento memento);
        IReadOnlyCollection<MastermindMemento> Mementos { get; }
        void Save(MastermindMemento memento);
    }
}
