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
    <p>Il progetto "Programmazione Enterprise A.A. 2023/2024" propone di sviluppare una Web Api per effettuare delle ordinazioni in un ristorante.
        L'applicazione permetterà di effettuare diverse chiamate, come la creazione di un nuovo utente cliente o amministratore,
        un'autenticazione JWT, una lista delle portate create dall'amministratore, una lista degli ordini effettuati dagli utenti
        o la visualizzazione di tutti gli ordini.</p>
    <h2>Installazione e Utilizzo</h2>
    <ol>
        <li>Clona il repository.</li>
        <li>Eseguire il dump.sql situato al percorso <code>Progetto.Paradigmi/Paradigmi.Models/DumpDatabase/dump.sql</code></li>
        <li>Imposta la configurazione dal file <code>Progetto.Paradigmi/Paradigmi.Web/Paradigmi.Web - Dev.run.xml</code>.</li>
        <li>Avviare l'applicazione<code>Paradigmi.Web - Dev</code>.</li>
    </ol>
    <p>Una volta avviata si aprirà automaticamente la schermata con l'interfaccia UI Swagger dove si potranno effettuare
    le varie chiamate http disponibili nell'applicazione.</p>
    <p>Al primo accesso si dovrà creare un Token JWT per autenticarsi come amministratore di defaul con le seguenti credenziali:</p>
    <ul>
        <li><strong>Email</strong>: <code>changeMe@admin.com</code></li>
        <li><strong>Password</strong>: <code>Adm1n!!</code></li>
    </ul>
    <p>Una volta autenticato sarà possibile creare dei nuovi amministratori(consigliato).</p>
    <p>E' possibile creare degli utenti con il ruolo di <strong>"cliente"</strong> senza effettuare l'autenticazione JWT.<br>
        I Clienti che vogliono eseguire operazioni, ad esempio un ordine, dovranno creare un Token JWT, nel caso in cui son </p>
    <p>Il gestore del database viene creato in automatico e avrà le seguenti credenziali:</p>
    <ul>
        <li><strong>Username</strong>: <code>admin</code></li>
        <li><strong>Password</strong>: <code>admin</code></li>
    </ul>
    <p>L'username sarà necessario per richiamare i metodi del gestore della piattaforma. Non sono messi a disposizione metodi per la modifica delle credenziali del gestore della piattaforma.</p>
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
