using CriEduc.School.Api.Models;
using CriEduc.School.Border.Dtos.Teacher;
using CriEduc.School.Border.UseCases;
using CriEduc.School.Border.Validators;
using CriEduc.School.Repository.Data;
using CriEduc.School.Repository.Interfaces;
using CriEduc.School.Repository.Repositories;
using CriEduc.School.Repository.UoW;
using CriEduc.School.UseCase.Teacher;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CriEduc.School.Api.Configuration
{
    public static class RepositoryConfiguration
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<DbSession>((session) => {                
                return new DbSession(connectionString: config.GetConnectionString("MyPostgresConnection"));                
            });

            // UseCase
            services.AddScoped<ICreateTeacherUseCase, CreateTeacherUseCase>();
            services.AddScoped<IGetTeacherUseCase, GetTeacherUseCase>();
            services.AddScoped<ISearchTeacherUseCase, SearchTeacherUseCase>();
            services.AddScoped<IUpdateTeacherUseCase, UpdateTeacherUseCase>();

            services.AddScoped<IActionResultConverter, ActionResultConverter>();
            
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ITeachersRepository, TeachersRepository>();

            //Validation
            services.AddScoped<IValidator<CreateTeacherRequest>, CreateTeacherValidation>();
            services.AddScoped<IValidator<TeacherRequest>, TeacherRequestValidation>();
            services.AddScoped<IValidator<UpdateTeacherRequest>, UpdateTeacherValidation>();

            return services;
        }
    }
}
