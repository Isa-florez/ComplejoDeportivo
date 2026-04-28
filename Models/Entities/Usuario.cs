namespace ComplejoDeportivo.Models.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Documento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; } = DateTime.Now; // ← le faltaba el valor por defecto, así se registra automáticamente la fecha actual

        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}