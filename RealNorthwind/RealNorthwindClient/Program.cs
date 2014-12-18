using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RealNorthwindClient.ProductServiceRef;
using System.ServiceModel;

namespace RealNorthwindClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductServiceClient client =
                new ProductServiceClient();
            Product product = client.GetProduct(23);
            Console.WriteLine("product name is " +
                    product.ProductName);
            Console.WriteLine("product price is " +
                        product.UnitPrice.ToString());
            product.UnitPrice = 20.0m;
            string message = "";
            bool result = client.UpdateProduct(product,
                ref message);
            Console.WriteLine("Update result is " +
                                result.ToString());
            Console.WriteLine("Update message is " +
                                message);
            // FaultException
            TestException(client, 0);
            // regular C# exception
            TestException(client, 999);

            Console.WriteLine("Press any key to continue ...");
            Console.ReadLine();
        }

        static void TestException(ProductServiceClient client,
            int id)
        {
            if (id != 999)
                Console.WriteLine("\n\nTest Fault Exception");
            else
                Console.WriteLine("\n\nTest normal Exception");

            try
            {
                Product product = client.GetProduct(id);
            }
            catch (TimeoutException ex)
            {
                Console.WriteLine("Timeout exception");
            }
            catch (FaultException<ProductFault> ex)
            {
                Console.WriteLine("ProductFault. ");
                Console.WriteLine("\tFault reason:" +
                    ex.Reason);
                Console.WriteLine("\tFault message:" +
                    ex.Detail.FaultMessage);
            }
            catch (FaultException ex)
            {
                Console.WriteLine("Unknown Fault");
                Console.WriteLine(ex.Message);
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine("Communication exception");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unknown exception");
            }
        }
    }
}
