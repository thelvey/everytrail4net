<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UsersFollowers.aspx.cs" Inherits="User_UsersFollowers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        User id: <asp:TextBox ID="txtUserId" runat="server" />
        <asp:Button ID="btnGet" runat="server" Text="Get Followers" OnClick="btnGet_Click" />
        <br />
        <asp:GridView ID="grdFollowers" runat="server" />
    </div>
    </form>
</body>
</html>
