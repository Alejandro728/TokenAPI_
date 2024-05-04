using Microsoft.EntityFrameworkCore;
using TokenAPI.Data;
using TokenAPI.Data.TokenModels;
using TokenAPI.Data.DTOs;


namespace TokenAPI.Services;

public class ClienteService 
{

private readonly TokenApiContext _context;
    public ClienteService(TokenApiContext context){
        _context = context;

    }

public async Task<Cliente?> GetCliente(ClienteDto cliente)
{
    return await _context.Clientes.
        SingleOrDefaultAsync(x => x.Nombre == cliente.Nombre && x.Correo == cliente.Correo); 
}

public async Task<IEnumerable<Cliente>> GetAll(){
    return await _context.Clientes.ToListAsync();
}


public async Task<Cliente?> GetById(int id){

return await _context.Clientes.FindAsync(id);
} 

public async Task<Cliente> Create(Cliente newCliente){
    _context.Clientes.Add(newCliente);
    await _context.SaveChangesAsync();

    return newCliente;
}

public async Task Update(int id, Cliente cliente){
    var existingCliente = await GetById(id);

    if(existingCliente is not null){
    existingCliente.Nombre = cliente.Nombre;
    existingCliente.Direccion = cliente.Direccion;
    existingCliente.Telefono = cliente.Telefono;
    existingCliente.Identificador = cliente.Identificador;
    existingCliente.FechaCreacion = cliente.FechaCreacion;
    existingCliente.Correo = cliente.Correo;


    await _context.SaveChangesAsync();

    }
}



public async Task Delete(int id){
    var clienteToDelete = await GetById(id);

    if(clienteToDelete is not null){
        _context.Clientes.Remove(clienteToDelete);
        await _context.SaveChangesAsync();
        
    }
}


}