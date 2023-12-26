namespace TestNet.Api.Models.Response
{
    public class AddProductResponseModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public bool Status { get; set; }

        public decimal Stock { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CreatedUser { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? CreatedUpdated { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
