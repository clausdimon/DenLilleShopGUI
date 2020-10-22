using CL_Den_Lille_Shop;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace DenLilleShopGUI
{
    public class SqlInteraction
    {
        private SqlConnection conn;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private Visibility seeMe = Visibility.Visible;
        private Visibility hideMe = Visibility.Hidden;
        /// <summary>
        /// used to create connection to the sql server, using app.config file to store the connection, so it will be easy to use when the method need to run
        /// </summary>
        /// <param name="connString">name of the connection added in app.config file</param>
        private void CreateConnection(string connString)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connString].ConnectionString);
            conn = sqlConnection;
        }
        /// <summary>
        /// using a defualt connection, based on a generic name "conString" in the app.config
        /// 
        /// calls the method CreateConnection to add the connection string to the object
        /// </summary>
        public SqlInteraction()
        {
            CreateConnection("conString");
        }
        /// <summary>
        /// calling the method CreateConnection to create the connectionstring, used to make the connection to the sql server.
        /// 
        ///
        /// </summary>
        /// <param name="conn">the name in app.config for the connectionstring that have been added</param>
        public SqlInteraction(string conn)
        {
            CreateConnection(conn);
        }
        /// <summary>
        /// a method to use a stored procedure called AddKunde, where it uses the stored procedure to add the param with the values connected to the right param.
        /// </summary>
        /// <param name="fN">the first name of the new Kunde that needed to be added</param>
        /// <param name="lN">the last name of the new Kunde that needed to be added</param>
        /// <param name="phN">the phonenumber of the new Kunde that needed to be added</param>
        /// <param name="eM">the email of the new Kunde that needed to be added</param>
        /// <param name="adr">the adress of the new Kunde that needed to be added</param>
        /// <param name="pC">the postcode of the new Kunde that needed to be added</param>
        /// <param name="error">a Label for the catch, to send the exceptions that have been caught</param>
        /// <param name="succes">a Label for the message to be given, that everything is succesfull</param>
        public void AddNewKunde(string fN, string lN, int phN, string eM, string adr, int pC, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddKunde", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@forNavn", SqlDbType.NVarChar).Value = fN;
                        sqlCommand.Parameters.AddWithValue("@efterNavn", SqlDbType.NVarChar).Value = lN;
                        sqlCommand.Parameters.AddWithValue("@phoNumber", SqlDbType.Int).Value = phN;
                        sqlCommand.Parameters.AddWithValue("@mail", SqlDbType.NVarChar).Value = eM;
                        sqlCommand.Parameters.AddWithValue("@adress", SqlDbType.NVarChar).Value = adr;
                        sqlCommand.Parameters.AddWithValue("@postCode", SqlDbType.Int).Value = pC;
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

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
        /// <summary>
        /// a method to use a stored procedure called AddKunde, where it uses the stored procedure to add the param with the values connected to the right param.
        /// in this the parans are the  pure textbox, so no need to give the different type of param to make the method to run correctly.
        /// </summary>
        /// <param name="firstName">a textbox with the first name of the new Kunde, for the sql stored procedure.</param>
        /// <param name="lastName">a textbox with the last name of the new Kunde, for the sql stored procedure.</param>
        /// <param name="phoneNumber">a textbox with the phonenumber of the new Kunde, for the sql stored procedure.</param>
        /// <param name="email">a textbox with the email of the new Kunde, for the sql stored procedure.</param>
        /// <param name="adresse">a textbox with the adress of the new Kunde, for the sql stored procedure.</param>
        /// <param name="postCode">a textbox with the postcode of the new Kunde, for the sql stored procedure.</param>
        /// <param name="error">a Label for the catch, to send the exceptions that have been caught</param>
        /// <param name="succes">a Label for the message to be given, that everything is succesfull</param>
        public void AddNewKunde(TextBox firstName, TextBox lastName, TextBox phoneNumber, TextBox email, TextBox adresse, TextBox postCode, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddKunde", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@forNavn", SqlDbType.NVarChar).Value = firstName.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@efterNavn", SqlDbType.NVarChar).Value = lastName.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@phoNumber", SqlDbType.Int).Value = int.Parse(phoneNumber.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@mail", SqlDbType.NVarChar).Value = email.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@adress", SqlDbType.NVarChar).Value = adresse.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@postCode", SqlDbType.Int).Value = int.Parse(postCode.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        error.Visibility = Visibility.Hidden;
                        succes.Content = "Added new Kunde";
                        succes.Visibility =Visibility.Visible;
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
        /// <summary>
        /// a method to use a stored procedure called AddOrdre.
        /// </summary>
        /// <param name="kundeID">the int value that is coming from the Kunnde Table in the database, telling what customer that get the new Ordre</param>
        /// <param name="error">a Label for the catch, to send the exceptions that have been caught</param>
        /// <param name="succes">a Label for the message to be given, that everything is succesfull</param>
        public void AddNewOrdre(int kundeID, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = kundeID;
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Added new Ordre";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void AddNewOrdre(TextBox kundeID, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = int.Parse(kundeID.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Added new Ordre";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void AddNewVare(string titel, string beskrivelse, double pris, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddVarer", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@titel", SqlDbType.NVarChar).Value = titel;
                        sqlCommand.Parameters.AddWithValue("@beskrivelse", SqlDbType.NText).Value = beskrivelse;
                        sqlCommand.Parameters.AddWithValue("@pris", SqlDbType.Real).Value = pris;
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Added new Vare";
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void AddNewVare(TextBox titel, TextBox beskrivelse, TextBox pris, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddVarer", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@titel", SqlDbType.NVarChar).Value = titel.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@beskrivelse", SqlDbType.NText).Value = beskrivelse.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@pris", SqlDbType.Real).Value = double.Parse(pris.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Added new Vare";
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public Kunder GetKunde(int kundeID, Label error)
        {
            Kunder kunde = new Kunder();
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("SelectKundeTilUpdate", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = kundeID;
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                kunde.Id = kundeID;
                                kunde.firstName = reader["FirstName"].ToString();
                                kunde.lastName = reader["LastName"].ToString();
                                kunde.tlfNummer = int.Parse(reader["PhoneNumber"].ToString());
                                kunde.email = reader["Email"].ToString();
                                kunde.adresse = reader["Adress"].ToString();
                                kunde.postcode = int.Parse(reader["PostCode"].ToString());
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += ex.Message;
            }
            return kunde;
        }
        public Kunder GetKunde(TextBox kundeId, Label error)
        {
            Kunder kunde = new Kunder();
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("SelectKundeTilUpdate", conn))
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
                        conn.Close();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += ex.Message;
            }
            return kunde;
        }
        public Ordre GetOrdre(int ordreID, Label error)
        {
            Ordre ordre = new Ordre();
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("SelectOrdreTilUpdate", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@OrdreID", SqlDbType.Int).Value = ordreID;
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ordre.Id = ordreID;
                                ordre.KundeId = int.Parse(reader["KundeID"].ToString());
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
            return ordre;

        }
        public Ordre GetOrdre(TextBox ordreId, Label error)
        {
            Ordre ordre = new Ordre();
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("SelectOrdreTilUpdate", conn))
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
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
            return ordre;
        }
        public Varer GetVarer(int vareID, Label error)
        {
            Varer vare = new Varer();
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("SelectVareTilUpdate", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = vareID;
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                vare.Id = vareID;
                                vare.Titel = reader["Titel"].ToString();
                                vare.Beskrivelse = reader["Beskrivelse"].ToString();
                                vare.Pris = double.Parse(reader["Pris"].ToString());

                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
            return vare;
        }
        public Varer GetVarer(TextBox vareID, Label error)
        {
            Varer vare = new Varer();
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("SelectVareTilUpdate", conn))
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
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
            return vare;
        }
        public void UpdateKunde(string fN, string lN, int phN, string eM, string adr, int pC, int kId, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("UpdateKunde", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@newFNavn", SqlDbType.NVarChar).Value = fN;
                        sqlCommand.Parameters.AddWithValue("@newENavn", SqlDbType.NVarChar).Value = lN;
                        sqlCommand.Parameters.AddWithValue("@newPhoNumber", SqlDbType.Int).Value = phN;
                        sqlCommand.Parameters.AddWithValue("@newMail", SqlDbType.NVarChar).Value = eM;
                        sqlCommand.Parameters.AddWithValue("@newAdrss", SqlDbType.NVarChar).Value = adr;
                        sqlCommand.Parameters.AddWithValue("@newpostCode", SqlDbType.Int).Value = pC;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = kId;
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Updated chosen Kunde";
                    }

                }

            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += ex.Message;
            }
        }
        public void UpdateKunde(TextBox firstName, TextBox lastName, TextBox phoneNumber, TextBox email, TextBox adresse, TextBox postCode, TextBox kId, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("UpdateKunde", conn))
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
                        conn.Close();

                        succes.Content = "Updated chosen Kunde";
                    }

                }

            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += ex.Message;
            }
        }
        public void UpdateOrdre(int kundeID, int ordreID, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("UpdateOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@newKundeID", SqlDbType.Int).Value = kundeID;
                        sqlCommand.Parameters.AddWithValue("@newOrdreID", SqlDbType.Int).Value = ordreID;
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Updated chosen Ordre";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void UpdateOrdre(TextBox kundeID, TextBox ordreID, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("UpdateOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@newKundeID", SqlDbType.Int).Value = int.Parse(kundeID.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@newOrdreID", SqlDbType.Int).Value = int.Parse(ordreID.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Updated chosen Ordre";
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void UpdateVarer(string titel, string beskrivelse, double pris, Label error, int id, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("UpdateVarer", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = id;
                        sqlCommand.Parameters.AddWithValue("@newTitel", SqlDbType.NVarChar).Value = titel;
                        sqlCommand.Parameters.AddWithValue("@newBeskrivelse", SqlDbType.NText).Value = beskrivelse;
                        sqlCommand.Parameters.AddWithValue("@newPris", SqlDbType.Real).Value = pris;
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Updated chosen Vare";
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void UpdateVarer(TextBox titel, TextBox beskrivelse, TextBox pris, Label error, TextBox id, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("UpdateVarer", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@newTitel", SqlDbType.NVarChar).Value = titel.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newBeskrivelse", SqlDbType.NText).Value = beskrivelse.Text.ToString();
                        sqlCommand.Parameters.AddWithValue("@newPris", SqlDbType.Real).Value = double.Parse(pris.Text.ToString());
                        sqlCommand.ExecuteNonQuery();
                        conn.Close();

                        succes.Content = "Updated chosen Vare";
                    }
                }

            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void DeleteKunde(int id, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("DeleteKunde", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = id;
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
                throw;
            }
        }
        public void DeleteKunde(TextBox id, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("DeleteKunde", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
                throw;
            }
        }
        public void DeleteOrdre(int id, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("DeleteOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ordreID", SqlDbType.Int).Value = id;
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
                throw;
            }
        }
        public void DeleteOrdre(TextBox id, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("DeleteOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ordreID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
                throw;
            }
        }
        public void DeleteVarer(int id, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("DeleteVarer", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = id;
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;

            }

        }
        public void DeleteVarer(TextBox id, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("DeleteVarer", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
                    }
                    conn.Close();
                }
            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;

            }
        }
        public void AddVareToOrdre(int oId, int vId, int kvantitet, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddVareToOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ordreID", SqlDbType.Int).Value = oId;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = vId;
                        sqlCommand.Parameters.AddWithValue("@kvantitet", SqlDbType.Int).Value = kvantitet;
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Added Vare to Ordre";
                    }
                    conn.Close();
                }


            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void AddVareToOrdre(TextBox oId, TextBox vId, TextBox kvantitet, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand("AddVareToOrdre", conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ordreID", SqlDbType.Int).Value = int.Parse(oId.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = int.Parse(vId.Text.ToString());
                        sqlCommand.Parameters.AddWithValue("@kvantitet", SqlDbType.Int).Value = int.Parse(kvantitet.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Added Vare to Ordre";
                    }
                    conn.Close();
                }


            }
            catch (SqlException sqlEx)
            {
                error.Content = sqlEx.Message;
            }
            catch (Exception ex)
            {
                error.Content += " " + ex.Message;
            }
        }
        public void UseViewWithSearch( int id, DataGrid dataGrid, Label error, string viewName, string searchName)
        {
            DataTable dt = new DataTable();

            using (conn)
            {
                conn.Open();
                using (sqlCommand = new SqlCommand("SELECT *FROM " + viewName + " WHERE " + searchName + " = " + id, conn))
                {
                    dt.Load(sqlCommand.ExecuteReader());

                }
                conn.Close();

            }
            dataGrid.ItemsSource = dt.DefaultView;
        }
        public void AddProcedures( List<string> parameters, List<string> values, List<string> type, string storedProcedureName, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand(storedProcedureName, conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            switch (type[i])
                            {
                                case "int":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.Int).Value = int.Parse(values[i]);
                                    break;
                                case "nvarchar":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.NVarChar).Value = values[i];
                                    break;
                                case "text":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.NText).Value = values[i];
                                    break;
                                case "real":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.Real).Value = double.Parse(values[i]);
                                    break;
                                case "datetime2":
                                    var format = "yyyy-MM-dd HH:mm:ss:fff";
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.DateTime2).Value = DateTime.ParseExact(values[i], format, CultureInfo.InvariantCulture);
                                    break;
                                default:
                                    break;
                            }
                        }

                        sqlCommand.ExecuteNonQuery();
                        error.Visibility = Visibility.Hidden;
                        succes.Content = "Succesfull adding to your table";
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
                throw;
            }
        }
        public void UpdateProcedures(List<TextBox> newDatas, List<string> dataTypes, List<string> parameters, string procedureName, int id, string idParam, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand(procedureName, conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        for (int i = 0; i < parameters.Count; i++)
                        {
                            switch (dataTypes[i])
                            {
                                case "int":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.Int).Value = int.Parse(newDatas[i].Text.ToString());
                                    break;
                                case "nvarchar":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.NVarChar).Value = newDatas[i].Text.ToString();
                                    break;
                                case "text":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.NText).Value = newDatas[i].Text.ToString();
                                    break;
                                case "real":
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.Real).Value = double.Parse(newDatas[i].Text.ToString());
                                    break;
                                case "datetime2":
                                    var format = "yyyy-MM-dd HH:mm:ss:fff";
                                    sqlCommand.Parameters.AddWithValue(parameters[i].ToString(), SqlDbType.DateTime2).Value = DateTime.ParseExact(newDatas[i].Text.ToString(), format, CultureInfo.InvariantCulture);
                                    break;
                                default:
                                    break;
                            }
                        }
                        sqlCommand.Parameters.AddWithValue(idParam, SqlDbType.Int).Value = id;
                        sqlCommand.ExecuteNonQuery();
                        error.Visibility = Visibility.Hidden;
                        succes.Content = "Succesfull updating your table";
                        succes.Visibility = Visibility.Visible;

                    }
                    conn.Close();

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
                throw;
            }
        }
        public void DeleteProcedures(List<Parameters> parameters, string procedureName, Label error, Label succes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand(procedureName, conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        foreach (Parameters item in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(item.Parameter, item.Type).Value = item.Value;
                        }
                        sqlCommand.ExecuteNonQuery();
                        error.Visibility = hideMe;
                        succes.Content = "Succesful Deleted data from your table";
                        succes.Visibility = seeMe;
                    }
                }
            }
            catch( SqlException sqlEx)
            {
                succes.Visibility = hideMe;
                error.Content = sqlEx.Message;
                error.Visibility = seeMe;
            }
            catch (Exception ex)
            {
                succes.Visibility = hideMe;
                error.Content += " "+ ex.Message;
                error.Visibility = seeMe;
                throw;
            }
        }
        public void SelectProceduresKunde(List<Parameters> parameters, string procedureName, Label error, Label succes, List<TextBox> textBoxes)
        {
            try
            {
                using (conn)
                {
                    conn.Open();
                    using (sqlCommand = new SqlCommand(procedureName, conn))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        foreach (Parameters item in parameters)
                        {
                            sqlCommand.Parameters.AddWithValue(item.Parameter, item.Type).Value = item.Value;
                        }
                        using (reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                foreach (TextBox item in textBoxes)
                                {

                                }
                        }
                        error.Visibility = hideMe;
                        succes.Content = "Succesful got data from your table";
                        succes.Visibility = seeMe;
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
                error.Content += " " + ex.Message;
                error.Visibility = seeMe;
                throw;
            }
        }
    }
}
