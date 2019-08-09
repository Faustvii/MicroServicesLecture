Lav et simpelt API i ASP.NET  Core (2.2) Web.API, der kan håndtere en simpel indkøbskurv.

Start med at bruge dotnet new web til at lave grundprojektet.

Følgende features skal understøttes:

    Lægge en vare i kurven

    Fjerne en vare

    Få vist de vare der er i kurven.

Varerne, der kommer i kurven, skal ikke være taget fra et produkt katalog, men kan bare være fritekst. En vare bør indeholde et Id og en titel.

Kurven skal ikke gemmes i en database, men bare gennem in-memory.

Bonus point: Håndter flere brugere med hver deres indkøbskurv. Brugere er bare et ID.