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
        var portataOrdinataRepo = new PortateOrdinateRepository(ctx,portataRepo);
        var ordineService = new OrdineService(ordineRepo, portataRepo,null);
        var portateOrdinateService = new PortateOrdinateService(portataOrdinataRepo);

        
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
        
        var primo1 = CreaPortata("Gnocchi", 10.50M, Tipologia.Primo,portataRepo);
        var secondo = CreaPortata("Spezzatino", 13.50M, Tipologia.Secondo,portataRepo);
        var contorno = CreaPortata("Insalata", 5, Tipologia.Contorno,portataRepo);
        var antipasto = CreaPortata("Tagliere", 12, Tipologia.Antipasto,portataRepo);
        var dolce = CreaPortata("Tiramisu", 5.5M, Tipologia.Dolce,portataRepo);
        var primo2 = CreaPortata( "Tagliatelle", 12.5M, Tipologia.Primo,portataRepo);
        var vino = CreaPortata("Vino", 10, Tipologia.Bevande,portataRepo);
        
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

        int numeroPasti;
        decimal costoTotale;
        decimal costoTotaleScontato;
        
        int idOrdine = ordineService.AddOrdine(nuovoUtente.Email, nuovaPortataOrdinata,new Address
            {
                Cap = "60035",
                Citta = "Jesi",
                Civico = "2",
                Via = "Via Madonna delle Carceri"
            },
         out costoTotaleScontato ,out costoTotale, out numeroPasti);
        
        Console.WriteLine(costoTotale);

        int totalNum = 0;
        var elenco = ordineService.GetStoricoOrdini(0, 3, amministratore.Ruolo, null, null, null,  out totalNum);

        
        foreach (var riga in elenco)
        {
            Console.WriteLine("____________________________________________");
            var portateOrdine=portateOrdinateService.GetPortateOrdine(riga.NumeroOrdine);
            Console.WriteLine(riga.ClienteEmail + " , " + riga.DataOrdine + " ' " + riga.NumeroOrdine);

            foreach (var portate in portateOrdine)
            {
                Console.WriteLine("-- " +portate.Quantita+" x "+ portate.PortataNome+" = "+portateOrdinateService.getCostoPortata(portate.OrdinazioneId,portate.PortataNome));
            }
        }
        Console.WriteLine("Current Page: "+1+"/"+totalNum/3);
        Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
        var elencoUtente = ordineService.GetStoricoOrdini(0, 10, nuovoUtente.Ruolo, DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),null, nuovoUtente.Email,  out totalNum);

        
        foreach (var riga in elencoUtente)
        {
            Console.WriteLine("____________________________________________");
            var portateOrdine=portateOrdinateService.GetPortateOrdine(riga.NumeroOrdine);
            Console.WriteLine(riga.ClienteEmail + " , " + riga.DataOrdine + " ' " + riga.NumeroOrdine);

            foreach (var portate in portateOrdine)
            {
                Console.WriteLine("-- " +portate.Quantita+" x "+ portate.PortataNome+" = "+portateOrdinateService.getCostoPortata(portate.OrdinazioneId,portate.PortataNome));
            }
        }


    }

    private Portata CreaPortata(String nome,decimal prezzo, Tipologia tipo,PortataRepository portataRepo)
    {
        Portata portata = new Portata(nome, prezzo, tipo);
        portataRepo.Aggiungi(portata);
        portataRepo.Save();
        return portata;
    }
    
}