namespace TeamTaskManager.API.Entities;

public class Comment
{
    public int Id { get; set; } //Yorumun benzersiz kimliğini temsil eder. Bu, veritabanında her yorum için otomatik olarak artan birincil anahtar olarak kullanılabilir.
    public string Content { get; set; } = string.Empty; //Yorumun içeriğini temsil eder. Bu, yorumun metin içeriğini tutmak için kullanılan bir özelliktir ve genellikle kullanıcı tarafından sağlanır.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //Yorumun oluşturulma tarihini temsil eder. Bu, yorumun ne zaman oluşturulduğunu belirtmek için kullanılan bir DateTime değeridir.

    //Hangi göreve yorum yapıldı?
    public int TaskItemId { get; set; } //Bu, yorumun hangi göreve ait olduğunu belirtmek için kullanılan bir yabancı anahtar (foreign key) özelliğidir. Bu özellik, Comment sınıfının TaskItem sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır.
    public TaskItem? TaskItem { get; set; } //Bu, yorumun ait olduğu görevi temsil eden bir navigasyon özelliğidir. Bu özellik, Comment sınıfının TaskItem sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır. Bu sayede, bir yorum üzerinden ait olduğu göreve kolayca erişilebilir.

    //Yorumu kim yaptı?
    public int UserId { get; set; } //Bu, yorumun hangi kullanıcı tarafından yapıldığını belirtmek için kullanılan bir yabancı anahtar (foreign key) özelliğidir. Bu özellik, Comment sınıfının User sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır. Bu sayede, bir yorum üzerinden hangi kullanıcı tarafından yapıldığını kolayca erişilebilir.
    public User? User { get; set; } //Bu, yorumun hangi kullanıcı tarafından yapıldığını temsil eden bir navigasyon özelliğidir. Bu özellik, Comment sınıfının User sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır. Bu sayede, bir yorum üzerinden hangi kullanıcı tarafından yapıldığını kolayca erişilebilir.

}