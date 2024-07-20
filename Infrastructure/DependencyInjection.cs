using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

/*
docker run --name cassandra -p 127.0.0.1:9042:9042 -p 127.0.0.1:9160:9160   -d cassandra -e CASSANDRA_USER=user -e CASSANDRA_PASSWORD=password

docker run --name cassandra -p 127.0.0.1:9042:9042 -p 127.0.0.1:9160:9160   -d cassandra 


*/
public static class DependencyInjection
{
    public static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging();

        builder.Services.AddScoped<ICassandraDbConnector, CassandraDbConnector>();
        builder.Services.AddScoped<ICassandraDbRepository, CassandraDbRepository>();


        return builder;
    }


}
