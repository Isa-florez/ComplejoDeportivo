using Microsoft.EntityFrameworkCore;
using ComplejoDeportivo.Data;
using ComplejoDeportivo.Models.Entities;
using ComplejoDeportivo.Services.Interfaces;

namespace ComplejoDeportivo.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos los usuarios
        public async Task<List<Usuario>> ObtenerTodos()
        {
            return await _context.Usuarios.ToListAsync();
        }

        // Retorna un usuario por su id, o null si no existe
        public async Task<Usuario?> ObtenerPorId(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        // Crea un usuario nuevo
        public async Task Crear(Usuario usuario)
        {
            // Validar que no exista otro usuario con el mismo documento o email
            bool existe = await _context.Usuarios
                .AnyAsync(u => u.Documento == usuario.Documento || u.Email == usuario.Email);

            if (existe)
                throw new Exception("Ya existe un usuario con ese documento o email.");

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        // Edita un usuario existente
        public async Task Editar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}