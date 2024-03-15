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
        //var portataOrdinataRepo = new PortateOrdinateRepository(ctx);

        //var utente = utenteRepo.Ottieni("Umbe");
        //var ordine = ordineRepo.Ottieni(1L);
        //var portata = portataRepo.Ottieni("Lasagna");
        /*
        utente.Nome = "nuovo";
        utenteRepo.Modifica(utente);
        utenteRepo.Save();
        */

        var nuovoUtente = new Utente();
        nuovoUtente.Ruolo = Ruolo.Cliente;
        nuovoUtente.Nome = "Mario";
        nuovoUtente.Cognome = "Bianchi";
        nuovoUtente.Email = "prova6@mail.com";
        nuovoUtente.Password= "NuovaPassw";

        utenteRepo.Aggiungi(nuovoUtente);
        utenteRepo.Save();
        
        var lasagna = new Portata();
        lasagna.Nome = "tortelli";
        lasagna.Prezzo =10.50;
        lasagna.Tipo = Tipologia.Primo;

        portataRepo.Aggiungi(lasagna);
        portataRepo.Save();
        
        
        var arrosto = new Portata();
        arrosto.Nome = "Arrosti";
        arrosto.Prezzo =13.50;
        arrosto.Tipo = Tipologia.Secondo;

        portataRepo.Aggiungi(arrosto);
        portataRepo.Save();

        var nuovoOrdine = new Ordine(nuovoUtente,DateTime.Now);
        nuovoOrdine.Portate = new Dictionary<int, Portata>();
        nuovoOrdine.Portate.Add(2,lasagna);
        nuovoOrdine.Portate.Add(1,arrosto);
        
        ordineRepo.Aggiungi(nuovoOrdine);
        ordineRepo.Save();

        var nuovoOrdine2 = new Ordine(nuovoUtente, DateTime.Now);
        nuovoOrdine2.Portate = new Dictionary<int, Portata>();
        nuovoOrdine2.Portate.Add(3,lasagna);
        nuovoOrdine2.Portate.Add(7,arrosto);
        
        ordineRepo.Aggiungi(nuovoOrdine2);
        ordineRepo.Save();
        
    }

    public Task RunProjectAsync()
    {
        throw new NotImplementedException();
    }
}