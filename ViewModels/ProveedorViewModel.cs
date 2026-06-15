using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ProveedoresApp.Models;
using ProveedoresApp.Services;
using System.Collections.ObjectModel;

namespace ProveedoresApp.ViewModels;

public partial class ProveedorViewModel : ObservableObject
{
    private readonly DatabaseService _dbService;

    public ObservableCollection<Proveedor> Proveedores { get; set; } = new();

    [ObservableProperty]
    private string nombre;

    [ObservableProperty]
    private string empresa;

    [ObservableProperty]
    private string telefono;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string direccion;

    public ProveedorViewModel()
    {
        _dbService = new DatabaseService();
        LoadData();
    }

    public async void LoadData()
    {
        var data = await _dbService.GetAllAsync();
        Proveedores.Clear();
        foreach (var item in data)
            Proveedores.Add(item);
    }

    [RelayCommand]
    public async Task AddProveedor()
    {
        var proveedor = new Proveedor
        {
            Nombre = Nombre,
            Empresa = Empresa,
            Telefono = Telefono,
            Email = Email,
            Direccion = Direccion
        };

        await _dbService.AddAsync(proveedor);
        LoadData();
        ClearFields();
    }

    [RelayCommand]
    public async Task DeleteProveedor(Proveedor proveedor)
    {
        await _dbService.DeleteAsync(proveedor);
        LoadData();
    }

    private void ClearFields()
    {
        Nombre = Empresa = Telefono = Email = Direccion = string.Empty;
    }
}