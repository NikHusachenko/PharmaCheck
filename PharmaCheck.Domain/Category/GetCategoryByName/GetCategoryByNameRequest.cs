﻿using MediatR;
using PharmaCheck.Domain.Models;
using PharmaCheck.Services.Response;

namespace PharmaCheck.Domain.Category.GetCategoryByName;

public sealed record GetCategoryByNameRequest(string Name) : IRequest<Result<CategoryModel>>;