﻿using MediatR;
using PharmaCheck.Domain.ProductType.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.ProductType.GetProductTypeByName;

public sealed record GetProductTypeByNameRequest(string Name) : IRequest<Result<ProductTypeModel>>;