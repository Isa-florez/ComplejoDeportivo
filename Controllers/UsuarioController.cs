using Microsoft.AspNetCore.Mvc;
using ComplejoDeportivo.Models.Entities;
using ComplejoDeportivo.Services.Interfaces;

namespace ComplejoDeportivo.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Muestra la lista de usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.ObtenerTodos();
            return View(usuarios);
        }

        // Muestra el formulario para crear un usuario
        public IActionResult Create()
        {
            return View();
        }

        // Recibe el formulario y crea el usuario
        [HttpPost]
        public async Task<IActionResult> Create(Usuario usuario)
        {
            try
            {
                await _usuarioService.Crear(usuario);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(usuario);
            }
        }

        // Muestra el formulario para editar un usuario
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioService.ObtenerPorId(id);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        // Recibe el formulario y edita el usuario
        [HttpPost]
        public async Task<IActionResult> Edit(Usuario usuario)
        {
            try
            {
                await _usuarioService.Editar(usuario);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(usuario);
            }
        }
    }
}