using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldClientTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            HelloWorldServiceRef.HelloWorldServiceClient client =
                new HelloWorldServiceRef.HelloWorldServiceClient();
            Console.WriteLine(client.GetMessage("Mike Liu"));
        }
    }
}
