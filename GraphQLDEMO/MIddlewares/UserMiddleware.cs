using FirebaseAdminAuthentication.DependencyInjection.Models;
using GraphQLDEMO.Schema.Courses.Models;
using HotChocolate.Resolvers;
using System.Security.Claims;

namespace GraphQLDEMO.MIddlewares
{
    public class UserMiddleware
    {
        private readonly FieldDelegate nextMiddleware;

        public UserMiddleware(FieldDelegate nextMiddleware)
        {
            this.nextMiddleware = nextMiddleware;
        }

        public async Task Invoke(IMiddlewareContext context) 
        {
            if (context.ContextData.TryGetValue("ClaimsPrincipal", out object claims) && claims is ClaimsPrincipal claimsPrincipal)
            {
                var user = new UserModel()
                {
                    Id = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.ID),
                    Email = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL),
                    Verified = bool.TryParse(claimsPrincipal.FindFirstValue(FirebaseUserClaimType.EMAIL_VERIFIED), out bool verified) ? verified : false,
                    UserName = claimsPrincipal.FindFirstValue(FirebaseUserClaimType.USERNAME),
                };
                context.ContextData.Add("User", user);
            }
            await nextMiddleware(context);        
        }
    }
}
