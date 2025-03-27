using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Events.Infastructure.Authentification
{
    public class AuthConfiguration
    {
        public const string ISSUER = "BNTU1";
        public const string AUDIENCE = "OurClients";
        const string KEY = "thiskeyisnotsupposedtobeherebutitsjustpracticesoyeah";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
