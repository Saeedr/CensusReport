<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckNationalCode.aspx.cs" Inherits="Shahab.CensusReport.CheckNationalCode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    National Code: <asp:TextBox ID="txtNationalCode" runat="server" /><asp:Button ID="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
    </div>
    </form>
</body>
</html>
