* Klonirati repozitorij lokalno
* Pokrenuti command prompt kao administrator
* Pozicionirati se u .../CurrencyConverter/ClientApp
* Predlažem update-ati npm naredbom `npm install -g npm@latest` - u suprotnom instalacija (sljedeći korak) može značajno duže trajati 
* Pokrenuti instalaciju npm paketa naredbom `npm i`
* Potrebno je imati instaliran SQL Server i podesiti connection string u Startup.cs file-u tako da odgovara lokalnim postavkama
* U Visual Studiju otvoriti Package Manager Console i pokrenuti naredbu Update-Database kako bi se baza inicijalizirala
* Pokrenuti applikaciju
