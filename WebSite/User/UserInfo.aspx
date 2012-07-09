<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserInfo.aspx.cs" Inherits="User_UserInfo" %>

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
        <asp:Button ID="btnUserInfo" runat="server" Text="Get User Info" OnClick="btnUserInfo_Click" />
        <br />
        <asp:Literal ID="ltlStatus" runat="server" />
        <br />
        <asp:GridView ID="grdInfo" runat="server" />
    </div>
    </form>
</body>
</html>
