namespace Infrastructure;

using Cassandra;
using Cassandra.Data.Linq;

public interface ICassandraDbRepository
{
    public void Connect();
    public ISession GetSession();

    public Table<T> Table<T>() where T : class;
    public Table<Article> Articles();

}
