using System.Configuration;
using System.Data;
using System.Globalization;
using System.Windows;

namespace ResxWpfTranslator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var info = CultureInfo.GetCultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = info;
            Thread.CurrentThread.CurrentUICulture = info;
            base.OnStartup(e);
        }
    }

}
