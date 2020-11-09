using CL_Den_Lille_Shop;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.Windows.Input;

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
        public SqlConnection CreateConnection(string connString)
        {
            SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings[connString].ConnectionString);
            conn = sqlConnection;
            return sqlConnection;
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
        
        public void UseViewWithSearch( int id, DataGrid dataGrid, Label error, string viewName, string searchName)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            using (conn)
            {
                conn.Open();
                using (sqlCommand = new SqlCommand("SELECT *FROM " + viewName + " WHERE " + searchName + " = " + id, conn))
                {
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                    sda.Fill(ds);
                    //dt.Load(sqlCommand.ExecuteReader());

                }
                conn.Close();

            }
            dataGrid.ItemsSource = ds.Tables[0].DefaultView;
        }
    }
}
