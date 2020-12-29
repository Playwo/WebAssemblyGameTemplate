using System;

namespace WebAssemblyGameTemplate.Shared
{
    public class StateUpdateRequest
    {
        public Guid TabCode { get; set; }
        public SaveState SaveState { get; set; }

        public StateUpdateRequest(Guid tabCode, SaveState saveState)
        {
            TabCode = tabCode;
            SaveState = saveState;
        }
    }
}