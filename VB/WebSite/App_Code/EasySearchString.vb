Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.Data.Filtering 'ICustomFunctionOperator

' How to register a custom filter function and use it in ASPxGridView
' see: http://www.devexpress.com/Support/Center/Example/Details/E4099

Namespace SearchString
	Public NotInheritable Class EasySearchStatic
		Private Sub New()
		End Sub
		Public Shared ReadOnly Property Name() As String
			Get
				Return "EasySearch"
			End Get
		End Property
	End Class

	''' <summary>
	'''
	''' </summary>
	Public Class EasySearchString
        Implements ICustomFunctionOperator

        Public Function Evaluate(ParamArray operands() As Object) As Object Implements ICustomFunctionOperator.Evaluate
            Dim SearchString As String = Canonical(operands(0).ToString())
            Dim CellString As String = Canonical(operands(1).ToString())
            Dim IndexNr As Integer = CellString.IndexOf(SearchString)
            Return IndexNr >= 0
        End Function

        Public ReadOnly Property Name() As String Implements ICustomFunctionOperator.Name
            Get
                Return EasySearchStatic.Name
            End Get
        End Property

        Public Function ResultType(ParamArray operands() As Type) As Type Implements ICustomFunctionOperator.ResultType
            Return GetType(Boolean)
        End Function

        Private Function Canonical(ByVal str_in As String) As String
            Dim str_out As String = ""
            For i As Integer = 0 To str_in.Length - 1
                Dim ch As Char = str_in.Chars(i)
                If ch <> " "c AndAlso ch <> "."c Then 'remove spaces, punctuation
                    str_out = str_out & Char.ToLower(ch) 'remove case
                End If
            Next i
            Return str_out
        End Function
       

        
    End Class 'end class
End Namespace 'end ns
