using Microsoft.AspNetCore.Mvc;
using ComplejoDeportivo.Models.Entities;
using ComplejoDeportivo.Services.Interfaces;

namespace ComplejoDeportivo.Controllers
{
    public class EspacioDeportivoController : Controller
    {
        private readonly IEspacioDeportivoService _espacioService;

        public EspacioDeportivoController(IEspacioDeportivoService espacioService)
        {
            _espacioService = espacioService;
        }

        // Muestra la lista de espacios, con opción de filtrar por tipo
        public async Task<IActionResult> Index(string? tipo)
        {
            var espacios = string.IsNullOrEmpty(tipo)
                ? await _espacioService.ObtenerTodos()
                : await _espacioService.ObtenerPorTipo(tipo);

            return View(espacios);
        }

        // Muestra el formulario para crear un espacio
        public IActionResult Create()
        {
            return View();
        }

        // Recibe el formulario y crea el espacio
        [HttpPost]
        public async Task<IActionResult> Create(EspacioDeportivo espacio)
        {
            try
            {
                await _espacioService.Crear(espacio);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(espacio);
            }
        }

        // Muestra el formulario para editar un espacio
        public async Task<IActionResult> Edit(int id)
        {
            var espacio = await _espacioService.ObtenerPorId(id);
            if (espacio == null) return NotFound();
            return View(espacio);
        }

        // Recibe el formulario y edita el espacio
        [HttpPost]
        public async Task<IActionResult> Edit(EspacioDeportivo espacio)
        {
            try
            {
                await _espacioService.Editar(espacio);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(espacio);
            }
        }
    }
}