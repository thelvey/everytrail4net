<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckEmail.aspx.cs" Inherits="User_CheckEmail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Email: <asp:TextBox ID="txtEmail" runat="server" />
        <br />
        <asp:Button id="btnCheck" runat="server" Text="Check" OnClick="btnCheck_Click" />
        <br />
        <asp:Literal ID="ltlResponse" runat="server" />
    </div>
    </form>
</body>
</html>
