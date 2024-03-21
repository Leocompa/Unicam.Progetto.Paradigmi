using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreateStoricoRequest
{
    public int PaginaCorrente { get; set; }
    public int RighePerPagina { get; set; }
    public DateOnly? DataInizio { get; set; }
    public DateOnly? DataFine { get; set; }
    public string? EmailUtenteCercato { get; set; }
    
}