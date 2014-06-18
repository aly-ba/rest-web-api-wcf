using System;
using System.IO;
using System.Text;
using System.Windows;

namespace XML_Processing.Samples
{
  public partial class winSerialize : Window
  {
    public winSerialize()
    {
      InitializeComponent();
    }

    private void btnEmpToFile_Click(object sender, RoutedEventArgs e)
    {
      EmpToFile();
    }

    private void EmpToFile()
    {
      Employee emp = new Employee();

      emp.Id = 200;
      emp.FirstName = "George";
      emp.LastName = "Jungle";

      ObjectSerializer.ObjectToXmlFile(emp, AppConfig.XmlFile);

      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnFileToEmp_Click(object sender, RoutedEventArgs e)
    {
      FileToEmp();
    }

    private void FileToEmp()
    {
      Employee emp;
      StringBuilder sb = new StringBuilder(512);

      emp = (Employee)ObjectSerializer.XmlFileToObject(AppConfig.XmlFile);

      sb.AppendFormat("id={0}", emp.Id);
      sb.Append(Environment.NewLine);
      sb.AppendFormat("FirstName={0}", emp.FirstName);
      sb.Append(Environment.NewLine);
      sb.AppendFormat("LastName={0}", emp.LastName);

      txtResult.Text = sb.ToString();
    }
  }
}
