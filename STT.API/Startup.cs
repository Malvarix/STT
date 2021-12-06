using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using STT.Application.AutoMapperProfiles;
using STT.Application.Clients.Implementations.Imdb;
using STT.Application.Clients.Implementations.Imdb.Models.Options;
using STT.Application.Clients.Implementations.Imdb.Models.Request;
using STT.Application.Clients.Implementations.Imdb.Validators;
using STT.Application.Clients.Interfaces;
using STT.Application.Dto.Request;
using STT.Application.DtoValidators.Request;
using STT.Application.Services.Implementations;
using STT.Application.Services.Interfaces;
using STT.Persistence.Extensions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace STT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext(Configuration["STT:ConnectionString"]);

            services.AddAutoMapper(typeof(DtoToEntityMappingProfile), typeof(EntityToDtoMappingProfile));

            services.AddControllers();
            services.AddHttpClient(nameof(HttpClient), client =>
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
            });

            services.AddFluentValidation();
            services.AddTransient<IValidator<SearchRequestModel>, SearchRequestModelValidator>();
            services.AddTransient<IValidator<CreateWatchlistItemRequestDto>, CreateWatchlistItemRequestDtoValidator>();
            services.AddTransient<IValidator<CreateWatchlistRequestDto>, CreateWatchlistRequestDtoValidator>();
            services.AddTransient<IValidator<GetAllWatchlistItemsRequestDto>, GetAllWatchlistItemsRequestDtoValidator>();
            services.AddTransient<IValidator<UpdateWatchlistItemIsWatchedRequestDto>, UpdateWatchlistItemIsWatchedRequestDtoValidator>();

            services.AddOptions();
            services.Configure<ImdbClientOptions>(Configuration.GetSection("ImdbApi"));
            
            services.AddScoped<IImdbClient, ImdbClient>();

            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IWatchlistService, WatchlistService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "STT.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "STT.API v1"));
            }

            app.ApplicationServices.DatabaseMigrate();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}