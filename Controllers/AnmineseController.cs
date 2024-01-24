using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NutriSoftwareV1.Data;
using NutriSoftwareV1.Models;

namespace NutriSoftwareV1.Controllers
{
    public class AnmineseController : Controller
    {
        public IActionResult Index(long Id)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                var anminese = db.Anminese
                    .Include(d => d.DiagnosticoMedico)
                    .Include(s => s.HistoricoFamiliar)
                    .Include(a => a.AtividadesFiscia)
                    .Include(p => p.Paciente)
                    .FirstOrDefault(p => p.PacienteId == Id);
                ViewBag.Id = Id;

                return View(anminese);
            }

        }

        [HttpGet]
        public IActionResult CadastrarEditarAnminese(long Id, int? AnmineseId = null)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                ViewBag.Titulo = "Cadastro de Anminese";
                if (AnmineseId.HasValue)
                {
                    var anminese = db.Anminese.Find(AnmineseId);
                    ViewBag.DiagnosticoMedico = ListaDoencas(db, anminese.DiagnosticoMedicoId);
                    ViewBag.HistoricoFamiliar = ListaDoencas(db, anminese.HistoricoFamiliarId);
                    ViewBag.AtividadesFisicas = ListaAtividadesFisicas(db, anminese.AtividadesFisciaId);
                    ViewBag.Titulo = "Alterar dados de  Anminese";
                    ViewBag.PacienteId = Id;
                    ViewBag.Id = Id;
                    return PartialView("Partiais/_FormularioAnminese", anminese);

                }
                ViewBag.PacienteId = Id;
                ViewBag.Id = Id;
                ViewBag.DiagnosticoMedico = ListaDoencas(db);
                ViewBag.HistoricoFamiliar = ListaDoencas(db);
                ViewBag.AtividadesFisicas = ListaAtividadesFisicas(db);
                return PartialView("Partiais/_FormularioAnminese");
            }
        }


        [HttpPost]
        public IActionResult CadastrarEditarAnminese(Anminese pAnminese)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                if (pAnminese.Id > 0)
                    db.Anminese.Update(pAnminese);
                else
                    db.Anminese.Add(pAnminese);

                db.SaveChanges();
                var anminese = db.Anminese.Include(d => d.DiagnosticoMedico)
                    .Include(s => s.HistoricoFamiliar)
                    .Include(a => a.AtividadesFiscia)
                    .Include(p => p.Paciente)
                    .FirstOrDefault(p => p.PacienteId == pAnminese.PacienteId);
                ViewBag.Id = pAnminese.PacienteId;
                return PartialView("Partiais/_GeralAnminese", anminese);
            }
        }
        public static SelectList ListaDoencas(NutriDbContext db, int? IdDoenca = null)
        {
            var doecas = db.Doenca.ToList();
            return new SelectList(doecas, "Id", "Descricao", IdDoenca);
        }

        public static SelectList ListaAtividadesFisicas(NutriDbContext db, int? IdAtividade = null)
        {
            var doecas = db.AtividadeFisica.ToList();
            return new SelectList(doecas, "Id", "Descricao", IdAtividade);
        }
    }
}
