namespace TestNet.Api.Models.Response
{
    public class UpdateProductResponseModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public decimal Stock { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string? UpdatedUser { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
