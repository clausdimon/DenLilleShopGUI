using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace DenLilleShopGUI
{
    public class SqlDelete
    {
        private SqlInteraction interaction = new SqlInteraction();
        private SqlConnection sql;
        private SqlCommand sqlCommand;
        private SqlDataReader reader;
        private Visibility seeMe = Visibility.Visible;
        private Visibility hideMe = Visibility.Hidden;
        public void DeleteKunde(TextBox id, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("DeleteKunde", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@kundeID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
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
        public void DeleteOrdre(TextBox id, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("DeleteOrdre", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@ordreID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
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
        public void DeleteVarer(TextBox id, Label error, Label succes)
        {
            try
            {
                using (sql = interaction.CreateConnection("conString"))
                {
                    sql.Open();
                    using (sqlCommand = new SqlCommand("DeleteVarer", sql))
                    {
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                        sqlCommand.Parameters.AddWithValue("@vareID", SqlDbType.Int).Value = int.Parse(id.Text.ToString());
                        sqlCommand.ExecuteNonQuery();

                        succes.Content = "Delete was Succesful";
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
