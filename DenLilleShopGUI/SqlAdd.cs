using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DenLilleShopGUI
{
    public class SqlAdd
    {
        private SqlInteraction interaction = new SqlInteraction();
        private SqlConnection sql;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private Visibility seeMe = Visibility.Visible;
        private Visibility hideMe = Visibility.Hidden;
        public void AddNewKunde(TextBox firstName, TextBox lastName, TextBox phoneNumber, TextBox email, TextBox adresse, TextBox postCode, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("AddKunde", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@forNavn", SqlDbType.NVarChar).Value = firstName.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@efterNavn", SqlDbType.NVarChar).Value = lastName.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@phoNumber", SqlDbType.Int).Value = int.Parse(phoneNumber.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@mail", SqlDbType.NVarChar).Value = email.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@adress", SqlDbType.NVarChar).Value = adresse.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@postCode", SqlDbType.Int).Value = int.Parse(postCode.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        sql.Close();

                        error.Visibility = Visibility.Hidden;
                        succes.Content = "Added new Kunde";
                        succes.Visibility = Visibility.Visible;
                    }

                }

            }
            catch (SqlException sqlEx)
            {
                succes.Visibility = Visibility.Hidden;
                error.Content = sqlEx.Message;
                error.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                succes.Visibility = Visibility.Hidden;
                error.Content += ex.Message;
                error.Visibility = Visibility.Visible;
            }
        }
        public void AddNewOrdre(TextBox kundeID, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("AddOrdre", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = int.Parse(kundeID.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        sql.Close();

                        succes.Content = "Added new Ordre";
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
        public void AddNewVare(TextBox titel, TextBox beskrivelse, TextBox pris, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("AddVarer", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@titel", SqlDbType.NVarChar).Value = titel.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@beskrivelse", SqlDbType.NText).Value = beskrivelse.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@pris", SqlDbType.Real).Value = double.Parse(pris.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        sql.Close();

                        succes.Content = "Added new Vare";
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
        public void AddVareToOrdre(TextBox oId, TextBox vId, TextBox kvantitet, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("AddVareToOrdre", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ordreID", SqlDbType.Int).Value = int.Parse(oId.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = int.Parse(vId.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@kvantitet", SqlDbType.Int).Value = int.Parse(kvantitet.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Added Vare to Ordre";
                    }
                    sql.Close();
                    succes.Visibility = seeMe;
                    error.Visibility = hideMe;
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
