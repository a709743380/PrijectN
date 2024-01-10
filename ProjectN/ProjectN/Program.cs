using Microsoft.OpenApi.Models;
using ProjectN.Repository.Implement;
using ProjectN.Service.Interface;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ProjectN.Service.Implement;

namespace ProjectN
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            #region  註冊界面
            builder.Services.AddScoped<ICardRepository, CardRepository>();
            builder.Services.AddScoped<ICardService, CardService>();
            builder.Services.AddScoped<CardRepository>();
            builder.Services.AddScoped<CardService>();
            #endregion

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                // API 服務簡介
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "菜雞練習 API",
                    Description = "菜雞新訓記的範例 練習 API",
                    TermsOfService = new Uri("https://igouist.github.io/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Igouist",
                        Url = new Uri("https://igouist.github.io/about/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "TEST",
                        Url = new Uri("https://igouist.github.io/about/"),
                    }
                });
                //可以在API顯示summary
                //.csproj 檔案，並找到 PropertyGroup 加上 GenerateDocumentationFile
                /*
                   <PropertyGroup>
                    <GenerateDocumentationFile>true</GenerateDocumentationFile>
                   </PropertyGroup>
                 */
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            builder.Services.AddScoped<IDbConnection>((_) =>
                new SqlConnection(builder.Configuration.GetConnectionString("Conn")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}