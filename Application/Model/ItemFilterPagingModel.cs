namespace Application.Model;

public class ItemFilterPagingModel
{
   public PagingModel Paging { get; set; } = new();
   public ItemFilterModel? Filter { get; set; }
}