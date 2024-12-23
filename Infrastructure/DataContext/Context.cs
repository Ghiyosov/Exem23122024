using System.Data;
using System.Data.Common;
using Npgsql;




namespace Infrastructure.DataContext;

public class Context:IContext
{
    readonly string connectionString =
        "Server=localhost; Port = 5432; Database = productdb; User Id = postgres; Password = 832111;";

    public IDbConnection Connection()
    {
        return new NpgsqlConnection(connectionString);
    }
}

public interface IContext
{
    IDbConnection Connection();
}