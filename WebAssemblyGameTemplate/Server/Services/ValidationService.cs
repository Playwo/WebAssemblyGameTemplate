using Common.Services;
using WebAssemblyGameTemplate.Server.Models;
using WebAssemblyGameTemplate.Shared;

namespace WebAssemblyGameTemplate.Server.Services
{
    public class ValidationService : Service
    {
        public ValidationResult ValidateStateUpdate(SaveState oldState, SaveState newState)
        {
            var result = new ValidationResult();

            return result;
        }
    }
}