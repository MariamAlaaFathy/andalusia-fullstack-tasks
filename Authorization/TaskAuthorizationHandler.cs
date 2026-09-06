using FullStackSession6.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.Security.Claims;

namespace TaskTen.Authorization
{
    public class TaskAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Tasks>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Tasks resource)
        {
            var userId = context.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (resource.UserId.ToString() == userId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
