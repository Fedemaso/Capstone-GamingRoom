


File readme in lavorazione :) 


# Gaming Room

Gaming Room, il TicketOne del gaming :joystick:


## Cos'è

Gaming Room è una piattaforma e-commerce dove poter acquistare biglietti per assistere ad eventi e tornei di videogiochi.


## Come Funziona 


La piattaforma è divisa in 2 livelli: Customer e Business, diviso a sua volta in Admin e SuperAdmin

### Funzionalità Customer

L'utente finale, dopo essersi registrato e aver fatto il login ha accesso alle funzioni dedicate al lato customer:

- **Home Page:**
  Panoramica su tutta la piattaforma con preview degli eventi in programma e dei teams Esport inseriti nel database, sezione con foto delle Arene dove i tornei verranno disputati e sezione contatti

- **Eventi:**
  Panoramica di tutti gli eventi in programma, ogni evento è associato ad un titolo di videogame, entrando nei dettagli del titolo sarà data la possibilità all'utente di vedere nello specifico i dettagli, il luogo di svolgimento, i team partecipanti e sarà possibile acquistare fino a 5 biglietti per assistere all'evento

- **Teams**
  Panoramica di tutti i Teams Esport inseriti nel database,  entrando nei dettagli del team sarà data la possibilità all'utente di vedere una descrizione della storia del team e tutti i player che fanno parte del roster

- **Carrello**
  Carrello con funzione di riepilogo ordini inseriti, modifica quantità, rimozione singolo articolo o svuotamento totale.
  I dati di quantità e prezzo totale si modificano automaticamenmte premendo il tasto aggiorna, una volta inseriti anche i dati della carta di credito e formalizzato il pagamento l'ordine viene registrato nel database.

- **Logout**
  Normale funzione di logout

### Funzionalità Business lato _ADMIN_

Il ruolo ADMIN sarà dato agli operatori delle varie aziende di videogame, publisher, teams e organizzazioni che vogliono creare un'evento e venderne i biglietti.
Tramite la compilazione di un form verrà inoltrata al gestore di Gaming Room la richiesta di creazione evento o creazione nuovo team che verranno inseriti nella piattaforma.



- **Home Page:**
  Panoramica su tutta la piattaforma con preview degli eventi in programma e dei teams Esport inseriti nel database, sezione con foto delle Arene dove i tornei verranno disputati e sezione contatti

- **Eventi**
  Lista degli eventi inseriti a sistema, possibilità di vedere i dettagli dell'evento, ma non di modificarlo ne cancellarlo.

- **Teams**
  Lista dei teams inseriti a sistema, possibilità di vedere i dettagli del team, ma non di modificarli ne cancellarli.

- **Players**
  Lista dei players inseriti a sistema, possibilità di vedere i dettagli del player, ma non di modificarli ne cancellarli.
  
- **Ordini**
  Lista degli ordini fatti dal lato customer, possibilità di vedere i dettagli dell'ordine, ma non di modificarli ne cancellarli.

- **Richiedi Evento**
  Con questa funzione l'admin, compilando un form con i dettagli necessari, invia al lato SuperAdmin una richiesta di creazione nuovo evento, che potrà essere Approvata o Eliminata.
  Nel caso la richiesta venisse approvata l'evento si creerà automaticamente e verrà inserito nella pagina degli eventi in programma.

- **Richiedi Team**
  Con questa funzione l'admin, compilando un form con i dettagli necessari, invia al lato SuperAdmin una richiesta di creazione nuovo team, che potrà essere Approvata o Eliminata.
  Nel caso la richiesta venisse approvata il team si creerà automaticamente e verrà inserito nella pagina dei Teams.

- **Logout**
  Normale funzione di logout



### Funzionalità Business lato _SUPERADMIN_

Il ruolo SUPERADMIN sarà dato agli operatori di GamingRoom, che avranno pieni poteri di inserimento, modifica ed eliminazione di: eventi, teams, titoli, arene, utenti, ordini, oltre alla possibilità di visualizzare le richieste di creazione nuovi eventi e teams fatte dal lato Admin, 



- **Home Page:**
  Panoramica su tutta la piattaforma con preview degli eventi in programma e dei teams Esport inseriti nel database, sezione con foto delle Arene dove i tornei verranno disputati e sezione contatti

- **Eventi**
  Lista degli eventi inseriti a sistema, possibilità di vedere i dettagli dell'evento, modificarlo ed eliminarlo.

- **Teams**
  Lista dei teams inseriti a sistema, possibilità di vedere i dettagli del team, modificarlo ed eliminarlo.

- **Players**
  Lista dei players inseriti a sistema, possibilità di vedere i dettagli del player, modificarlo ed eliminarlo.

- **Titoli**
  Lista dei titoli inseriti, possibilità di vedere i dettagli del titolo, modificarlo ed eliminarlo.

- **Arene**
  Lista delle arene di gioco, possibilità di vedere i dettagli dell'arena, modificarla ed eliminarla.

- **Aziende**
  Lista delle aziende inserite a sistema, possibilità di vedere i dettagli dell'azienda, modificarla ed eliminarla.

- **Utenti**
  Lista degli Utenti registrati tramite il form di registrazione o inseriti manualmente a sistema, possibilità di vedere i dettagli dell'utente, modificarlo ed eliminarlo.
  Nella tabella dedicata nel database verranno modificati i ruoli per assegnare quello di ADMIN alle aziende/agenzie/teams ecc.
  
- **Ordini**
  Lista degli ordini fatti dal lato customer, possibilità di vedere i dettagli dell'ordine, modificarlo ed eliminarlo.

- **Approva Evento**
  Con questa funzione il SuperAdmin accede ad una lista di richieste creazione nuovo evento inviate dalle varie aziende/teams/organizzazioni, può vederne i dettagli ed ogni richiesta potrà essere Approvata o Eliminata.
  Nel caso la richiesta venisse approvata l'evento si creerà automaticamente e verrà inserito nella pagina degli eventi in programma.

- **Approva Team**
  Con questa funzione il SuperAdmin accede ad una lista di richieste creazione nuovo Team inviate dalle varie aziende/teams/organizzazioni, può vederne i dettagli ed ogni richiesta potrà essere Approvata o Eliminata.
  Nel caso la richiesta venisse approvata Il team si creerà automaticamente e la sua card verrà inserita nella pagina dei Teams registrati.

- **Logout**
  Normale funzione di logout












Funzionalità
Gestione Eventi: Gli utenti possono richiedere la creazione di nuovi eventi, fornendo dettagli come nome, descrizione, data proposta, luogo, biglietti disponibili, e prezzo del biglietto. Gli amministratori possono poi approvare o declinare queste richieste.
Gestione Team: Simile alla gestione degli eventi, gli utenti possono richiedere la creazione di nuovi team. Queste richieste includono nome del team, descrizione e una foto proposta.
Gestione Utenti e Aziende: Gli amministratori possono gestire gli utenti e i dettagli aziendali associati, compresa la creazione, modifica ed eliminazione di tali entità.
Visualizzazione e Modifica di Eventi e Team: Gli eventi e i team approvati sono visibili in elenchi dedicati, dove possono essere modificati o eliminati.
Tecnologie Utilizzate
Frontend: HTML, CSS, JavaScript
Backend: ASP.NET MVC
Database: Entity Framework con SQL Server
Installazione e Configurazione
Per eseguire l'applicazione, segui questi passaggi:

Clona il repository su GitHub.
Apri la soluzione con Visual Studio.
Configura la stringa di connessione del database nel file web.config.
Esegui il comando Update-Database nella console di Gestione Pacchetti per configurare il database.
Avvia l'applicazione tramite Visual Studio.
Struttura del Progetto
Controllers: Contiene i controller MVC per la gestione delle varie funzionalità dell'app.
Models: Definisce i modelli dati utilizzati nell'applicazione.
Views: Contiene le viste Razor per l'interfaccia utente.
Scripts: Contiene script JavaScript per la logica frontend.
Content: Include file CSS e immagini.
Contribuire
Per contribuire al progetto:

Fork il repository.
Crea un nuovo branch per la tua feature.
Invia una pull request con la tua modifica.
Licenza
Questo progetto è rilasciato sotto la licenza [INSERISCI LICENZA].
