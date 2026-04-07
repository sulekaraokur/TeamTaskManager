using Microsoft.EntityFrameworkCore;
using TeamTaskManager.API.Data;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Repositories;

//IUserRepoistory sözleşmesiniz(interface) bu sınıfa uyguluyoruz.

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    //Dependency Injection ile AppDbContext'i alıyoruz. 
    // Bu, veritabanı işlemlerini gerçekleştirmek 
    // için kullanacağımız DbContext nesnesidir.
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        //ID'ye göre kullanıcı getirir
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        //Email'e göre kullanıcı getirir(Giriş yaparken kullanacağız)
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddUserAsync(User user)
    {
        //Yeni bir kullanıcı ekler(Kayıt olurken kullanacağız)
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydet
        return user;
    }
    
}

//Dependenc Injection,veritabanına bağlanmak için,
//kendi içimizde yeni bir bağlantı yaratmak yerine
//API'nin ana merkezinden(AppDbContext)
 //bize hazır bir AppDbContext verilmesini(enjekte edilmesini) sağlar.
 //Hız ve güvenlik

 //FirstOrDefaultAsync,
 // Entity Framework Core'un sunduğu bir yetenektir
 //"Users tablosuna git verdiğim şarta (örneğin e-postası şu olan)
 // ilk kaydı bul ve getir.Bulamazsan daa null dön"
 //anlamına gelir.


 //SaveChangesAsync, "AddAsync" ile kullanıcıyı hafızaya
 //ekledikten sonra,
 //bu komutu çağırmazsak veritabanı fiziksel olarak
 //yazılmaz.
 //İşlem commit tuşumuzdur.

 