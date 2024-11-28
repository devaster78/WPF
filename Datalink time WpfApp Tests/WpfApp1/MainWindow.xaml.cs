using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string MyDocs { get; set; } = @"<FlowDocument xml:space=""preserve"" xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation""><Paragraph FontSize=""22pt"">My First Heading</Paragraph><Paragraph>My first paragraph.</Paragraph></FlowDocument>";
        public FrameworkContentElement Factory { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            //wb.NavigateToString("Members 18-85 with diabetes (type 1 and type 2) who received a kidney health evaluation, defined by estimated glomerular filtration rate (eGFR) and a <b>urine albumin-creatinine</b> ratio (uACR) during the measure year\r\n");
            //Factory = XamlReader.Parse(MyDocs) as FrameworkContentElement;
            //this.DataContext = this;

        }
    }
}