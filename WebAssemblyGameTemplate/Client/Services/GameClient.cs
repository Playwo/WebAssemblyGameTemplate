using System;
using System.Net;
using System.Threading.Tasks;
using WebAssemblyGameTemplate.Client.Models;
using WebAssemblyGameTemplate.Shared;

namespace WebAssemblyGameTemplate.Client.Services
{
    public class GameClient
    {
        private readonly RequestClient Client;

        private string LoginCode { get; set; }
        private Guid TabCode { get; set; }
        private Guid StateId { get; set; }

        public GameClient(RequestClient client)
        {
            Client = client;
        }

        public async Task<RequestResult<PlayerCreateResult>> CreatePlayerAsync()
        {
            var response = await Client.CreatePlayerAsync();

            if (response.IsSuccessStatusCode)
            {
                LoginCode = response.Result.Player.LoginCode;
                return RequestResult<PlayerCreateResult>.Success(response.Result);
            }

            return response.StatusCode switch
            {
                _ => RequestResult<PlayerCreateResult>.ErrorCode(response.StatusCode),
            };
        }

        public async Task<RequestResult<PlayerUpdateResult>> UpdatePlayerAsync(PlayerInfo playerInfo)
        {
            if (LoginCode == default)
            {
                return RequestResult<PlayerUpdateResult>.Failed("This cannot be called before the player is logged in!");
            }

            playerInfo.LoginCode = LoginCode;
            var request = new PlayerUpdateRequest(playerInfo);
            var response = await Client.UpdatePlayerAsync(request);

            return response.IsSuccessStatusCode
                ? RequestResult<PlayerUpdateResult>.Success(response.Result)
                : (response.StatusCode switch
                {
                    HttpStatusCode.NotFound => RequestResult<PlayerUpdateResult>.Failed(
                        "Invalid Savecode, Has it changed?\n" +
                        "Close the tab or message the devs to keep the progress!"),
                    _ => RequestResult<PlayerUpdateResult>.ErrorCode(response.StatusCode),
                });
        }

        public async Task<RequestResult<StateLoginResult>> LoginStateAsync(string loginCode)
        {
            var response = await Client.LoginStateAsync(loginCode);

            if (response.IsSuccessStatusCode)
            {
                LoginCode = response.Result.PlayerInfo.LoginCode;
                TabCode = response.Result.TabCode;
                StateId = response.Result.State.Id;
                return RequestResult<StateLoginResult>.Success(response.Result);
            }

            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => RequestResult<StateLoginResult>.Failed(
                    "Invalid save code!\n" +
                    "Check your spelling!"),
                _ => RequestResult<StateLoginResult>.ErrorCode(response.StatusCode),
            };
        }

        public async Task<RequestResult<StateUpdateResult>> UpdateStateAsync(SaveState state)
        {
            if (TabCode == default)
            {
                return RequestResult<StateUpdateResult>.Failed("This cannot be called before the player is logged in!");
            }

            var request = new StateUpdateRequest(TabCode, state);
            var response = await Client.UpdateStateAsync(request);

            return response.IsSuccessStatusCode
                ? RequestResult<StateUpdateResult>.Success(response.Result)
                : (response.StatusCode switch
                {
                    HttpStatusCode.NotFound => RequestResult<StateUpdateResult>.Failed(
                        "It seems like there is no save related to your current tab.\n" +
                        "Close the tab or message the devs to keep the progress!"),
                    HttpStatusCode.Conflict => RequestResult<StateUpdateResult>.Failed(
                        "You cannot save with this tab because the game has been opened in another one\n"),
                    HttpStatusCode.BadRequest => RequestResult<StateUpdateResult>.Failed(
                        "The server rejected your State Update. The game might be in an errored state.\n" +
                        "Close the tab or message the devs to keep the progress!"),
                    _ => RequestResult<StateUpdateResult>.ErrorCode(response.StatusCode),
                });
        }

        public async Task<RequestResult<StateActivationResult>> ActivateTabAsync()
        {
            if (LoginCode == default || StateId == default)
            {
                return RequestResult<StateActivationResult>.Failed("This cannot be called before the player is logged in!");
            }

            var request = new StateActivationRequest(LoginCode, StateId);
            var response = await Client.ActivateTabAsync(request);

            return response.IsSuccessStatusCode
                ? RequestResult<StateActivationResult>.Success(response.Result)
                : (response.StatusCode switch
                {
                    HttpStatusCode.NotFound => RequestResult<StateActivationResult>.Failed(
                        "It seems like there is no save related to your current tab.\n" +
                        "Close the tab or message the devs to keep the progress!"),
                    HttpStatusCode.Forbidden => RequestResult<StateActivationResult>.Failed(
                        "Invalid Savecode, Has it changed?\n" +
                        "Close the tab or message the devs to keep the progress!"),
                    _ => RequestResult<StateActivationResult>.ErrorCode(response.StatusCode),
                });
        }
    }
}