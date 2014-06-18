using System.Windows;
using XML_Processing.Read;
using XML_Processing.Samples;
using XML_Processing.Write;

namespace XML_Processing
{
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
    }

    private void btnReadXmlReader_Click(object sender, RoutedEventArgs e)
    {
      winReadXmlReader frm = new winReadXmlReader();

      frm.Show();
    }

    private void btnReadXmlDoc_Click(object sender, RoutedEventArgs e)
    {
      winReadXDocument frm = new winReadXDocument();

      frm.Show();
    }

    private void btnReadDataSet_Click(object sender, RoutedEventArgs e)
    {
      winReadDataSet frm = new winReadDataSet();

      frm.Show();
    }

    private void btnReadLINQ_Click(object sender, RoutedEventArgs e)
    {
      winReadLinq frm = new winReadLinq();

      frm.Show();
    }

    private void btnWriteXmlWriter_Click(object sender, RoutedEventArgs e)
    {
      winWriteXmlWriter frm = new winWriteXmlWriter();

      frm.Show();
    }

    private void btnWriteXmlDoc_Click(object sender, RoutedEventArgs e)
    {
      winWriteXDocument frm = new winWriteXDocument();

      frm.Show();
    }

    private void btnWriteDataSet_Click(object sender, RoutedEventArgs e)
    {
      winWriteDataSet frm = new winWriteDataSet();

      frm.Show();
    }

    private void btnWriteLINQ_Click(object sender, RoutedEventArgs e)
    {
      winWriteLINQ frm = new winWriteLINQ();

      frm.Show();
    }

    private void btnSamples_Click(object sender, RoutedEventArgs e)
    {
      winSamples frm = new winSamples();

      frm.Show();
    }
  }
}
