using System;
using System.Linq;

namespace WebAssemblyGameTemplate.Shared
{
    public static class TabHelper
    {
        public static Guid IncrementTabCode(Guid tabCode)
        //You might want to add something less obvious here
        //It makes it so that you need to decompile the WebAssembly to send requests manually
        {
            byte[] tabCodeBytes = tabCode.ToByteArray();
            byte[] incrementedBytes = tabCodeBytes.Select(x => x++).ToArray();

            return new Guid(incrementedBytes);
        }
    }
}