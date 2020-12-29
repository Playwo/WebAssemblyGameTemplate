namespace WebAssemblyGameTemplate.Shared
{
    public class PlayerUpdateRequest
    {
        public PlayerInfo Player { get; set; }

        public PlayerUpdateRequest(PlayerInfo player)
        {
            Player = player;
        }
    }
}