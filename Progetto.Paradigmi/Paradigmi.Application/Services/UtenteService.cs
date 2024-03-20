using Paradigmi.Application.Abstractions.Services;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;

namespace Paradigmi.Application.Services;

public class UtenteService : IUtenteService
{
    private readonly UtenteRepository _utenteRepository;

    public UtenteService(UtenteRepository utenteRepository)
    {
        _utenteRepository = utenteRepository;
    }

    public List<Utente> GetUtenti()
    {
        return _utenteRepository.GetUtenti();
    }

    public List<Utente> GetUtenti(int from, int num, string? name, out int totalNum)
    {
        return _utenteRepository.GetUtenti(from, num, name, out totalNum);
    }

    public Task AddUtenteAsync(Utente utente)
    {
        //TODO verifica se metodo async
        _utenteRepository.Aggiungi(utente);
        _utenteRepository.Save();
        return Task.CompletedTask;
    }

    
}