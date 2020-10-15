using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DenLilleShopGUI.Classes
{
    class Ordre : Interfaces.IInfo
    {
        public int Id { get; set; }
        private List<Varer> ordreListe = new List<Varer>();
        public List<Varer> OrdreListe { get { return ordreListe; } }
        private int kundeId;
        public int KundeId { get { return kundeId; } set { kundeId = value; } }
        public Ordre()
        {

        }
        public Ordre(int id)
        {
            this.Id = id;
        }
        public Ordre(int id, int kId)
        {
            this.Id = id;
            this.KundeId = kId;
        }
        public void AddVarer(Varer vare)
        {
            OrdreListe.Add(vare);
        }
        ~Ordre()
        {
            this.Id = 0;
            this.KundeId = 0;
            //Console.WriteLine("Deconstructor was Called");
        }
    }
}
