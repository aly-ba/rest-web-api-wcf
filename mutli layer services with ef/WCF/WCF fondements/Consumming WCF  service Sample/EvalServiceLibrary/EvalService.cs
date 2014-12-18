using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace EvalServiceLibrary
{
    [DataContract]
    public class Eval
    {
        public Eval() { }
        public Eval(string submitter, string comments)
        {
            this.Submitter = submitter;
            this.Comments = comments;
            this.Timesent = DateTime.Now;
        }
        [DataMember]
        public string Submitter;
        [DataMember]
        public DateTime Timesent;
        [DataMember]
        public string Comments;
    }

    [ServiceContract]
    public interface IEvalService
    {
        [OperationContract]
        void SubmitEval(Eval eval);
        [OperationContract]
        List<Eval> GetEvals();
    }

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
    public class EvalService : IEvalService
    {
        List<Eval> evals = new List<Eval>();

        #region IEvalService Members

        public void SubmitEval(Eval eval)
        {
            //if (eval.Submitter.Equals("Throw"))
            //    throw new FaultException("Error within SubmitEval");
            evals.Add(eval);
        }

        public List<Eval> GetEvals()
        {
            //System.Threading.Thread.Sleep(5000);
            return evals;
        }

        #endregion
    }
}
