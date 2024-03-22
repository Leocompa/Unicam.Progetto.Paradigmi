namespace Paradigmi.Application.Models.Responses;

public class CreatePortateOrdinateRigaResponse
{
    
    public string Riga { get; set; }

    public CreatePortateOrdinateRigaResponse(string nomePortata, int quantita, decimal costo)
    {
        Riga = $"{quantita}x{nomePortata} = {costo}";
    }
}