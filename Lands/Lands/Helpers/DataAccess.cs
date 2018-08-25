namespace Lands.Helpers
{
    using Interfaces;
    using Models;
    using SQLite;
    //using SQLite.Net;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Xamarin.Forms;

    public class DataAccess : IDisposable
    {
        private SQLiteConnection connection;

        public DataAccess()
        {
            var config = DependencyService.Get<IConfig>();

            //this.connection = new SQLiteConnection(
            //    config.Platform,
            //    Path.Combine(config.DirectoryDB, "Lands.db3"));

            this.connection = new SQLiteConnection(
                Path.Combine(config.DirectoryDB, "Lands.db3"));

            connection.CreateTable<UserLocal>();
        }

        public void Insert<T>(T model)
        {
            this.connection.Insert(model);
        }

        public void Update<T>(T model)
        {
            this.connection.Update(model);
        }

        public void Delete<T>(T model)
        {
            this.connection.Delete(model);
        }


        //public T First<T>(bool WithChildren) where T : class
        public T First<T>(bool WithChildren) where T : new()
        {
            //if (WithChildren)
            //{
            //    return connection.GetAllWithChildren<T>().FirstOrDefault();
            //}
            //else
            //{
                return connection.Table<T>().FirstOrDefault();
            //}
        }

        //public List<T> GetList<T>(bool WithChildren) where T : class
        public List<T> GetList<T>(bool WithChildren) where T : new()
        {
            //if (WithChildren)
            //{
            //    return connection.GetAllWithChildren<T>().ToList();
            //}
            //else
            //{
                return connection.Table<T>().ToList();
            //}
        }

        //public T Find<T>(int pk, bool WithChildren) where T : class
        public T Find<T>(int pk, bool WithChildren) where T : new()
        {
                //if (WithChildren)
                //{
                //    return connection.GetAllWithChildren<T>().FirstOrDefault(m => m.GetHashCode() == pk);
                //}
                //else
                //{
                    return connection.Table<T>().FirstOrDefault(m => m.GetHashCode() == pk);
                //}
            }

        public void Dispose()
        {
            connection.Dispose();
        }
    }
}
