using Microsoft.OpenApi.Models;

namespace RentalOfPremises.Api.Infrastructure
{
    static internal class DocumentExtensions
    {
        public static void GetSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("User", new OpenApiInfo { Title = "Сущность пользователя", Version = "v1" });
                c.SwaggerDoc("Tenant", new OpenApiInfo { Title = "Сущность арендатора", Version = "v1" });
                c.SwaggerDoc("Room", new OpenApiInfo { Title = "Сущность помещения", Version = "v1" });
                c.SwaggerDoc("Price", new OpenApiInfo { Title = "Сущность прейскуранта", Version = "v1" });
                c.SwaggerDoc("PaymentInvoice", new OpenApiInfo { Title = "Сущность счета на оплату", Version = "v1" });
                c.SwaggerDoc("Contract", new OpenApiInfo { Title = "Сущность договора аренды", Version = "v1" });
                var filePath = Path.Combine(AppContext.BaseDirectory, "RentalOfPremises.Api.xml");
                c.IncludeXmlComments(filePath);
            });
        }
        public static void GetSwaggerUI(this WebApplication web)
        {
            web.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("User/swagger.json", "Пользователи");
                x.SwaggerEndpoint("Tenant/swagger.json", "Арендаторы");
                x.SwaggerEndpoint("Room/swagger.json", "Помещения");
                x.SwaggerEndpoint("Price/swagger.json", "Прейскуранты");
                x.SwaggerEndpoint("PaymentInvoice/swagger.json", "Счета на оплату");
                x.SwaggerEndpoint("Contract/swagger.json", "Договоры аренды");
            });
        }
    }
}
