using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;

namespace HostCmdLineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host =
            new ServiceHost(typeof(MyWCFServices.HelloWorldService),
            new Uri("http://localhost:1054/HostCmdLineApp/HelloWorldService.svc"));
            host.Open();
            Console.WriteLine("HelloWorldService is now running. ");
            Console.WriteLine("Press any key to stop it ...");
            Console.ReadKey();
            host.Close();
        }
    }
}
