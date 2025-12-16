
namespace VollMed.Consultas.Handlers
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.Request.Headers.TryGetValue("Authorization", out var authHeader) == true)
            {
                request.Headers.TryAddWithoutValidation("Authorization", authHeader.ToString());
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
