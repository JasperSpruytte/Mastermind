using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System;

namespace JasperSpruytte.MastermindWindows.SavingLoading
{
    class MastermindDAL : IMastermindRepository
    {
        private const string FileName = "mastermind.sav";
        private List<MastermindMemento> mementos;
        private XmlSerializer xmlSerializer;

        public MastermindDAL()
        {
            xmlSerializer = new XmlSerializer(typeof(List<MastermindMemento>));
            if (!File.Exists(FileName))
            {
                File.Create(FileName).Close();
                mementos = new List<MastermindMemento>();
            }
            else
            {
                mementos = GetMementos();
            }
        }

        public IReadOnlyCollection<MastermindMemento> Mementos { get { return mementos.AsReadOnly(); } }

        public void Save(MastermindMemento memento)
        {
            foreach (MastermindMemento savedMemento in mementos)
            {
                if (savedMemento.CreatedOn == memento.PreviousMementoCreatedOn)
                {
                    mementos.Remove(savedMemento);
                    break;
                }
            }

            mementos.Add(memento);
            SaveMementos();
        }

        public void Delete(MastermindMemento mementoToRemove)
        {
            foreach (MastermindMemento memento in mementos)
            {
                if (memento.CreatedOn == mementoToRemove.CreatedOn)
                {
                    mementos.Remove(memento);
                    break;
                }
            }

            SaveMementos();
        }

        private void SaveMementos()
        {
            StreamWriter writer = new StreamWriter(FileName);
            xmlSerializer.Serialize(writer, mementos);
            writer.Close();
        }

        private List<MastermindMemento> GetMementos()
        {
            List<MastermindMemento> mastermindMementos;
            FileStream filestream = new FileStream(FileName, FileMode.Open);
            XmlReader xmlReader = XmlReader.Create(filestream);
            mastermindMementos = xmlSerializer.Deserialize(xmlReader) as List<MastermindMemento>;
            filestream.Close();
            return mastermindMementos;
        }
        
    }
}
