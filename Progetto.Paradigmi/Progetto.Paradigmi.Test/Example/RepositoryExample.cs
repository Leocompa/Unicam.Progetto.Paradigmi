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
/*
        var utente = utenteRepo.Ottieni("Umbe");
        var ordine = ordineRepo.Ottieni(1L);
        var portata = portataRepo.Ottieni("Lasagna");
        */
        /*
        utente.Nome = "nuovo";
        utenteRepo.Modifica(utente);
        utenteRepo.Save();
        */

        var nuovoUtente = new Utente();
        nuovoUtente.Ruolo = Ruolo.Cliente;
        nuovoUtente.Nome = "Mario";
        nuovoUtente.Cognome = "Bianchi";
        nuovoUtente.Email = "provaaa@mail.com"+DateTime.Now;
        nuovoUtente.Password= "NuovaPassw";

        utenteRepo.Aggiungi(nuovoUtente);
        utenteRepo.Save();
        
        var lasagna = new Portata();
        lasagna.Nome = "gnocchi aai funghi rossi"+DateTime.Now;
        lasagna.Prezzo =10.50;
        lasagna.Tipo = Tipologia.Primo;

        portataRepo.Aggiungi(lasagna);
        portataRepo.Save();
        
        
        var arrosto = new Portata();
        arrosto.Nome = "Spezzatino di Manzo"+DateTime.Now;
        arrosto.Prezzo =13.50;
        arrosto.Tipo = Tipologia.Secondo;

        portataRepo.Aggiungi(arrosto);
        portataRepo.Save();

        var nuovoOrdine = new Ordine();
        nuovoOrdine.Utente = nuovoUtente;
        nuovoOrdine.DataOrdine = DateTime.Now;
        nuovoOrdine.PortateSelezionate = new List<PortataOrdinata>();
        nuovoOrdine.PortateSelezionate.Add(new PortataOrdinata()
        {
            Portata = lasagna,Quantita = 2
        });
        nuovoOrdine.PortateSelezionate.Add(new PortataOrdinata()
        {
            Portata = arrosto,Quantita = 1
        });
        
        ordineRepo.Aggiungi(nuovoOrdine);
        ordineRepo.Save();
        
        var nuovoOrdine2 = new Ordine();
        nuovoOrdine2.Utente = nuovoUtente;
        nuovoOrdine2.DataOrdine = DateTime.Now;
        nuovoOrdine2.PortateSelezionate = new List<PortataOrdinata>();
        nuovoOrdine2.PortateSelezionate.Add(new PortataOrdinata()
        {
            Portata = lasagna,Quantita = 3
        });
        nuovoOrdine2.PortateSelezionate.Add(new PortataOrdinata()
        {
            Portata = arrosto,Quantita = 7
        });
        
        
        ordineRepo.Aggiungi(nuovoOrdine2);
        ordineRepo.Save();
        
    }

    public Task RunProjectAsync()
    {
        throw new NotImplementedException();
    }
}