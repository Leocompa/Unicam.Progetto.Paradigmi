using Paradigmi.Models.Entities;

namespace Paradigmi.Application.Abstractions.Services;

public interface IUtenteService
{
    List<Utente> GetUtenti();
    List<Utente> GetUtenti(int from, int num, string? name, out int totalNum);
    Task AddUtenteAsync(Utente utente);
    Utente? GetUtente(string email);
}