﻿using BlogAPI.PL.Common.Middlewares;
using System.Net;
using System.Security.Claims;

namespace BlogAPI.PL.Common.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            var userIdString = GetInfoByDataName(principal, "userId");

            if (!int.TryParse(userIdString, out int userId))
            {
                throw new BusinessException(HttpStatusCode.BadRequest, $"Unable to parse userId '{userIdString}' to an integer.");
            }

            return userId;
        }

        private static string GetInfoByDataName(ClaimsPrincipal principal, string name)
        {
            var data = principal.FindFirstValue(name);

            if (data == null)
            {
                throw new BusinessException(HttpStatusCode.BadRequest, $"No such data as {name} in Token");
            }

            return data;
        }
    }

}
