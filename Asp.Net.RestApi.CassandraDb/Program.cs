using Cassandra;
using Domain;
using Infrastructure;
/*
docker run --name cassandra -p 127.0.0.1:9042:9042 -p 127.0.0.1:9160:9160   -d cassandra -e CASSANDRA_USER=user -e CASSANDRA_PASSWORD=password

docker run --name cassandra -p 127.0.0.1:9042:9042 -p 127.0.0.1:9160:9160   -d cassandra 


*/

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
{
    builder.AddInfrastructure();
    builder.AddUseCases();

}


var app = builder.Build();
{
    //using var scope = app.Services.CreateScope();
    //var repo = scope.ServiceProvider.GetRequiredService<ICassandraDbRepository>();
    //Test.Run(repo);
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();





