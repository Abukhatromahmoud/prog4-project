using D6UWHX_HFT_2021221.Data;
using D6UWHX_HFT_2021221.EndPoint.Services;
using D6UWHX_HFT_2021221.Logic.Classes;
using D6UWHX_HFT_2021221.Logic.Interfaces;
using D6UWHX_HFT_2021221.Models;
using D6UWHX_HFT_2021221.Repository.Classes;
using D6UWHX_HFT_2021221.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace D6UWHX_HFT_2021221.EndPoint
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAlbumLogic<Album>, AlbumLogic>();
            services.AddTransient<IArtistLogic<Artist>, ArtistLogic>();
            services.AddTransient<ITrackLogic<Track>, TrackLogic>();

            services.AddTransient<IAlbumRepository<Album>, AlbumRepository>();
            services.AddTransient<IArtistRepository<Artist>, ArtistRepository>();
            services.AddTransient<ITrackRepository<Track>, TrackRepository>();

            services.AddTransient<MusicLibraryContext>();
            //services.AddSingleton<MusicLibraryContext, MusicLibraryContext>();
            services.AddSignalR();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowCredentials()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithOrigins("http://localhost:48275"));


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SignalRHub>("/hub");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Album API");
            });
        }
    }
}
