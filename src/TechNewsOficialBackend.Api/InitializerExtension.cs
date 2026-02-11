namespace TechNewsOficialBackend.Api
{
    public class InitializerExtension
    {
        public static WebApplication Initialize(WebApplicationBuilder builder)
        {
            ConfigureServices(builder);

            var app = builder.Build();

            ConfigureMiddleWare(app);
            SeedDatabase(app);

            return app;
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();

            builder.Services.AddDbContext<OracleContext>(opt =>
                opt.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

            builder.Services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(RegisterUser).Assembly));

            builder.Services.AddAutoMapper(typeof(ElementProfile).Assembly);

            builder.Services.AddValidatorsFromAssembly(typeof(ElementRequestDtoValidator).Assembly);

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Description = "API built with Clean Architecture, CQRS and .NET 6",
                    Contact = new OpenApiContact
                    {
                        Name = "Nicolas Liberatto",
                        Url = new Uri("https://github.com/nick27liberatto")
                    }
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });
        }

        public static void ConfigureMiddleWare(WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public static void SeedDatabase(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<OracleContext>();
                    SeedData.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while initializing the database.");
                    throw;
                }
            }
        }
    }
