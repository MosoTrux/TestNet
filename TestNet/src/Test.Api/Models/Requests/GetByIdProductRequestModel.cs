using MediatR;
using TestNet.Api.Models.Response;

namespace TestNet.Api.Models.Request;

public class GetByIdProductRequestModel : IRequest<GetByIdProductResponseModel>
{
    public int ProductId { get; set; }
}