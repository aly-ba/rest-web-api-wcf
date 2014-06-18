using System;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XML_Processing.Read
{
  public partial class winReadXDocument : Window
  {
    public winReadXDocument()
    {
      InitializeComponent();
    }

    private void btnSimple_Click(object sender, RoutedEventArgs e)
    {
      SimpleXmlDocument();
    }

    private void SimpleXmlDocument()
    {
      StringBuilder sb = new StringBuilder(512);
      XDocument xd;
      XElement node;

      xd = XDocument.Load(AppConfig.GetEmployeesFile());

      node = xd.XPathSelectElement("/Employees/Employee");
      sb.AppendFormat("id={0}", node.Element("id").Value);
      sb.Append(Environment.NewLine);
      sb.AppendFormat("FirstName={0}", node.Element("FirstName").Value);
      sb.Append(Environment.NewLine);

      txtResult.Text = sb.ToString();
    }

    private void btnReadNodes_Click(object sender, RoutedEventArgs e)
    {
      ReadNodes();
    }

    private void ReadNodes()
    {
      StringBuilder sb = new StringBuilder(512);
      XDocument xd;

      xd = XDocument.Load(AppConfig.GetEmployeesFile());

      var list = xd.XPathSelectElements("//Employee");
      foreach (XElement xn in list)
      {
        sb.AppendFormat("id={0}", xn.Element("id").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("FirstName={0}", xn.Element("FirstName").Value);
        sb.Append(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }

    private void btnReadAll_Click(object sender, RoutedEventArgs e)
    {
      ReadAllNodes();
    }

    private void ReadAllNodes()
    {
      StringBuilder sb = new StringBuilder(512);
      XDocument xd;

      xd = XDocument.Load(AppConfig.GetEmployeesFile());

      var list = xd.XPathSelectElements("//Employee");
      foreach (XElement node in list)
      {
        sb.AppendFormat("Element Name={0}", node.Name);
        sb.Append(Environment.NewLine);
        foreach (XElement item in node.Elements())
        {
          sb.AppendFormat("   Element={0}", item.Name);
          sb.Append(", ");
          sb.AppendFormat("Value={0}", node.Element(item.Name).Value);
          sb.Append(Environment.NewLine);
        }
      }

      txtResult.Text = sb.ToString();
    }

    private void btnLoadXml_Click(object sender, RoutedEventArgs e)
    {
      LoadXmlSample();
    }

    private void LoadXmlSample()
    {
      string xml;
      XDocument xd;

      xml = "<Employees>" +
        "<Employee><id>1</id><FirstName>Bill</FirstName><LastName>Gates</LastName></Employee>" +
        "<Employee><id>2</id><FirstName>John</FirstName><LastName>Kuhn</LastName></Employee>" +
        "</Employees>";

      //xd = XDocument.Load(new System.IO.StringReader(xml));
      xd = XDocument.Parse(xml);

      txtResult.Text = xd.ToString();
    }

    private void btnGetAttributes_Click(object sender, RoutedEventArgs e)
    {
      GetAttributes();
    }

    private void GetAttributes()
    {
      StringBuilder sb = new StringBuilder(512);
      XDocument xd;

      xd = XDocument.Load(AppConfig.GetEmployeesFileWithAttributes());

      var list = xd.XPathSelectElements("//Employee");
      foreach (XElement xn in list)
      {
        sb.AppendFormat("id={0}", xn.Attribute("id").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("FirstName={0}", xn.Element("FirstName").Value);
        sb.Append(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }

    private void btnFindByAttribute_Click(object sender, RoutedEventArgs e)
    {
      FindByAttribute();
    }

    private void FindByAttribute()
    {
      StringBuilder sb = new StringBuilder(512);
      XDocument xd;

      xd = XDocument.Load(AppConfig.GetEmployeesFileWithAttributes());

      var node = xd.XPathSelectElement("//Employee/[@id='2']");
      foreach (XElement child in node.Elements())
      {
        sb.AppendFormat("Element Name={0}", child.Name);
        sb.Append(" - ");
        sb.AppendFormat("Value={0}", node.Element(child.Name).Value);
        sb.Append(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }
  }
}
