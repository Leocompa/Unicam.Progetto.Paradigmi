using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Models.Dtos;

public class UtenteDto
{
    
    public string Nome { get; set; } = String.Empty;
    public string Cognome { get; set; } = String.Empty;
    public Ruolo Ruolo { get; set; }
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    
    public UtenteDto()
    {
        
    }

    public UtenteDto(Utente utente)
    {
        Nome = utente.Nome;
        Cognome = utente.Cognome;
        Ruolo = utente.Ruolo;
        Email = utente.Email;
        Password = utente.Password;
    }
    
}