using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Services;

public class AuthService : IAuthService
{
    
    private readonly IUserRepository _userRepository;

    //Dependecy Injection:
    //Veritabanı işçimizi(Repository) buraya çağırıyoruz

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> RegisterAsync(string username, string email, string password)
    {
        //1.Kural: E-posta adresi sistemde zaten var mı?
        var existingUser = await _userRepository.GetUserByEmailAsync(email);
        if (existingUser != null)
        {
            throw new InvalidOperationException("E-posta adresi zaten kullanılıyor.");
        }
        //2.Kural: Şifreyi döndürülemez şekilde hashle
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        //3.Kural:Yeni kullanıcı nesnesini oluştur ve 
        //Repository'ye (veritabanına) gönder
        var newUser = new User
        {
            Username = username,
            Email = email,
            PasswordHash = passwordHash
        };

        return await _userRepository.AddUserAsync(newUser);
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        //1. Kullanıcıyı e-posta adresinden bul
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
        throw new Exception("E-poste veya şifre hatalı.");
        }
        
        //2.Girilen şifre ile veritabanındaki
        //hashlenmiş şifrei karşılaştır
        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        if (!isPasswordValid)
        {
            throw new Exception("E-posta veya şifre hatalı.");
        }
        
        //3.Şifre doğruysa başarılı mesajı dön
        //(bir sonraki adımda buraya) JWT token ekleyeceğiz.

        
        return "Giriş Başarılı! (Gerçek Token buraya gelecek.)"; // Bu kısmı gerçek JWT token ile değiştireceğiz.
    }
}

//N-tier dikkat edersen AuthService direkt olarak veritabanına
//bağlanmadı, Kontrollerini yaptı ve en son işlemi
//_userRepository'ye devretti.
//N-tier mimarisi bunu sağlıyor.

