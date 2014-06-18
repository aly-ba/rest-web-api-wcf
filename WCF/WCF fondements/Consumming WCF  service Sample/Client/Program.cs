using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Client.EvalServiceReference;
using System.ServiceModel.Description;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            #region example using ChannelFactory<T> directly
            //ChannelFactory<IEvalService> cf =
            //    new ChannelFactory<IEvalService>("WSHttpBinding_IEvalService");
            //IEvalService channel = cf.CreateChannel();
            #endregion

            Console.WriteLine("Retrieving endpoints via MEX...");

            ServiceEndpointCollection endpoints = 
                MetadataResolver.Resolve(typeof(IEvalService),
                    new EndpointAddress("http://localhost:8080/evals/mex"));

            foreach (ServiceEndpoint se in endpoints)
            {
                EvalServiceClient channel =
                    new EvalServiceClient(se.Binding, se.Address);

                try
                {
                    EvalServiceLibrary.Eval eval =
                        new EvalServiceLibrary.Eval("Aaron", "I'm really liking this");
                    channel.SubmitEval(eval);
                    channel.SubmitEval(eval);

                    List<EvalServiceLibrary.Eval> evals = channel.GetEvals();
                    Console.WriteLine("Number of evals: {0}", evals.Count);

                    #region async example
                    //channel.GetEvalsCompleted += new EventHandler<GetEvalsCompletedEventArgs>(channel_GetEvalsCompleted);
                    //channel.GetEvalsAsync();
                    //Console.WriteLine("Waiting...");
                    //Console.ReadLine();
                    #endregion

                    channel.Close();
                }
                catch (FaultException fe)
                {
                    Console.WriteLine("FaultException handler: {0}",
                        fe.GetType());
                    channel.Abort();
                }
                catch (CommunicationException ce)
                {
                    Console.WriteLine("CommunicationException handler: {0}",
                        ce.GetType());
                    channel.Abort();
                }
                catch (TimeoutException te)
                {
                    Console.WriteLine("TimeoutException handler: {0}",
                        te.GetType());
                    channel.Abort();
                }
            }
        }

        static void channel_GetEvalsCompleted(object sender, GetEvalsCompletedEventArgs e)
        {
            Console.WriteLine("Number of evals: {0}", e.Result.Count);
        }
    }
}
