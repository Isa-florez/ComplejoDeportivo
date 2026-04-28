using Microsoft.AspNetCore.Mvc;
using ComplejoDeportivo.Models.Entities;
using ComplejoDeportivo.Services.Interfaces;

namespace ComplejoDeportivo.Controllers
{
    public class ReservaController : Controller
    {
        private readonly IReservaService _reservaService;
        private readonly IUsuarioService _usuarioService;
        private readonly IEspacioDeportivoService _espacioService;

        public ReservaController(IReservaService reservaService, IUsuarioService usuarioService, IEspacioDeportivoService espacioService)
        {
            _reservaService = reservaService;
            _usuarioService = usuarioService;
            _espacioService = espacioService;
        }

        // Muestra las reservas de un usuario
        public async Task<IActionResult> Index(int? usuarioId)
        {
            var reservas = usuarioId.HasValue
                ? await _reservaService.ObtenerPorUsuario(usuarioId.Value)
                : await _reservaService.ObtenerTodas();

            return View(reservas);
        }

        // Muestra el formulario para crear una reserva
        public async Task<IActionResult> Create()
        {
            ViewBag.Usuarios = await _usuarioService.ObtenerTodos();
            ViewBag.Espacios = await _espacioService.ObtenerTodos();
            return View();
        }

        // Recibe el formulario y crea la reserva
        [HttpPost]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            try
            {
                await _reservaService.Crear(reserva);
                return RedirectToAction("Index", new { usuarioId = reserva.UsuarioId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                ViewBag.Usuarios = await _usuarioService.ObtenerTodos();
                ViewBag.Espacios = await _espacioService.ObtenerTodos();
                return View(reserva);
            }
        }

        // Muestra el detalle de una reserva
        public async Task<IActionResult> Detail(int id)
        {
            var reservas = await _reservaService.ObtenerPorUsuario(id);
            return View(reservas);
        }

        // Cancela una reserva
        [HttpPost]
        public async Task<IActionResult> Cancelar(int id, int usuarioId)
        {
            try
            {
                await _reservaService.Cancelar(id);
                return RedirectToAction("Index", new { usuarioId });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return RedirectToAction("Index", new { usuarioId });
            }
        }
    }
}