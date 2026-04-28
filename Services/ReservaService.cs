using Microsoft.EntityFrameworkCore;
using ComplejoDeportivo.Data;
using ComplejoDeportivo.Models.Entities;
using ComplejoDeportivo.Models.Enums;
using ComplejoDeportivo.Services.Interfaces;

namespace ComplejoDeportivo.Services
{
    public class ReservaService : IReservaService
    {
        private readonly AppDbContext _context;

        public ReservaService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todas las reservas de un usuario CON sus relaciones cargadas
        public async Task<List<Reserva>> ObtenerPorUsuario(int usuarioId)
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.EspacioDeportivo)
                .Where(r => r.UsuarioId == usuarioId)
                .ToListAsync();
        }

        // Retorna todas las reservas de un espacio CON sus relaciones cargadas
        public async Task<List<Reserva>> ObtenerPorEspacio(int espacioId)
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.EspacioDeportivo)
                .Where(r => r.EspacioDeportivoId == espacioId)
                .ToListAsync();
        }

        // Crea una reserva nueva con todas las validaciones
        public async Task Crear(Reserva reserva)
        {
            if (reserva.HoraFin <= reserva.HoraInicio)
                throw new Exception("La hora de fin debe ser mayor a la hora de inicio.");

            if (reserva.HoraInicio < DateTime.Now)
                throw new Exception("No se pueden crear reservas en fechas u horas pasadas.");

            bool espacioOcupado = await _context.Reservas
                .AnyAsync(r => r.EspacioDeportivoId == reserva.EspacioDeportivoId
                    && r.Estado == EstadoReserva.Activa
                    && r.HoraInicio < reserva.HoraFin
                    && r.HoraFin > reserva.HoraInicio);

            if (espacioOcupado)
                throw new Exception("El espacio deportivo ya tiene una reserva en ese horario.");

            bool usuarioOcupado = await _context.Reservas
                .AnyAsync(r => r.UsuarioId == reserva.UsuarioId
                    && r.Estado == EstadoReserva.Activa
                    && r.HoraInicio < reserva.HoraFin
                    && r.HoraFin > reserva.HoraInicio);

            if (usuarioOcupado)
                throw new Exception("El usuario ya tiene una reserva en ese horario.");

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();
        }

        // Cancela una reserva cambiando su estado
        public async Task Cancelar(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
                throw new Exception("Reserva no encontrada.");

            reserva.Estado = EstadoReserva.Cancelada;
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Reserva>> ObtenerTodas()
        {
            return await _context.Reservas
                .Include(r => r.Usuario)
                .Include(r => r.EspacioDeportivo)
                .ToListAsync();
        }
    }
}