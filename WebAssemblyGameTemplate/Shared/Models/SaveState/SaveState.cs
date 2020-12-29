using System;
using System.Text.Json.Serialization;

namespace WebAssemblyGameTemplate.Shared
{
    public class SaveState
    {
        /// <summary>
        /// The Id of the <see cref="SaveState"/>.
        /// </summary>
        public Guid Id { get; set; }

        [JsonIgnore]
        /// <summary>
        /// The id of the <see cref="Player"/> who owns this state.
        /// </summary>
        public Guid PlayerId { get; set; }

        [JsonIgnore]
        /// <summary>
        /// A navigation property to the <see cref="Player "/> who owns this <see cref="SaveState"/>.
        /// </summary>
        public Player Player { get; set; } //Nav Property

        public SaveState(Player player)
        {
            Id = Guid.NewGuid();
            PlayerId = player.Id;
            Player = player;
        }
    }
}