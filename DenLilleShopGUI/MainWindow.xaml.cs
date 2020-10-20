using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using CL_Den_Lille_Shop;

namespace DenLilleShopGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        // Auto Incrementing tal til id for de forskellige, som kunde, ordre og vare.
        /*AutoIncrement autoKunde = new AutoIncrement();
        AutoIncrement autoOrdre = new AutoIncrement();
        AutoIncrement autoVare = new AutoIncrement();
        */
        // lister for kunder, ordre og varer
        List<Kunder> shopKunder = new List<Kunder>();
        List<Ordre> shopOrdre = new List<Ordre>();
        List<Varer> shopVare = new List<Varer>();
        SqlConnection conn = null;
        SqlDataReader dataReader = null;
        SqlCommand sqlCommand = null;
        SqlInteraction interaction = new SqlInteraction();
        SqlInteraction sqlInteraction = new SqlInteraction("conString2");
        /// <summary>
        /// starter vinduet op, når programmet begynder
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            //var con = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;

        }
        /// <summary>
        /// bruges til at tjekke en textbox om der kun er tal fra [0-9] i teksten.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        /// <summary>
        /// bruges til at tjekke en textbox om der er tal[0-9] og decimaltegn [.] i teksten.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }
        /// <summary>
        /// Laver en kunde fra vinduet, og hvor den derefter vil sørge for at felterne bliver tømt,
        /// så de er klar til næste indtastning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            LblErrorOpretKunde.Content = string.Empty;
            interaction.AddNewKunde(txtForNavn.Text, txtEfterNavn.Text, int.Parse(txtTelefonNummer.Text), txtEmail.Text, txtAdress.Text, int.Parse(txtPostCode.Text), LblErrorOpretKunde);
            txtEmail.Text = string.Empty;
            txtForNavn.Text = string.Empty;
            txtTelefonNummer.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtAdress.Text = string.Empty;
            txtPostCode.Text = string.Empty;

            /*
            try
            {
                Kunder kunde = new Kunder(txtForNavn.Text.ToString(), txtEfterNavn.Text.ToString(), int.Parse(txtTelefonNummer.Text.ToString()), txtEmail.Text.ToString(), "Get Adress", 5000);
                shopKunder.Add(kunde);


            }
            catch (Exception ex)
            {

                LblErrorOpretKunde.Content = ex.Message;
            }
            */
        }
        /// <summary>
        /// laver en ordre fra vinduet, hvor den sætter kunde id og id på ordren, 
        /// og sætter det ind på kundens ordre liste i kunde listen, hvorefter textboxen vil blive
        /// tømt og gjort klar til den nye indtastning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butnSubmitOrdre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ordre ordre = new Ordre(int.Parse(txtKundeID.Text.ToString()));
                shopOrdre.Add(ordre);

            }
            catch (Exception ex)
            {

                LblErrorOpretOrdre.Content = ex.Message;
            }
            

        }
        /// <summary>
        /// laver en ordre der ikke bliver knyttet til en kunde, så ordren for bare et id, 
        /// som man så senere kan bruge til at sætte opdatere hvad ordren har af vare i sig.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void butnSubmitUkId_Click(object sender, RoutedEventArgs e)
        {
            Ordre ordre = new Ordre();
        }
        /// <summary>
        /// Laver en vare fra vinduet, som bliver givet til en liste over mulige vare man kan købe/sætte på ordre.
        /// efter ordren er lavet, vil textbox'ene med informationerne blive tømt og gjort klar til næste vare der skal indsættes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmitVare_Click(object sender, RoutedEventArgs e)
        {
            Varer vare = new Varer( txtTitel.Text.ToString(), txtBeskriv.Text.ToString(), double.Parse(txtPris.Text.ToString()));
            shopVare.Add(vare);
            txtPris.Text = string.Empty;
            txtBeskriv.Text = string.Empty;
            txtTitel.Text = string.Empty;
        }
    }
}
