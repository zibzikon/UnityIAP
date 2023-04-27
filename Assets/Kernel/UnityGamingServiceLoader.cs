using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

namespace Kernel
{
    public class UnityGamingServiceLoader
    {
        public static async Task LoadAsync()
        {
            var options = new InitializationOptions();
            
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            options.SetEnvironmentName("test");
#else
            options.SetEnvironmentName("production");
#endif
            
           await UnityServices.InitializeAsync(options);
        }
    }
}