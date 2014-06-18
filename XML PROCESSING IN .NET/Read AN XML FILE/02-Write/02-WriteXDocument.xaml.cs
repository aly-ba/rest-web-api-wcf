using System.Text;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XML_Processing.Write
{
  public partial class winWriteXDocument : Window
  {
    public winWriteXDocument()
    {
      InitializeComponent();
    }
    
    private void btnSimpleWritingVerbose_Click(object sender, RoutedEventArgs e)
    {
      SimpleWritingVerbose();
    }

    private void SimpleWritingVerbose()
    {
      XDocument xd;
      XElement root;
      XElement child;
      XElement child2;

      xd =
        new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
          new XComment("Employee Records"),
          new XElement("Employees"));

      root = xd.XPathSelectElement("//Employees");

      child = new XElement("Employee");
      root.Add(child);
      child2 = new XElement("id", 1);
      child.Add(child2);
      child2 = new XElement("FirstName", "Bruce");
      child.Add(child2);
      child2 = new XElement("LastName", "Jones");
      child.Add(child2);

      xd.Save(AppConfig.XmlFile);

      txtResult.Text = xd.ToString();
    }

    private void btnSimpleWriting_Click(object sender, RoutedEventArgs e)
    {
      SimpleWriting();
    }

    private void SimpleWriting()
    {
      XDocument xd;
      XElement root;
      XElement child;

      xd =
        new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
          new XComment("Employee Records"),
          new XElement("Employees"));

      root = xd.XPathSelectElement("//Employees");

      child = new XElement("Employee",
              new XElement("id", 1),
              new XElement("FirstName", "Bruce"),
              new XElement("LastName", "Jones"));

      root.Add(child);

      xd.Save(AppConfig.XmlFile);

      txtResult.Text = xd.ToString();
    }

    private void btnWriteAttributes_Click(object sender, RoutedEventArgs e)
    {
      WriteAttributes();
    }

    private void WriteAttributes()
    {
      XDocument xd;
      XElement root;
      XElement child;

      xd =
        new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
          new XComment("Employee Records"),
          new XElement("Employees"));

      root = xd.XPathSelectElement("//Employees");

      child = new XElement("Employee",
              new XAttribute("id", 1),
              new XElement("FirstName", "Bruce"),
              new XElement("LastName", "Jones"));

      root.Add(child);

      xd.Save(AppConfig.XmlFile);

      txtResult.Text = xd.ToString();
    }

    private void btnNoFormatting_Click(object sender, RoutedEventArgs e)
    {
      NoFormatting();
    }

    private void NoFormatting()
    {
      XDocument xd;
      XElement root;
      XElement child;
     
      xd =
        new XDocument(
          new XDeclaration("1.0", "utf-8", "yes"),
          new XComment("Employee Records"),
          new XElement("Employees"));

      root = xd.XPathSelectElement("//Employees");

      child = new XElement("Employee",
              new XAttribute("id", 1),
              new XElement("FirstName", "Bruce"),
              new XElement("LastName", "Jones"));

      root.Add(child);

      xd.Save(AppConfig.XmlFile, SaveOptions.DisableFormatting);

      txtResult.Text = xd.ToString(SaveOptions.DisableFormatting);
    }
  }
}
