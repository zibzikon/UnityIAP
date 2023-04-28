using System.Threading.Tasks;
using Unity.Services.Authentication;

namespace Kernel
{
    public static class UserAuthenticator 
    {
        private static readonly IAuthenticationService AuthenticationService = Unity.Services.Authentication.AuthenticationService.Instance;

        public static Task SignInAnonymouslyAsync()
        {
            return AuthenticationService.SignInAnonymouslyAsync();
        }
    }
}