using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using SimpleBotCore.Bot;
using SimpleBotCore.Logic;
using SimpleBotCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBotCore
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

            string connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var questionRepository = new QuestionMongoRepository(client);
            services.AddSingleton<IQuestionMongoRepository>(questionRepository);

            string connectionStringSQL = "Server=#;Database=#;User=#;Password=#";
            var questionSqlRepository = new QuestionSqlRepository(connectionStringSQL);
            services.AddSingleton<IQuestionSqlRepository>(questionSqlRepository);

            services.AddSingleton<IUserProfileRepository>(new UserProfileMockRepository());
            services.AddSingleton<IBotDialogHub, BotDialogHub>();
            services.AddSingleton<BotDialog, SimpleBot>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
