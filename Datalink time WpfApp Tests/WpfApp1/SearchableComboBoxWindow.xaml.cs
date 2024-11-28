using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for SearchableComboBoxWindow.xaml
    /// </summary>
    public partial class SearchableComboBoxWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<string> items;
        public ObservableCollection<string> ItemList
        {
            get { return items; }
            set
            {
                this.items = value;
                this.OnPropertyChanged("ItemList");
            }
        }

        public SearchableComboBoxWindow()
        {
            InitializeComponent();
            this.ItemList = new ObservableCollection<string>()
            {
                "Hello1",
                "Test",
                "Evoke App",
                "Jan Evoke",
                "Micheal Vanita",
                "Dashboard Manika",
                "Dashboard Manika Test",
                "Dashboard Manika Dash",
                "Dashboard Rahul Manika",
                "Dashboard Manika Doul",
                "Hello1",
                "Test",
                "Evoke App",
                "Jan Evoke",
                "Hello1",
                "Test",
                "Evoke App",
                "Jan Evoke",
                "Hello1",
                "Test",
                "Evoke App",
                "Jan Evoke",

            };
            this.DataContext = this;

        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null
                && PropertyChanged is not null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
