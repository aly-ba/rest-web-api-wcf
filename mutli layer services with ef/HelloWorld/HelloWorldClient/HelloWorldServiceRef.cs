﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17626
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IHelloWorldService")]
public interface IHelloWorldService
{
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHelloWorldService/GetMessage", ReplyAction="http://tempuri.org/IHelloWorldService/GetMessageResponse")]
    string GetMessage(string name);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IHelloWorldService/GetMessage", ReplyAction="http://tempuri.org/IHelloWorldService/GetMessageResponse")]
    System.Threading.Tasks.Task<string> GetMessageAsync(string name);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IHelloWorldServiceChannel : IHelloWorldService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class HelloWorldServiceClient : System.ServiceModel.ClientBase<IHelloWorldService>, IHelloWorldService
{
    
    public HelloWorldServiceClient()
    {
    }
    
    public HelloWorldServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public HelloWorldServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public HelloWorldServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public HelloWorldServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public string GetMessage(string name)
    {
        return base.Channel.GetMessage(name);
    }
    
    public System.Threading.Tasks.Task<string> GetMessageAsync(string name)
    {
        return base.Channel.GetMessageAsync(name);
    }
}
