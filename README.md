# KlasGarage
Garage 1.0 En första ihopbindning

## Torsdag 27-10-16
Rapport: Följande åtgärder har färdigställts fram till lunch:
* **Skapa fordon.** Skapa fordon använder sig nu av samma gränssnitt som SökPåOlikaVariabler och har standardvärden om man inte orkar fylla i allt. Man kan även byta mellan fordonstyperna utan att det går att skriva fel.
* **Sök på olika variabler.** SökPåOlikaVariabler FUNGERAR nu, vilket det inte gjorde tidigare. Det var ett problem med kommandot Sök som egentligen skulle starta sökningen men råkade räknas som ett sökargument som inget fordon kunde uppnå. Sökningarna blev därför alltid tomma.
* **Engelska.** Engelska har valts som standardspråk i applikationen. Allt som skrivs nu skrivs på engelska.

Följande åtgärder kvarstår:
* **Kommentering.** Fortfarande inga kommentarer för att förklara koden.
* **SvEngelska.** Fortfarande blandat svenska och engelska. Allt ska vara på Engelska.

## Onsdag 26-10-16
Rapport: Samtliga funktioner finns nu tillgängliga i programmet men några viktiga punkter är fortfarande inte färdiga:
* **Kommenterin.** All kod saknar kommentering. XML-comments vore lämpliga till att börja med.
* **SvEngelska.** Både Svenska och Engelska används i koden, även gentemot användaren i menyer osv.
* **Typsäkring i Sök-funktion.** För närvarande kan man skriva in ett tal som inte går att parsa i ett sökfält för int eller double utan att felet hanteras någonstans. Detta leder till krash.
* **Onödigt användarkrav.** Vid ångrad borttagning tvingas användaren trycka enter två gånger för att återgå till huvudmenyn
