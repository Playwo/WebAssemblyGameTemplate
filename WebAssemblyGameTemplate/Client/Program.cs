using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebAssemblyGameTemplate.Client.Services;

namespace WebAssemblyGameTemplate.Client
{
    public class Program
    {
        [SuppressMessage("Style", "IDE1006")]
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddSingleton<GameClient>();
            builder.Services.AddSingleton<InvokableService>();
            builder.Services.AddSingleton<RequestClient>();

            await builder.Build().RunAsync();
        }
    }
}