<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin.aspx.cs" Inherits="User_UserLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Username: <asp:TextBox ID="txtUserName" runat="server" />
        <br />
        Password: <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
        <br />
        <asp:Button ID="btnUserLogin" runat="server" OnClick="btnUserLogin_Click" Text="Log in"  />
        <br />
        <br />
        <asp:Literal ID="ltlResponse" runat="server" />
    </div>
    </form>
</body>
</html>
