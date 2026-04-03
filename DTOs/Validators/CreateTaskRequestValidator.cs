using FluentValidation;
using TeamTaskManager.API.DTOs;

namespace TeamTaskManager.API.DTOs.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        //Başlık boş olamaz ve en az üç karakter içermelidir.
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Görev başlığı boş olamaz.")
            .MinimumLength(3).WithMessage("Görev başlığı en az 3 karakter olmalıdır.")
            .MaximumLength(100).WithMessage("Görev başlığı en fazla 100 karakter olabilir.");

        //Açıklama en fazla 500 karakter olabilir.
        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Görev açıklaması en fazla 500 karakter olabilir.");
        //Proje ID'si mutlaka 0'dan büyük olmalıdır
        RuleFor(x => x.ProjectId)
            .GreaterThan(0).WithMessage("Geçerli bir proje ID'si girilmelidir.");
    }
}
//AbstractValidator<T> : FluentValidation'dan gelen temel bir sınıftır
//Hangi DTO'yu denetleyeceğimizi(CreateTaskRequest) buraya yazarız

//RuleFor(x => x.Prop) : Hangi özellik(property) için kural yazacağımızı seçeriz

//.NotEmpty() , .MinimumLength() bunlar zincirleme(fluent) şekilde eklenen kurallardır
//WithMessage() : Kural ihlal edildiğinde dönecek hata mesajını belirtir.

