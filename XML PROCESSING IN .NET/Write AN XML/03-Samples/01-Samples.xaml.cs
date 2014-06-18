using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace XML_Processing.Samples
{
  public partial class winSamples : Window
  {
    public winSamples()
    {
      InitializeComponent();
    }

    private void btnWriteFromDatabase_Click(object sender, RoutedEventArgs e)
    {
      WriteFromDatabase();
    }

    private void WriteFromDatabase()
    {
      DataSet ds = new DataSet("Customers");  // Sets the Root name
      SqlDataAdapter da;

      da = new SqlDataAdapter("SELECT * FROM SalesLT.Customer", AppConfig.ConnectString);

      da.Fill(ds);
      ds.Tables[0].TableName = "Customer"; // Set the Element names

      ds.WriteXml(AppConfig.XmlFile);

      System.Diagnostics.Process.Start(AppConfig.XmlFile);
    }

    private void btnPrototyping_Click(object sender, RoutedEventArgs e)
    {
      winPrototype frm = new winPrototype();

      frm.Show();
    }

    private void btnSerialization_Click(object sender, RoutedEventArgs e)
    {
      winSerialize frm = new winSerialize();

      frm.Show();
    }

    private void btnConfiguration_Click(object sender, RoutedEventArgs e)
    {
      winConfigSettings frm = new winConfigSettings();

      frm.Show();
    }

    private void btnOpenXML_Click(object sender, RoutedEventArgs e)
    {
      MessageBox.Show("Rename Word .docx and show XML");
    }
  }
}
