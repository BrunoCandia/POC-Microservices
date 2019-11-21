using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using usersModel = Users.API.Model;

namespace Users.API.Infrastructure
{
    public class UsersContext : IUsersContext, IDisposable
    {
        private const string usersCollectionName = "Users";
        private readonly IMongoDatabase _database = null;
        private IMongoCollection<usersModel.UsersModel> users;

        //public UsersContext(IOptions<UserSettings> settings)
        //{
        //    var client = new MongoClient(settings.Value.ConnectionString);

        //    if (client != null)
        //        _database = client.GetDatabase(settings.Value.Database);            
        //}

        public UsersContext(/*IMongoDatabase _database*/ IMongoClient client, string databaseName)
        {            
            this._database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<usersModel.UsersModel> Users
        {
            get
            {
                if (users is null)
                { 
                    users = _database.GetCollection<usersModel.UsersModel>(usersCollectionName); 
                }

                return users;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UsersContext()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion        
    }
}
