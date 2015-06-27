using JasperSpruytte.MastermindWindows.Game;
using JasperSpruytte.MastermindWindows.Presenters;
using JasperSpruytte.MastermindWindows.Properties;
using System;
using System.Windows.Forms;

namespace JasperSpruytte.MastermindWindows.Views
{
    public partial class SettingsView : Form, ISettingsView
    {
        private IMastermindSettings mastermindSettings;

        public SettingsView()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Presenter.Cancel();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Presenter.Save();
        }

        public SettingsViewPresenter Presenter
        {
            get;
            set;
        }

        public void ShowToUser()
        {
            this.ShowDialog();
        }


        public IMastermindSettings MastermindSettings
        {
            get
            {
                mastermindSettings.NumberOfTurns = (int)nudNumberOfTurns.Value;
                mastermindSettings.NumberOfColors = (int)nudNumberOfColors.Value;
                mastermindSettings.LengthOfSecretCode = (int)nudLengthOfSecretCode.Value;
                return mastermindSettings;
            }
            set
            {
                mastermindSettings = value;
                nudNumberOfTurns.Value = mastermindSettings.NumberOfTurns;
                nudNumberOfTurns.Minimum = mastermindSettings.MinimumNumberOfTurns;
                nudNumberOfTurns.Maximum = mastermindSettings.MaximumNumberOfTurns;
                nudNumberOfColors.Value = mastermindSettings.NumberOfColors;
                nudNumberOfColors.Minimum = mastermindSettings.MinimumNumberOfColors;
                nudNumberOfColors.Maximum = mastermindSettings.MaximumNumberOfColors;
                nudLengthOfSecretCode.Value = mastermindSettings.LengthOfSecretCode;
                nudLengthOfSecretCode.Minimum = mastermindSettings.MinimumLengthOfSecretCode;
                nudLengthOfSecretCode.Maximum = mastermindSettings.MaximumLengthOfSecretCode;
                if (mastermindSettings.UserIsGuessing)
                    rbUser.Checked = true;
                else
                    rbComputer.Checked = true;
            }
        }
    }
}
