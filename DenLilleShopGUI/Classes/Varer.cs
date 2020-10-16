using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenLilleShopGUI.Classes
{
    class Varer : Interfaces.IInfo
    {
        public int Id { get; set; }
        private string titel;
        public string Titel { get { return titel; } set { titel = value; } }
        private string beskrivelse;
        public string Beskrivelse { get { return beskrivelse; } set { beskrivelse = value; } }
        private double pris;
        public double Pris { get { return pris; } set { pris = value; } }
        public Varer()
        { 

        }
        public Varer( string titelS, string beskrivelseS, double prisD)
        {
            this.Titel = titelS;
            this.Beskrivelse = beskrivelseS;
            this.Pris = prisD;
        }

        ~Varer()
        {
            this.Titel = string.Empty;
            this.Beskrivelse = string.Empty;
            this.Pris = 0;
            //Console.WriteLine("Deconstructor was Called");
        }
    }
}
