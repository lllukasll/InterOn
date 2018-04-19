namespace InterOn.Data.ModelsDto.Event
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public int EventRef { get; set; }
    }
}