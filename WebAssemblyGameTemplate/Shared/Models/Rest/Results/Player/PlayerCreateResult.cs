namespace WebAssemblyGameTemplate.Shared
{
    public class PlayerCreateResult
    {
        public PlayerInfo Player { get; set; }

        public PlayerCreateResult(PlayerInfo player)
        {
            Player = player;
        }
    }
}