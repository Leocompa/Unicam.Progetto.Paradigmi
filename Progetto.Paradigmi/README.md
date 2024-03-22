<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Progetto "Programmazione Enterprise A.A. 2023/2024"</title>
</head>
<body>
    <h1>Progetto "Programmazione Enterprise A.A. 2023/2024"</h1>
    <h2>Descrizione del Progetto</h2>
    <p>Il progetto "Programmazione Enterprise A.A. 2023/2024" propone lo sviluppo di una Web API per gestire le ordinazioni in un ristorante. L'applicazione permetterà di effettuare diverse chiamate, come la creazione di un nuovo utente cliente o amministratore, l'autenticazione JWT, la visualizzazione delle portate disponibili e la gestione degli ordini effettuati.</p>
    <p>Il progetto è sviluppato utilizzando il framework .NET.</p>
    <h2>Installazione e Utilizzo</h2>
    <ol>
        <li>Clona il repository.</li>
        <li>Esegui il dump.sql situato al percorso <code>Progetto.Paradigmi/Paradigmi.Models/DumpDatabase/dump.sql</code>.</li>
        <li>Imposta la configurazione dal file <code>Progetto.Paradigmi/Paradigmi.Web/Paradigmi.Web - Dev.run.xml</code>.</li>
        <li>Avvia l'applicazione <code>Paradigmi.Web - Dev</code>.</li>
    </ol>
    <p>Dopo l'avvio, accedi all'interfaccia UI Swagger per effettuare le varie chiamate HTTP disponibili nell'applicazione.</p>
    <p>Per autenticarsi come amministratore di default, utilizzare le seguenti credenziali:</p>
    <ul>
        <li><strong>Email</strong>: <code>changeMe@admin.com</code></li>
        <li><strong>Password</strong>: <code>Adm1n!!</code></li>
    </ul>
    <h2>Spiegazione delle chiamate Swagger</h2>
<h3>Ordine:</h3>
<ul>
    <li>
        <strong>newOrdine</strong>: Creazione di un nuovo ordine da parte di un utente.
        <ul>
            <li>Parametri richiesti: email dell'utente che effettua l'ordine, elenco delle portate ordinate e un indirizzo di consegna (opzionale).</li>
            <li>Requisiti di ritorno: numero dell'ordine, indirizzo (se presente), costo totale, costo totale scontato, informazioni sull'ordine (compreso il numero dei pasti completi).</li>
        </ul>
    </li>
    <li>
        <strong>getStoricoOrdini</strong>: Ottieni un elenco paginato degli ordini effettuati da un utente o tutti gli ordini presenti nel ristorante (se sei un amministratore).
        <ul>
            <li>Parametri richiesti: numero della pagina, quanti ordini visualizzare su ogni pagina, periodo di ricerca e email dell'utente (opzionale).</li>
            <li>Nota: se sei un amministratore otterrai tutti gli ordini presenti nel ristorante nel periodo selezionato, altrimenti otterrai solamente i tuoi ordini nel periodo prescelto.</li>
        </ul>
    </li>
    <li>
        <strong>getOrdineByNumeroOrdine</strong>: Ottieni un riepilogo dell'ordine in base al numero dell'ordine ricercato.
        <ul>
            <li>Parametro richiesto: numero dell'ordine.</li>
            <li>Dati restituiti: email dell'utente, data dell'ordine, numero dell'ordine e indirizzo (se presente).</li>
        </ul>
    </li>
</ul>

<h3>Portata:</h3>
<ul>
    <li>
        <strong>newPortata</strong>: Creazione di una nuova portata da parte di un amministratore.
        <ul>
            <li>Parametri richiesti: tipologia della portata, nome e prezzo.</li>
        </ul>
    </li>
    <li>
        <strong>getPortate</strong>: Ottieni una lista delle portate esistenti.
        <ul>
            <li>Puoi effettuare la richiesta senza passare parametri o specificare la tipologia o il nome della portata (parziale o completo).</li>
        </ul>
    </li>
</ul>

<h3>PortataOrdinata:</h3>
<ul>
    <li>
        <strong>getPortateByNumeroOrdine</strong>: Ottieni una lista delle portate di uno specifico ordine.
        <ul>
            <li>Se autenticato come amministratore, puoi visualizzare gli ordini di qualsiasi utente. Se autenticato come cliente, puoi visualizzare solamente i tuoi ordini.</li>
            <li>Dati restituiti: nome della portata e quantità associata.</li>
        </ul>
    </li>
</ul>

<h3>Token:</h3>
<ul>
    <li>
        <strong>create</strong>: Creazione di un nuovo token JWT per l'autenticazione.
        <ul>
            <li>Parametri richiesti: email dell'utente e password.</li>
            <li>Nota: verrà inviato un messaggio di errore se la mail o la password non rispettano i requisiti.</li>
        </ul>
    </li>
</ul>

<h3>Utente:</h3>
<ul>
    <li>
        <strong>new</strong>: Creazione di un nuovo utente.
        <ul>
            <li>Parametri richiesti: ruolo del nuovo utente (solo se autenticato come amministratore), nome, cognome, email e password.</li>
        </ul>
    </li>
</ul>
    <h2>Studenti per lo sviluppo del progetto</h2>
   <table>
    <thead>
        <tr>
            <th>Nome</th>
            <th>Cognome</th>
            <th>Matricola</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <td>Umberto</td>
            <td>Di Antonio</td>
            <td>120024</td>
        </tr>
        <tr>
            <td>Leonardo</td>
            <td>Compagnucci</td>
            <td>118708</td>
        </tr>
    </tbody>
</table>
</body>
</html>
