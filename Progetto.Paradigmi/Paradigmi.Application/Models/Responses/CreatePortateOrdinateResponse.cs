namespace Paradigmi.Application.Models.Responses;

public class CreatePortateOrdinateResponse
{
    public string NomePortata { get; set; }
    public int Quantita { get; set; }
    

    public CreatePortateOrdinateResponse(string nomePortata, int quantita)
    {
        NomePortata = nomePortata;
        Quantita = quantita;
    }
}