using Paradigmi.Abstraction;
using Paradigmi.Models.Context;
using Paradigmi.Models.Entities;
using Paradigmi.Models.Repositories;


namespace Progetto.Paradigmi.Test.Example;

public class RepositoryExample : IProject
{
    public async Task RunExampleAsync()
    {
    }

    public void RunProject()
    {
        var ctx = new MyDbContext();
        var utenteRepo = new UtenteRepository(ctx);
        var ordineRepo = new OrdineRepository(ctx);
        var portataRepo = new PortataRepository(ctx);

        var utente = utenteRepo.Ottieni("Umbe");
        var ordine = ordineRepo.Ottieni(1);
        var portata = portataRepo.Ottieni(1);
        
        utente.Name = "nuovo";
        utenteRepo.Modifica(utente);
        utenteRepo.Save();

        var nuovoUtente = new Utente();
        nuovoUtente.Ruolo = Ruolo.Cliente;
        nuovoUtente.Name = "Mario";
        nuovoUtente.Cognome = "Bianchi";
        //nuovoDipendente.IdAzienda = 5;

        nuovoUtente.Email = "aaa@mail.com";
        nuovoUtente.Password= "NuovaPassw";

        utenteRepo.Aggiungi(nuovoUtente);
        utenteRepo.Save();
    }

    public Task RunProjectAsync()
    {
        throw new NotImplementedException();
    }
}