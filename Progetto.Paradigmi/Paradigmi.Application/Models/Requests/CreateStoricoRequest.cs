using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreateStoricoRequest
{
    public int PaginaCorrente { get; set; }
    public int RighePerPagina { get; set; }
    public Utente Utente { get; set; }
    public DateTime? DataInizio { get; set; }
    public DateTime? DataFine { get; set; }
    public string? UtenteCercato { get; set; }
    
}