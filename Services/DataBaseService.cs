using SQLite;
using ProveedoresApp.Models;

namespace ProveedoresApp.Services;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _db;

    public DatabaseService()
    {
        var dbPath = Path.Combine(FileSystem.AppDataDirectory, "proveedores.db3");
        _db = new SQLiteAsyncConnection(dbPath);

        _db.CreateTableAsync<Proveedor>().Wait();
    }

    public Task<List<Proveedor>> GetAllAsync()
        => _db.Table<Proveedor>().ToListAsync();

    public Task<int> AddAsync(Proveedor proveedor)
        => _db.InsertAsync(proveedor);

    public Task<int> UpdateAsync(Proveedor proveedor)
        => _db.UpdateAsync(proveedor);

    public Task<int> DeleteAsync(Proveedor proveedor)
        => _db.DeleteAsync(proveedor);
}