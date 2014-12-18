using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloWorldClientSecure
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            var client = new HelloWorldServiceRef.HelloWorldServiceClient();
            // replace the user credential to your own one in following code
            client.ClientCredentials.UserName.UserName = "your_user_name";
            client.ClientCredentials.UserName.Password = "your_password";
            Console.WriteLine(client.GetMessage("Basic Authentication caller"));
            */

            var client = new HelloWorldServiceRef.HelloWorldServiceClient();
            client.ChannelFactory.Credentials.Windows.ClientCredential =
                System.Net.CredentialCache.DefaultNetworkCredentials;
            Console.WriteLine(client.GetMessage("Windows Authentication caller"));
        }
    }
}
