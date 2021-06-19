using Livraria.Domain.Entities;
using Livraria.Domain.Entities.FolderUsuario;
using Livraria.Domain.Entities.FolderInstituicaoDeEnsino;
using Livraria.Domain.Entities.FolderLivro;
using Livraria.Domain.Entities.FolderUsuarioLivroEmprestado;
using Livraria.Domain.Interfaces.Repositories;
using Livraria.Domain.Interfaces.Services;
using Livraria.Domain.Services;
using Livraria.Infra.Data.Context;
using Livraria.Infra.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.CrossCutting.IoC
{
    public class NativeInjectorMapping
    {
        public static void RegisterServices(IServiceCollection services)
        {
            RegisterServicesDomain(services);
            RegisterServicesRepository(services);
            RegisterValidator(services);
            services.AddScoped<LivrariaContext>();

        }

        private static void RegisterServicesDomain(IServiceCollection services)
        {
            services.AddScoped<ILivroService, LivroService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IInstituicaoDeEnsinoService, InstituicaoDeEnsinoService>();
        }

        private static void RegisterServicesRepository(IServiceCollection services)
        {
            services.AddScoped<ILivroRepository, LivroRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IInstituicaoDeEnsinoRepository, InstituicaoDeEnsinoRepository>();
            services.AddScoped<IUsuarioLivroEmprestadoRepository, UsuarioLivroEmprestadoRepository>();
        }

        private static void RegisterValidator(IServiceCollection services)
        {

            services.AddScoped<UsuarioValidator>();
            services.AddScoped<ValidatorBase>();
            services.AddScoped<LivroValidator>();
            services.AddScoped<InstituicaoDeEnsinoValidator>();
        }
    }
}
