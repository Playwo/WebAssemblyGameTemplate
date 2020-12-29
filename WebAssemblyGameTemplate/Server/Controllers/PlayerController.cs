using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAssemblyGameTemplate.Server.Models;
using WebAssemblyGameTemplate.Shared;

namespace WebAssemblyGameTemplate.Server.Controllers
{
    [Route("Player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly GameContext DbContext;

        public PlayerController(GameContext dbContext)
        {
            DbContext = dbContext;
        }

        [HttpGet("Create")]
        public async Task<ActionResult<PlayerCreateResult>> PlayerCreateAsync()
        {
            string loginCode = GenerateLoginCode();
            var player = new Player(loginCode);
            var state = new SaveState(player);

            player.ActiveStateId = state.Id;

            DbContext.Players.Add(player);
            DbContext.SaveStates.Add(state);
            await DbContext.SaveChangesAsync();

            var result = new PlayerCreateResult(player.GetInfo());
            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<ActionResult> PlayerUpdateAsync([FromBody] PlayerUpdateRequest request)
        {
            var account = await DbContext.Players
                .Where(x => x.LoginCode == request.Player.LoginCode)
                .FirstOrDefaultAsync();

            if (account == null)
            {
                return NotFound();
            }

            account.SetInfo(request.Player);
            await DbContext.SaveChangesAsync();
            return Ok();
        }

        private string GenerateLoginCode()
            => Guid.NewGuid().ToString();
    }
}