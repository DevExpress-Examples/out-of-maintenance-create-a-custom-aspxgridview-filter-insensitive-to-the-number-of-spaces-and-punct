Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq

Imports DevExpress.Data.Filtering
Imports DevExpress.Web.ASPxGridView

Imports SearchString


Partial Public Class SandboxSearchstring
	Inherits System.Web.UI.Page
	Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
		If CriteriaOperator.GetCustomFunction(EasySearchStatic.Name) Is Nothing Then
			CriteriaOperator.RegisterCustomFunction(New EasySearchString())
		End If
	End Sub

	
	Protected Sub gvClients_ProcessColumnAutoFilter(ByVal sender As Object, ByVal e As ASPxGridViewAutoFilterEventArgs)
		If e.Kind <> GridViewAutoFilterEventKind.CreateCriteria Then
			Return
		End If
		Dim co As CriteriaOperator = CriteriaOperator.Parse(gvClients.FilterExpression)
		FindAndRemoveCustomOperator(co, e.Column.FieldName)

		Dim fo As FunctionOperator = Nothing
		If e.Value.Length > 0 Then
			fo = New FunctionOperator(FunctionOperatorType.Custom, EasySearchStatic.Name, e.Value, New OperandProperty(e.Column.FieldName))
		End If
		If (Not ReferenceEquals(co, Nothing)) OrElse (Not ReferenceEquals(fo, Nothing)) Then
			gvClients.FilterExpression = MergeCriterias(co, fo).ToString()
		Else
			gvClients.FilterExpression = ""
		End If
		e.Criteria = Nothing
	End Sub

	Protected Function MergeCriterias(ByVal co As CriteriaOperator, ByVal fo As FunctionOperator) As CriteriaOperator
		If ReferenceEquals(fo, Nothing) Then
			Return co
		End If
		If ReferenceEquals(co, Nothing) Then
			Return fo
		End If
		Dim go = TryCast(co, GroupOperator)
		If ReferenceEquals(go, Nothing) OrElse go.OperatorType <> GroupOperatorType.And Then
			Return GroupOperator.And(co, fo)
		End If
		go.Operands.Add(fo)
		Return go
	End Function

	Protected Function FindAndRemoveCustomOperator(ByRef co As CriteriaOperator, ByVal fieldName As String) As FunctionOperator
		Dim fo = TryCast(co, FunctionOperator)
		If IsValidFuncOperator(fo, fieldName) Then
			co = Nothing
			Return fo
		End If
		Dim go = TryCast(co, GroupOperator)
		If ReferenceEquals(go, Nothing) Then
			Return Nothing
		End If
		fo = TryCast(go.Operands.FirstOrDefault(Function(op) IsValidFuncOperator(TryCast(op, FunctionOperator), fieldName)), FunctionOperator)
		If ReferenceEquals(fo, Nothing) Then
			Return Nothing
		End If
		go.Operands.Remove(fo)
		Return fo
	End Function

	Protected Function IsValidFuncOperator(ByVal fo As FunctionOperator, ByVal fieldName As String) As Boolean
		If ReferenceEquals(fo, Nothing) OrElse fo.OperatorType <> FunctionOperatorType.Custom OrElse fo.Operands.Count <> 3 Then
			Return False
		End If

		Dim customNameOp = TryCast(fo.Operands(0), OperandValue)
		If ReferenceEquals(customNameOp, Nothing) OrElse customNameOp.Value.ToString() <> EasySearchStatic.Name Then
			Return False
		End If

		Dim oProp = TryCast(fo.Operands(2), OperandProperty)
		If oProp.PropertyName <> fieldName Then
			Return False
		End If
		Return True
	End Function

	Protected Sub gvClients_AutoFilterCellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs)
		If String.IsNullOrEmpty(gvClients.FilterExpression) OrElse (Not gvClients.FilterExpression.Contains(EasySearchStatic.Name)) Then
			Return
		End If
		Dim co = CriteriaOperator.Parse(gvClients.FilterExpression)
		Dim fo = FindAndRemoveCustomOperator(co, e.Column.FieldName)
		If ReferenceEquals(fo, Nothing) Then
			Return
		End If
		Dim oValue = TryCast(fo.Operands(1), OperandValue)
		If ReferenceEquals(oValue, Nothing) Then
			Return
		End If
		e.Editor.Value = oValue.Value.ToString()
	End Sub

End Class