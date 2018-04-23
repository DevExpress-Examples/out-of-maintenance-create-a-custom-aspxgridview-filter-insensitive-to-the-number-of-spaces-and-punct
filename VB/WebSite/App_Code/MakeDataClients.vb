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
Public NotInheritable Class MakeDataClients
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
			Dim mySelectQuery As String = "Select Count(*) from Clients"
			Using myCommand As New OleDbCommand(mySelectQuery, myConnection)
				myConnection.Open()
				Try
					Dim myReader = myCommand.ExecuteReader()
					If myReader.Read() Then
						Dim meta(9) As Object
						Dim NumberOfColums As Integer = myReader.GetValues(meta)
						Count = CInt(Fix(meta(0)))
					End If
				Catch e As Exception
					' e.g.: table does not exist
					'string ie_msg = e.InnerException.Message; //debug breakpoint
				End Try
				myConnection.Close()
			End Using
		End Using
		Return Count
	End Function

	Public Shared Sub CreateTable()
		Using myConnection As New OleDbConnection(ConnectionString())
			Dim InsertQuery As New OleDbCommand()
			InsertQuery.CommandType = CommandType.Text
			InsertQuery.CommandText = "create table [Clients] ([ClientID] number, [Name] char(30), [Address] char(30), [Zip] char(10), [City] char(30))"
			InsertQuery.Connection = myConnection
			myConnection.Open()
			InsertQuery.ExecuteNonQuery()
		End Using
		Return
	End Sub

	
	Public Shared Sub RecordAdd(ByVal ClientID As Integer, ByVal Name As String, ByVal Address As String, ByVal Zip As String, ByVal City As String)
		Using myConnection As New OleDbConnection(ConnectionString())
			Dim InsertQuery As New OleDbCommand()
			InsertQuery.CommandType = CommandType.Text
			InsertQuery.CommandText = "insert into Clients ([ClientID], [Name], [Address], [Zip], [City]) values (?,?,?,?,?)"
			InsertQuery.Parameters.AddWithValue("@ClientID", ClientID)
			InsertQuery.Parameters.AddWithValue("@Name", Name)
			InsertQuery.Parameters.AddWithValue("@Address", Address)
			InsertQuery.Parameters.AddWithValue("@Zip", Zip)
			InsertQuery.Parameters.AddWithValue("@City", City)
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
		If rc < 1 Then
			CreateTable()
			RecordAdd(0, "Holland-Transport BV", "Gooische weg 23", "1234AB", "s-gRavenhage")
			RecordAdd(1, "Gelderland Transport b.v.", "Mooistraat 25", "1234AB", "sGravenhage")
			RecordAdd(2, "De Ameland&Tram", "G.o.o.i.s.c.h.e weg 27", "1234AB", "'s Gravenhage")
			RecordAdd(3, "Landtraject", "Gooi-sche weg 29", "1234AB", "Den Haag")
			RecordAdd(4, "Land-Traan", "Gooischeweg 31", "1234AB", "DenHaag")
		End If
		rc = RecordCount() 'debug
		Return
	End Sub
End Class 'end class
