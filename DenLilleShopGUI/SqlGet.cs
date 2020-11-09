using CL_Den_Lille_Shop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DenLilleShopGUI
{
    public class SqlGet
    {
        private SqlInteraction interaction = new SqlInteraction();
        private SqlConnection sql;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private Visibility seeMe = Visibility.Visible;
        private Visibility hideMe = Visibility.Hidden;
        public Kunder GetKunde(TextBox kundeId, Label error, Label succes)
        {
            Kunder kunde = new Kunder();
            try
            {
                using (sql= interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("SelectKundeTilUpdate", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = int.Parse(kundeId.Text.ToString());
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                kunde.Id = int.Parse(kundeId.Text.ToString());
                                kunde.firstName = reader["FirstName"].ToString();
                                kunde.lastName = reader["LastName"].ToString();
                                kunde.tlfNummer = int.Parse(reader["PhoneNumber"].ToString());
                                kunde.email = reader["Email"].ToString();
                                kunde.adresse = reader["Adress"].ToString();
                                kunde.postcode = int.Parse(reader["PostCode"].ToString());
                            }
                        }
                        sql.Close();
                        succes.Content = "got data from database";
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
            return kunde;
        }
        public Ordre GetOrdre(TextBox ordreId, Label error, Label succes)
        {
            Ordre ordre = new Ordre();
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("SelectOrdreTilUpdate", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@OrdreID", SqlDbType.Int).Value = int.Parse(ordreId.Text.ToString());
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ordre.Id = int.Parse(ordreId.Text.ToString());
                                ordre.KundeId = int.Parse(reader["KundeID"].ToString());
                            }
                        }
                    }
                    sql.Close();
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
            return ordre;
        }
        public Varer GetVarer(TextBox vareID, Label error, Label succes)
        {
            Varer vare = new Varer();
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("SelectVareTilUpdate", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = int.Parse(vareID.Text.ToString());
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vare.Id = int.Parse(vareID.Text.ToString());
                                vare.Titel = reader["Titel"].ToString();
                                vare.Beskrivelse = reader["Beskrivelse"].ToString();
                                vare.Pris = double.Parse(reader["Pris"].ToString());

                            }
                        }
                    }
                    sql.Close();
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
            return vare;
        }
        public List<Kunder> getKunder(Label error, Label succes)
        {
            List<Kunder> kundeID = new List<Kunder>();
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("AllKunder", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Kunder k = new Kunder();
                                k.Id = int.Parse(reader["KundeID"].ToString());
                                k.email = reader["Email"].ToString();
                                k.firstName = reader["FirstName"].ToString();
                                k.lastName = reader["LastName"].ToString();

                                kundeID.Add(k);
                            }
                        }
                        sql.Close();
                        succes.Content = "got data from database";
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

            return kundeID;
        }

    }
}
