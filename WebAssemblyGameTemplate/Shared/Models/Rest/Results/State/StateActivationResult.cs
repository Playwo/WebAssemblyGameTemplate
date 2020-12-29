using System;

namespace WebAssemblyGameTemplate.Shared
{
    public class StateActivationResult
    {
        public Guid TabCode { get; set; }

        public StateActivationResult(Guid tabCode)
        {
            TabCode = tabCode;
        }
    }
}