using MediatR;
using PharmaCheck.Database.Entities;
using PharmaCheck.EntityFramework.Repositories;
using PharmaCheck.EntityFramework.Repositories.Factories;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Check.New;

public sealed class NewCheckRequestHandler(
    IRepositoryFactory repositoryFactory)
    : IRequestHandler<NewCheckRequest, Result<CheckEntity>>
{
    private const string ErrorWhileCreate = "Error while create new check";

    public async Task<Result<CheckEntity>> Handle(NewCheckRequest request, CancellationToken cancellationToken)
    {
        CheckRepository  repository = repositoryFactory.NewCheckRepository();

        CheckEntity entity = new CheckEntity()
        {
            PharmacyId = request.PharmacyId,
            ClientId = request.ClientId
        };

        try
        {
            await repository.Create(entity);
        }
        catch
        {
            return Result<CheckEntity>.Error(ErrorWhileCreate, ResultErrorStatusCode.InternalError);
        }
        return Result<CheckEntity>.Ok(entity, ResultSuccessStatusCode.Created);
    }
}