using System;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml.Linq;
using System.Xml.XPath;

namespace XML_Processing.Read
{
  public partial class winReadLinq : Window
  {
    public winReadLinq()
    {
      InitializeComponent();
    }
    
    private void btnSelectXPathSingle_Click(object sender, RoutedEventArgs e)
    {
      SelectXPathSingle();
    }

    private void SelectXPathSingle()
    {
      StringBuilder sb = new StringBuilder(1024);
      var xElem = XElement.Load(AppConfig.GetEmployeesFile());

      // Get a Specific Employee
      var emp = xElem.XPathSelectElement("//Employee[id='1']");

      if (emp != null)
      {
        sb.AppendFormat("id={0}", emp.Element("id").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("FirstName={0}",
          emp.Element("FirstName").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("LastName={0}",
          emp.Element("LastName").Value);
        sb.Append(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }

    private void btnSelectLINQSingle_Click(object sender, RoutedEventArgs e)
    {
      SelectLINQSingle();
    }

    private void SelectLINQSingle()
    {
      StringBuilder sb = new StringBuilder(1024);
      var xElem = XElement.Load(
        AppConfig.GetEmployeesFile());

      // Get a Specific Employee
      var emp = (from e in xElem.Descendants("Employee")
                 where e.Element("id").Value == "1"
                 select e).SingleOrDefault();

      if (emp != null)
      {
        sb.AppendFormat("id={0}", emp.Element("id").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("FirstName={0}",
          emp.Element("FirstName").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("LastName={0}",
          emp.Element("LastName").Value);
        sb.Append(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }

    private void btnSelectXPathMultiple_Click(object sender, RoutedEventArgs e)
    {
      SelectXPathMultiple();
    }

    private void SelectXPathMultiple()
    {
      var xElem = XElement.Load(AppConfig.GetEmployeesFile());
      StringBuilder sb = new StringBuilder(1024);

      // Get All Employees with LastName starting with 'S'
      var emps = xElem.XPathSelectElements(
        "//Employee[LastName[starts-with(.,'S')]]");

      foreach (var emp in emps)
      {
        sb.AppendFormat("id={0}", emp.Element("id").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("FirstName={0}",
          emp.Element("FirstName").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("LastName={0}",
          emp.Element("LastName").Value);
        sb.Append(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }

    private void btnSelectLINQMultiple_Click(object sender, RoutedEventArgs e)
    {
      SelectLINQMultiple();
    }

    private void SelectLINQMultiple()
    {
      StringBuilder sb = new StringBuilder(1024);
      var xElem = XElement.Load(AppConfig.GetEmployeesFile());

      // Get All Employees with LastName starting with 'S'
      var emps = from emp in xElem.Descendants("Employee")
                 where emp.Element("LastName").Value.StartsWith("S")
                 select emp;

      foreach (var emp in emps)
      {
        sb.AppendFormat("id={0}", emp.Element("id").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("FirstName={0}",
          emp.Element("FirstName").Value);
        sb.Append(Environment.NewLine);
        sb.AppendFormat("LastName={0}",
          emp.Element("LastName").Value);
        sb.Append(Environment.NewLine);
      }

      txtResult.Text = sb.ToString();
    }
  }
}
