using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenLilleShopGUI.Classes
{
    class Kunder : Interfaces.IInfo
    {
        public int Id { get; set; }
        private string forNavn;
        private string efterNavn;
        public string ForNavn { get { return forNavn; } set { forNavn = value; } }
        public string EfterNavn { get { return efterNavn; } set { efterNavn = value; } }
        private string name;
        public string Name { get { return name; } set { name = ForNavn + " " + EfterNavn; } }
        private int tlfNummer;
        public int TlfNummer { get { return tlfNummer; } set { tlfNummer = value; } }
        private string email;
        public string Email { get { return email; } set { email = value; } }
        private List<Ordre> kundeOrdre = new List<Ordre>();
        public List<Ordre> KundeOrdre { get { return kundeOrdre; } }
        public Kunder()
        {

        }
        public Kunder(int id, string firstName, string surName, int teleNr, string eMail)
        {
            this.Id = id;
            this.ForNavn = firstName;
            this.EfterNavn = surName;
            this.TlfNummer = teleNr;
            this.Email = eMail;
        }
        public void AddKundeOrdre(Ordre ordre)
        {
            KundeOrdre.Add(ordre);
        }

        ~Kunder()
        {
            this.Id = 0;
            this.ForNavn = string.Empty;
            this.EfterNavn = string.Empty;
            this.TlfNummer = 0;
            this.Email = string.Empty;
            //Console.WriteLine("Deconstructor was Called");
        }
    }
}
