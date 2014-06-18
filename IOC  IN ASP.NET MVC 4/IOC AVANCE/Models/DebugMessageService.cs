using System;
using MvcIoC.Filters;

namespace MvcIoC.Models
{
    public class DebugMessageService : IDebugMessageService
    {
        public string Message { get { return DateTime.Now.ToString(); } }
    }
}