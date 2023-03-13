using Prism.Mvvm;
using System;

namespace Debt_Book.Models
{
    public class Transaction : BindableBase
    {
        double trans;
        string date;

        public Transaction()
        {
            date = DateTime.Now.ToLongDateString();
            trans = 0;
        }

        public Transaction(double _trans)
        {
            date = DateTime.Now.ToLongDateString();
            trans = _trans;
        }

        public string Date
        {
            get
            {
                return date;
            }
            set
            {
                SetProperty(ref date, value);
            }
        }
        public double Trans
        {
            get
            {
                return trans;
            }
            set
            {
                SetProperty(ref trans, value);
            }
        }
    }
}
