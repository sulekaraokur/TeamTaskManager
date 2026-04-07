using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

public interface IAuthService
{
    //kayıt olmmma işlemi
    //işlem bitince bize oluşturulan User'ı döndürecek
    Task<User> RegisterAsync(string username,string email, string password);

    //giriş yapma işlemi
    //başarılı olursa ilerde bize bir JW token
    //yani dijitale bir giriş anahtarı dönecek
    //o yüzden string yazdık

    Task<string> LoginAsync(string email, string password);    
}
