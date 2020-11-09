using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace DenLilleShopGUI
{
    public class SqlUpdate
    {
        private SqlInteraction interaction = new SqlInteraction();
        private SqlConnection sql;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private Visibility seeMe = Visibility.Visible;
        private Visibility hideMe = Visibility.Hidden;
        public void UpdateKunde(TextBox firstName, TextBox lastName, TextBox phoneNumber, TextBox email, TextBox adresse, TextBox postCode, TextBox kId, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("UpdateKunde", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@newFNavn", SqlDbType.NVarChar).Value = firstName.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newENavn", SqlDbType.NVarChar).Value = lastName.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newPhoNumber", SqlDbType.Int).Value = int.Parse(phoneNumber.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@newMail", SqlDbType.NVarChar).Value = email.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newAdrss", SqlDbType.NVarChar).Value = adresse.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newpostCode", SqlDbType.Int).Value = int.Parse(postCode.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = int.Parse(kId.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        sql.Close();

                        succes.Content = "Updated chosen Kunde";
                        succes.Visibility = seeMe;
                        error.Visibility = hideMe;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                succes.Visibility = hideMe;
                error.Content = sqlEx.Message;
                error.Visibility = seeMe;
            }
            catch (Exception ex)
            {
                succes.Visibility = hideMe;
                error.Content += ex.Message;
                error.Visibility = seeMe;
            }
        }
        public void UpdateOrdre(TextBox kundeID, TextBox ordreID, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("UpdateOrdre", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@newKundeID", SqlDbType.Int).Value = int.Parse(kundeID.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@newOrdreID", SqlDbType.Int).Value = int.Parse(ordreID.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        sql.Close();

                        succes.Content = "Updated chosen Ordre";
                        succes.Visibility = seeMe;
                        error.Visibility = hideMe;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                succes.Visibility = hideMe;
                error.Content = sqlEx.Message;
                error.Visibility = seeMe;
            }
            catch (Exception ex)
            {
                succes.Visibility = hideMe;
                error.Content += ex.Message;
                error.Visibility = seeMe;
            }
        }
        public void UpdateVarer(TextBox titel, TextBox beskrivelse, TextBox pris, Label error, TextBox id, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("UpdateVarer", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@newTitel", SqlDbType.NVarChar).Value = titel.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newBeskrivelse", SqlDbType.NText).Value = beskrivelse.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newPris", SqlDbType.Real).Value = double.Parse(pris.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        sql.Close();

                        succes.Content = "Updated chosen Vare";
                        succes.Visibility = seeMe;
                        error.Visibility = hideMe;
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                succes.Visibility = hideMe;
                error.Content = sqlEx.Message;
                error.Visibility = seeMe;
            }
            catch (Exception ex)
            {
                succes.Visibility = hideMe;
                error.Content += ex.Message;
                error.Visibility = seeMe;
            }
        }
    }
}
