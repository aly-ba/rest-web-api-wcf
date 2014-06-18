using System;
using System.Text;
using System.Windows;
using System.Xml;

namespace XML_Processing.Read
{
  public partial class winReadXmlReader : Window
  {
    public winReadXmlReader()
    {
      InitializeComponent();
    }
    
    private void btnSimple_Click(object sender, RoutedEventArgs e)
    {
      SimpleReader();
    }

    private void SimpleReader()
    {
      StringBuilder sb = new StringBuilder(512);
      XmlReader rdr;

      // Open an XmlReader
      rdr = XmlReader.Create(AppConfig.GetEmployeesFile());
      // Skip over <?xml...> element
      rdr.Read();
      // Skip over CRLF
      rdr.Read();
      // Move to '<Employees>' Element 
      rdr.Read();
      if (rdr.LocalName.Equals("Employees"))
      {
        // Skip over CRLF
        rdr.Read();
        // Move to '<Employee>' Element
        rdr.Read();
        if (rdr.LocalName.Equals("Employee") &&
        rdr.NamespaceURI.Equals(""))
        {
          // Skip over CRLF
          rdr.Read();
          // Move to '<id>' Element
          rdr.Read();
          if (rdr.LocalName.Equals("id") &&
          rdr.NamespaceURI.Equals(""))
          {
            // Move to the Value of <id>
            rdr.Read();
            sb.AppendFormat("id={0}", rdr.Value);
            sb.Append(Environment.NewLine);
            // Move to '</id>' Element
            rdr.Read();
            // Skip over CRLF
            rdr.Read();
          }
          else
          {
            sb.Append("Can't find <id> Element");
            sb.Append(Environment.NewLine);
          }
        }
        else
        {
          sb.Append("Can't find <Employee> Element");
          sb.Append(Environment.NewLine);
        }
        // Move to <FirstName> Element
        rdr.Read();
        if (rdr.LocalName.Equals("FirstName") &&
        rdr.NamespaceURI.Equals(""))
        {
          // Move to Value of <FirstName> Element
          rdr.Read();
          sb.AppendFormat("FirstName={0}", rdr.Value);
          sb.Append(Environment.NewLine);
          // Move to '</FirstName>' Element
          rdr.Read();
          // Skip over CRLF
          rdr.Read();
        }
        else
        {
          sb.Append("Can't find <FirstName> Element");
          sb.Append(Environment.NewLine);
        }
        // Just Read To End - Skip Rest of Document
        while (rdr.Read()) ;
      }
      else
      {
        sb.Append("Can't find <Employees> Element");
        sb.Append(Environment.NewLine);
      }
      rdr.Close();
      rdr.Dispose();

      txtResult.Text = sb.ToString();
    }

    private void btnMoveToContent_Click(object sender, RoutedEventArgs e)
    {
      MoveToContent();
    }

    private void MoveToContent()
    {
      StringBuilder sb = new StringBuilder(512);
      XmlReader rdr;

      // Open an XmlReader
      rdr = XmlReader.Create(AppConfig.GetEmployeesFile());
      // Move to '<Employees>' Element 
      rdr.MoveToContent();
      if (rdr.LocalName.Equals("Employees"))
      {
        // Move to Next Line
        rdr.Read();
        // Move to '<Employee>' Element
        rdr.MoveToContent();
        if (rdr.LocalName.Equals("Employee"))
        {
          // Move to Next Line
          rdr.Read();
          // Move to '<id>' Element
          rdr.MoveToContent();
          if (rdr.LocalName.Equals("id"))
          {
            // Move to the Value of <id>
            rdr.Read();
            sb.AppendFormat("id={0}", rdr.Value);
            sb.Append(Environment.NewLine);
            rdr.Read();
          }
          else
          {
            sb.Append("Can't find <id> Element");
            sb.Append(Environment.NewLine);
          }
        }
        else
        {
          sb.Append("Can't find <Employee> Element");
          sb.Append(Environment.NewLine);
        }
        // Move to Next Line
        rdr.Read();
        // Move to <FirstName> Element
        rdr.MoveToContent();
        if (rdr.LocalName.Equals("FirstName") )
        {
          // Move to Value of <FirstName> Element
          rdr.Read();
          sb.AppendFormat("FirstName={0}", rdr.Value);
          sb.Append(Environment.NewLine);
        }
        else
        {
          sb.Append("Can't find <FirstName> Element");
          sb.Append(Environment.NewLine);
        }
        // Just Read To End - Skip Rest of Document
        while (rdr.Read()) ;
      }
      else
      {
        sb.Append("Can't find <Employees> Element");
        sb.Append(Environment.NewLine);
      }
      rdr.Close();
      rdr.Dispose();

      txtResult.Text = sb.ToString();
    }

    private void btnReadStartElement_Click(object sender, RoutedEventArgs e)
    {
      ReadStartElement();
    }

    private void ReadStartElement()
    {
      StringBuilder sb = new StringBuilder(512);
      XmlReader rdr;

      // Open an XmlReader
      rdr = XmlReader.Create(AppConfig.GetEmployeesFile());
      rdr.ReadStartElement("Employees");
      rdr.ReadStartElement("Employee");
      rdr.ReadStartElement("id");
      sb.AppendFormat("id={0}", rdr.Value);
      sb.Append(Environment.NewLine);
      // Read past the Text Node
      rdr.Read();
      // Move past the /id element
      rdr.ReadEndElement();
      // Read the <FirstName> element
      rdr.ReadStartElement("FirstName");
      sb.AppendFormat("FirstName={0}", rdr.Value);
      sb.Append(Environment.NewLine);
      // Just Read To End - Skip Rest of Document
      while (rdr.Read()) ;
      rdr.Close();
      rdr.Dispose();

      txtResult.Text = sb.ToString();
    }

    private void btnReadElementString_Click(object sender, RoutedEventArgs e)
    {
      ReadElementString();
    }

    private void ReadElementString()
    {
      StringBuilder sb = new StringBuilder(512);
      XmlReader rdr;

      // Open an XmlReader
      rdr = XmlReader.Create(AppConfig.GetEmployeesFile());
      rdr.ReadStartElement("Employees");
      rdr.ReadStartElement("Employee");
      sb.Append("id=" + rdr.ReadElementString("id"));
      sb.Append(Environment.NewLine);
      sb.Append("FirstName=" + rdr.ReadElementString("FirstName"));
      sb.Append(Environment.NewLine);
      // Just Read To End - Skip Rest of Document
      while (rdr.Read()) ;
      // Close Reader
      rdr.Close();
      rdr.Dispose();

      txtResult.Text = sb.ToString();
    }
  }
}
