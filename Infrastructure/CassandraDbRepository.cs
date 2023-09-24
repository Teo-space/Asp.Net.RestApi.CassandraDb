using Microsoft.Extensions.Logging;

namespace Infrastructure;

internal class CassandraDbRepository(
    ILogger<CassandraDbRepository> logger, 
    ICassandraDbConnector cassandraDbConnector
    )
    : ICassandraDbRepository
{
    static CassandraDbRepository()
    {
        var mapper =
            MappingConfiguration.Global
            .Define(
               new Map<Article>()
                  .KeyspaceName("apitests")
                  .TableName("Articles")
                  .PartitionKey(x => x.ArticleId)
                  .ClusteringKey(x => x.ArticleVersionId)
                  .Column(x => x.ArticleId)
                  .Column(x => x.ArticleVersionId)
                  .Column(x => x.Title)
                  .Column(x => x.Description)
                  .Column(x => x.Text)
                  .Column(x => x.CreatedAt)
                  .Column(x => x.UpdatedAt)
            );
    }

    public void Connect() => cassandraDbConnector.Connect();
    public ISession GetSession() => cassandraDbConnector.GetSession();

    public Table<T> Table<T>() where T : class
    {
        var table = new Table<T>(cassandraDbConnector.GetSession());
        table.CreateIfNotExists();
        return table;
    }

    public Table<Article> Articles() => Table<Article>();





}

