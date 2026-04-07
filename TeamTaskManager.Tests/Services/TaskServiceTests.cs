using AutoMapper;
using Moq;
using TeamTaskManager.API.DTOs;
using TeamTaskManager.API.Entities;
using TeamTaskManager.API.Interfaces;
using TeamTaskManager.API.Services;
using Xunit;

namespace TeamTaskManager.Tests.Services;

public class TaskServiceTests
{
    private readonly Mock<ITaskRepository> _mockTaskRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly TaskService _taskService;

    public TaskServiceTests()
    {
        //1.Arrange: Service'ın bağımlılıklarını mock(sahte) olarak üretiyoruz
        _mockTaskRepository = new Mock<ITaskRepository>();
        _mockMapper = new Mock<IMapper>();

        //service i sahte nesnelerle(veritabanına bağlanmayan kopyalarla)
        //ayağa kaldırıyoruz.
        _taskService = new TaskService(_mockTaskRepository.Object, _mockMapper.Object);
    }

    [Fact] //bunun bir test metodu olduğunu xUnit'e bildirir
    public async Task CreateTaskAsync_ShouldReturnTaskItem_WhenCalled()
    {
        //Arrange-Test verileri
        var request = new CreateTaskRequest
        {
            Title = "Test Task",
            ProjectId=1
        };

        var mappedTask = new TaskItem
        {
            Id = 1,
            Title = "Test Task",
            ProjectId =1
        };
        //sahte mapper'a komut : "sana bu request gelirse
        // bana bu mappedTask'ı döndür"
        _mockMapper.Setup(m => m.Map<TaskItem>(request)).Returns(mappedTask);

        //sahte repository'e komut: "AddTaskAsync çağrıldığında
        //başarılı birTask dön"
       _mockTaskRepository.Setup(repo => repo.AddTaskAsync(It.IsAny<TaskItem>())).ReturnsAsync(mappedTask); 
    
        //2.Act: Şefin metodunu(service)gerçekten çalıştırır
        var result = await _taskService.CreateTaskAsync(request);

        //3.Assert:Sonuçları kontrol et
            Assert.NotNull(result); //Sonuç null olmamalı
            Assert.Equal("Test Task", result.Title); //Başlık bizim verdiğimiz başlık olmalı

        //Bu metodun gerçekten tam 1 kere çağırılıp çağrılmadığını doğrular
        _mockTaskRepository.Verify(repo => repo.AddTaskAsync(It.IsAny<TaskItem>()), Times.Once);    
    }
}       