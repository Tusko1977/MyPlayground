using System.Windows;
using System.Windows.Navigation;

namespace MyPlayground
{
    /// <summary>
    /// Interaction logic for JsonValidator.xaml
    /// </summary>
    public partial class JsonValidator : Window
    {
        public JsonValidator()
        {
            InitializeComponent();

            webBrowser.LoadCompleted += webBrowser_LoadCompleted;
        }

        //Add Reference System.Windows.Forms

        private void webBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            webBrowser.Focus();

            System.Windows.Forms.SendKeys.SendWait("^a ^v");
        }
    }
}
