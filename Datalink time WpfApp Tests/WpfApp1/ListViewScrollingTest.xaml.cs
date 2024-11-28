using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for ListViewScrollingTest.xaml
    /// </summary>
    public partial class ListViewScrollingTest : Window
    {
        ObservableCollection<string> Strings { get; set; }
        public ListViewScrollingTest()
        {
            InitializeComponent();
            Strings = new ObservableCollection<string>()
            {
                "Hello",
                "Hello1",
                "Hello2",
                "Hello3",
                "Hello4",
                "Hello5",
                "Hello6",
                "Hello7",
                "Hello8",
                "Hello9",
                "Hello10",
                "Hello41",
                "Hello42",
                "Hello43",
                "Hello44",
                "Hello45",
                "Hello46",
                "Hello47",
                "Hello48",
                "Hello48",
                "Hello49",
                "Hello423"
            };

            this.DataContext = this;
        }
    }
}
