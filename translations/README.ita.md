<kbd>[<img title="Inglese" alt="Inglese" src="https://upload.wikimedia.org/wikipedia/en/thumb/a/ae/Flag_of_the_United_Kingdom.svg/1200px-Flag_of_the_United_Kingdom.svg.png" width="22">](README.md)</kbd>
<kbd>[<img title="Spagnolo" alt="Spagnolo" src="https://cdn11.bigcommerce.com/s-hhbbk/images/stencil/1280x1280/products/854/42341/SPAN0001__19783.1580480313.png?c=2" width="22">](translations/README.spa.md)</kbd>

<div style="display: flex; justify-content: space-between; align-items: center;">
  <h1>Gestore delle Spese</h1>
  <img src="https://i.postimg.cc/VsKQJpRb/logo.png" width="38" />
</div>

### Panoramica

Questa applicazione basata su .NET e React è progettata per aiutare gli utenti a gestire ed monitorare le loro spese e entrate in modo efficace. L'obiettivo è fornire una soluzione completa per organizzare transazioni finanziarie, generare report e offrire funzionalità premium aggiuntive per un'esperienza utente migliorata.

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
   - Pulsanti per navigare alle pagine di elenco gruppi di spese e entrate
   - Pulsante per aggiungere spese/entrate attraverso un popup

2. **Operazioni CRUD**
   - Creare, leggere, aggiornare e eliminare spese
   - Creare, leggere, aggiornare e eliminare entrate
   - Creare, leggere, aggiornare e eliminare gruppi di entrate
   - Creare, leggere, aggiornare e eliminare gruppi di spese

## Modelli di Database

- **Gruppo di Spese**
  - Nome
  - Descrizione
  - ID Utente
  - [spese]

- **Spesa**
  - Descrizione
  - Importo
  - ID Gruppo di Spese
  - ID Utente

- **Gruppo di Entrate**
  - Nome
  - Descrizione
  - ID Utente
  - [entrate]

- **Entrata**
  - Descrizione
  - Importo
  - ID Gruppo di Entrate
  - ID Utente

- **Promemoria** (Funzione Premium)
  - Giorno del promemoria
  - Tipo
  - Attivo (booleano)

- **Blog** (Funzione Premium)
  - Descrizione
  - Testo
  - ID Utente

## Interfaccia Utente - UI

1. **Dashboard**
   - Riepilogo della settimana scorsa - Grafico
   - Ultimi 5 cambiamenti nelle spese + cambiamento grafico in relazione all'importo più alto delle spese
   - Ultimi 5 cambiamenti nelle entrate + cambiamento grafico in relazione all'importo più alto delle entrate
   - Menu per navigare alle pagine di elenco gruppi di spese e entrate
   - Piani personalizzati per il tuo percorso finanziario
   - Sezione testimonianze
   - Sezione Domande frequenti (FAQ)
   - Sezione Newsletter
   - Piè di pagina con link correlati

2. **Pagina di Elenco Entrate/Spese**
   - Tabella
     - Id
     - Nome (descrizione)
     - Importo
     - Gruppo
     - Pulsanti Modifica ed Elimina
   - Pulsante checkbox per selezionare tutte le entrate/spese disponibili
   - Pulsante icona Elimina per eliminare tutte le entrate/spese disponibili
   - Frecce di ordinamento per ordinare tutte le entrate/spese disponibili
   - Pulsante per aggiungere entrate/spese attraverso un popup
   - Campo di input di ricerca per cercare entrate/spese
   - Menu a discesa filtro per filtrare entrate/spese
   - Opzioni di filtro disponibili: Importo minimo, importo massimo e filtro per gruppo di entrate/spese (nome)
   - Pulsante per esportare le statistiche via email
   - Opzioni di paginazione per paginare entrate/spese
   - Funzionalità "Righe per pagina" per mostrare un certo numero di entrate/spese

3. **Popup Modifica Entrate/Spese**
   - Campo nome modificabile
   - Campo descrizione modificabile
   - Campo importo modificabile
   - Campo gruppo di entrate modificabile
   - Pulsante Salva
   - Pulsante Annulla

4. **Popup Elimina Entrate/Spese**
   - Pulsante Salva
   - Pulsante Annulla

5. **Pagina di Elenco Gruppi Entrate/Spese**
   - Tabella
     - Id
     - Nome
     - Descrizione
     - Pulsanti Modifica ed Elimina
   - Pulsante checkbox per selezionare tutti i gruppi di entrate/spese disponibili
   - Pulsante icona Elimina per eliminare tutti i gruppi di entrate/spese disponibili
   - Frecce di ordinamento per ordinare tutti i gruppi di entrate/spese disponibili
   - Pulsante per aggiungere gruppi di entrate/spese attraverso un popup
   - Opzioni di paginazione per paginare gruppi di entrate/spese
   - Funzionalità "Righe per pagina" per mostrare un certo numero di gruppi di entrate/spese
   - Modale Elimina con opzioni di conferma e annulla
   - Nome del gruppo di entrate/spese cliccabile che va alla pagina dei dettagli

6. **Popup Modifica Gruppi Entrate/Spese**
   - Campo nome modificabile
   - Campo descrizione modificabile
   - Pulsante Salva
   - Pulsante Annulla

7. **Popup Elimina Gruppi Entrate/Spese**
   - Pulsante Salva
   - Pulsante Annulla

8. **Pagina di Dettagli Gruppi Entrate/Spese**
   - Nome
   - Descrizione
   - Ultimi 5 cambiamenti dell'account per quel gruppo
   - Breadcrumb per una navigazione semplice

9. **Promemoria (Funzione Premium nelle Impostazioni del Profilo)**
   - Pulsante per creare un promemoria attraverso un popup
   - Dettagli del promemoria attualmente impostato sulla dashboard
   - Breadcrumb per una navigazione semplice

10. **Popup Elimina Promemoria**
    - Pulsante Salva
    - Pulsante Annulla

11. **Blogs (Funzione Premium)**
    - Pulsante per creare un blog attraverso un popup
    - Card con tutti i blog disponibili
    - Breadcrumb per una navigazione semplice

12. **Popup Elimina Blog**
    - Pulsante Salva
    - Pulsante Annulla

13. **Pagina di Dettagli Blog**
    - Titolo
    - Descrizione
    - Autore
    - Data di creazione
    - Corpo
    - Breadcrumb per una navigazione semplice

## Installazione
Per la guida all'installazione, visita il nostro <a href="https://git.vegaitsourcing.rs/nikola.perisic/vega-internship-project/-/wikis/Project-setup-and-installation" target="_blank">Wiki</a>

## Integrazioni aggiuntive

### Email di stato della pipeline
**Pipeline Backend:**
Abilita i test nel progetto .NET.
<br/>
<br/>
**Pipeline Frontend:**
Compila il progetto.

**Notifiche via email:**

Invia email di stato della pipeline a una lista predefinita di destinatari, concentrando l'attenzione solo sulle pipeline fallite.

### Integrazione Mailchimp
Registra automaticamente gli utenti che si iscrivono alla newsletter nell'audience di Mailchimp collegata.

<hr/>

Tutti i diritti riservati.
<br/>
Copyright &copy; 2024 
