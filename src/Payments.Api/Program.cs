using Payments.Api.DependencyInjection;
using Payments.Domain.Command.Payment;
using Payments.Domain.Queries.Payment;
using Rebus.Config;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddDependencies();
builder.Services.AddRebus(builder.Configuration);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(PaymentCommandHandler).GetTypeInfo().Assembly, typeof(GetPaymentByTransactionIdQueryHandler).GetTypeInfo().Assembly));
builder.Services.AddLogger(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
