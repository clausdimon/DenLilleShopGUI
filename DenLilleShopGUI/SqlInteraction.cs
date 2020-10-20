using CL_Den_Lille_Shop;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace DenLilleShopGUI
{
    public class SqlInteraction
    {
        private SqlConnection conn;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        public void CreateConnection(string connString)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connString].ConnectionString);
            conn = sqlConnection;
        }
        public SqlInteraction()
        {
            CreateConnection("conString");
        }
        public SqlInteraction(string conn)
        {
            CreateConnection(conn);
        }
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

                        succes.Content = "Added new Kunde";
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

                        succes.Content = "Added new Kunde";
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
            dataGrid.DataSource = dt;
        }
    }
}
