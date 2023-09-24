using Cassandra;

namespace Infrastructure;

public interface ICassandraDbConnector
{
    public void Connect();
    public ISession GetSession();
}