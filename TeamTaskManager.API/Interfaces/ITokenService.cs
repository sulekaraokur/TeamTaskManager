using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface ITokenService
{
    //Kullanıcı nesnesini alacak ve geriye 
    //şifrelenmiş Token metnini (string) dönecek

    string CreateToken(User user);

} 

