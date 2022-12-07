'INSTANT VB NOTE: This code snippet uses implicit typing. You will need to set 'Option Infer On' in the VB file or set 'Option Infer' at the project level:

<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="SandboxSearchstring" %>

<%@ Register Assembly="DevExpress.Web.v15.1"
	Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>How to create a custom ASPxGridView's filter insensitive to the number of spaces and punctuation</title>
</head>
<body>
	<form id="form1" runat="server">
	<div>		
		<dx:ASPxGridView ID="gvClients" ClientInstanceName="gvClients" runat="server" 
			AutoGenerateColumns="False" 
			DataSourceID="AccessDataSource1" KeyFieldName="ClientID"
			onautofiltercelleditorinitialize="gvClients_AutoFilterCellEditorInitialize" 
			onprocesscolumnautofilter="gvClients_ProcessColumnAutoFilter" Width="647px" 
			EnableCallBacks="False" >
			<Columns>
				<dx:GridViewDataTextColumn Caption="ID" FieldName="ClientID" ReadOnly="True" VisibleIndex="0">
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn Caption="Klant" FieldName="Name" VisibleIndex="1" >
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn Caption="Adres" FieldName="Address" VisibleIndex="2" >
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn Caption="Postcode" FieldName="Zip" VisibleIndex="3" >
				</dx:GridViewDataTextColumn>
				<dx:GridViewDataTextColumn Caption="Plaats" FieldName="City" VisibleIndex="4" >
				</dx:GridViewDataTextColumn>
			</Columns>
			<SettingsBehavior ColumnResizeMode="Control" />
			<Settings ShowFilterRow="true" ShowFilterRowMenu="True" />
		</dx:ASPxGridView>
			<asp:AccessDataSource ID="AccessDataSource1" runat="server" 
				DataFile="~/App_Data/data.mdb" 
				SelectCommand="SELECT [ClientID], [Name], [Address], [Zip], [City] FROM [Clients]">
			</asp:AccessDataSource>
	</div>
	</form>
</body>
</html>