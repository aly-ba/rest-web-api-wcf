using System;
using System.Data;
using System.Text;
using System.Windows;

namespace XML_Processing.Read
{
  public partial class winReadDataSet : Window
  {
    public winReadDataSet()
    {
      InitializeComponent();
    }

    private void btnSimple_Click(object sender, RoutedEventArgs e)
    {
      SimpleDataSet();
    }

    private void SimpleDataSet()
    {
      DataSet ds = new DataSet();
      StringBuilder sb = new StringBuilder(1024);

      ds.ReadXml(AppConfig.GetEmployeesFile());

      foreach (DataRow dr in ds.Tables[0].Rows)
      {
        sb.AppendFormat("id={0}", dr["id"]);
        sb.AppendFormat(Environment.NewLine);
        sb.AppendFormat("FirstName={0}", dr["FirstName"]);
        sb.AppendFormat(Environment.NewLine);
        sb.AppendFormat("LastName={0}", dr["LastName"]);
        sb.AppendFormat(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }

    private void btnSelect_Click(object sender, RoutedEventArgs e)
    {
      SelectSample();
    }

    private void SelectSample()
    {
      DataSet ds = new DataSet();
      StringBuilder sb = new StringBuilder(1024);
      DataRow[] rows;

      ds.ReadXml(AppConfig.GetEmployeesFile());

      rows = ds.Tables[0].Select("LastName LIKE 's%'");

      foreach (DataRow dr in rows)
      {
        sb.AppendFormat("id={0}", dr["id"]);
        sb.AppendFormat(Environment.NewLine);
        sb.AppendFormat("FirstName={0}", dr["FirstName"]);
        sb.AppendFormat(Environment.NewLine);
        sb.AppendFormat("LastName={0}", dr["LastName"]);
        sb.AppendFormat(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }

    private void btnReadSchema_Click(object sender, RoutedEventArgs e)
    {
      ReadSchema();
    }

    private void ReadSchema()
    {
      DataSet ds = new DataSet();
      StringBuilder sb = new StringBuilder(1024);

      // Read Schema File First
      ds.ReadXmlSchema(AppConfig.GetEmployeesSchemaFile());
      ds.ReadXml(AppConfig.GetEmployeesFile());

      txtResult.Text = "Done";
    }

    private void btnReadSchemaBad_Click(object sender, RoutedEventArgs e)
    {
      ReadSchemaBad();
    }

    private void ReadSchemaBad()
    {
      DataSet ds = new DataSet();
      StringBuilder sb = new StringBuilder(1024);

      try
      {
        // Read Schema File First
        ds.ReadXmlSchema(AppConfig.GetEmployeesSchemaFile());
        ds.ReadXml(AppConfig.GetEmployeesBadFile());

        foreach (DataRow dr in ds.Tables[0].Rows)
        {
          sb.AppendFormat("id={0}", dr["id"]);
          sb.AppendFormat(Environment.NewLine);
          sb.AppendFormat("FirstName={0}", dr["FirstName"]);
          sb.AppendFormat(Environment.NewLine);
          sb.AppendFormat("LastName={0}", dr["LastName"]);
          sb.AppendFormat(Environment.NewLine);
        }

        txtResult.Text = sb.ToString();
      }
      catch (Exception ex)
      {
        txtResult.Text = ex.ToString();
      }
    }
  }
}
