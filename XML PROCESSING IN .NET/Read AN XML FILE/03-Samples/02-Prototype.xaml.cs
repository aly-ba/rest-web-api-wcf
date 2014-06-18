using System.Data;
using System.Windows;

namespace XML_Processing.Samples
{
  public partial class winPrototype : Window
  {
    public winPrototype()
    {
      InitializeComponent();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      DataSet ds = new DataSet();

      ds.ReadXml(AppConfig.GetEmployeesFile());

      lvEmps.View = PDSA.WPF.PDSAWPFListView.CreateGridViewColumns(ds.Tables[0]);
      lvEmps.DataContext = ds.Tables[0];
    }
  }
}
