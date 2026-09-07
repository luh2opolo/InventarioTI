// Helper para obtener el UserId del token JWT.
// Se usa dentro de los servicios para registrar auditoría.

using System.IdentityModel.Tokens.Jwt;

namespace InventarioTI.Helpers
{
    public static class UserContextHelper
    {
        // Extrae el UserId del token JWT enviado en el header Authorization.
        public static int GetUserIdFromToken(string authorizationHeader)
        {
            if (string.IsNullOrWhiteSpace(authorizationHeader))
                return 0;

            if (!authorizationHeader.StartsWith("Bearer "))
                return 0;

            var token = authorizationHeader.Substring("Bearer ".Length);

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var claim = jwt.Claims.FirstOrDefault(c => c.Type == "UserId");

            if (claim == null)
                return 0;

            return int.Parse(claim.Value);
        }
    }
}