using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NewcomersTask.DB;

namespace NewcomersTask.Test
{
    public class ConnectionFactory : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public SagaContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<SagaContext>().UseInMemoryDatabase(databaseName: "Test_Database").Options;

            var context = new SagaContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public SagaContext CreateContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<SagaContext>().UseSqlite(connection).Options;

            var context = new SagaContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}