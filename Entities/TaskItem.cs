namespace TeamTaskManager.API.Entities;

public class TaskItem
{
    public int Id { get; set; } //Görev öğesinin benzersiz kimliğini temsil eder. Bu, veritabanında her görev için otomatik olarak artan birincil anahtar olarak kullanılabilir.
    public string Title { get; set; } = string.Empty; //Görevin başlığını temsil eder. Bu, görevin tanımlayıcı bir özelliğidir ve genellikle kullanıcı tarafından sağlanır.
    public string Description { get; set; } = string.Empty; //Görevin açıklamasını temsil eder. Bu, görevin amacını veya içeriğini açıklamak için kullanılabilir.
    public string Status { get; set; } = "Todo"; //Görevin tamamlanıp tamamlanmadığını temsil eder. Bu, görevin durumunu belirtmek için kullanılan bir boolean değerdir.
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; //Görevin oluşturulma tarihini temsil eder. Bu, görevin ne zaman oluşturulduğunu belirtmek için kullanılan bir DateTime değeridir.

    //Eğitim notu: Bir görev yalnızca bir projeye ait olabilir,
    //N-1 ilişkisidir
    
    public int ProjectId { get; set; } //Bu, görevin hangi projeye ait olduğunu belirtmek için kullanılan bir yabancı anahtar (foreign key) özelliğidir. Bu özellik, TaskItem sınıfının Project sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır.
    public Project? Project { get; set; } //Bu, görevin ait olduğu projeyi temsil eden bir navigasyon özelliğidir. Bu özellik, TaskItem sınıfının Project sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır. Bu sayede, bir görev öğesi üzerinden ait olduğu projeye kolayca erişilebilir.

    //Bu görev kime atandı?
    public int? AssignedUserId { get; set; } //Bu, görevin atanmış olduğu kullanıcının kimliğini temsil eden bir yabancı anahtar (foreign key) özelliğidir. Bu özellik, TaskItem sınıfının User sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır. Bu sayede, bir görev öğesi üzerinden atanmış olduğu kullanıcıya kolayca erişilebilir.
    public User? AssignedUser { get; set; } //Bu, görevin atanmış olduğu kullanıcıyı temsil eden bir navigasyon özelliğidir. Bu özellik, TaskItem sınıfının User sınıfıyla ilişkili olduğunu gösterir ve veritabanında bu ilişkiyi kurmak için kullanılır. Bu sayede, bir görev öğesi üzerinden atanmış olduğu kullanıcıya kolayca erişilebilir.


    //Bir görevin birden fazla yorumu olabilir,
    //1-N ilişkisidir
    public ICollection<Comment> Comments { get; set; } = []; //Görevin yorumlarını temsil eder. Bu, görevin yorumlarını tutmak için kullanılan bir koleksiyondur ve her yorum, Comment sınıfında tanımlanan özelliklere sahip olabilir.    
}