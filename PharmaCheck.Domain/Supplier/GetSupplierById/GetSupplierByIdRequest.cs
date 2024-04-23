using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supplier.GetSupplierById;

public sealed record GetSupplierByIdRequest(Guid Id) : IRequest<Result<SupplierModel>>;