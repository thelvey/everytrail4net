<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FavoriteTrips.aspx.cs" Inherits="User_FavoriteTrips" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        User id: <asp:TextBox ID="txtUserId" runat="server" />
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        <br />
        <br />
        <asp:GridView ID="grdFavoriteTrips" runat="server">
        </asp:GridView>
    </div>
    </form>
</body>
</html>
