using System;
using System.Collections.Generic;
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
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window, INotifyPropertyChanged
    {

        private bool mEnable = true;

        public bool Enable
        {
            get
            {
                return mEnable;
            }
            set
            {
                mEnable = value;
                OnPropertyChanged(nameof(Enable));
            }
        }


        public Window2()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void toggle_Click(object sender, RoutedEventArgs e)
        {
            Enable = !Enable;
        }

        private void changeValue_Click(object sender, RoutedEventArgs e)
        {
            if (radio1.IsChecked == true)
            {
                radio2.IsChecked = true;
            }
            else if (radio2.IsChecked == true)
            {
                radio1.IsChecked = true;
            }

            if (toggle1.IsChecked == true)
            {
                toggle2.IsChecked = true;
            }
            else if (toggle2.IsChecked == true)
            {
                toggle1.IsChecked = true;
            }
        }
    }
}
