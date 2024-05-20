using Infrastructure.Data;
using Infrastructure.Seed;
using Infrastructure.Services.ClassScheduleService;
using Infrastructure.Services.MembershipService;
using Infrastructure.Services.PaymentService;
using Infrastructure.Services.UserAdminService;
using Infrastructure.Services.UserService;
using Infrastructure.Services.WorkoutService;
using Microsoft.EntityFrameworkCore;

namespace WebApi.ExtensionMethods.RegisterService
{
    public static class RegisterService
    {
        public static void AddRegisterService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(configure =>
                configure.UseNpgsql(configuration.GetConnectionString("Connection")));

            services.AddScoped<Seeder>();
            services.AddScoped<IClassScheduleService, ClassScheduleService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IUserService, UserService>();            
            services.AddScoped<IWorkoutService, WorkoutService>();

        }
    }
}
