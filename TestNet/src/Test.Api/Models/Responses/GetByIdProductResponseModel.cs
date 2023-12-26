using System.Text.Json.Serialization;
using TestNet.Infrastructure.Repositories.Mockapi.io;

namespace TestNet.Api.Models.Response;

public class GetByIdProductResponseModel
{
    [JsonPropertyName("ProductId")]
    public long Id { get; set; }

    public string Name { get; set; }

    [JsonIgnore]
    public bool Status { get; set; }
    public string StatusName { get; set; }

    public decimal Stock { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    [JsonIgnore]
    public string CreatedUser { get; set; }

    [JsonIgnore]
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public string? UpdatedUser { get; set; }

    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
    public decimal Discount { get; set; }
    public decimal FinalPrice
    {
        get
        {
            return (Price * (100 - Discount) / 100);
        }
        set { }
    }
}