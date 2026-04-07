namespace TeamTaskMaager.API.DTOs;

public class PaginationFilter
{
   //Varsayılan olarak her zaman 1.sayfayı getir.
   public int PageNumber {get; set;} = 1;

   //varsayılan olarak her sayfada 10 kayıt gösterir
   private int _pageSize = 10;

   //müşteri sayfada 1000 kayıt isteyip sistemi yormasın diye
   //maksimum limiti 50 ile sınırlandırıyoruz. 
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > 50) ? 50 : value; 
 }

}

//Repo güncelleyeceğiz.