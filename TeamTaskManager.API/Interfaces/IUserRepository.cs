using TeamTaskManager.API.Entities;

namespace TeamTaskManager.API.Interfaces;

public interface IUserRepository
{
    //ID'ye göre kullanıcı getirir
    //Bulamazsa boş dönebilir, bu yüzden User? yazacağız.
    Task<User?> GetUserByIdAsync(int id);

    //Email'e göre kullanıcı getirir(Giriş yaparken kullanacağız)
    Task<User?> GetUserByEmailAsync(string email);

    //Yeni bir kullanıcı ekler(Kayıt olurken kullanacağız)
    Task<User> AddUserAsync(User user);

}

//Task ve Async ekleyerek, 
//bu işlemlerin veritabanı işlemleri olduğunu 
//ve asenkron olarak çalışacağını belirtiyoruz. 
//Bu, uygulamanın performansını artırır
// ve kullanıcı deneyimini iyileştirir.
