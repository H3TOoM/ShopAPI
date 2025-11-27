using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ShopAPI.Controllers;

/// <summary>
/// Provides shared helpers for API controllers.
/// </summary>
public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult HandleException(Exception exception)
        => exception switch
        {
            ArgumentNullException or ArgumentException => BadRequest(exception.Message),
            KeyNotFoundException => NotFound(exception.Message),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };

    protected ActionResult<T> HandleException<T>(Exception exception)
        => exception switch
        {
            ArgumentNullException or ArgumentException => BadRequest(exception.Message),
            KeyNotFoundException => NotFound(exception.Message),
            _ => StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
        };
}

