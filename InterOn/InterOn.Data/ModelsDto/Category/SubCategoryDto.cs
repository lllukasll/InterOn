namespace InterOn.Data.ModelsDto.Category
{
    public class SubCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MainCategoryId { get; set; }
        public string SubCategoryPhoto { get; set; }
    }
}