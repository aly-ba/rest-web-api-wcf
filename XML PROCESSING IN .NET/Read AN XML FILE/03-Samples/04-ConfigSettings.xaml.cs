using System.Configuration;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace XML_Processing.Samples
{
  public partial class winConfigSettings : Window
  {
    public winConfigSettings()
    {
      InitializeComponent();
    }

    private void btnConfig_Click(object sender, RoutedEventArgs e)
    {
      ReadFromConfig();
    }

    private void ReadFromConfig()
    {
      txtResult.Text = ConfigurationManager.AppSettings["XmlFile"];

      // Show AppConfig class
    }

    private void btnExternal_Click(object sender, RoutedEventArgs e)
    {
      ReadExternalConfig();
    }

    private void ReadExternalConfig()
    {
      var xElem = XElement.Load(AppConfig.GetSampleAppConfig());

      // Find XmlFile element
      var file = (from f in xElem.Descendants("appSettings").Elements()
                  where f.Attribute("key").Value == "XmlFile"
                  select f).SingleOrDefault();

      txtResult.Text = file.Attribute("value").Value;
    }
  }
}
