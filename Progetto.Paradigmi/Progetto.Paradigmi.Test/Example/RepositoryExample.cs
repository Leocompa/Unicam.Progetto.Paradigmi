using Paradigmi.Abstraction;
using Paradigmi.Application.Services;
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
        var ordineService = new OrdineService(ordineRepo);
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

        List<PortataOrdinata> nuovaPortataOrdinata = new List<PortataOrdinata>();
        
        var primo1 = new Portata("Gnocchi", 10.50, Tipologia.Primo);
        portataRepo.Aggiungi(primo1);
        portataRepo.Save();
        
        var secondo = new Portata("Spezzatino", 13.50, Tipologia.Secondo);
        portataRepo.Aggiungi(secondo);
        portataRepo.Save();

        var contorno = new Portata("Insalata", 5, Tipologia.Contorno);
        portataRepo.Aggiungi(contorno);
        portataRepo.Save();

        var antipasto = new Portata("Tagliere", 12, Tipologia.Antipasto);
        portataRepo.Aggiungi(antipasto);
        portataRepo.Save();

        var dolce = new Portata("Tiramisu", 5.5, Tipologia.Dolce);
        portataRepo.Aggiungi(dolce);
        portataRepo.Save();
        
        var primo2 = new Portata("Tagliatelle", 12.50, Tipologia.Primo);
        portataRepo.Aggiungi(primo2);
        portataRepo.Save();
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = primo1
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = secondo,
            Quantita = 2
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = contorno
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = antipasto
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = dolce
        });
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = primo2
        });

        double costoTotale = 0;
        
        int idOrdine = ordineService.AddOrdine(nuovoUtente, nuovaPortataOrdinata, out costoTotale);
        
        Console.WriteLine(costoTotale);
        ordineRepo.Aggiungi(ordineService.GetOrdine(idOrdine)!);
        ordineRepo.Save();
        
    }

    public Task RunProjectAsync()
    {
        throw new NotImplementedException();
    }
}