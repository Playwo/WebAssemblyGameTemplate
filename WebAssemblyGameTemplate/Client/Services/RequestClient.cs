using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAssemblyGameTemplate.Client.Models;
using WebAssemblyGameTemplate.Shared;

namespace WebAssemblyGameTemplate.Client.Services
{
    public class RequestClient
    {
        private readonly HttpClient Client;
        private readonly NavigationManager NavManager;

        public RequestClient(HttpClient client, NavigationManager navManager)
        {
            Client = client;
            NavManager = navManager;
        }

        public Task<StatusResult<PlayerCreateResult>> CreatePlayerAsync()
            => GetAsync<PlayerCreateResult>(Routes.PlayerCreate(NavManager.BaseUri));

        public Task<StatusResult<PlayerUpdateResult>> UpdatePlayerAsync(PlayerUpdateRequest request)
            => PostAsync<PlayerUpdateResult, PlayerUpdateRequest>(Routes.PlayerUpdate(NavManager.BaseUri), request);

        public Task<StatusResult<StateLoginResult>> LoginStateAsync(string loginCode)
            => GetAsync<StateLoginResult>(Routes.StateLogin(NavManager.BaseUri, loginCode));

        public Task<StatusResult<StateUpdateResult>> UpdateStateAsync(StateUpdateRequest request)
            => PostAsync<StateUpdateResult, StateUpdateRequest>(Routes.StateUpdate(NavManager.BaseUri), request);

        public Task<StatusResult<StateActivationResult>> ActivateTabAsync(StateActivationRequest request)
            => PostAsync<StateActivationResult, StateActivationRequest>(Routes.StateActivate(NavManager.BaseUri), request);

        public async Task<StatusResult<T>> GetAsync<T>(Uri route)
        {
            var response = await Client.GetAsync(route);

            if (!response.IsSuccessStatusCode)
            {
                return StatusResult<T>.Failed(response.StatusCode);
            }

            var result = await response.Content.ReadFromJsonAsync<T>();
            return StatusResult<T>.Success(response.StatusCode, result);
        }

        public async Task<StatusResult<TValue>> PostAsync<TValue, TParam>(Uri route, TParam bodyValue)
        {
            var response = await Client.PostAsJsonAsync(route, bodyValue);

            if (!response.IsSuccessStatusCode)
            {
                return StatusResult<TValue>.Failed(response.StatusCode);
            }

            if (typeof(TValue) != typeof(object))
            {
                var result = await response.Content.ReadFromJsonAsync<TValue>();
                return StatusResult<TValue>.Success(response.StatusCode, result);
            }

            return StatusResult<TValue>.Success(response.StatusCode, default);
        }
    }
}