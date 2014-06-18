using System.Data;
using System.IO;
using System.Text;
using System.Windows;

namespace XML_Processing.Write
{
  public partial class winWriteDataSet : Window
  {
    public winWriteDataSet()
    {
      InitializeComponent();
    }

    private void btnWriteSchema_Click(object sender, RoutedEventArgs e)
    {
      WriteSchema();
    }

    private void WriteSchema()
    {
      DataSet ds = new DataSet();
      StringBuilder sb = new StringBuilder(1024);

      ds.ReadXml(AppConfig.GetEmployeesFile());

      ds.WriteXml(AppConfig.XmlFile);
      ds.WriteXmlSchema(AppConfig.XsdFile);

      txtResult.Text = File.ReadAllText(AppConfig.XsdFile);
    }

    private void btnStringToDataSet_Click(object sender, RoutedEventArgs e)
    {
      StringToDataSet();
    }

    private void StringToDataSet()
    {
      DataSet ds;
      StringReader rdr;
      StringBuilder sb = new StringBuilder(1024);

      sb.Append("<Products>");
      sb.Append("  <Product>");
      sb.Append("    <ProductID>1</ProductID>");
      sb.Append("    <ProductName>Chai</ProductName>");
      sb.Append("    <UnitPrice>18</UnitPrice>");
      sb.Append("    <Discontinued>false</Discontinued>");
      sb.Append("  </Product>");
      sb.Append("  <Product>");
      sb.Append("    <ProductID>2</ProductID>");
      sb.Append("    <ProductName>Chang</ProductName>");
      sb.Append("    <UnitPrice>19</UnitPrice>");
      sb.Append("    <Discontinued>false</Discontinued>");
      sb.Append("  </Product>");
      sb.Append("</Products>");

      rdr = new System.IO.StringReader(sb.ToString());

      ds = new DataSet();
      ds.ReadXml(rdr);
      ds.WriteXml(AppConfig.XmlFile);

      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }
  }
}
