using TestNet.Api.Controllers;
using TestNet.Api.Models.Request;
using TestNet.Api.Models.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using Xunit;
using Microsoft.Extensions.Logging;
using static Dapper.SqlMapper;
using Azure.Core;

namespace TestNet.Tests;

public class UnitTest1
{
    private Mock<IMediator> _mediator;

    public UnitTest1()
    {
        _mediator = new Mock<IMediator>();
    }

    [Fact]
    public void AddProduct_Success_Result()
    {
        _mediator.Setup(a => a.Send(It.IsAny<AddProductRequestModel>(), new CancellationToken()))
            .ReturnsAsync(
            new AddProductResponseModel
            {
                Id = 2,
                Name = "Name Product",
                Status = true,
                Stock = 200,
                Description = "Description Product",
                Price = 99.99M,
                CreatedUser = "MosoTrux"
            });

        var controller = new ProductController(_mediator.Object);

        //Action
        var result = controller.Post(new AddProductRequestModel());

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }
    [Fact]
    public void UpdateProduct_Success_Result()
    {
        _mediator.Setup(a => a.Send(It.IsAny<UpdateProductRequestModel>(), new CancellationToken()))
            .ReturnsAsync(
            new UpdateProductResponseModel
            {
                Id = 2,
                Name = "Name Product",
                Status = true,
                Stock = 200,
                Description = "Description Product",
                Price = 99.99M,
                UpdatedUser = "MosoTrux",
                UpdatedAt = DateTime.Now
            });

        var controller = new ProductController(_mediator.Object);

        //Action
        var result = controller.Put(new UpdateProductRequestModel());

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public void GetProductById_Success_Result()
    {
        _mediator.Setup(a => a.Send(It.IsAny<GetByIdProductRequestModel>(), new CancellationToken()))
            .ReturnsAsync(
            new GetByIdProductResponseModel
            {
                Id = 7,
                Name = "Updated Product",
                StatusName = "Inactive",
                Stock = 5,
                Description = "Updated Description",
                Price = 999.99M,
                Discount = 0.1M,
                FinalPrice = 899.99M
            });

        var controller = new ProductController(_mediator.Object);

        //Action
        var result = controller.Get(new GetByIdProductRequestModel());

        //Assert
        Assert.IsType<OkObjectResult>(result);
    }
}