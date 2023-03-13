using Debt_Book.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Debt_Book.ViewModel
{
    public class DebtValueViewModel : BindableBase
    {
        public DebtValueViewModel(string _title, Agent _agent)
        {
            Title = _title;
            CurrentAgent = _agent;
            transactions = _agent.Transactions;
            CurrentTransaction = new Transaction();
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

        private Transaction currentTransaction;

        public Transaction CurrentTransaction
        {
            get
            {
                return currentTransaction;
            }
            set
            {
                SetProperty(ref currentTransaction, value);
            }
        }

        private ObservableCollection<Transaction> transactions;

        public ObservableCollection<Transaction> Transactions
        {
            get
            {
                return transactions;
            }
            set
            {
                SetProperty(ref transactions, value);
            }
        }

        public bool Valid
        {
            get
            {
                bool valid = true;

                return valid;
            }
        }

        #endregion

        #region commands

        public ICommand addValueCommand;

        public ICommand AddValueCommand
        {
            get
            {
                return addValueCommand ?? (addValueCommand = new DelegateCommand(
                    AddValueCommand_Execute));

            }
        }
        private void AddValueCommand_Execute()
        {
            Transaction newTransaction = new Transaction();
            newTransaction.Trans = CurrentTransaction.Trans;
            currentAgent.Transactions.Add(newTransaction); // TODO - 
            Debtsum();
        }
        void Debtsum()
        {
            double debtSum = CurrentAgent.TAmount;
            foreach (Transaction transaction in currentAgent.Transactions)
            {
                debtSum += transaction.Trans;
            }

            currentAgent.TAmount = debtSum;
        }

        private bool AddValueCommand_CanExecute()
        {
            return Valid;
        }

        #endregion
    }
}
