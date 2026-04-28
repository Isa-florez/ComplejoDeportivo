using ComplejoDeportivo.Models.Enums;

namespace ComplejoDeportivo.Models.Entities
{
    public class Reserva
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }  
        public EstadoReserva Estado { get; set; } = EstadoReserva.Activa;

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;  

        public int EspacioDeportivoId { get; set; }
        public EspacioDeportivo EspacioDeportivo { get; set; } = null!; 
    }
}