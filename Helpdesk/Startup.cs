using Helpdesk.Core;
using Helpdesk.Core.Common.Mailer;
using Helpdesk.Core.Services;
using Helpdesk.Infrastructure;
using Helpdesk.Infrastructure.Common.Mailer;
using Helpdesk.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk
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

            /*services.AddSingleton<IJWTManagerRepository, JWTManagerRepository>();*/

            services.AddAutoMapper(typeof(Helpdesk.MappingProfile));
            services.Configure<MailConfig>(Configuration.GetSection(MailConfig.EmailConfiguration));
            services.AddDbContext<HelpdeskDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HelpdeskAMN")));
            services.AddTransient<IHelpdeskUnitOfWork, HelpdeskUnitOfWork>();
            services.AddTransient<IVwLastWeekTicketService, VwLastWeekTicketServ>();
            services.AddTransient<IVwQuotaServ, VwQuotaServ>();
            services.AddTransient<IVwTicketPICServ, VwTicketPICServ>();
            services.AddTransient<IVwTicketSummaryServ, VwTicketSummaryServ>();
            services.AddTransient<IVwActiveTicketSummaryServ, VwActiveTicketSummaryServ>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<ITimerService, TimerService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IStatusService, StatusService>();
            services.AddTransient<IExcelReportService, ExcelReportService>();
            services.AddTransient<IConversationService, ConversationService>();
            services.AddTransient<IEmailStackService, EmailStackService>();
            services.AddTransient<IMailerService, MailerService>();
            services.AddTransient<IEmailServices, EmailService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Helpdesk", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Helpdesk v1"));
            }
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
