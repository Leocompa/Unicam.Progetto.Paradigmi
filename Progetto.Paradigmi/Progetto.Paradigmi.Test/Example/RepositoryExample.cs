using System.Security.Cryptography.X509Certificates;
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
        var amministratore = new Utente();
        amministratore.Ruolo = Ruolo.Amministratore;
        amministratore.Nome = "Amministratore";
        amministratore.Cognome = "Ristorante";
        amministratore.Email = "amministratore@mail.com"+DateTime.Now;
        amministratore.Password= "Passw2";

        var nuovoUtente = new Utente();
        nuovoUtente.Ruolo = Ruolo.Cliente;
        nuovoUtente.Nome = "Mario";
        nuovoUtente.Cognome = "Bianchi";
        nuovoUtente.Email = "provaaa@mail.com"+DateTime.Now;
        nuovoUtente.Password= "NuovaPassw";

        utenteRepo.Aggiungi(nuovoUtente);
        utenteRepo.Save();

        List<PortataOrdinata> nuovaPortataOrdinata = new List<PortataOrdinata>();
        
        var primo1 = CreaPortata("Gnocchi", 10.50, Tipologia.Primo,portataRepo);
        var secondo = CreaPortata("Spezzatino", 13.50, Tipologia.Secondo,portataRepo);
        var contorno = CreaPortata("Insalata", 5, Tipologia.Contorno,portataRepo);
        var antipasto = CreaPortata("Tagliere", 12, Tipologia.Antipasto,portataRepo);
        var dolce = CreaPortata("Tiramisu", 5.5, Tipologia.Dolce,portataRepo);
        var primo2 = CreaPortata( "Tagliatelle", 12.5, Tipologia.Primo,portataRepo);
        var vino = CreaPortata("Vino", 10, Tipologia.Vino,portataRepo);
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = vino,
            Quantita = 1
        });
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = primo1,
            Quantita = 2
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = secondo,
            Quantita = 2
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = contorno,
            Quantita = 2
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = antipasto,
            Quantita = 2
        });
        
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = dolce,
            Quantita = 2
        });
        nuovaPortataOrdinata.Add(new PortataOrdinata
        {
            Portata = primo2
        });

        double costoTotale;
        
        int idOrdine = ordineService.AddOrdine(nuovoUtente, nuovaPortataOrdinata,new Address
            {
                Cap = "60035",
                Citta = "Jesi",
                Civico = 2,
                Via = "Via Madonna delle Carceri"
            }
        ,out costoTotale);
        
        Console.WriteLine(costoTotale);

        int totalNum = 0;
        var elenco = ordineService.GetStoricoOrdini(0, 10, amministratore, null, null, null,  out totalNum);

        foreach (var riga in elenco)
        {
            Console.WriteLine(riga.Utente + " , " + riga.DataOrdine + " ' " + riga.NumeroOrdine);
            foreach (var portate in riga.PortateSelezionate)
            {
                Console.WriteLine("-- " + portate.Portata + ", " + portate.Quantita);
            }
        }


    }

    private Portata CreaPortata(String nome,double prezzo, Tipologia tipo,PortataRepository portataRepo)
    {
        Portata portata = new Portata(nome, prezzo, tipo);
        portataRepo.Aggiungi(portata);
        portataRepo.Save();
        return portata;
    }

    public Task RunProjectAsync()
    {
        throw new NotImplementedException();
    }
}