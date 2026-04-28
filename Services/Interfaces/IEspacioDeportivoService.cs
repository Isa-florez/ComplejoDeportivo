using ComplejoDeportivo.Models.Entities;

namespace ComplejoDeportivo.Services.Interfaces
{
    public interface IEspacioDeportivoService
    {
        Task<List<EspacioDeportivo>> ObtenerTodos();
        Task<List<EspacioDeportivo>> ObtenerPorTipo(string tipo);
        Task<EspacioDeportivo?> ObtenerPorId(int id);
        Task Crear(EspacioDeportivo espacio);
        Task Editar(EspacioDeportivo espacio);
    }
}