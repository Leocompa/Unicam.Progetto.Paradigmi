using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreatePortateOrdinateRequest
{
    public string NomePortata { get; set; }
    public int Quantita { get; set; }
    public int Turno { get; set; }

    public PortataOrdinata ToEntity()
    {
        return new PortataOrdinata(NomePortata, Quantita, Turno);
    }
}