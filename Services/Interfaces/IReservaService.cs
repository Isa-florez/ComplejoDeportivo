using ComplejoDeportivo.Models.Entities;

namespace ComplejoDeportivo.Services.Interfaces
{
    public interface IReservaService
    {
        Task<List<Reserva>> ObtenerPorUsuario(int usuarioId);
        Task<List<Reserva>> ObtenerPorEspacio(int espacioId);
        Task Crear(Reserva reserva);
        Task Cancelar(int id);
        Task<List<Reserva>> ObtenerTodas();
    }
}