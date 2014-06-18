using System.IO;
using System.Text;
using System.Windows;
using System.Xml;

namespace XML_Processing.Write
{
  public partial class winWriteXmlWriter : Window
  {
    public winWriteXmlWriter()
    {
      InitializeComponent();
    }

    private void btnSimpleWriting_Click(object sender, RoutedEventArgs e)
    {
      SimpleWriting();
    }

    private void SimpleWriting()
    {
      XmlWriter writer;

      // Create the XML Writer
      writer = XmlWriter.Create(AppConfig.XmlFile);
      // Write a Start Element (Root)
      writer.WriteStartElement("Employees");
      // Write a Start Element (Parent)
      writer.WriteStartElement("Employee");
     
      // Write a Start Element (Child)
      writer.WriteStartElement("id");
      // Write the value
      writer.WriteString("1");
      // Write the End Element
      writer.WriteEndElement();
      // Write a Start Element (Child)
      writer.WriteStartElement("FirstName");
      // Write the value
      writer.WriteString("Bruce");
      // Write the End Element
      writer.WriteEndElement();
      // Write a Start Element (Child)
      writer.WriteStartElement("LastName");
      // Write the value
      writer.WriteString("Jones");
      // Write the End Element
      writer.WriteEndElement();

      // Write the End Element (Parent)
      writer.WriteEndElement();
      // Write the End Element (Root)
      writer.WriteEndElement();
      // Close the Writer
      writer.Close();

      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnWriteAttributes_Click(object sender, RoutedEventArgs e)
    {
      WriteAttributes();
    }

    private void WriteAttributes()
    {
      XmlWriter writer;

      writer = XmlWriter.Create(AppConfig.XmlFile);
      writer.WriteStartElement("Employees");
      writer.WriteStartElement("Employee");
      // Write an Attribute
      writer.WriteAttributeString("id", "1");
      writer.WriteStartElement("FirstName");
      writer.WriteString("Bruce");
      writer.WriteEndElement();
      writer.WriteStartElement("LastName");
      writer.WriteString("Jones");
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.Close();

      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnFormatting_Click(object sender, RoutedEventArgs e)
    {
      FormattingSample();
    }

    private void FormattingSample()
    {
      XmlWriter writer;
      XmlWriterSettings settings = new XmlWriterSettings();

      // Set the Format Options
      settings.Encoding = Encoding.UTF8;
      settings.Indent = true;

      // Create an XML File using the Format options
      writer = XmlWriter.Create(AppConfig.XmlFile, settings);

      writer.WriteStartElement("Employees");
      writer.WriteStartElement("Employee");
      writer.WriteAttributeString("id", "1");
      writer.WriteStartElement("FirstName");
      writer.WriteString("Bruce");
      writer.WriteEndElement();
      writer.WriteStartElement("LastName");
      writer.WriteString("Jones");
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.Close();

      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnWriteStringBuilder_Click(object sender, RoutedEventArgs e)
    {
      WriteStringBuilder();
    }

    private void WriteStringBuilder()
    {
      XmlWriter writer;
      StringBuilder sb = new StringBuilder(1024);
      XmlWriterSettings settings = new XmlWriterSettings();

      // Set the Format Options
      settings.Encoding = Encoding.UTF8;
      settings.Indent = true;

      // Create into a StringBuilder
      writer = XmlWriter.Create(sb, settings);

      writer.WriteStartElement("Employees");
      writer.WriteStartElement("Employee");
      writer.WriteStartElement("id");
      writer.WriteString("1");
      writer.WriteEndElement();
      writer.WriteStartElement("FirstName");
      writer.WriteString("Bruce");
      writer.WriteEndElement();
      writer.WriteStartElement("LastName");
      writer.WriteString("Jones");
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.WriteEndElement();
      writer.Close();

      txtResult.Text = sb.ToString();
    }
  }
}
