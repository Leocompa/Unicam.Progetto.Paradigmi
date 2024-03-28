namespace Paradigmi.Models.Entities;

public class Utente
{
    public string Nome { get; set; } = String.Empty;
    public string Cognome { get; set; } = String.Empty;
    public Ruolo Ruolo { get; set; }
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
}