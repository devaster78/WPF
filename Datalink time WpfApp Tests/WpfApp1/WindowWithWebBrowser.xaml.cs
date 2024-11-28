using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for WindowWithWebBrowser.xaml
    /// </summary>
    public partial class WindowWithWebBrowser : Window
    {
        public WindowWithWebBrowser()
        {
            InitializeComponent();
            wb.NavigateToString("Members 18-85 with diabetes (type 1 and type 2) who received a kidney health evaluation, defined by estimated glomerular filtration rate (eGFR) and a <b>urine albumin-creatinine</b> ratio (uACR) during the measure year\r\n");
        }
    }
}
