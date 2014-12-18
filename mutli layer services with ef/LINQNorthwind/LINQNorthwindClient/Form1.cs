using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LINQNorthwindClient.ProductServiceRef;
using System.ServiceModel;

namespace LINQNorthwindClient
{
    public partial class Form1 : Form
    {
        Product product;
        BackgroundWorker bw;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetProductDetail_Click(object sender, EventArgs e)
        {
            var client = new ProductServiceClient();
            string result = "";

            try
            {
                int productID = Int32.Parse(txtProductID.Text);
                product = client.GetProduct(productID);

                var sb = new StringBuilder();
                sb.Append("ProductID:" +
                    product.ProductID.ToString() + "\r\n");
                sb.Append("ProductName:" +
                    product.ProductName + "\r\n");
                sb.Append("QuantityPerUnit:" +
                    product.QuantityPerUnit + "\r\n");
                sb.Append("UnitPrice:" +
                    product.UnitPrice.ToString() + "\r\n");
                sb.Append("Discontinued:" +
                    product.Discontinued.ToString() + "\r\n");
                sb.Append("RowVersion:");
                foreach (var x in product.RowVersion.AsEnumerable())
                {
                    sb.Append(x.ToString());
                    sb.Append(" ");
                }

                result = sb.ToString();
            }
            catch (TimeoutException ex)
            {
                result = "The service operation timed out. " +
                    ex.Message;
            }
            catch (FaultException<ProductFault> ex)
            {
                result = "ProductFault returned: " +
                    ex.Detail.FaultMessage;
            }
            catch (FaultException ex)
            {
                result = "Unknown Fault: " +
                    ex.ToString();
            }
            catch (CommunicationException ex)
            {
                result = "There was a communication problem. " +
                    ex.Message + ex.StackTrace;
            }
            catch (Exception ex)
            {
                result = "Other exception: " +
                    ex.Message + ex.StackTrace;
            }

            txtProductDetails.Text = result;
        }

        private void btnUpdatePrice_Click(object sender, EventArgs e)
        {
            string result = "";

            if (product != null)
            {
                try
                {
                    // update its price
                    product.UnitPrice =
                       Decimal.Parse(txtNewPrice.Text);

                    var client = new ProductServiceClient();
                    var sb = new StringBuilder();
                    string message = "";
                    sb.Append("Price updated to ");
                    sb.Append(txtNewPrice.Text);
                    sb.Append("\r\n");
                    sb.Append("Update result:");
                    sb.Append(client.UpdateProduct(ref product,
                              ref message).ToString());
                    sb.Append("\r\n");
                    sb.Append("Update message: ");
                    sb.Append(message);
                    sb.Append("\r\n");
                    sb.Append("New RowVersion:");
                    foreach (var x in product.RowVersion.AsEnumerable())
                    {
                        sb.Append(x.ToString());
                        sb.Append(" ");
                    }
                    result = sb.ToString();
                }
                catch (TimeoutException ex)
                {
                    result = "The service operation timed out. " +
                             ex.Message;
                }
                catch (FaultException<ProductFault> ex)
                {
                    result = "ProductFault returned: " +
                              ex.Detail.FaultMessage;
                }
                catch (FaultException ex)
                {
                    result = "Unknown Fault: " +
                              ex.ToString();
                }
                catch (CommunicationException ex)
                {
                    result = "There was a communication problem. " +
                             ex.Message + ex.StackTrace;
                }
                catch (Exception ex)
                {
                    result = "Other exception: " +
                              ex.Message + ex.StackTrace;
                }
            }
            else
            {
                result = "Get product details first";
            }

            txtUpdateResult.Text = result;
        }


        private void btnAutoUpdate_Click(object sender, EventArgs e)
        {
            if (product != null)
            {
                btnAutoUpdate.Text = "Updating Price ...";
                btnAutoUpdate.Enabled = false;

                bw = new BackgroundWorker();
                bw.WorkerReportsProgress = true;
                bw.DoWork += AutoUpdatePrice;
                bw.ProgressChanged += PriceChanged;
                bw.RunWorkerCompleted += AutoUpdateEnd;
                bw.RunWorkerAsync();
            }
            else
            {
                txtUpdateResult.Text = "Get product details first";
            }
        }

        private void AutoUpdateEnd(object sender,
                RunWorkerCompletedEventArgs e)
        {
            btnAutoUpdate.Text = "&Auto Update";
            btnAutoUpdate.Enabled = true;
        }

        private void PriceChanged(object sender, ProgressChangedEventArgs e)
        {
            txtUpdateResult.Text = e.UserState.ToString();
            // Scroll to end of textbox
            txtUpdateResult.SelectionStart = txtUpdateResult.TextLength - 4;
            txtUpdateResult.ScrollToCaret();
        }
        
        private void AutoUpdatePrice(object sender, DoWorkEventArgs e)
        {
            var client = new ProductServiceClient();
            string result = "";
            try
            {
                // update its price
                for (int i = 0; i < 100; i++)
                {
                    // refresh the product first
                    product = client.GetProduct(product.ProductID);

                    // update its price
                    product.UnitPrice += 1.0m;

                    var sb = new StringBuilder();
                    String message = "";
                    sb.Append("Price updated to ");
                    sb.Append(product.UnitPrice.ToString());
                    sb.Append("\r\n");
                    sb.Append("Update result:");
                    bool updateResult = client.UpdateProduct(ref product,
                                             ref message);
                    sb.Append(updateResult.ToString());
                    sb.Append("\r\n");
                    sb.Append("Update message: ");
                    sb.Append(message);
                    sb.Append("\r\n");
                    sb.Append("New RowVersion:");
                    foreach (var x in product.RowVersion.AsEnumerable())
                    {
                        sb.Append(x.ToString());
                        sb.Append(" ");
                    }
                    sb.Append("\r\n");

                    sb.Append("Price updated ");
                    sb.Append((i + 1).ToString());
                    sb.Append(" times\r\n\r\n");

                    result += sb.ToString();

                    // report progress
                    bw.ReportProgress(i + 1, result);

                    // sleep a while
                    var random = new Random();
                    int randomNumber = random.Next(0, 1000);
                    System.Threading.Thread.Sleep(randomNumber);
                }
            }
            catch (TimeoutException ex)
            {
                result += "The service operation timed out. " +
                          ex.Message;
            }
            catch (FaultException<ProductFault> ex)
            {
                result += "ProductFault returned: " +
                         ex.Detail.FaultMessage;
            }
            catch (FaultException ex)
            {
                result += "Unknown Fault: " +
                         ex.ToString();
            }
            catch (CommunicationException ex)
            {
                result += "There was a communication problem. " +
                          ex.Message + ex.StackTrace;
            }
            catch (Exception ex)
            {
                result += "Other exception: " +
                          ex.Message + ex.StackTrace;
            }

            // report progress
            bw.ReportProgress(100, result);
        }

    }
}
