using EasyLearn.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyLearn.Services.Managers
{
    /// <summary>Generic manager</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public abstract class GenericManager<T>
    {
        protected SQLiteAsyncConnection connection;

        public GenericManager(SQLiteAsyncConnection connection)
        {
            this.connection = connection;
        }

        public async Task<T> create(T item)
        {
            try
            {
                await connection.InsertAsync(item);
                return item;
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }

        public async Task<bool> delete(T item)
        {
            try
            {
                int id = await connection.DeleteAsync(item);
                return id > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<bool> deleteAll()
        {
            try
            {
                int i = await connection.ExecuteAsync("DELETE FROM " + typeof(T).Name);
                return i > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<bool> saveAll(ObservableCollection<T> objects)
        {
            try
            {
                int i = await connection.InsertAllAsync(objects);
                return i > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public async Task<bool> updateAll(ObservableCollection<T> objects)
        {
            try
            {
                int i = await connection.UpdateAllAsync(objects);
                return i > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<bool> update(T item)
        {
            try
            {
                int i = await connection.UpdateAsync(item);
                return i > 0;
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public async Task<int> count()
        {
            try
            {
                var c = await connection.ExecuteScalarAsync<int>("SELECT COUNT(*) FROM " + typeof(T).Name);
                return c;
            }
            catch (Exception ex)
            {

            }
            return -1;
        }

    }
}
