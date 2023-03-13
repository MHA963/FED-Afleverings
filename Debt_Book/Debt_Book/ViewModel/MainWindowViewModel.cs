using Debt_Book.Data;
using Debt_Book.Models;
using Debt_Book.ViewModel;
using Debt_Book.Views;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace Debt_Book.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly string AppTitle = "Debt book";
        private string filePath = ""; 


        public MainWindowViewModel()
        {

            Agents = new ObservableCollection<Agent>();

            Agents.Add(new Agent("James Bond", -300));
            Agents.Add(new Agent("Mohammed", -2000));
            Agents.Add(new Agent("Mads", -2000000));

            currentAgent = Agents[0];
        }

        #region Properties

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

        private int currentIndex;

        public int CurrentIndex
        {
            get
            {
                return currentIndex;
            }
            set
            {
                SetProperty(ref currentIndex, value);
            }
        }

        private ObservableCollection<Agent> agents;

        public ObservableCollection<Agent> Agents
        {
            get
            {
                return agents;
            }
            set
            {
                SetProperty(ref agents, value);
            }
        }

        private string filename = "";

        public string Filename
        {
            get { return filename; }
            set { 
                SetProperty(ref filename, value);
                RaisePropertyChanged("Title");
            }
        }

        public string Title
        {
            get
            {
                var sa = "";
                if (Check)
                {
                    sa = "*";
                }

                return Filename + sa + " - " + AppTitle;

            }
        }

        private bool check = false;

        public bool Check
        {
            get { return check; }
            set
            {
                SetProperty(ref check, value);
                RaisePropertyChanged("Title");
            }
        }


        #endregion

        #region Commands

        private DelegateCommand? addCommand;
        public DelegateCommand AddCommand =>
            addCommand ?? (addCommand = new DelegateCommand(ExecuteAddCommand));

        void ExecuteAddCommand()
        {
            var newAgent = new Agent();
            var vm = new AddNewDebtorModel("Add new Depters", newAgent);
            var dlg = new AddNewDebtor()
            {
                DataContext = vm
            };
            if (dlg.ShowDialog() == true)
            {
                Agents.Add(newAgent);
                CurrentAgent = newAgent;
            }
        }

        bool CanExecuteAddCommand()
        {
            return true;
        }

        private DelegateCommand editCommand;
        public DelegateCommand EditCommand =>
            editCommand ?? (editCommand = new DelegateCommand(ExecuteEditCommand)
            .ObservesProperty(() => CurrentIndex));

        void ExecuteEditCommand()
        {
            var tempAgent = CurrentAgent.Clone();

            var vm = new DebtValueViewModel("Edit debtor", CurrentAgent)
            {

            };
            var dlg = new DebtValue
            {
                DataContext = vm,
                Owner = Application.Current.MainWindow
            };
            if (dlg.ShowDialog() == true)
            {

                foreach (var newTransaction in tempAgent.Transactions)
                {
                    int count = 0;
                    currentAgent.Transactions.Add(tempAgent.Transactions[count]);
                    count++;
                }
            }
        }

        DelegateCommand _openfileCommand;

        public DelegateCommand Open_FileCommand
        {
            get { return _openfileCommand ?? (_openfileCommand = new DelegateCommand(ExecuteOpen_FileCommand)); }
        }

        void ExecuteOpen_FileCommand()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "TheDebtBook assignment documents|.agn|All Files|.* ",
                DefaultExt = "agn"
            };

            if (filePath == "")
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            }
            else
            {
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);
            }

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                try
                {
                    Agents = Repository.ReadFile(filePath);
                    Check = false;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Not able to open the file", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private DelegateCommand _save_as_Command;

        public DelegateCommand Save_As_Command
        {
            get { return _save_as_Command ?? (_save_as_Command = new DelegateCommand(ExecuteSave_As_Command)); }
        }

        void ExecuteSave_As_Command()
        {
            var dialog = new SaveFileDialog()
            {
                Filter = "TheDebtBook assignment documents|.agn|All Files|.* ",
                DefaultExt = "agn"
            };

            if (filePath == "")
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            else
            {
                dialog.InitialDirectory = Path.GetDirectoryName(filePath);
            }

            if (dialog.ShowDialog(App.Current.MainWindow) == true)
            {
                filePath = dialog.FileName;
                Filename = Path.GetFileName(filePath);
                SaveFile();
            }
        }

        private void SaveFile()
        {
            try
            {
                Repository.SaveFile(filePath, Agents);
                Check = false;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Not able to save the file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion Commands
    }
}
