using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreatePortataRequest
{
    public string Nome { get; set; }
    public decimal Prezzo { get; set; }
    //public Tipologia Tipo { get; set; }
}