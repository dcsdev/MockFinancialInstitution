namespace Application
{
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.Extensions.Options;
    using System.Security.Claims;
    using System.Text.Encodings.Web;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Configuration;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Primitives;

    public class ApiKeyAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private const string XApiKeyHeaderName = "X-Api-Key";
        private readonly string _apiKey;

        public ApiKeyAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IConfiguration configuration) : base(options, logger, encoder, clock)
        {
            _apiKey = configuration["APIServerKey"];
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(XApiKeyHeaderName, out StringValues apiKeyHeaderValues))
            {
                return AuthenticateResult.Fail("API Key Is Missing IN Request, Please Verify and Try Again");
            }

            var providedApiKey = apiKeyHeaderValues.FirstOrDefault();
            if (_apiKey == null || providedApiKey != _apiKey)
            {
                return AuthenticateResult.Fail("There is a Key Mismatch, Please Verify and Try Again");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, "ApiKeyUser") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }

}