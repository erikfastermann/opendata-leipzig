using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace data_team
{
    class Strasse
    {
        public string Name { get; }
        public string Ortsteil { get; }
        public string Erlaeuterung { get; }
        public Strasse(string name, string ortsteil, string erlaeuterung)
        {
            Name = name;
            Ortsteil = ortsteil;
            Erlaeuterung = erlaeuterung;
        }

        public override string ToString()
        {
            return String.Format("{0} ({1}, {2})", Name, Ortsteil, Erlaeuterung);
        }
    }
    class Strassen
    {
        List<Strasse> strassen;
        HashSet<string> namen;
        public Strassen(string path)
        {
            var doc = XDocument.Load(path);
            strassen = new List<Strasse>();
            namen = new HashSet<string>();
            foreach (var strasse in doc.Descendants("STRASSE"))
            {
                var name = (string)strasse.Element("STAMMDATEN").Element("NAME");
                var ortsteile = (string)strasse.Element("LAGE").Element("ORTSTEILE");
                var erlaeuterung = (string)strasse.Element("ERKLAERUNG").Element("ERLAEUTERUNG");
                strassen.Add(new Strasse(name, ortsteile, erlaeuterung));
                namen.Add(name);
            }
        }
        public bool Contains(string name)
        {
            return namen.Contains(name);
        }
    }
    class UI
    {
        public static void loop(Strassen strassen)
        {
            while(true)
            {
                Console.Write("> ");
                string input = Console.ReadLine();
                if (strassen.Contains(input)) {
                    Console.WriteLine("{0} wurde gefunden", input);
                } else {
                    Console.WriteLine("{0} wurde nicht gefunden", input);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Strassen strassen = new Strassen("Strassenverzeichnis.xml");
            UI.loop(strassen);
        }
    }
}
