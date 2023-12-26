using MediatR;
using TestNet.Api.Models.Response;

namespace TestNet.Api.Models.Request
{
    public class AddProductRequestModel : IRequest<AddProductResponseModel>
    {
        public string Name { get; set; }

        public bool Status { get; set; }

        public decimal Stock { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string CreatedUser { get; set; }
    }
}
