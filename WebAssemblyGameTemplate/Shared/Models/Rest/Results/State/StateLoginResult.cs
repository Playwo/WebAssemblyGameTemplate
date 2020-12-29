using System;

namespace WebAssemblyGameTemplate.Shared
{
    public class StateLoginResult
    {
        public PlayerInfo PlayerInfo { get; set; }
        public Guid TabCode { get; set; }
        public SaveState State { get; set; }

        public StateLoginResult(PlayerInfo playerInfo, Guid tabCode, SaveState state)
        {
            PlayerInfo = playerInfo;
            TabCode = tabCode;
            State = state;
        }
    }
}