using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutriSoftwareV1.Data;
using NutriSoftwareV1.Models;

namespace NutriSoftwareV1.Controllers
{
    public class ConsumosController : Controller
    {
        public IActionResult Index(long Id)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                var consumos = db.ConsumoAlimentar
                    .Include(p=>p.Paciente)
                    .Include(l => l.LocalAlmoco)
                    .FirstOrDefault(p => p.PacienteId == Id);
                ViewBag.Id = Id;
                return View(consumos);
            }
        }

        [HttpGet]
        public IActionResult CadastrarEditarConsumos(long Id, int? ConsumosId = null)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                ViewBag.Titulo = "Cadastro de Consumo";
                if (ConsumosId.HasValue)
                {
                    var consumo = db.ConsumoAlimentar.Find(ConsumosId);
                    ViewBag.LocaisAlmoco = ListaLocaisAlmoco(db, consumo.LocalAlmocoId);
                 
                    ViewBag.Titulo = "Alterar dados de consumo";
                    ViewBag.PacienteId = Id;
                    ViewBag.Id = Id;
                    return PartialView("Partiais/_FormularioConsumos", consumo);

                }
                ViewBag.PacienteId = Id;
                ViewBag.Id = Id;
                ViewBag.LocaisAlmoco = ListaLocaisAlmoco(db);
                return PartialView("Partiais/_FormularioConsumos");
            }
        }


        [HttpPost]
        public IActionResult CadastrarEditarConsumos(ConsumoAlimentar pConsumo)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                if (pConsumo.Id > 0)
                    db.ConsumoAlimentar.Update(pConsumo);
                else
                    db.ConsumoAlimentar.Add(pConsumo);

                db.SaveChanges();
                var consumo = db.ConsumoAlimentar
                    .Include(p => p.Paciente)
                    .Include(l => l.LocalAlmoco)
                    .FirstOrDefault(p => p.PacienteId == pConsumo.PacienteId);
                ViewBag.Id = pConsumo.PacienteId;
                return PartialView("Partiais/_GeralConsumos", consumo);
            }
        }

        public static SelectList ListaLocaisAlmoco(NutriDbContext db, int ?id=null)
        {
            var locais = db.LocalAlmoco.ToList();
            return new SelectList(locais, "Id", "Descricao", id);
        }


    }
}
