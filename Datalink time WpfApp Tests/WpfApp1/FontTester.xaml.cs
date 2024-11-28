using System;
using System.Collections.Generic;
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
    /// Interaction logic for FontTester.xaml
    /// </summary>
    public partial class FontTester : Window
    {
        public string Essay { get; set; }
        public FontTester()
        {
            
            InitializeComponent();
            this.Essay = "Elon Musk, a South African-born entrepreneur, has revolutionized multiple industries with his innovative ideas and unwavering dedication. As the CEO and CTO of SpaceX, CEO and product architect of Tesla, and founder of Neuralink and The Boring Company, Musk has disrupted traditional thinking and pushed the boundaries of what is thought possible. This essay will explore Musk’s entrepreneurial journey, his leadership style, and the impact of his ventures on society.";
            this.Essay = "Evoke Smart Care";
            this.DataContext = this;
        }
    }
}
