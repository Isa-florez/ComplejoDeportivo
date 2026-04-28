using ComplejoDeportivo.Models.Entities;

namespace ComplejoDeportivo.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ObtenerTodos();
        Task<Usuario?> ObtenerPorId(int id);
        Task Crear(Usuario usuario);
        Task Editar(Usuario usuario);
    }
}