Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Data.OleDb 'OleDbCommand
Imports System.IO 'Path
Imports System.Data 'CommandType

''' <summary>
''' Functionality on the testdata in App_Data/data.mdb.
''' This needs to run only once, as the new data is stored
''' and available for the next run.
''' Data could also be added directly using MS Access.
''' </summary>
Public NotInheritable Class MakeDataBeverages
	Private Sub New()
	End Sub
	Private Shared Function ConnectionString() As String
		Dim AppDataPath As String = HttpContext.Current.Server.MapPath("App_Data")
		Dim mdbPath As String = Path.Combine(AppDataPath, "data.mdb")
		Dim myConnString As String = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", mdbPath)
		Return myConnString
	End Function

	''' <summary>
	''' Returns the number of records in the table
	''' </summary>
	Public Shared Function RecordCount() As Integer
		Dim Count As Integer = 0
		Using myConnection As New OleDbConnection(ConnectionString())
			Dim mySelectQuery As String = "Select Count(*) from Categories"
			Using myCommand As New OleDbCommand(mySelectQuery, myConnection)
				myConnection.Open()
				Dim myReader = myCommand.ExecuteReader()
				If myReader.Read() Then
					Dim meta(9) As Object
					Dim NumberOfColums As Integer = myReader.GetValues(meta)
					Count = CInt(Fix(meta(0)))
				End If
				myConnection.Close()
			End Using
		End Using
		Return Count
	End Function

	''' <summary>
	''' Add one record to the database table
	''' </summary>
	''' <param name="Id"></param>
	''' <param name="Name"></param>
	''' <param name="Numbers"></param>
	Public Shared Sub RecordAdd(ByVal Id As Integer, ByVal Name As String, ByVal Numbers As String)
		Using myConnection As New OleDbConnection(ConnectionString())
			Dim InsertQuery As New OleDbCommand()
			InsertQuery.CommandType = CommandType.Text
			InsertQuery.CommandText = "insert into Categories ([CategoryID], [CategoryName], [Numbers]) values (?,?,?)"
			InsertQuery.Parameters.AddWithValue("@CategoryID", Id)
			InsertQuery.Parameters.AddWithValue("@CategoryName", Name)
			InsertQuery.Parameters.AddWithValue("@Numbers", Numbers)
			InsertQuery.Connection = myConnection
			myConnection.Open()
			InsertQuery.ExecuteNonQuery()
		End Using
		Return
	End Sub

	''' <summary>
	''' Add all records that are needed.
	''' Do not add data if the extra data is already in the table.
	''' Adding data twice is not required, 
	''' and impossible because of primary key constraint on the first column.
	''' </summary>
	Public Shared Sub AddRecords()
		Dim rc As Integer = RecordCount()
		If rc < 9 Then
			'RecordAdd( 9, "Coci Cola", "1");
			RecordAdd(10, "CociCola", "1")
			RecordAdd(11, "Coci.Cola", "1")
			RecordAdd(12, "Coci,Cola", "1")
			RecordAdd(13, "Coci  cola", "1")
			RecordAdd(14, "Coci cola ", "1")
		End If
		rc = RecordCount() 'debug
		Return
	End Sub
End Class 'end class
