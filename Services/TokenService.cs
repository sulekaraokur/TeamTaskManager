using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;

namespace TeamTaskManager.API.Services;

public class TokenService : ITokenService
{
    //IConfiguration:
    //appsetting.json dosyasını okumamızı sağlayan araç

    private readonly IConfiguration _config;
    private readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        //appsetting.json içindeki "Key"
        //değerini okuyup makinenin anlayacağı bir şifreleme
        //anahtarına dönüştürüyoruz
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
    }

    public string CreateToken(User user)
    {
        //1.Claims(Kullanıcı bilgileri): Token içine koyacağımız veriler
        var claims =new List<Claim>
        {
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };


        //2.Credentials(Kimlik Bilgileri):Gizli şifremizi kullanarak
        //oluşturduğumuz dijital imza
        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256Signature);
   
        //3.Token Descriptor(Tanımlayıcı): Token'ın içeriği ve özellikleri
        //(Kimlik bilgileri,süresi,imzası)
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7), // Bu anahtar 7 gün boyunca geçerli olacak
            SigningCredentials = creds,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["Jwt:Audience"]
        };

        //4.Token Handler(Üretici): Planı alıp gerçek Token metnini
        //üreten makine
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        //Oluşturulan token'ı metin(string) olarak geri döndürüyoruz
        return tokenHandler.WriteToken(token);
   
    } 
 
 }

