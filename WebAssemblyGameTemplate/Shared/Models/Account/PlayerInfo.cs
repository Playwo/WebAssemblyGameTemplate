namespace WebAssemblyGameTemplate.Shared
{
    public class PlayerInfo
    {
        public string LoginCode { get; set; }

        public PlayerInfo(string loginCode)
        {
            LoginCode = loginCode;
        }
    }
}