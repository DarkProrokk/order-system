namespace Application.Model;

public class ItemQueryDto
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }

    public ItemFilterPagingModel Map()
    {
        var model = new ItemFilterPagingModel();
        var pagingModel = new PagingModel();
        var filterModel = new ItemFilterModel();
        pagingModel.PageSize = PageSize;
        pagingModel.PageNumber = PageNumber;
        model.Paging = pagingModel;
        filterModel.PriceRange.MinPrice = MinPrice;
        filterModel.PriceRange.MaxPrice = MaxPrice;
        model.Filter = filterModel;
        return model;
    }
}