using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;

namespace Kernel
{
    public class UnityGamingServiceLoader
    {
        public static async Task LoadAsync()
        {
            await UnityServices.InitializeAsync();
        }
    }
}