using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Token
{
    public interface ITokenService
    {
        // Bu metodun parametrelerine, token payload içine gömülmek istene tüm datalar verilebilir.
        string CreateToken(int id, string phone, string name, string lastname, int role);
        Task<List<Claim>> GetUserClaims();
    }
}
