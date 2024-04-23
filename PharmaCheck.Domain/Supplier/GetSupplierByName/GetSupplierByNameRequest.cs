using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Supplier.GetSupplierByName;

public sealed record GetSupplierByNameRequest(string Name) : IRequest<Result<SupplierModel>>;