using Microsoft.Identity.Client;

namespace TeamTaskManager.API.Entities;

public class Project
{
    public int Id { get; set; } //Proje sınıfının benzersiz kimliğini temsil eder. Bu, veritabanında her proje için otomatik olarak artan birincil anahtar olarak kullanılabilir.
    public string Name { get; set; } = string.Empty; //Projenin adını temsil eder. Bu, projenin tanımlayıcı bir özelliğidir ve genellikle kullanıcı tarafından sağlanır.
    public string Description { get; set; } = string.Empty; //Projenin açıklamasını temsil eder. Bu, projenin amacını veya içeriğini açıklamak için kullanılabilir.
    public DateTime CreatedAt
     { get; set; } = DateTime.UtcNow; //Projenin oluşturulma tarihi

    //Eğitim notu: bir projenin içinde birden fazla görev olabilir,
    //1-N ilişkisidir

    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>(); //Projenin içinde birden fazla görevi temsil eder. Bu, projenin görevlerini tutmak için kullanılan bir koleksiyondur ve her görev, Task sınıfında tanımlanan özelliklere sahip olabilir.
}