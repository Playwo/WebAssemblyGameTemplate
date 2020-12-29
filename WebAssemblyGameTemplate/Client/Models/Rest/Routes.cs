using System;

namespace WebAssemblyGameTemplate.Client.Models
{
    public static class Routes
    {
        public static Uri PlayerCreate(string baseUri)
            => new Uri($"{baseUri}Player/Create");

        public static Uri PlayerUpdate(string baseUri)
            => new Uri($"{baseUri}Player/Update");

        public static Uri StateLogin(string baseUri, string loginCode)
            => new Uri($"{baseUri}State/Login/{loginCode}");

        public static Uri StateUpdate(string baseUri)
            => new Uri($"{baseUri}State/Update");

        public static Uri StateActivate(string baseUri)
            => new Uri($"{baseUri}State/Activate");
    }
}