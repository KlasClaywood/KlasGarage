# KlasGarage
Garage 1.0 En första ihopbindning

# Viktiga lärdomar
* **Deferred Execution** Koden väntar med exekveringen av en Query tills den tvingas till det av en `.ToArray()` eller liknande. Så om exekveringen hamnar efter någon ändring av det som queryn går på så blir det på den nya datan som Queryn körs.
* **XML-filer** Garage använder sig av `XDocument`-, `XElement`-, och `XAttribute`-objekt för att spara och läsa in data mellan körningar.
* **Planera din kod** Ännu en gång blir det väldigt tydligt vad viktigt det är att planera sin kod. Grundläggande strukturer blir väldigt mödosamma att ändra på sent in i projektet, så var säker på strukturen innan den implementeras. I detta fall var det `GarageUI` och `UIItem` klasserna som fick smaka på detta. Alla fordon ser praktiskt taget identiska ut från den första committen, men `GarageUI` växte sig större och större tills den plötsligt skötte ALLA operationer. Att byta UI vore likvärdigt med att skriva om halva projektet. `UIItem` representerar meny-alternativ och behövde flera gånger få nya egenskaper som nya funktioner krävde, för att inte behöva upprepa meny-kod.
* **Jag kan inte MVC** Jag kan inte MVC men jag ser mycket fram emot att kunna det. För närvarande vore det väldigt svårt att plocka isär View från Controller och jag kan för mitt liv inte komma på hur jag ska undvika det problemet i framtiden. Jag måste typ hårdlobotomera ut alla kontroller från gränssnittet.

## Torsdag 27-10-16
Rapport: Följande åtgärder har färdigställts:
* **Skapa fordon.** Skapa fordon använder sig nu av samma gränssnitt som `SökPåOlikaVariabler` och har standardvärden om man inte orkar fylla i allt. Man kan även byta mellan fordonstyperna utan att det går att skriva fel.
* **Sök på olika variabler.** `SökPåOlikaVariabler` FUNGERAR nu, vilket det inte gjorde tidigare. Det var ett problem med kommandot Sök som egentligen skulle starta sökningen men råkade räknas som ett sökargument som inget fordon kunde uppnå. Sökningarna blev därför alltid tomma.
* **Engelska.** Engelska har valts som standardspråk i applikationen. Allt som skrivs nu skrivs på engelska. All text användaren har tillgång till är också översatt till engelska.
* **Lista Typer av Fordon.** Programmet ska kunna lista samtliga olika typer av fordon i garaget och hur många av dessa som står i garaget.

Följande åtgärder kvarstår:

* **Kommentering.** Fortfarande inga kommentarer för att förklara koden.


## Onsdag 26-10-16
Rapport: Samtliga funktioner finns nu tillgängliga i programmet men några viktiga punkter är fortfarande inte färdiga:
* **Kommenterin.** All kod saknar kommentering. XML-comments vore lämpliga till att börja med.
* **SvEngelska.** Både Svenska och Engelska används i koden, även gentemot användaren i menyer osv.
* **Typsäkring i Sök-funktion.** För närvarande kan man skriva in ett tal som inte går att parsa i ett sökfält för int eller double utan att felet hanteras någonstans. Detta leder till krash.
* **Onödigt användarkrav.** Vid ångrad borttagning tvingas användaren trycka enter två gånger för att återgå till huvudmenyn
