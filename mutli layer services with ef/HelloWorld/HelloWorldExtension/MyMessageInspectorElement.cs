using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ServiceModel.Configuration;

namespace HelloWorldExtension
{
    class MyMessageInspectorElement : BehaviorExtensionElement
    {
        public override Type BehaviorType
        {
            get { return typeof(MyMessageInspectorBehavior); }
        }
        protected override object CreateBehavior()
        {
            return new MyMessageInspectorBehavior();
        }
    }
}
