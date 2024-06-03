using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.Domain.Models;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Check.GetById;

public sealed class GetCheckByIdRequestHandler(
    IRepositoryFactory factory)
    : IRequestHandler<GetCheckByIdRequest, Result<CheckModel>>
{
    private const string CheckNotFoundError = "Check not found.";

    public async Task<Result<CheckModel>> Handle(GetCheckByIdRequest request, CancellationToken cancellationToken)
    {
        CheckRepository repository = factory.NewCheckRepository();
        CheckEntity? entity = await repository.GetById(request.Id);

        return entity is null ?
            Result<CheckModel>.Error(CheckNotFoundError, ResultErrorStatusCode.NotFound) :
            Result<CheckModel>.Ok(new CheckModel()
            {
                PaidAt = entity.PaidAt,
                Products = entity.Products.Select(x => new ProductModel()
                {
                    Description = x.Product.Description,
                    Id = x.ProductId,
                    Manufacturer = x.Product.Manufacturer,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                }).ToList(),
            },
            ResultSuccessStatusCode.Ok);
    }
}