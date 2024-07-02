using Microsoft.AspNetCore.Mvc;
using Talabat.Core;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Service;
using Talabate.PL.ErrorsHandle;
using Talabate.PL.Helper;

namespace Talabate.PL.Extention
{
    public static class ApplicationServices
    {
        public static IServiceCollection  AddAplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<ITokenService, TokenService>();
            
            Services.AddAutoMapper(typeof(MappingProfile));
            Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                             .SelectMany(p => p.Value.Errors)
                                             .Select(e => e.ErrorMessage)
                                             .ToArray();
                    var validtionErrorResponse = new ApiValidtionErrorRespones()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validtionErrorResponse);
                };
            }
            );

            return Services;

        }
    }
}
