using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.GenreCommands;
using Application.Commands.ImageCommands;
using Application.Commands.MovieCommands;
using Application.Commands.RoleCommands;
using Application.Commands.UserCommands;
using Application.Interfaces;
using EfCommands.GenreCommands;
using EfCommands.ImageCommands;
using EfCommands.MovieCommands;
using EfCommands.RoleCommands;
using EfCommands.UserCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication.Email;

namespace WebApplication
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});


			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddDbContext<MovieBlogContext>();

			services.AddTransient<IGetMoviesCommand, EfGetMoviesCommand>();
			services.AddTransient<IGetMovieCommand, EfGetMovieCommand>();
			services.AddTransient<IAddMovieCommand, EfAddMovieCommand>();
			services.AddTransient<IEditMovieCommand, EfEditMovieCommand>();
			services.AddTransient<IDeleteMovieCommand, EfDeleteMovieCommand>();

			services.AddTransient<IGetUsersCommand, EfGetUsersCommand>();
			services.AddTransient<IGetUserCommand, EfGetUserCommand>();
			services.AddTransient<IAddUserCommand, EfAddUserCommand>();
			services.AddTransient<IEditUserCommand, EfEditUserCommand>();
			services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();

			services.AddTransient<IAddImageCommand, EfAddImageCommand>();

			services.AddTransient<IGetAllUsersCommand, EfGetAllUsersCommand>();
			services.AddTransient<IGetAllGenresCommand, EfGetAllGenresCommand>();

            services.AddTransient<IGetAllRolesCommand, EfGetAllRolesCommand>();

            var section = Configuration.GetSection("Email");
            var sender = new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);
            services.AddSingleton<IEmailSender>(sender);
        }

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});

            app.UseStaticFiles();
        }
	}
}
