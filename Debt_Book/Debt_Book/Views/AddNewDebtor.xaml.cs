using Debt_Book.ViewModel;
using System.Windows;


namespace Debt_Book.Views
{
    /// <summary>
    /// Interaction logic for AddNewDebtor.xaml
    /// </summary>
    public partial class AddNewDebtor : Window
    {
        public AddNewDebtor()
        {
            InitializeComponent();
        }

        private void save_btn_Click(object sender, RoutedEventArgs e)
        {
            var dc = DataContext as AddNewDebtorModel;
            if (dc.Valid)
            {
                DialogResult = true;

            }
            else
            {
                MessageBox.Show("Please fill all the fields");
            }

        }

    }
}
