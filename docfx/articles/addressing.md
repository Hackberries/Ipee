# IPv4-Netzwerke & IPv4-Adressen

## Grundsätzliches

Für die Berechnung, Verwaltung und Validierung von IPv4-Netzwerken und deren IPv4-Adressen ist der [Ipee.Core.Addressing](~/api/Ipee.Core.Addressing.yml) Namespace zuständig. Dabei sind vor allem die beiden Klassen [IPv4Value](~/api/Ipee.Core.Addressing.IPv4Value.yml) und [IPv4Network](~/api/Ipee.Core.Addressing.IPv4Network.yml) vorhanden. Über denen wird dementsprechend der hauptsächliche Use-Case abgebildet.

### IPv4Value

Um nicht anhand von Strings alles an Berechnungen durchführen zu müssen, werden die Werte in dieser Klasse mittels eines Byte-Array aus 4 Elementen abgebildet. Dabei soll [IPv4Value](~/api/Ipee.Core.Addressing.IPv4Value.yml) die Basis einer IP-Adresse, als auch einer Subnetzmaske sein, da beides aus jeweils 4 Oktetten besteht und untereinander mit den selben Methoden verrechnet werden kann.

### IPv4Network

Dieses Objekt soll ein komplettes Netzwerk von zugeordneten IP-Adressen, dessen Netz-, Broadcast- und Host-Adresse, sowie allen Subnetzen darstellen. Dabei sind die Subnetzen nichts anderes als andere [IPv4Network](~/api/Ipee.Core.Addressing.IPv4Network.yml) Objekte, welche lediglich einem anderen untergeordnet wurden. So können [IPv4Network](~/api/Ipee.Core.Addressing.IPv4Network.yml) Objekte untereinander eine komplexe Baumstruktur erzeugen.

## Berechnungen zwischen IP-Adressen

Die Berechnungen zwischen IP-Adressen finden gänzlich über der [IPv4Value](~/api/Ipee.Core.Addressing.IPv4Value.yml) Klasse statt. Hierfür wurden folgende Operatoren zur Verfügung gestellt:

- #### BitwiseAnd ( & )

- #### BitwiseOr ( | )

- #### Equality ( == )

- #### Inequality ( != )

- #### GreaterThan ( > )

- #### GreaterThanOrEqual ( >= )

- #### LessThan ( < )

- #### LessThanOrEqual ( <= )

Da [IPv4Value](~/api/Ipee.Core.Addressing.IPv4Value.yml) seinen Wert, also die IP-Adresse (z.B. 192.168.0.5), in einem Byte-Array aus 4 Elementen speichert, wären direkte Operationen zwischen zwei Adressen mit höherem Aufwand verbunden. Um die oben genannten Operationen zu erleichtern, **werden die Bytes aus von dem Objekt immer zuerst in einen Integer konvertiert**, da dieser Datentyp ebenfalls aus 4 Bytes besteht und sich perfekt dafür eignen, miteinander verglichen zu werden.

Wenn wir also beispielsweise wissen wollen, ob "253.21.161.14" größer ist als "253.21.162.16", geht es folgendermaßen vor:

```
"253.21.161.14".toInt() = -48914162 //left
"253.21.162.16".ToInt()	= -48913904 //right

-48914162 > -48913904 = true
```

Ähnlich verläuft es dementsprechend auch bei AND oder OR Operationen. Auch hier werden die Bytes zuerst in Integer konvertiert, zwischen denen wird dann operiert und das Resultat wird anschließend wieder in ein Byte-Array zurück konvertiert, welches einem neuen [IPv4Value](~/api/Ipee.Core.Addressing.IPv4Value.yml) zugeordnet wird.

## Berechnung von Netz-, Broadcast- und Host-Adresse

Neben [IPv4Value](~/api/Ipee.Core.Addressing.IPv4Value.yml) verläuft auch einiges an notwendiger Berechnungslogik in der [IPv4Network](~/api/Ipee.Core.Addressing.IPv4Network.yml) ab. Hier kommt es zur Berechnung der Netz-, Broadcast- und Host-Adresse. Um ein [IPv4Network](~/api/Ipee.Core.Addressing.IPv4Network.yml) zu erzeugen, wird immer eine Ursprungs-Adresse und eine Subnetzmaske vorausgesetzt, um auf dessen Basis sämtliche Berechnungen vollziehen zu können.

### Netz-Adresse

Die Netzadresse wird mittels BitwiseAnd-Operation zwischen der Ursprungs-Adresse und der Subnetzmaske ermittelt.

```c#
NetAddress = SourceAddress & SubnetMask
```

### Broadcast-Adresse

Die Broadcast-Adresse ist das Resultat einer BitwiseOr-Operation zwischen der Ursprungs-Adresse und der **invertierten** Subnetzmaske.

Beispiel:

```
BroadcastAddress = SourceAddress | ~SubnetMask

// Daten
Ursprungs-Adresse	= 192.168.10.5	= 1100 0000   1010 1000   0000 1010   0000 0101
Subnetzmaske		= 255.255.252.0	= 1111 1111   1111 1111   1111 1100   0000 0000

// 1. Subnetzmaske invertieren
1111 1111   1111 1111   1111 1100   0000 0000
wird zu
0000 0000   0000 0000   0000 0011   1111 1111

// BitwiseOr-Operation
0000 0000   0000 0000   0000 0011   1111 1111	| Subnetzmaske invertiert
1100 0000   1010 1000   0000 1010   0000 0101	| Ursprungs-Adresse
---------------------------------------------
1100 0000   1010 1000   0000 1011   1111 1111
== 192.168.11.255

```

## Berechnung aller freien Adressen im Netzwerk

Ein [IPv4Network](~/api/Ipee.Core.Addressing.IPv4Network.yml) liefert automatisch immer eine Iteration aller in ihm noch zu Verfügung stehenden IP-Adressen mit. Dafür werden **alle möglichen Adressen zwischen der Host-Adresse und der Broadcast-Adresse** zurückgegeben, mit Ausnahme aller Adressen die bereits in dem Netzwerk, oder einem untergeordnetem Netzwerk vergeben wurden.
