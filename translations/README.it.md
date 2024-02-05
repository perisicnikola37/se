<kbd>[<img title="Inglese" alt="Inglese" src="https://upload.wikimedia.org/wikipedia/en/thumb/a/ae/Flag_of_the_United_Kingdom.svg/1200px-Flag_of_the_United_Kingdom.svg.png" width="22">](translations/README.en.md)</kbd>
<kbd>[<img title="Italiano" alt="Italiano" src="https://upload.wikimedia.org/wikipedia/en/thumb/0/03/Flag_of_Italy.svg/1500px-Flag_of_Italy.svg.png" width="22">](translations/README.ita.md)</kbd>

<div style="display: flex; justify-content: space-between; align-items: center;">
  <h1>Expense Tracker</h1>
  <img src="https://i.postimg.cc/VsKQJpRb/logo.png" width="38" />
</div>

### Panoramica

Questa applicazione basata su .NET e React è progettata per aiutare gli utenti a gestire ed monitorare efficacemente le proprie spese e entrate. L'obiettivo è fornire una soluzione completa per organizzare transazioni finanziarie, generare report e offrire funzionalità premium aggiuntive per un'esperienza utente avanzata.

## Documentazione API Postman
<a href="https://documenter.getpostman.com/view/21619259/2s9YsRd9TF#757dd6bd-9a08-40fd-b5f9-7b19dfaf9b81" target="_blank">Clicca qui</a>

## Documentazione API Swagger
[applicationURL]/swagger

## Utente Amministratore predefinito

### Credenziali:
Indirizzo email: admin@gmail.com
<br />
Password: password

## Funzionalità

1. **Dashboard**
   - Visualizza l'importo corrente
   - Mostra gli ultimi 5 cambiamenti nelle spese
   - Mostra gli ultimi 5 cambiamenti nelle entrate
   - Pulsanti per navigare alle pagine di elenco gruppi spese ed entrate
   - Pulsante per aggiungere spese/entrate tramite un popup

2. **Operazioni CRUD**
   - Creare, Leggere, Aggiornare ed Eliminare spese
   - Creare, Leggere, Aggiornare ed Eliminare entrate
   - Creare, Leggere, Aggiornare ed Eliminare gruppi di entrate
   - Creare, Leggere, Aggiornare ed Eliminare gruppi di spese

## Modelli di Database

- **Gruppo Spese**
  - Nome
  - Descrizione
  - ID Utente
  - [spese]

- **Spesa**
  - Descrizione
  - Importo
  - ID Gruppo spese
  - ID Utente

- **Gruppo Entrate**
  - Nome
  - Descrizione
  - ID Utente
  - [entrate]

- **Entrata**
  - Descrizione
  - Importo
  - ID Gruppo entrate
  - ID Utente

- **Promemoria** (Funzionalità Premium)
  - Giorno promemoria
  - Tipo
  - Attivo (booleano)

- **Blog** (Funzionalità Premium)
  - Descrizione
  - Testo
  - ID Utente

## Interfaccia Utente - UI

1. **Dashboard**
   - Riassunto della scorsa settimana - Grafico
   - Ultimi 5 cambiamenti nelle spese + cambiamento grafico in relazione all'importo più alto delle spese
   - Ultimi 5 cambiamenti nelle entrate + cambiamento grafico in relazione all'importo più alto delle entrate
   - Menu per navigare alle pagine di elenco gruppi spese ed entrate
   - Piani personalizzati per il tuo percorso finanziario
   - Sezione testimonianze
   - Sezione domande frequenti (FAQ)
   - Sezione newsletter
   - Piè di pagina con collegamenti correlati

2. **Pagina Elenco Entrate/Spese**
   - Tabella
     - Id
     - Nome (descrizione)
     - Importo
     - Gruppo
     - Pulsanti Modifica ed Elimina
   - Pulsante checkbox per selezionare tutte le entrate/spese disponibili
   - Pulsante icona Elimina per eliminare tutte le entrate/spese disponibili
   - Frecce di ordinamento per ordinare tutte le entrate/spese disponibili
   - Pulsante per aggiungere entrate/spese tramite popup
   - Campo di input di ricerca per cercare entrate/spese
   - Menù a discesa per filtrare entrate/spese
   - Opzioni di filtro disponibili: Importo minimo, Importo massimo e filtro per gruppo entrate/spese (nome)
   - Pulsante per esportare le statistiche via email
   - Opzioni di paginazione per paginare entrate/spese
   - Funzionalità "Righe per pagina" per mostrare un certo numero di entrate/spese

3. **Popup Modifica Entrate/Spese**
   - Campo nome modificabile
   - Campo descrizione modificabile
   - Campo importo modificabile
   - Campo gruppo entrate modificabile
   - Pulsante Salva
   - Pulsante Annulla

4. **Popup Elimina Entrate/Spese**
   - Pulsante Salva
   - Pulsante Annulla

5. **Pagina Elenco Gruppi Entrate/Spese**
   - Tabella
     - Id
     - Nome
     - Descrizione
     - Pulsanti Modifica ed Elimina
   - Pulsante checkbox per selezionare tutti i gruppi entrate/spese disponibili
   - Pulsante icona Elimina per eliminare tutti i gruppi entrate/spese disponibili
   - Frecce di ordinamento per ordinare tutti i gruppi entrate/spese disponibili
   - Pulsante per aggiungere gruppi entrate/spese tramite popup
   - Opzioni di paginazione per paginare gruppi entrate/spese
   - Funzionalità "Righe per pagina" per mostrare un certo numero di gruppi entrate/spese
   - Modale di eliminazione con opzioni di conferma e annulla
   - Nome cliccabile del gruppo entrate/spese che va alla pagina dei dettagli

6. **Popup Modifica Gruppi Entrate/Spese**
   - Campo nome modificabile
   - Campo descrizione modificabile
   - Pulsante Salva
   - Pulsante Annulla

7. **Popup Elimina Gruppi Entrate/Spese**
   - Pulsante Salva
   - Pulsante Annulla

8. **Pagina Dettagli Gruppo Entrate/Spese**
   - Nome
   - Descrizione
   - Ultimi 5 cambiamenti nel conto per quel gruppo
   - Breadcrumb per una navigazione semplice

9. **Promemorie (Funzionalità Premium nelle Impostazioni Profilo)**
   - Pulsante per creare promemoria tramite un popup
   - Dettagli del promemoria attualmente impostato sulla dashboard
   - Breadcrumb per una navigazione semplice

10. **Popup Elimina Promemoria**
   - Pulsante Salva
   - Pulsante Annulla

11. **Blogs (Funzionalità Premium)**
   - Card con tutti i blog disponibili
   - Breadcrumb per una navigazione semplice

12. **Pagina Dettagli Blog**
   - Titolo
   - Descrizione
   - Autore
   - Data di creazione
   - Corpo
   - Breadcrumb per una navigazione semplice

## Installazione
Per la guida all'installazione, visita il nostro <a href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project/-/wikis/Project-setup-and-installation" target="_blank">Wiki</a>

## Altre integrazioni

### Email stato pipeline
**Pipeline Backend:**
Abilita i test nel progetto .NET.
<br/>
<br/>
**Pipeline Frontend:**
Compila il progetto.

**Notifiche via Email:**

Invia email di stato della pipeline a una lista predefinita di destinatari, concentrandosi solo sulle pipeline fallite.

### Integrazione Mailchimp
Registra automaticamente gli utenti che si iscrivono alla newsletter nell'audience collegata di Mailchimp.

<hr/>

Tutti i diritti riservati.
<br/>
Copyright &copy; 2023 
