using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using TestNet.Api.Models.Request;
using TestNet.Api.Models.Response;
using TestNet.Application.Services.Implements;
using TestNet.Application.Services.Interfaces;
using TestNet.Core.Entities;
using TestNet.Core.Options;
using TestNet.Core.Repositories;
using TestNet.Core.Repositories.Mockapi.io;
using TestNet.Infrastructure.Repositories;
using TestNet.Infrastructure.Repositories.Mockapi.io;

namespace TestNet.Api.Handlers.Queries;
public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductRequestModel, GetByIdProductResponseModel>
{
    private readonly IDapperUnitOfWork _unitOfWork;
    private readonly IMockapiIORespository _mockapiIORespository;
    private readonly IProductService _productService;
    public GetByIdProductQueryHandler(
        IDapperUnitOfWork unitOfWork, 
        IMockapiIORespository mockapiIORespository,
        IProductService productService)
    {
        _unitOfWork = unitOfWork;
        _mockapiIORespository = mockapiIORespository;
        _productService = productService;
    }
    public async Task<GetByIdProductResponseModel> Handle(GetByIdProductRequestModel request, CancellationToken cancellationToken)
    {
        var result = _unitOfWork.ProductRepository.GetById(request.ProductId);
        if (result == null) return new GetByIdProductResponseModel();

        var discountResponse = _mockapiIORespository.GetDiscount(request.ProductId);

        var discount = 0;
        if (discountResponse != null)
        {
            if (discountResponse.discount < 0) discount = 0;
            else if (discountResponse.discount > 70) discount = 70;
        }

        var states = _productService.GetProductStates();

        return new GetByIdProductResponseModel()
        {
            Id = result.Id,
            Name = result.Name,
            Status = result.Status,
            StatusName = states[Convert.ToInt32(result.Status)],
            Stock = result.Stock,
            Description = result.Description,
            Price = result.Price,
            CreatedUser = result.CreatedUser,
            CreatedAt = result.CreatedAt,
            UpdatedUser = result.UpdatedUser,
            UpdatedAt = result.UpdatedAt,
            Discount = discount,
        };
    }


}