using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using System.Security.Claims;

namespace ContosoDashboard.Services
{
    /// <summary>
    /// Custom authentication state provider for Blazor Server with cookie authentication
    /// </summary>
    public class CustomAuthenticationStateProvider : RevalidatingServerAuthenticationStateProvider
    {
        public CustomAuthenticationStateProvider(
            ILoggerFactory loggerFactory,
            IServiceScopeFactory serviceScopeFactory)
            : base(loggerFactory)
        {
        }

        protected override TimeSpan RevalidationInterval => TimeSpan.FromMinutes(30);

        protected override Task<bool> ValidateAuthenticationStateAsync(
            AuthenticationState authenticationState, CancellationToken cancellationToken)
        {
            // For the mock authentication system, we'll accept the authentication state as-is
            // In a production system, you would validate the user still exists and has valid permissions
            return Task.FromResult(true);
        }
    }
}
