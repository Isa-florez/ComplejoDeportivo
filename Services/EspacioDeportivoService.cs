using Microsoft.EntityFrameworkCore;
using ComplejoDeportivo.Data;
using ComplejoDeportivo.Models.Entities;
using ComplejoDeportivo.Services.Interfaces;

namespace ComplejoDeportivo.Services
{
    public class EspacioDeportivoService : IEspacioDeportivoService
    {
        private readonly AppDbContext _context;

        public EspacioDeportivoService(AppDbContext context)
        {
            _context = context;
        }

        // Retorna todos los espacios deportivos
        public async Task<List<EspacioDeportivo>> ObtenerTodos()
        {
            return await _context.EspaciosDeportivos.ToListAsync();
        }

        // Filtra espacios por tipo (fútbol, piscina, etc.)
        public async Task<List<EspacioDeportivo>> ObtenerPorTipo(string tipo)
        {
            return await _context.EspaciosDeportivos
                .Where(e => e.Tipo == tipo)
                .ToListAsync();
        }

        // Retorna un espacio por su id, o null si no existe
        public async Task<EspacioDeportivo?> ObtenerPorId(int id)
        {
            return await _context.EspaciosDeportivos.FindAsync(id);
        }

        // Crea un espacio deportivo nuevo
        public async Task Crear(EspacioDeportivo espacio)
        {
            bool existe = await _context.EspaciosDeportivos
                .AnyAsync(e => e.Nombre == espacio.Nombre);

            if (existe)
                throw new Exception("Ya existe un espacio deportivo con ese nombre.");

            _context.EspaciosDeportivos.Add(espacio);
            await _context.SaveChangesAsync();
        }

        // Edita un espacio deportivo existente
        public async Task Editar(EspacioDeportivo espacio)
        {
            _context.EspaciosDeportivos.Update(espacio);
            await _context.SaveChangesAsync();
        }
    }
}