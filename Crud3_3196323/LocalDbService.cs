using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace Crud3_3196323
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_sueldo.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDbService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));

            //Le indica al sistema que cree la tabla de nuestro contexto
            _connection.CreateTableAsync<Trabajador>();
        }

        //Metodo para listar los registros de nuestra tabla
        public async Task<List<Trabajador>> GetTrabajadores()
        {
            return await _connection.Table<Trabajador>().ToListAsync();
        }
        //metodo para listar los registro por id
        public async Task<Trabajador> GetById(int id)
        {
            return await _connection.Table<Trabajador>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //Metodo para crear registro
        public async Task Create(Trabajador trabajador)
        {
            await _connection.InsertAsync(trabajador);
        }

        //Metodo para actualizar
        public async Task Update(Trabajador trabajador)
        {
            await _connection.UpdateAsync(trabajador);
        }

        //Metodo para eliminar
        public async Task Delete(Trabajador trabajador)
        {
            await _connection.DeleteAsync(trabajador);
        }
    }
}
