using MediatR;
using System.Reflection;
using TestNet.Api.Models.Request;
using TestNet.Api.Models.Response;
using TestNet.Application.Services.Interfaces;
using TestNet.Core.Entities;
using TestNet.Core.Repositories;

namespace TestNet.Api.Handlers.Commands
{
    public class AddProductCommandHandler : IRequestHandler<AddProductRequestModel, AddProductResponseModel>
    {
        private readonly IDapperUnitOfWork _unitOfWork;
        public AddProductCommandHandler(IDapperUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddProductResponseModel> Handle(AddProductRequestModel request, CancellationToken cancellationToken)
        {
            Product entity = new Product
            {
                Name = request.Name,
                Status = request.Status,
                Stock = request.Stock,
                Description = request.Description,
                Price = request.Price,  
                CreatedUser = request.CreatedUser
            };

            _unitOfWork.ProductRepository.Add(entity, ObjectToDictionary(request));
            _unitOfWork.Commit();

            return new AddProductResponseModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Status = entity.Status,
                Stock = entity.Stock,
                Description = entity.Description,
                Price = entity.Price,
                CreatedUser = entity.CreatedUser
            };
        }

        private Dictionary<string, object> ObjectToDictionary(object obj)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string propertyName = property.Name;
                object propertyValue = property.GetValue(obj);
                dict.Add(propertyName, propertyValue);
            }
            return dict;
        }
    }
}