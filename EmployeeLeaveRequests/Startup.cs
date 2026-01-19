using AutoMapper;
using EmployeeLeaveRequests.Domain.Persistence.Contexts;
using EmployeeLeaveRequests.Domain.Persistence.Repositories;
using EmployeeLeaveRequests.Domain.Services;
using EmployeeLeaveRequests.Exceptions;
using EmployeeLeaveRequests.Extensions;
using EmployeeLeaveRequests.Mapping;
using EmployeeLeaveRequests.Persistence.Repository;
using EmployeeLeaveRequests.Services;
using EmployeeLeaveRequests.Services.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi;

namespace EmployeeLeaveRequests
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Add CORS Support
            services.AddCors();

            services.AddControllers();

            //Database

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Dependency Injection Configuration
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ILeaveRequestService, LeaveRequestService>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            // Apply Endpoints Naming Convention
            services.AddRouting(options => options.LowercaseUrls = true);

            // AutoMapper Setup
            //services.AddAutoMapper(typeof(Startup).Assembly);

            var loggerFactory = new LoggerFactory();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ResourceToModelProfile>();
                cfg.AddProfile<ModelToResourceProfile>();
            }, loggerFactory);

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddJwtAuthentication(Configuration);
            services.AddScoped<IJwtTokenService, JwtTokenService>();

            services.AddAuthorization();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EMPLOYEE LEAVE REQUESTS", Version = "v1" });
                //c.EnableAnnotations();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EMPLOYEE LEAVE REQUESTS v1"));

            app.UseHttpsRedirection();
            app.UseRouting();

            // CORS Configuration
            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            //Authentication Support
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
