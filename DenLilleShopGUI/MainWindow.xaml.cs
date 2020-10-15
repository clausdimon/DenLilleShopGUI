using DenLilleShopGUI.Classes;
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


namespace DenLilleShopGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Auto Incrementing tal til id for de forskellige, som kunde, ordre og vare.
        AutoIncrement autoKunde = new AutoIncrement();
        AutoIncrement autoOrdre = new AutoIncrement();
        AutoIncrement autoVare = new AutoIncrement();
        // lister for kunder, ordre og varer
        List<Kunder> shopKunder = new List<Kunder>();
        List<Ordre> shopOrdre = new List<Ordre>();
        List<Varer> shopVare = new List<Varer>();

        public MainWindow()
        {
            InitializeComponent();
            var con = ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("^[.][0-9]+$|^[0-9]*[.]{0,1}[0-9]*$");
            e.Handled = !regex.IsMatch((sender as TextBox).Text.Insert((sender as TextBox).SelectionStart, e.Text));
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            Kunder kunde = new Kunder(autoKunde.GenerateId(), txtForNavn.Text.ToString(), txtEfterNavn.Text.ToString(), int.Parse(txtTelefonNummer.Text.ToString()), txtEmail.Text.ToString());
            shopKunder.Add(kunde);
            txtEmail.Text = string.Empty;
            txtForNavn.Text = string.Empty;
            txtTelefonNummer.Text = string.Empty;
            txtEmail.Text = string.Empty;
        }

        private void butnSubmitOrdre_Click(object sender, RoutedEventArgs e)
        {
            Ordre ordre = new Ordre(autoOrdre.GenerateId(), int.Parse(txtKundeID.Text.ToString()));
            shopOrdre.Add(ordre);
            foreach(Kunder kunde in shopKunder)
            {
                if(kunde.Id == int.Parse(txtKundeID.ToString()))
                {
                    kunde.KundeOrdre.Add(ordre);
                }
            }
            txtKundeID.Text = string.Empty;

        }

        private void butnSubmitUkId_Click(object sender, RoutedEventArgs e)
        {
            Ordre ordre = new Ordre(autoOrdre.GenerateId());
        }

        private void btnSubmitVare_Click(object sender, RoutedEventArgs e)
        {
            Varer vare = new Varer(autoVare.GenerateId(), txtTitel.Text.ToString(), txtBeskriv.Text.ToString(), double.Parse(txtPris.Text.ToString()));
            shopVare.Add(vare);
            txtPris.Text = string.Empty;
            txtBeskriv.Text = string.Empty;
            txtTitel.Text = string.Empty;
        }
    }
}
