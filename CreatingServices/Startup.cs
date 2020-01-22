using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CreatingServices.Services;
namespace CreatingServices
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddTransient<IMessageSender, EmailMessageSender>();
            //services.AddTransient<TimeService>();
            //services.AddTimeService();
            services.AddTransient<MessageService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMessageSender messageSender1, MessageService messageService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseMiddleware<MessageMiddleware>(); // �������� ������������ ����� �������� ������ Invoke ���������� Middleware

            app.Run(async (context) =>
            {
                IMessageSender messageSender2 = context.RequestServices.GetService<IMessageSender>();

                IMessageSender messageSender3 = app.ApplicationServices.GetService<IMessageSender>();


               await context.Response.WriteAsync(messageSender1.Send() + " "); // �������� ����������� ����� �������� ������ Configure ������ Startup
               await context.Response.WriteAsync(messageSender2.Send() + " "); // �������� ����������� ����� ��������� RequestServices 
                await context.Response.WriteAsync(messageSender3.Send() + " "); // �������� ����������� ����� �������� ApplicationServices
                await context.Response.WriteAsync(messageService.Send() + " "); // �������� ����������� ����� ����������� ������ (!=Startup)
            });


            //app.Run(async context =>
            //{
            //    context.Response.ContentType = "text/html;charset=utf-8";
            //    await context.Response.WriteAsync($"������� �����: {timeService?.GetTime()}");
            //});
        }
    }
}

