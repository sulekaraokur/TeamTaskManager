namespace TeamTaskManager.API.Entities;//Bu dosyanın projedeki adresidir,
//(TeamTaskManager.API.Entities) Başka bir dosyada bu kullanıcıyı çağırmak istediğimizde bu adresi kullanırız. 

public class User
{   //EF Core, sınıfları veritabanı tablolarına dönüştürürken, genellikle birincil anahtar olarak kullanılan bir "Id" özelliği bekler. Bu nedenle, "Id" özelliği eklenmiştir.
    //direkt olarak primary key olarak kullanılır ve genellikle otomatik olarak artan
    // bir değere sahiptir. Bu, her kullanıcıya benzersiz bir kimlik sağlar ve veritabanında kolayca sorgulanabilir hale getirir.
    public int Id { get; set;} //property'ler, sınıfın özelliklerini 
    // tanımlar. Bu özellikler, sınıfın örnekleri tarafından kullanılabilir ve genellikle veri tutmak için kullanılır.
    public string Username {get; set;} = string.Empty;
    public string Email {get; set;} = string.Empty;
    public string PasswordHash {get; set;} = string.Empty;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;    
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;    

}
