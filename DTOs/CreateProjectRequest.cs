namespace TeamTaskManager.API.DTOs;

public class CreateProjectRequest
{
    public string Name { get; set;} = string.Empty;
    public string Description { get; set;} = string.Empty;
}

//Data Transfer Object (DTO) olarak adlandırılan bu sınıf, istemciden gelen proje oluşturma isteğini temsil eder. 
//İstemci, bu sınıfın özelliklerini doldurarak yeni bir proje oluşturmak istediğinde, API bu verileri alır ve işleyerek yeni bir proje oluşturur. 
//DTO'lar, genellikle API'nin dış dünyaya açılan yüzü olarak kullanılır ve veri transferi sırasında kullanılan yapıları tanımlar.   