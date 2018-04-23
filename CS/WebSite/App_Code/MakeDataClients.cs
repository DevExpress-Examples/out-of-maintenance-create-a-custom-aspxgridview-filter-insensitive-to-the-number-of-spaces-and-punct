using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;//OleDbCommand
using System.IO; //Path
using System.Data;//CommandType

/// <summary>
/// Functionality on the testdata in App_Data/data.mdb.
/// This needs to run only once, as the new data is stored
/// and available for the next run.
/// Data could also be added directly using MS Access.
/// </summary>
public static class MakeDataClients
{
    private static string ConnectionString()
    {
        string AppDataPath = HttpContext.Current.Server.MapPath("App_Data");
        string mdbPath = Path.Combine(AppDataPath, "data.mdb");
        string myConnString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", mdbPath);
        return myConnString;
    }

    /// <summary>
    /// Returns the number of records in the table
    /// </summary>
    public static int RecordCount()
    {
        int Count = 0;
        using (OleDbConnection myConnection = new OleDbConnection(ConnectionString()))
        {
            string mySelectQuery = "Select Count(*) from Clients";
            using (OleDbCommand myCommand = new OleDbCommand(mySelectQuery, myConnection))
            {
                myConnection.Open();
                try
                {
                    var myReader = myCommand.ExecuteReader();
                    if (myReader.Read())
                    {
                        object[] meta = new object[10];
                        int NumberOfColums = myReader.GetValues(meta);
                        Count = (int)meta[0];
                    }
                }
                catch (Exception e)
                {
                    // e.g.: table does not exist
                    //string ie_msg = e.InnerException.Message; //debug breakpoint
                }
                myConnection.Close();
            }
        }
        return Count;
    }

    public static void CreateTable()
    {
        using (OleDbConnection myConnection = new OleDbConnection(ConnectionString()))
        {
            OleDbCommand InsertQuery = new OleDbCommand();
            InsertQuery.CommandType = CommandType.Text;
            InsertQuery.CommandText = "create table [Clients] ([ClientID] number, [Name] char(30), [Address] char(30), [Zip] char(10), [City] char(30))";
            InsertQuery.Connection = myConnection;
            myConnection.Open();
            InsertQuery.ExecuteNonQuery();
        }
        return;
    }

    /// <summary>
    /// Add one record to the database table
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Numbers"></param>
    public static void RecordAdd(int ClientID, string Name, string Address, string Zip, string City)
    {
        using (OleDbConnection myConnection = new OleDbConnection(ConnectionString()))
        {
            OleDbCommand InsertQuery = new OleDbCommand();
            InsertQuery.CommandType = CommandType.Text;
            InsertQuery.CommandText = "insert into Clients ([ClientID], [Name], [Address], [Zip], [City]) values (?,?,?,?,?)";
            InsertQuery.Parameters.AddWithValue("@ClientID", ClientID);
            InsertQuery.Parameters.AddWithValue("@Name", Name);
            InsertQuery.Parameters.AddWithValue("@Address", Address);
            InsertQuery.Parameters.AddWithValue("@Zip", Zip);
            InsertQuery.Parameters.AddWithValue("@City", City);
            InsertQuery.Connection = myConnection;
            myConnection.Open();
            InsertQuery.ExecuteNonQuery();
        }
        return;
    }

    /// <summary>
    /// Add all records that are needed.
    /// Do not add data if the extra data is already in the table.
    /// Adding data twice is not required, 
    /// and impossible because of primary key constraint on the first column.
    /// </summary>
    public static void AddRecords()
    {
        int rc = RecordCount();
        if (rc < 1)
        {
            CreateTable();
            RecordAdd(0, "Holland-Transport BV", "Gooische weg 23", "1234AB", "s-gRavenhage");
            RecordAdd(1, "Gelderland Transport b.v.", "Mooistraat 25", "1234AB", "sGravenhage");
            RecordAdd(2, "De Ameland&Tram", "G.o.o.i.s.c.h.e weg 27", "1234AB", "'s Gravenhage");
            RecordAdd(3, "Landtraject", "Gooi-sche weg 29", "1234AB", "Den Haag");
            RecordAdd(4, "Land-Traan", "Gooischeweg 31", "1234AB", "DenHaag");
        }
        rc = RecordCount();//debug
        return;
    }
}//end class
