using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace HelloWorldService
{
    [DataContract]
    public class Name
    {
        [DataMember]
        public string First;
        [DataMember]
        public string Last;
    }

    [ServiceContract]
    public interface IHelloWorld
    {
        [OperationContract]
        string SayHello(Name person);
    }

    public class HelloWorldService : IHelloWorld
    {
        #region IHelloWorld Members

        public string SayHello(Name person)
        {
            return string.Format("Hello {0} {1}",
                person.First, person.Last);
        }

        #endregion
    }
}
