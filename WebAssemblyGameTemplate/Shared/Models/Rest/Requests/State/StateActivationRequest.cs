using System;

namespace WebAssemblyGameTemplate.Shared
{
    public class StateActivationRequest
    {
        public string LoginCode { get; set; }
        public Guid StateId { get; set; }

        public StateActivationRequest(string loginCode, Guid stateId)
        {
            LoginCode = loginCode;
            StateId = stateId;
        }
    }
}