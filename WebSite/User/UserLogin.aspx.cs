using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EveryTrailNET.Core;
using EveryTrailNET.Core.QueryResponse;

public partial class User_UserLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUserLogin_Click(object sender, EventArgs e)
    {
        UserLoginResponse response = Actions.UserLogin(txtUserName.Text, txtPassword.Text);

        if (response.Status)
        {
            ltlResponse.Text = "Successful Login. User id is " + response.UserID;
        }
        else
        {
            ltlResponse.Text = "Login failed";
        }
    }
}