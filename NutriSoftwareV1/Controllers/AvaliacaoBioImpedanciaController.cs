using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSoftwareV1.Data;
using NutriSoftwareV1.Dto;
using NutriSoftwareV1.Models;
using System.Text.Json;

namespace NutriSoftwareV1.Controllers
{
    public class AvaliacaoBioImpedanciaController : Controller
    {
        ErrorMessage errorMessage;
        public IActionResult Index(long Id)
        {
            using (NutriDbContext db = new NutriDbContext()) 
            {
                var bio = db.pacientes.Include(b => b.AvaliacoesBioImpedancia).FirstOrDefault(p => p.Id == Id);
                return View(bio);
            }
              
        }


        [HttpGet]
        public IActionResult CadastrarEditarAvaliacaoBioImpedancia(long Id, long? AvaliacoaId)
        {
            DtoCadastroAvaliacaoBioImpedancia dtoCadastroAvaliacaoBio = new DtoCadastroAvaliacaoBioImpedancia();
            dtoCadastroAvaliacaoBio.PacienteId = Id;
            dtoCadastroAvaliacaoBio.TituloCadastarEditar = "Cadastrar Avaliação";
            dtoCadastroAvaliacaoBio.Data = DateTime.Now;

            using (NutriDbContext db = new NutriDbContext())
            {
                var avaliacoes = db.AvalicaoBioImpedancia.Include(p => p.Paciente).Where(p => p.PacienteId == Id).OrderBy(p => p.Data).ToList();
                dtoCadastroAvaliacaoBio.UltimaAvalicao = avaliacoes.LastOrDefault();
                if (AvaliacoaId.HasValue)
                {
                    dtoCadastroAvaliacaoBio.TituloCadastarEditar = "Editar Avaliação";
                    dtoCadastroAvaliacaoBio.CriarAtulizar = db.AvalicaoBioImpedancia.Find(AvaliacoaId.Value);
                    dtoCadastroAvaliacaoBio.Data = dtoCadastroAvaliacaoBio.CriarAtulizar.Data;
                }
            }
            return PartialView("Partiais/_FormularioAvaliacaoBioImpedancia", dtoCadastroAvaliacaoBio);


        }


        [HttpPost]
        public IActionResult CadastrarEditarAvaliacaoBioImpedancia(AvalicaoBioImpedancia pAvaliacao)
        {
            if (!ModelState.IsValid)
            {
                errorMessage = new ErrorMessage();
                errorMessage.TituloErro = "validação(es)";
                ModelState.Values.Where(e => e.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).ToList().ForEach(p =>
                {
                    errorMessage.MensagemDeErro += p.Errors[0].ErrorMessage;
                });

                string jsonStr = JsonSerializer.Serialize<ErrorMessage>(errorMessage);
                return StatusCode(500, jsonStr);
            }

            using (NutriDbContext db = new NutriDbContext())
            {
                if (pAvaliacao?.Id > 0)
                    db.AvalicaoBioImpedancia.Update(pAvaliacao);
                else
                    db.AvalicaoBioImpedancia.Add(pAvaliacao);
                db.SaveChanges();
                return PartialView("Partiais/_ListaAvaliacoesBioImpedancia", db.AvalicaoBioImpedancia.Include(p => p.Paciente).Where(p => p.PacienteId == pAvaliacao.PacienteId).ToList());
            }
        }

        [HttpPost]
        public IActionResult Delete(long Id)
        {
            try
            {

                using (NutriDbContext db = new NutriDbContext())
                {
                    var avaliacao = db.AvalicaoBioImpedancia.Find(Id);
                    var pacienteId = avaliacao.PacienteId;
                    db.Remove(avaliacao);
                    db.SaveChanges();

                    return PartialView("Partiais/_ListaAvaliacoesBioImpedancia", db.AvalicaoBioImpedancia.Include(p => p.Paciente).Where(p => p.PacienteId == pacienteId).ToList());
                }
            }
            catch (Exception ex)
            {
                errorMessage = new ErrorMessage();
                errorMessage.TituloErro = "Exception";
                errorMessage.MensagemDeErro = !string.IsNullOrEmpty(ex?.InnerException?.Message) ? ex.InnerException.Message : ex.Message;
                string jsonStr = JsonSerializer.Serialize<ErrorMessage>(errorMessage);
                return StatusCode(500, jsonStr);
            }
        }
    }
}
