using Application.Behaviors;
using FluentValidation;
using LibraryIdentityProvider.EFCore;
using LibraryIdentityProvider.Features.AuthenticationAuthorization.PasswordSecurity;
using LibraryIdentityProvider.Features.UserManagement;
using LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryIdentityProvider
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(Program).Assembly.FullName)
                ));

            builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);

            builder.Services.AddMediatR(configuration => configuration.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior <,>));

            builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly, includeInternalTypes: true);

            AddRepositories(builder.Services);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserAccountRepository, UserAccountRepository>();
            services.AddScoped<IPasswordRepository, PasswordRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
        }
    }
}