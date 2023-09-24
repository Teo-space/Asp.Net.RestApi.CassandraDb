using Infrastructure;

namespace Asp.Net.RestApi.CassandraDb;

public static class Test
{
    public static void Run(ICassandraDbRepository repo)
    {
        repo.Connect();


        var keyspaceNames = repo.GetSession()
                    .Execute("SELECT * FROM system_schema.keyspaces")
                    .Select(row => row.GetValue<string>("keyspace_name"))
                    ;
        foreach (var keyspace in keyspaceNames)
        {
            print(keyspace, ConsoleColor.Cyan);
        }


        var articles = repo.Articles();

        var article = Article.Create("Title", "Description", "Text");
        var update = articles.Insert(article);
        //update.ConsistencyLevel
        //update.RetryPolicy
        //update.IfNotExists();
        print(update.QueryString, ConsoleColor.DarkYellow);
        update.Execute();
        print($"({article.ArticleId},    {article.ArticleVersionId}) {article.Title}", ConsoleColor.DarkBlue);


        print("Inserting", ConsoleColor.DarkGreen);
        //7ac78a01-a165-ac5f-2fd3-356eaf233808
        for (var i = 0; i < 100; i++)
        {
            var articleVersion = article.CreateVersion("Title", "Description", "Text");
            articles.Insert(articleVersion).Execute();
        }
        print("Ok", ConsoleColor.Green);


        print("Reading", ConsoleColor.DarkGreen);
        var results = articles.Where(x => x.ArticleId == article.ArticleId).Execute();
        print("Ok", ConsoleColor.Green);

        foreach (var result in results)
        {
            print($"({result.ArticleId},    {result.ArticleVersionId}) {result.Title} {result.CreatedAt}", ConsoleColor.DarkBlue);
        }
    }

}
