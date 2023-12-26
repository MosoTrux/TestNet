using MediatR;
using System.Text.Json.Serialization;
using TestNet.Api.Models.Response;

namespace TestNet.Api.Models.Request
{
    public class UpdateProductRequestModel : IRequest<UpdateProductResponseModel>
    {
        [JsonPropertyName("ProductId")]
        public long Id { get; set; }
        public string Name { get; set; }

        public bool Status { get; set; }

        public decimal Stock { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string UpdatedUser { get; set; }

        [JsonIgnore]
        public DateTime UpdatedAt { get; set; }
    }
}
