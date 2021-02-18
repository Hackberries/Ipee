# Ipee - IP Enterprise Editor

Ipee soll als Tool zur grafischen Verwaltung von IP-Netzwerken dienen und dem Admin dabei möglichst einfach helfen, neue Subnetze anzulegen, vorhandene zu verwaltet und dabei sicher sein zu können, dass stets gültige Subnetze eingerichtet oder Netzwerk-Adressen hinzugefügt werden.

## How to run:

1. Lade den Source-Code von Github herunter: https://github.com/Hackberries/Ipee.git
2. Stelle sicher, dass du das [.NET 5.0 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) installiert hast.
3. Führe folgenden Befehl in der Kommandozeile aus: `dotnet build`
4. Zu finden ist die ausführbare Datei dann unter: `Ipee\bin\Debug\net5.0-windows`

Alternativ dazu kannst du natürlich auch darauf hoffen, dass eine bereits kompilierte Version im Repo verlinkt wurde.

## Projektstruktur

**Ipee**

- Ipee
  - Oberfläche
- Ipee.Core
  - Eigentliche Logik
- Ipee.Test
  - Unit tests
