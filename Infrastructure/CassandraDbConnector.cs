namespace Infrastructure;

using Cassandra;
using Microsoft.Extensions.Logging;


public class CassandraDbConnector(ILogger<CassandraDbConnector> logger) : ICassandraDbConnector
{
    public ILogger<CassandraDbConnector> Logger {  get; private set; }
    public Cluster Cluster { get; private set; }
    public ISession Session { get; private set; }
    public ISession GetSession() => Session;

    public void Connect()
    {
        logger.LogInformation("Cluster Build");
        Cluster = Cluster.Builder()
            .AddContactPoints("127.0.0.1")
            .WithPort(9042)
            //.WithLoadBalancingPolicy(new DCAwareRoundRobinPolicy("<Data Centre (e.g AWS_VPC_US_EAST_1)>"))
            .WithAuthProvider(new PlainTextAuthProvider("user", "password"))
            .Build();

        logger.LogInformation($"Connecting", ConsoleColor.Cyan);
        Session = Cluster.Connect();
        logger.LogInformation($"Connected to cluster: {Cluster.Metadata.ClusterName}", ConsoleColor.Green);

        Dictionary<string, string> replication = new Dictionary<string, string>();
        replication.Add("class", "SimpleStrategy");
        replication.Add("replication_factor", "1");

        Session.CreateKeyspaceIfNotExists("apitests", replication);
        logger.LogInformation($"CreateKeyspaceIfNotExists", ConsoleColor.Cyan);


    }

}


