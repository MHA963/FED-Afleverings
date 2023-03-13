using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace Debt_Book.Models
{
    [Serializable]
    [XmlRoot(ElementName = "Debetor")]
    public class Agent : BindableBase
    {

        string Deb_Name;
        double Tamount;
        public ObservableCollection<Transaction> transactions;
        public Agent()
        {
            transactions = new ObservableCollection<Transaction>();
        }

        public Agent(string aName, double t_amount)
        {
            Deb_Name = aName;
            Tamount = t_amount;
            transactions = new ObservableCollection<Transaction>();
            transactions.Add(new Models.Transaction(t_amount));
        }

        [XmlElement(ElementName = "Name")]
        public string? NAME
        {
            get
            {
                return Deb_Name;
            }
            set
            {
                SetProperty(ref Deb_Name, value);
            }
        }

        [XmlElement(ElementName = "TAmount")]
        public double TAmount
        {
            get
            {
                return Tamount;
            }
            set
            {
                SetProperty(ref Tamount, value);
            }
        }
        public Agent Clone()
        {
            return this.MemberwiseClone() as Agent;
        }

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
    }
}
