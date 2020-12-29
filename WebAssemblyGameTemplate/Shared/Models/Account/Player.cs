using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebAssemblyGameTemplate.Shared
{
    public class Player
    {
        /// <summary>
        /// The Id of the <see cref="Player"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The login code of the <see cref="Player"/>.
        /// </summary>
        public string LoginCode { get; set; }

        /// <summary>
        /// The handshake code of the last/current active tab.
        /// </summary>
        public Guid TabCode { get; set; }

        /// <summary>
        /// The time at which the <see cref="Player"/> was created. (UTC)
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// The time at which the <see cref="Player"/> last logged in. (UTC)
        /// </summary>
        public DateTimeOffset LastLoginAt { get; set; }

        /// <summary>
        /// The savestates (saved tabs) of the <see cref="Player"/>.
        /// </summary>
        [JsonIgnore]
        public List<SaveState> SaveStates { get; set; } //Nav Property

        /// <summary>
        /// The id of the <see cref="SaveState"/> that is currently active.
        /// </summary>
        public Guid ActiveStateId { get; set; }

        public Player(string loginCode)
        {
            Id = Guid.NewGuid();
            LoginCode = loginCode;
            CreatedAt = DateTimeOffset.UtcNow;
            LastLoginAt = DateTimeOffset.UtcNow;
        }

        public PlayerInfo GetInfo()
            => new PlayerInfo(LoginCode);

        public void SetInfo(PlayerInfo playerInfo)
        {
            return;
        }
    }
}