<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckUserName.aspx.cs" Inherits="User_CheckUserName" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        User name: <asp:TextBox ID="txtUserName" runat="server" />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Check" OnClick="btnSubmit_Click" />
        <br />
        <asp:Literal ID="ltlResult" runat="server" />
    </div>
    </form>
</body>
</html>
