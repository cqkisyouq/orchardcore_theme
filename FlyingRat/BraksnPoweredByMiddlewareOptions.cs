using OrchardCore.Modules;

namespace FlyingRat
{
    public class BraksnPoweredByMiddlewareOptions : IPoweredByMiddlewareOptions
    {
        private const string PoweredByHeaderName = "X-Powered-By";
        private const string PoweredByHeaderValue = "OrchardCore";
        public bool Enabled { get; set; } = false;

        public string HeaderName => PoweredByHeaderName;

        public string HeaderValue { get; set; } = PoweredByHeaderValue;
    }
}
