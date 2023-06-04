using BLLAUTOS;
using BLLAUTOS.Services.Contracts;
using DALAUTOS.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AutosWeb.Controllers
{
    public class AutosController: Controller
    {
        //AutosBLL _BLLAutos = new AutosBLL();
        //MarcasBLL _marcasBLL = new MarcasBLL();
        //ColorBLL _colorBLL = new ColorBLL();
        //ModelosBLL _modelosBLL = new ModelosBLL();

        private readonly IServiceAutos _serviceAutos;
        //private readonly IServiceMarcas _serviceMarcas;
        //private readonly IServiceColor _serviceColor;
        //private readonly IServiceModelo _serviceModelo;

        public AutosController(IServiceAutos? serviceAutos)
        {
            _serviceAutos = serviceAutos;
            //_serviceMarcas = serviceMarcas;
            //_serviceColor = serviceColor;
            //_serviceModelo = serviceModelo;
        }

        public IActionResult VerAutos(string placas,int page)
        {
            var autos = _serviceAutos.GetAutos();
            if (!string.IsNullOrEmpty(placas))
            {
                return View(_serviceAutos.GetAutos().Where(a => a.Placas.Contains(placas)).ToList());
            }
            ViewBag.CurrentPage = page;
            if (page != 0 && _serviceAutos.GetAutosPaginated(page, 12).Count>0)
            {
                return View(_serviceAutos.GetAutosPaginated(page, 12));
            }
            else
            {
                ViewBag.CurrentPage = 1;
                return View(_serviceAutos.GetAutosPaginated(1, 12));
               
            }
            return View(autos);
        }

        public IActionResult CrearAuto()
        {
            //ViewBag.MarcaId = new SelectList(_serviceMarcas.GetMarcas(), "Id", "NMarca");
            //ViewBag.ColorId = new SelectList(_serviceColor.GetColors(), "Id", "NColor");
            return View();
        }

        public IActionResult DetallesDeAuto(int Id)
        {
            return View(_serviceAutos.GetAutoById(Id));
        }

        public IActionResult EditarAuto(int Id)
        {
            //ViewBag.MarcaId = new SelectList(_marcasBLL.GetMarcas(), "Id", "NMarca");
            //ViewBag.ColorId = new SelectList(_colorBLL.GetColors(), "Id", "NColor");
            return View(_serviceAutos.GetAutoById(Id));
        }

        public IActionResult BorrarAuto(int Id)
        {
            return View(_serviceAutos.GetAutoById(Id));
        }

        public IActionResult ConfirmacionBorrar(int Id)
        {
            _serviceAutos.EliminarAuto(Id);
            return RedirectToAction("VerAutos");
        }
        [HttpPost]
        public IActionResult CrearAuto(Auto auto)
        {
            if(auto.ColorId !=null&& auto.MarcaId!=null)
            {
                var color = _serviceAutos.ObtenerColor(auto.ColorId);
                var marca = _serviceAutos.GetMarcaName(auto.MarcaId);
                auto.Placas = $"{marca.ToUpper()[0]}{color.ToUpper()[0]}{_serviceAutos.GetAutos().Count.ToString().PadLeft(3, '0')}";
            }
            _serviceAutos.CrearAuto(auto);
            return RedirectToAction("VerAutos");
        }

        [HttpPost]
        public IActionResult EditarAuto(Auto auto)
        {

            _serviceAutos.EditarAuto(auto);
            return RedirectToAction("VerAutos");
        }

        [HttpGet]
        public IActionResult GetModelosByMarca(int marcaId)
        {
            var modelos = _serviceAutos.GetModelosByMarca(marcaId);

            // Retornar los modelos en formato JSON
            return Json(modelos);
        }

        [HttpGet]
        public IActionResult GetModeloById(int modeloId)
        {
            var modelos = _serviceAutos.GetModeloById(modeloId);

            // Retornar los modelos en formato JSON
                return Json(modelos);
        }

        [HttpGet]
        public IActionResult GetMarcas()
        {
            var marcas = _serviceAutos.GetMarcas();

            return Json(marcas);
        }
        [HttpGet]
        public IActionResult GetColores()
        {
            var colores = _serviceAutos.GetColors();

            return Json(colores);
        }
    }
}
