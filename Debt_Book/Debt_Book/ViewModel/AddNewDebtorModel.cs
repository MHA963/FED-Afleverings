using Debt_Book.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace Debt_Book.ViewModel
{


    public class AddNewDebtorModel : BindableBase
    {

        public AddNewDebtorModel(string Title, Agent agent)
        {
            Title = title;
            CurrentAgent = agent;
        }

        #region Properties

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                SetProperty(ref title, value);
            }
        }
        private Agent currentAgent;
        public Agent CurrentAgent
        {
            get
            {
                return currentAgent;
            }
            set
            {
                SetProperty(ref currentAgent, value);
            }
        }

        public bool Valid
        {
            get
            {
                bool valid = true;
                if (string.IsNullOrWhiteSpace(CurrentAgent.NAME))
                {
                    valid = false;
                }
                return valid;
            }
        }

        #endregion

        #region Commands

        public ICommand savebtnCmd;

        public ICommand SaveBtnCmd
        {
            get
            {
                return savebtnCmd ?? (savebtnCmd = new DelegateCommand(
                SaveBtnCmd_Execute, SaveBtnCmd_CanExecute)
                    .ObservesProperty(() => CurrentAgent.NAME)
                        .ObservesProperty(() => CurrentAgent.TAmount));
            }
        }

        private void SaveBtnCmd_Execute()
        {

        }

        private bool SaveBtnCmd_CanExecute()
        {
            return Valid;
        }

        #endregion
    }

}