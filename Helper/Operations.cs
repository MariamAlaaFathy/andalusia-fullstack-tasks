using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace TaskTen.Helper
{
    public static class Operations
    {
        public static readonly
            OperationAuthorizationRequirement
            Create = new() { Name = "Create" };

        public static readonly
            OperationAuthorizationRequirement
            Read = new() { Name = "Read" };

        public static readonly
            OperationAuthorizationRequirement
            Update = new() { Name = "Update" };

        public static readonly
            OperationAuthorizationRequirement
            Delete = new() { Name = "Delete" };
    }
}
