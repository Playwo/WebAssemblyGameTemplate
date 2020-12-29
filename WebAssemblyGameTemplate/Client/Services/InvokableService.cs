using System.Threading.Tasks;
using Microsoft.JSInterop;
using WebAssemblyGameTemplate.Client.Models;
using WebAssemblyGameTemplate.Shared;

namespace WebAssemblyGameTemplate.Client.Services
{
    public class InvokableService
    {
        private static GameClient Client;

        public InvokableService(GameClient client)
        {
            Client = client;
        }

        [JSInvokable]
        public static Task<RequestResult<PlayerCreateResult>> CreatePlayerAsync()
            => Client.CreatePlayerAsync();

        [JSInvokable]
        public static Task<RequestResult<PlayerUpdateResult>> UpdatePlayerAsync(PlayerInfo playerInfo)
            => Client.UpdatePlayerAsync(playerInfo);

        [JSInvokable]
        public static Task<RequestResult<StateLoginResult>> LoginStateAsync(string loginCode)
            => Client.LoginStateAsync(loginCode);

        [JSInvokable]
        public static Task<RequestResult<StateUpdateResult>> UpdateStateAsync(SaveState state)
            => Client.UpdateStateAsync(state);

        [JSInvokable]
        public static Task<RequestResult<StateActivationResult>> ActivateTabAsync()
            => Client.ActivateTabAsync();
    }
}