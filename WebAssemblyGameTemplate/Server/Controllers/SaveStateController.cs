using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAssemblyGameTemplate.Server.Models;
using WebAssemblyGameTemplate.Server.Services;
using WebAssemblyGameTemplate.Shared;

namespace WebAssemblyGameTemplate.Server.Controllers
{
    [Route("State")]
    [ApiController]
    public class SaveStateController : ControllerBase
    {
        private readonly GameContext DbContext;
        private readonly ValidationService ValidationService;

        public SaveStateController(GameContext dbContext, ValidationService validationService)
        {
            DbContext = dbContext;
            ValidationService = validationService;
        }

        [HttpGet("Login/{loginCode}")]
        public async Task<ActionResult<StateLoginResult>> StateLoginAsync([FromRoute] string loginCode)
        {
            var player = await DbContext.Players
                .Where(x => x.LoginCode == loginCode)
                .FirstOrDefaultAsync();

            if (player == null)
            {
                return NotFound();
            }

            player.TabCode = TabHelper.IncrementTabCode(player.TabCode);

            var activeState = await DbContext.SaveStates
                .AsNoTracking()
                .Where(x => x.Id == player.ActiveStateId)
                .FirstAsync();

            activeState.Id = Guid.NewGuid();
            player.ActiveStateId = activeState.Id;

            DbContext.SaveStates.Add(activeState);
            await DbContext.SaveChangesAsync();

            var result = new StateLoginResult(player.GetInfo(), player.TabCode, activeState);
            return result;
        }

        [HttpPost("Update")]
        public async Task<ActionResult<StateUpdateResult>> StateUpdateAsync([FromBody] StateUpdateRequest request)
        {
            var oldState = await DbContext.SaveStates
                .Include(x => x.Player)
                .AsNoTracking()
                .Where(x => x.Id == request.SaveState.Id)
                .FirstOrDefaultAsync();

            if (oldState == null)
            {
                return NotFound();
            }

            var player = oldState.Player;
            player.SaveStates = null; //Prevent recursively adding the old state
            request.SaveState.PlayerId = player.Id;
            DbContext.Players.Attach(player);

            player.TabCode = TabHelper.IncrementTabCode(player.TabCode);
            if (player.TabCode != request.TabCode)
            {
                return Conflict();
            }

            var validationResult = ValidationService.ValidateStateUpdate(oldState, request.SaveState);
            if (!validationResult.Valid)
            {
                return BadRequest();
            }

            //Could add Anticheat here (Similar to the validation)

            DbContext.SaveStates.Update(request.SaveState);

            await DbContext.SaveChangesAsync();

            var result = new StateUpdateResult();
            return Ok(result);
        }

        [HttpPost("Activate")]
        public async Task<ActionResult<StateActivationResult>> StateActivateAsync([FromBody] StateActivationRequest request)
        {
            var state = await DbContext.SaveStates
                .Include(x => x.Player)
                .Where(x => x.Id == request.StateId)
                .FirstOrDefaultAsync();

            if (state == null)
            {
                return NotFound();
            }

            var player = state.Player;

            if (player.LoginCode != request.LoginCode)
            {
                return Forbid();
            }

            player.ActiveStateId = request.StateId;
            player.TabCode = Guid.NewGuid();

            await DbContext.SaveChangesAsync();

            var result = new StateActivationResult(player.TabCode);
            return Ok(result);
        }
    }
}