using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Collections.ObjectModel;

namespace HelloWorldExtension
{
    class MyMessageInspectorBehavior : Attribute, IServiceBehavior
    {
        void IServiceBehavior.AddBindingParameters(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase,
            Collection<ServiceEndpoint> endpoints,
            BindingParameterCollection bindingParameters)
        {
            //do nothing
        }

        void IServiceBehavior.ApplyDispatchBehavior(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcher cDispatcher
                in serviceHostBase.ChannelDispatchers)
                foreach (EndpointDispatcher eDispatcher
                    in cDispatcher.Endpoints)
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(
                        new MyMessageInspector());
        }

        void IServiceBehavior.Validate(
            ServiceDescription serviceDescription,
            ServiceHostBase serviceHostBase)
        {
            //do nothing
        }
    }
}
