using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriSoftwareV1.Data;
using NutriSoftwareV1.Dto;
using NutriSoftwareV1.Models;
using System.Text.Json;

namespace NutriSoftwareV1.Controllers
{
    public class AvaliacaoController : Controller
    {
        ErrorMessage errorMessage;
        public IActionResult Index(long Id)
        {
            using (NutriDbContext db = new NutriDbContext())
            {
                var pacienteAvaliacoes = db.pacientes.Include(p => p.Avaliacoes).FirstOrDefault(p => p.Id == Id);
                return View(pacienteAvaliacoes);
            }
        }

        [HttpGet]
        public IActionResult CadastrarEditarAvaliacaoFisica(long Id, long? AvaliacoaId)
        {
            DtoCadastroAvaliacaoFisica dtoCadastroAvaliacaoFisica = new DtoCadastroAvaliacaoFisica();
            dtoCadastroAvaliacaoFisica.PacienteId = Id;
            dtoCadastroAvaliacaoFisica.TituloCadastarEditar = "Cadastrar Avaliação";
            dtoCadastroAvaliacaoFisica.Data = DateTime.Now;

            using (NutriDbContext db = new NutriDbContext())
            {
                var avaliacoes = db.AvaliacoesFisicas.Include(p => p.Paciente).Where(p => p.PacienteId == Id).OrderBy(p => p.DataAvaliacao).ToList();
                dtoCadastroAvaliacaoFisica.UltimaAvalicao = avaliacoes.LastOrDefault();
                dtoCadastroAvaliacaoFisica.NumeroAvaliacao = avaliacoes.Count() + 1;
                if (AvaliacoaId.HasValue)
                {
                    dtoCadastroAvaliacaoFisica.TituloCadastarEditar = "Editar Avaliação";
                    dtoCadastroAvaliacaoFisica.CriarAtulizar = db.AvaliacoesFisicas.Find(AvaliacoaId.Value);
                    dtoCadastroAvaliacaoFisica.NumeroAvaliacao = dtoCadastroAvaliacaoFisica.CriarAtulizar.Consulta;
                    dtoCadastroAvaliacaoFisica.Data = dtoCadastroAvaliacaoFisica.CriarAtulizar.DataAvaliacao;
                }
            }
            return PartialView("Partiais/_FormularioAvaliacao", dtoCadastroAvaliacaoFisica);


        }


        [HttpPost]
        public IActionResult CadastrarEditarAvaliacaoFisica(AvaliacaoFisica pAvaliacao)
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
                    db.AvaliacoesFisicas.Update(pAvaliacao);
                else
                    db.AvaliacoesFisicas.Add(pAvaliacao);
                db.SaveChanges();
                return PartialView("Partiais/_ListaAvaliacoes", db.AvaliacoesFisicas.Include(p => p.Paciente).Where(p => p.PacienteId == pAvaliacao.PacienteId).ToList());
            }
        }

        [HttpPost]
        public IActionResult Delete(long Id)
        {
            try
            {

                using (NutriDbContext db = new NutriDbContext())
                {
                    var avaliacao = db.AvaliacoesFisicas.Find(Id);
                    var pacienteId = avaliacao.PacienteId;
                    db.Remove(avaliacao);
                    db.SaveChanges();

                    return PartialView("Partiais/_ListaAvaliacoes", db.AvaliacoesFisicas.Include(p => p.Paciente).Where(p => p.PacienteId == pacienteId).ToList());
                }
            }catch (Exception ex)
            {
                errorMessage = new ErrorMessage();
                errorMessage.TituloErro = "Exception";
                errorMessage.MensagemDeErro = !string.IsNullOrEmpty(ex?.InnerException?.Message)? ex.InnerException.Message : ex.Message;
                string jsonStr = JsonSerializer.Serialize<ErrorMessage>(errorMessage);
                return StatusCode(500, jsonStr);
            }
        }
    }
}