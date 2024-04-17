﻿namespace PharmaCheck.Services.Response;

public enum ResultSuccessStatusCode
{
    Ok = 200,
}

public enum ResultErrorStatusCode
{
    BadRequest = 400,
    Unauthorized = 401,
    NotFound = 404,
    InternalError = 500,
}