using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Responses;

public class CreateStoricoOrdineNumPagineResponse
{
    public List<CreateStoricoOrdineResponse> StoricoOrdine { get; set; }
    public int PaginaCorrente { get; set; }
    public int PagineTotali { get; set; }

    public CreateStoricoOrdineNumPagineResponse(List<CreateStoricoOrdineResponse> storicoOrdine, int paginaCorrente, int pagineTotali )
    {
        StoricoOrdine = storicoOrdine;
        PaginaCorrente = paginaCorrente;
        PagineTotali = pagineTotali;
    }
}