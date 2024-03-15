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
        var ordine = ordineRepo.Ottieni(1L);
        var portata = portataRepo.Ottieni("Lasagna");
        /*
        utente.Nome = "nuovo";
        utenteRepo.Modifica(utente);
        utenteRepo.Save();
        */

        var nuovoUtente = new Utente();
        nuovoUtente.Ruolo = Ruolo.Cliente;
        nuovoUtente.Nome = "Mario";
        nuovoUtente.Cognome = "Bianchi";
        nuovoUtente.Email = "cccc@mail.com";
        nuovoUtente.Password= "NuovaPassw";

        utenteRepo.Aggiungi(nuovoUtente);
        utenteRepo.Save();
        
        var lasagna = new Portata();
        lasagna.Nome = "Lasagna";
        lasagna.Prezzo =10.50;
        lasagna.Tipo = Tipologia.Primo;
        lasagna.Quantita= 1;

        portataRepo.Aggiungi(lasagna);
        portataRepo.Save();
        
        
        var arrosto = new Portata();
        arrosto.Nome = "Arrosto";
        arrosto.Prezzo =13.50;
        arrosto.Tipo = Tipologia.Secondo;
        arrosto.Quantita= 2;

        portataRepo.Aggiungi(arrosto);
        portataRepo.Save();

        var nuovoOrdine = new Ordine();
        nuovoOrdine.Utente = nuovoUtente;
        nuovoOrdine.DataOrdine = DateTime.Now;
        nuovoOrdine.Portate.Add(new PortataOrdinata(lasagna,2));
        nuovoOrdine.Portate.Add(new PortataOrdinata(arrosto,1));
        
        var nuovoOrdine2 = new Ordine();
        nuovoOrdine2.Utente = nuovoUtente;
        nuovoOrdine2.DataOrdine = DateTime.Now;
        nuovoOrdine2.Portate.Add(new PortataOrdinata(lasagna,3));
        nuovoOrdine2.Portate.Add(new PortataOrdinata(arrosto,7));
        
        
        
    }

    public Task RunProjectAsync()
    {
        throw new NotImplementedException();
    }
}