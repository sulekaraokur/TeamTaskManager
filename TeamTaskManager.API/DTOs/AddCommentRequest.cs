namespace TeamTaskManager.API.DTOs;

public class AddCommentRequest
{
    //Müşteriden(kullanıcıdan) sadece yorumun metnini istiyoruz.
    public string Content { get; set; } = string.Empty;

}