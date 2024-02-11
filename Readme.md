# Sequenzboard
Repository für Projekt Sequenzboard von Maik Steffan & Younes Abdelwadoud am Fachbereich 4 der HTW Berlin im Modul Emerging Technologies im Studiengang Informatik in Kultur und Gesundheit

# Bearbeitetes Problem: 
- Abnehmende Gedächtnisleistung und Lebensqualität von Personen mit Demenz
- Fehlende Verfügbarkeit von interaktiven Spielen zur Verbesserung der Gedächtnisleistung mit kognitiven und motorischen Komponenten

# Projektziele:
- Verbesserung des episodischen Gedächtnisses von Demenzpatienten
- Verlangsamung des Krankheitsverlaufs und Erhaltung der Lebensqualität
- Abminderung von potentieller Frustration durch Möglichkeit des sequentiellen Lernens
- Durch spielerisches Training Motivation zur kontinuierlichen Benutzung steigern => wiederholtes kognitives Training wirkt sich signifikant positiv auf Gedächtnisleistung von Menschen mit Demenz aus

# Lösungsansatz:
- Entwicklung eines interaktiven Spiel zur Verbesserung der Gedächtnisleistung
- Kognitiv-motorisches Training zur stärkeren Stimulation des Gehirns und entsprechender Minderung der Symptome bzw. Verlangsamung des Krankheitsverlaufs [2]
- Kernidee:
   1. Sequenzen werden selbst erstellt oder automatisch generiert
   2. Spieler merkt sich die Sequenz und kann sie bei Bedarf im Trainingsmodus üben
   3. Die Sequenz wird über das physische Board in der richtigen Reihenfolge wiedergegeben
- Sequenzen können zum erneuten Spielen gespeichert werden
- Über UI kann Krankenpfleger das Spiel steuern, um dem Spieler den Umgang mit Technik zu erleichtern

# Aufbau des Systems: 
- User Input wird über Board & UI entgegengenommen und durch Controller an zuständige Logik Klassen weitergeleitet
- In Logik wird Input validiert, verarbeitet und der Programm Output an Controller gesendet
- Controller gibt Output an jeweilige Komponenten in oberster Schicht weiter, die den Output an den User bringen
- Kommunikation zwischen Komponenten und Datenfluss über Events
- Controller als zentrale Instanz zur Koordinierung von Events


