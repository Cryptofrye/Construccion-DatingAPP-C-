using API.Entities;

namespace API.Interface
{
    public interface ITokenService
    {
        string CrearToken(AppUser user);
    }
}
