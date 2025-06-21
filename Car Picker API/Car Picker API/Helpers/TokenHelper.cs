using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Car_Picker_API.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateJWTToken(string userId, string roleName)
        {
            //inlization handler
            var JWTTokenHandler = new JwtSecurityTokenHandler();
            //setup token Key
            //1- long secrect
            //2- convert secrect to bytes
            string secrect = "LongPrimarySecrectForPasswordManageApplicationASPCoreModuleForDevelopementPurppose";
            var tokenBytesKey = Encoding.UTF8.GetBytes(secrect);
            //setup Token Descriptior (Claims , Expiry , Signiture)
            var descriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(2),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId",userId),
                    new Claim("Role",roleName)
                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenBytesKey), SecurityAlgorithms.HmacSha256Signature)
            };
            //encoding data to a json format 
            var tokenJson = JWTTokenHandler.CreateToken(descriptor);
            //encoding json result as token string 
            var token = JWTTokenHandler.WriteToken(tokenJson);
            return token;
        }
        public static string IsValidToken(string token)
        {
            try
            {
                var JWTTokenHandler = new JwtSecurityTokenHandler();
                string secrect = "LongPrimarySecrectForPasswordManageApplicationASPCoreModuleForDevelopementPurppose";
                var tokenBytesKey = Encoding.UTF8.GetBytes(secrect);

                var tokenValidatorParms = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenBytesKey),
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }; var tokenBase = JWTTokenHandler.ValidateToken(token, tokenValidatorParms, out SecurityToken validatedToken);

                return "Valid";
            }

            catch (Exception ex)
            {
                return $"InValid {ex.Message}";
            }

        }
        public static bool IsValidToken(string token, string requiredRole)
        {
            //check if null
            if (string.IsNullOrWhiteSpace(token) || string.IsNullOrEmpty(token))
                return false;
            //decode the recived token 
            var decodedToken = new JwtSecurityTokenHandler().ReadJwtToken(token);

            //check expriation date
            if (decodedToken.ValidTo > DateTime.UtcNow)
            {
                // token not expired
                var userRole = decodedToken.Claims.FirstOrDefault(c => c.Type == "Role");
                if (userRole != null)
                    if (userRole.Value != null && userRole.Value.Equals(requiredRole))
                    {
                        return true;
                    }
                return false;
            }
            else
            {
                //token expired
                return false;
            }
        }
    }
}
