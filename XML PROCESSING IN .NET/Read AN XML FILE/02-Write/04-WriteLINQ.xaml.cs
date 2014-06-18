using System.IO;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace XML_Processing.Write
{
  public partial class winWriteLINQ : Window
  {
    public winWriteLINQ()
    {
      InitializeComponent();
    }

    private void btnWriteXDoc_Click(object sender, RoutedEventArgs e)
    {
      WriteUsingXDocument();
    }

    private void WriteUsingXDocument()
    {
      var emp =
        new XDocument(
         new XDeclaration("1.0", "utf-8", "yes"),
         new XComment("Employee List"),
         new XElement("Employees",
            new XElement("Employee",
            new XElement("id", "99"),
            new XElement("FirstName", "Henry"),
            new XElement("LastName", "Ford")
            )
          )
        );

      emp.Save(AppConfig.XmlFile);
      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnWriteXElement_Click(object sender, RoutedEventArgs e)
    {
      WriteUsingXElement();
    }

    private void WriteUsingXElement()
    {
      var emp =
         new XElement("Employees",
            new XElement("Employee",
            new XElement("id", "99"),
            new XElement("FirstName", "Henry"),
            new XElement("LastName", "Ford")
            )
        );

      emp.Save(AppConfig.XmlFile);
      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnAddConstructor_Click(object sender, RoutedEventArgs e)
    {
      AddUsingConstructor();
    }

    private void AddUsingConstructor()
    {
      var xElem = XElement.Load(AppConfig.GetEmployeesFile());
      
      var emp =
         new XElement("Employee",
            new XElement("ID", "99"),
            new XElement("FirstName", "John"),
            new XElement("LastName", "Brongo")
          );

      xElem.Add(emp);

      xElem.Save(AppConfig.XmlFile);
      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnAddClone_Click(object sender, RoutedEventArgs e)
    {
      AddClone();
    }

    private void AddClone()
    {
      var xElem = XElement.Load(AppConfig.GetEmployeesFile());

      // This assumes at least one element exists
      // Must use FirstOrDefault(), not First() as we 
      // want a return value, even if is null
      var emp = (from e in xElem.Descendants("Employee")
                 select e).FirstOrDefault();

      if (emp != null)
      {
        var newEmp = new XElement(emp);

        newEmp.Element("id").Value = "199";
        newEmp.Element("FirstName").Value = "Khanh";
        newEmp.Element("LastName").Value = "Vu";

        xElem.Add(newEmp);
      }
      else
      {
        // Did not find an element, do it the other way
        var emp2 =
           new XElement("Employee",
              new XElement("ID", "199"),
              new XElement("FirstName", "Khanh"),
              new XElement("LastName", "Vu")
          );

        xElem.Add(emp2);
      }

      xElem.Save(AppConfig.XmlFile);
      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnUpdate_Click(object sender, RoutedEventArgs e)
    {
      UpdateEmployee();
    }

    private void UpdateEmployee()
    {
      var xElem = XElement.Load(AppConfig.XmlFile);

      var emp = (from e in xElem.Descendants("Employee")
                 where e.Element("id").Value == "199"
                 select e).SingleOrDefault();

      if (emp != null)
      {
        emp.Element("LastName").Value = "Changed Name";
      }

      xElem.Save(AppConfig.XmlFile);
      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }

    private void btnDelete_Click(object sender, RoutedEventArgs e)
    {
      DeleteEmployee();
    }

    private void DeleteEmployee()
    {
      var xElem = XElement.Load(AppConfig.XmlFile);

      var emp = (from e in xElem.Descendants("Employee")
                 where e.Element("id").Value == "199"
                 select e).SingleOrDefault();

      if (emp != null)
      {
        emp.Remove();

        xElem.Save(AppConfig.XmlFile);
      }
      txtResult.Text = File.ReadAllText(AppConfig.XmlFile);
    }
  }
}
