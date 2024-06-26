﻿using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.GetProductTypeById;

public sealed record GetProductTypeByIdRequest(Guid Id) : IRequest<Result<ProductTypeModel>>;