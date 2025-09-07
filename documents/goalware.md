#Everyware

Io mi chiamo Cristiano Luelli e sviluppo software per conto della societa' CLS.


##Una trilogia software

Sto lavorando ad un progetto Blazor WebAssembly Standalone che si chiama Someware, collegato ad un progetto ASP.NET Core Web API che si 
chiama Elseware. Entrambi fanno parte di una soluzione denominata Everyware.


##Note al manuale

Viene utilizzato Visual Studio 2022 in lingua inglese, pertanto i comandi e gli elementi di Visual Studio descritti saranno relativi alla 
versione in inglese.


##Linee guida per la scrittura del codice

Nell'ambito di questo progetto, sto implementando delle linee guida per lo sviluppo di applicazioni client-server.


###Header

Tutti i file sorgenti devono essere dotati di header (.cs, .css, .html, .razor).
L'header di tutti i file sorgente deve essere preceduto sempre da una riga vuota.
L'header deve essere inserito in un commento multiriga e strutturato come segue:

/*
SolutionName - Copyright © 2025 by CompanyName

********************************************************************************************************************************************
****
**** Project:           ProjectName
****
**** Module:            FileName.FileNameExtension
****
**** Version:           Year.Version.Subversion.Serial [es.: 2025.1.00.0001]
****
**** Created By:        Author [First Name & Last Name]
**** Created On:        CreationDate [dd/MM/yyyy]
****
**** Last Changed By:   LastModifier [First Name & Last Name]
**** Last Changed On:   LastChangeDate [dd/MM/yyyy]
****
********************************************************************************************************************************************

SolutionName - Copyright © 2025 by CompanyName
*/

Il tipo di commento dipende dal tipo di file.


###Commenti

I commenti devono essere in inglese.
I commenti devono essere in terza persona singolare.
I commenti devono essere in stile descrittivo attivo, chiaro e conciso.
Tutti i commenti devono essere chiusi da un punto.
Una riga di commento deve essere preceduta da una riga vuota, a meno che non sia preceduta da un'altra riga di commento


###Convenzioni di nomenclatura

Le costanti devono usare il formato ALL_CAPS.
Le variabili locali devono essere dotate di prefisso l_ ed usare il formato PascalCase.
I parametri dei metodi e dei costruttori (anche primari) devono essere dotate di prefisso p_ ed usare il formato PascalCase.
I campi privati non hanno alcun prefisso e devono usare il formato camelCase.
Per i campi privati, dove consentito, e' obbligatorio utilizzare this.


###HTML

Il tag <html> è considerato contenitore radice, il suo contenuto (<head>, <body>) deve essere allineato alla colonna zero.


###Javascript


####Struttura dei file

In tutte le dichiarazioni di file esterni mettere prima l'attributo rel e poi l'attributo href.
Le dichiarazioni di file esterni devono essere raggruppate in base all'attributo rel.
Non devono esserci righe vuote all'interno del gruppo di dichiarazioni di file esterni.
Ciascun gruppo di dichiarazioni di file esterni deve essere separato dagli altri da una riga vuota, seguita da un commento esplicativo.

Tutte le variabili locali in JavaScript e TypeScript devono essere dichiarate con let.
Per le costanti (valori immutabili) deve essere usata la stessa convenzione di nomenclatura delle variabili locali.


##Avvio della soluzione


###Avvio del frontend

Il progetto frontend richiede che sia specificato un parametro "project" come query string di avvio. 
La struttura completa sarebbe dell'indirizzo dovrebbe essere: protocollo://host:porta/percorso?parametro=valore.

Per impostare tale parametro il parametro di avvio per un progetto Blazor WASM standalone occorre modificare il file launchSettings.json 
aggiungendo una riga nel profilo di avvio:

{
  "profiles": {
    "https": {
      "commandName": "Project",
      "launchBrowser": true,
      "dotnetRunMessages": true,
      "inspectUri": "{wsProtocol}://{url.hostname}:{url.port}/_framework/debug/ws-proxy?browser={browserInspectUri}",
      "applicationUrl": "https://localhost:7002",
      **"launchUrl": "?project=goalware",**
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  },
  "$schema": "https://json.schemastore.org/launchsettings.json"
}

L'indirizzo completo che appare nella barra del browser avviando il programma e' https://localhost:7002/?project=Goalware. 

#### Caricamento del file di progetto

Una volta che e' stato acquisito il nome del progetto, occorre leggere il file di configurazione dello stesso per determinare il nome e
l'icona da visualizzare nella schermata di caricamento del programma. Il nome del progetto corrisponde ad una cartella sotto 
wwwwroot/projects, nella quale sono definite a sua volta tre sottocartelle: settings, images e styles. Nella sottocartella settings si
trova il file project.json che contiene i parametri di configurazione del progetto in fase di esecuzione. Tale file viene letto, tramite
uno script Javascript, nella pagina index,html durante la fase di avvio del sistema.