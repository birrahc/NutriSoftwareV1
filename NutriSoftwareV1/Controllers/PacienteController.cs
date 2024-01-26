using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSoftwareV1.Data;
using NutriSoftwareV1.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Text.Json;

namespace NutriSoftwareV1.Controllers
{
    public class PacienteController : Controller
    {
        ErrorMessage errorMessage;

        [HttpGet]
        public IActionResult Index(long? Id, bool requestJs = false)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                var pacientes = db.pacientes.Include(p => p.Anotacoes).OrderBy(p => p.Nome).ToList();
                if (Id.HasValue)
                {
                    ViewBag.DetalhePaciente = Id.Value;
                    ViewBag.DetalhePaciente = pacientes.FirstOrDefault(p => p.Id == Id.Value);
                    if (requestJs)
                        return PartialView("Partiais/_DetalhesPaciente", pacientes.FirstOrDefault(p => p.Id == Id.Value));
                }
                return View(pacientes);
            }
        }

        public IActionResult CadastrarEditarPaciente(long? Id)
        {
            using (NutriDbContext db =new NutriDbContext())
            {
                if (Id.HasValue)
                {
                    return PartialView("Partiais/_FormularioCadastrarEditarPaciente", db.pacientes.Find(Id));
                }
                return PartialView("Partiais/_FormularioCadastrarEditarPaciente", new Paciente());
            }
        }

        [HttpPost]
        public IActionResult Create(Paciente pPaciente)
        {
            if (!ModelState.IsValid)
            {
                errorMessage = new ErrorMessage();
                errorMessage.TituloErro = " validação(es)";
                ModelState.Values.Where(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).ToList().ForEach(p =>
                {
                    errorMessage.MensagemDeErro += p.Errors[0].ErrorMessage;
                });

                string jsonStr = JsonSerializer.Serialize<ErrorMessage>(errorMessage);
                return StatusCode(500, jsonStr);
            }
            using (NutriDbContext db = new NutriDbContext())
            {
                pPaciente.Cpf = string.IsNullOrEmpty(pPaciente.Cpf) ? null : pPaciente.Cpf.Replace(".", "").Replace("-", "");
                db.pacientes.Add(pPaciente);
                db.SaveChanges();
                var pacienteCadastrado = db.pacientes.FirstOrDefault(p => p.Id == pPaciente.Id);

                return PartialView("Partiais/_DetalhesPaciente", pacienteCadastrado);
            }
        }

        [HttpPost]
        public IActionResult Edit(Paciente pPaciente)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                if (!string.IsNullOrEmpty(pPaciente.Cpf))
                    pPaciente.Cpf = pPaciente.Cpf.Replace(".", "").Replace("-", "");
                db.pacientes.Update(pPaciente);
                db.SaveChanges();
                var pacienteCadastrado = db.pacientes.FirstOrDefault(p => p.Id == pPaciente.Id);

                return PartialView("Partiais/_DetalhesPaciente", pacienteCadastrado);
            }
        }


        [HttpPost]
        public IActionResult Delete(long Id)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                var paciente = db.pacientes.Find(Id);
                var anotacoes = db.AnotacosPaciente.Where(p => p.PacienteId == Id).ToList();
                var avaliacoes = db.AvaliacoesFisicas.Where(p => p.PacienteId == Id).ToList();
                var avaliacoesBio = db.AvalicaoBioImpedancia.Where(p => p.PacienteId == Id).ToList();
                if (anotacoes.Count > 0)
                    db.AnotacosPaciente.RemoveRange(anotacoes);

                if (avaliacoes.Count > 0)
                    db.AvaliacoesFisicas.RemoveRange(avaliacoes);

                if (avaliacoesBio.Count > 0)
                    db.AvalicaoBioImpedancia.RemoveRange(avaliacoesBio);

                db.pacientes.Remove(paciente);
                db.SaveChanges();
                var pacientes = db.pacientes.OrderBy(p => p.Nome).ToList();

                return PartialView("Partiais/_GeralPaciente", pacientes);
            }
        }


        #region Anotacões
        public IActionResult ObservacoesPaciente(long Id)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                var anotacoes = db.pacientes.Include(p => p.Anotacoes).FirstOrDefault(p => p.Id == Id);
                var pacienteAnotacoes = anotacoes == null ? new Paciente { Id = Id } : anotacoes;
                return PartialView("Partiais/_AnotacoesPaciente", pacienteAnotacoes);
            }
        }

        [HttpGet]
        public IActionResult CadastrarAnotacaoPaciente(int Id)
        {
            ViewBag.TitleModalAnotacoes = "Cadastro de anotações";
            ViewBag.BotaoTituloCadastroAnotacoes = "Salvar";
            ViewBag.DetalhePaciente = Id;
            return PartialView("Partiais/_FormularioCadastrarEditarAnotacaoPaciente", new Observacao { PacienteId = Id, DataObservacao=DateTime.Now});

        }
        [HttpPost]
        public IActionResult CadastrarAnotacaoPaciente(Observacao pAnotacao)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                db.AnotacosPaciente.Add(pAnotacao);
                db.SaveChanges();
                var anotacoesPaciente = db.pacientes.Include(p => p.Anotacoes).FirstOrDefault(p=>p.Id == pAnotacao.PacienteId);

                return PartialView("Partiais/_AnotacoesPaciente", anotacoesPaciente);
            }
        }
        [HttpGet]
        public IActionResult EditarAnotacaoPaciente(int Id)
        {
            ViewBag.TitleModalAnotacoes = "Alteração de anotação";
            ViewBag.BotaoTituloCadastroAnotacoes = "Salvar alterações";
            using (NutriDbContext db = new NutriDbContext())
            {
                var anotacao = db.AnotacosPaciente.Include(p => p.Paciente).FirstOrDefault(p => p.Id == Id);

                return PartialView("Partiais/_FormularioCadastrarEditarAnotacaoPaciente", anotacao);
            }
        }

        [HttpPost]
        public IActionResult EditarAnotacaoPaciente(Observacao pAnotacao)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                db.AnotacosPaciente.Update(pAnotacao);
                db.SaveChanges();
                var anotacoesPaciente = db.pacientes.Include(a=>a.Anotacoes).FirstOrDefault(p=>p.Id==pAnotacao.PacienteId);
                return PartialView("Partiais/_AnotacoesPaciente", anotacoesPaciente);
            }
        }

        [HttpPost]
        public IActionResult RemoverAnotacaoPaciente(int Id)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                var anotacao = db.AnotacosPaciente.Find(Id);
                var idPaciente = anotacao.PacienteId;
                db.AnotacosPaciente.Remove(anotacao);
                db.SaveChanges();
                var anotacoesPaciente = db.pacientes.Include(a=>a.Anotacoes).FirstOrDefault(p=>p.Id== idPaciente);
                return PartialView("Partiais/_AnotacoesPaciente", anotacoesPaciente);
            }
        }

        #endregion

    }
}
