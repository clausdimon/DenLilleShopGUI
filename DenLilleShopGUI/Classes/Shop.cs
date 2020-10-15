using System;
using System.Collections.Generic;
using System.Text;

namespace DenLilleShopGUI.Classes
{
    class Shop
    {
        private string name;
        private List<Ordre> shopOrdre = new List<Ordre>();
        private List<Varer> shopVare = new List<Varer>();
        public string Name { get { return name; } set { name = value; } }
        public List<Ordre> ShopOrdre { get { return shopOrdre; } }
        public List<Varer> ShopVare { get { return shopVare; } }

        public Shop()
        {

        }
        public Shop(string nameS)
        {
            this.Name = nameS;
        }
        public void AddOrdre(Ordre ordre)
        {
            ShopOrdre.Add(ordre);
        }
        public void AddVare(Varer vare)
        {
            ShopVare.Add(vare);
        }
        ~Shop()
        {
            this.Name = string.Empty;
        }
    }
}
