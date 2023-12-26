using MediatR;
using System.Reflection;
using TestNet.Api.Models.Request;
using TestNet.Api.Models.Response;
using TestNet.Core.Entities;
using TestNet.Core.Repositories;

namespace TestNet.Api.Handlers.Commands
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequestModel, UpdateProductResponseModel>
    {
        private readonly IDapperUnitOfWork _unitOfWork;
        public UpdateProductCommandHandler(IDapperUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UpdateProductResponseModel> Handle(UpdateProductRequestModel request, CancellationToken cancellationToken)
        {
            var result = _unitOfWork.ProductRepository.GetById(request.Id);

            if(result != null)
            {
                request.UpdatedAt = DateTime.Now;
                _unitOfWork.ProductRepository.Update(ObjectToDictionary(request));
                _unitOfWork.Commit();
            }

            return new UpdateProductResponseModel()
            {
                Id = request.Id,
                Name = request.Name,
                Status = request.Status,
                Stock = request.Stock,
                Description = request.Description,
                Price = request.Price,
                UpdatedUser = request.UpdatedUser,
                UpdatedAt = request.UpdatedAt
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