using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Requests;

public class CreateUtenteRequest
{
    public string Nome { get; set; } = String.Empty;
    public string Cognome { get; set; } = String.Empty;

    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;

    public Utente ToEntity(Ruolo ruolo)
    {
        var utente = new Utente();
        utente.Nome = Nome;
        utente.Cognome = Cognome;
        utente.Ruolo = ruolo;
        utente.Email = Email;
        utente.Password = Password;
        return utente;
    }
}