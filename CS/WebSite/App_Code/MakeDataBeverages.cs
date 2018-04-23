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
public static class MakeDataBeverages
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
            string mySelectQuery = "Select Count(*) from Categories";
            using (OleDbCommand myCommand = new OleDbCommand(mySelectQuery, myConnection))
            {
                myConnection.Open();
                var myReader = myCommand.ExecuteReader();
                if (myReader.Read())
                {
                    object[] meta = new object[10];
                    int NumberOfColums = myReader.GetValues(meta);
                    Count = (int)meta[0];
                }
                myConnection.Close();
            }
        }
        return Count;
    }

    /// <summary>
    /// Add one record to the database table
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Name"></param>
    /// <param name="Numbers"></param>
    public static void RecordAdd(int Id, string Name, string Numbers)
    {
        using (OleDbConnection myConnection = new OleDbConnection(ConnectionString()))
        {
            OleDbCommand InsertQuery = new OleDbCommand();
            InsertQuery.CommandType = CommandType.Text;
            InsertQuery.CommandText = "insert into Categories ([CategoryID], [CategoryName], [Numbers]) values (?,?,?)";
            InsertQuery.Parameters.AddWithValue("@CategoryID", Id);
            InsertQuery.Parameters.AddWithValue("@CategoryName", Name);
            InsertQuery.Parameters.AddWithValue("@Numbers", Numbers);
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
        if (rc < 9)
        {
            //RecordAdd( 9, "Coci Cola", "1");
            RecordAdd(10, "CociCola", "1");
            RecordAdd(11, "Coci.Cola", "1");
            RecordAdd(12, "Coci,Cola", "1");
            RecordAdd(13, "Coci  cola", "1");
            RecordAdd(14, "Coci cola ", "1");
        }
        rc = RecordCount();//debug
        return;
    }
}//end class
