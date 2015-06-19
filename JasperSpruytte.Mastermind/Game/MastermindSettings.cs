using JasperSpruytte.MastermindWindows.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasperSpruytte.MastermindWindows.Game
{
    class MastermindSettings: IMastermindSettings
    {
        private Settings settings;

        public MastermindSettings()
        {
            settings = Settings.Default;
        }

        public int NumberOfTurns
        {
            get
            {
                return settings.NumberOfTurns;
            }
            set
            {
                settings.NumberOfTurns = value;
            }
        }

        public int NumberOfColors
        {
            get
            {
                return settings.NumberOfColors;
            }
            set
            {
                settings.NumberOfColors = value;
            }
        }

        public int LengthOfSecretCode
        {
            get
            {
                return settings.LengthOfSecretCode;
            }
            set
            {
                settings.LengthOfSecretCode = value;
            }
        }

        public bool UserIsGuessing
        {
            get
            {
                return settings.UserIsGuessing;
            }
            set
            {
                settings.UserIsGuessing = value;
            }
        }

        public void Save()
        {
            settings.Save();
        }
    }
}
