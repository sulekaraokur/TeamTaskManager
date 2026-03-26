namespace TeamTaskManager.API.DTOs;

public class CreateTaskRequest
{
    public int ProjectId { get; set;}
    public string Title{ get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;

}