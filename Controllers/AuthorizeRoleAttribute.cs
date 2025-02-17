﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

public class AuthorizeRoleAttribute : Attribute, IAuthorizationFilter
{
    private readonly string[] _roles; // Cambiar de string a string[]

    public AuthorizeRoleAttribute(params string[] roles)
    {
        _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var claimsIdentity = context.HttpContext.User.Identity as ClaimsIdentity;

        if (claimsIdentity == null || !claimsIdentity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var roleClaim = claimsIdentity.Claims.FirstOrDefault(c => c.Type == "NombreRol");

        if (roleClaim == null || !_roles.Contains(roleClaim.Value))
        {
            context.Result = new ObjectResult(new { message = "No tienes permiso para acceder a este recurso." })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}
