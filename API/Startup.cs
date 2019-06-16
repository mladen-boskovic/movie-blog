using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using API.Email;
using API.Helpers;
using Application.Auth;
using Application.Commands.CommentCommands;
using Application.Commands.GenreCommands;
using Application.Commands.ImageCommands;
using Application.Commands.LikeCommands;
using Application.Commands.MovieCommands;
using Application.Commands.RoleCommands;
using Application.Commands.UserCommands;
using Application.Interfaces;
using EfCommands.CommentCommands;
using EfCommands.GenreCommands;
using EfCommands.ImageCommands;
using EfCommands.LikeCommands;
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
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;

namespace API
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

			services.AddTransient<IGetCommentsCommand, EfGetCommentsCommand>();
			services.AddTransient<IGetCommentCommand, EfGetCommentCommand>();
			services.AddTransient<IAddCommentCommand, EfAddCommentCommand>();
			services.AddTransient<IEditCommentCommand, EfEditCommentCommand>();
			services.AddTransient<IDeleteCommentCommand, EfDeleteCommentCommand>();

			services.AddTransient<IGetGenresCommand, EfGetGenresCommand>();
			services.AddTransient<IGetGenreCommand, EfGetGenreCommand>();
			services.AddTransient<IAddGenreCommand, EfAddGenreCommand>();
			services.AddTransient<IEditGenreCommand, EfEditGenreCommand>();
			services.AddTransient<IDeleteGenreCommand, EfDeleteGenreCommand>();

			services.AddTransient<IGetRolesCommand, EfGetRolesCommand>();
			services.AddTransient<IGetRoleCommand, EfGetRoleCommand>();
			services.AddTransient<IAddRoleCommand, EfAddRoleCommand>();
			services.AddTransient<IEditRoleCommand, EfEditRoleCommand>();
			services.AddTransient<IDeleteRoleCommand, EfDeleteRoleCommand>();

			services.AddTransient<IAddDeleteLikeCommand, EfAddDeleteLikeCommand>();

			services.AddTransient<IAddImageCommand, EfAddImageCommand>();

            var section = Configuration.GetSection("Email");
			var sender = new SmtpEmailSender(section["host"], Int32.Parse(section["port"]), section["fromaddress"], section["password"]);
			services.AddSingleton<IEmailSender>(sender);

			services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
			var key = Configuration.GetSection("Encryption")["key"];
			var enc = new Encryption(key);
			services.AddSingleton(enc);
			services.AddTransient(s => {
				var http = s.GetRequiredService<IHttpContextAccessor>();
				var value = http.HttpContext.Request.Headers["Authorization"].ToString();
				var encryption = s.GetRequiredService<Encryption>();

				try
				{
					var decodedString = encryption.DecryptString(value);
					decodedString = decodedString.Substring(0, decodedString.LastIndexOf("}") + 1);
					var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);
					user.IsLogged = true;
					return user;
				}
				catch (Exception)
				{
					return new LoggedUser
					{
						IsLogged = false
					};
				}
			});

            services.AddTransient<ICheckUsersCredentials, EfCheckUsersCredentials>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MovieBlog API", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
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
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
			app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MovieBlog API V1");
            });
        }
	}
}
