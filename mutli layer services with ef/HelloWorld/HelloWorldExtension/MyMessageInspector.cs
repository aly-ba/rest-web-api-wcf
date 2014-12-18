using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.IO;
using System.Web.Hosting;

namespace HelloWorldExtension
{
    class MyMessageInspector : IDispatchMessageInspector
    {
        private Message TraceMessage(MessageBuffer buffer)
        {
            Message msg = buffer.CreateMessage();
            string appPath = HostingEnvironment.ApplicationPhysicalPath;
            string logPath = appPath + "\\log.txt";
            File.AppendAllText(logPath, DateTime.Now.ToString("G"));
            File.AppendAllText(logPath, "\r\n");
            File.AppendAllText(logPath, "HelloWorldService is invoked");
            File.AppendAllText(logPath, "\r\n");
            File.AppendAllText(logPath, string.Format("Message={0}", msg));
            File.AppendAllText(logPath, "\r\n");
            return buffer.CreateMessage();
        }

        public object AfterReceiveRequest(
            ref Message request, IClientChannel channel,
            InstanceContext instanceContext)
        {
            request =
                   TraceMessage(request.CreateBufferedCopy(int.MaxValue));
            return null;
        }

        public void BeforeSendReply(
            ref Message reply,
            object correlationState)
        {
            reply = TraceMessage(reply.CreateBufferedCopy(int.MaxValue));
        }
    }
}
