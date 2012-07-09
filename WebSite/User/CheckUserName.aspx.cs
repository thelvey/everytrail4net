using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EveryTrailNET.Core;
using EveryTrailNET.Core.QueryResponse;

public partial class User_CheckUserName : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        CheckUserNameResponse response = Actions.CheckUserName(txtUserName.Text);

        switch (response.Status)
        {
            case CheckUserNameStatus.Success:
                ltlResult.Text = "User name available";
                break;
            case CheckUserNameStatus.UserNameTaken:
                ltlResult.Text = "User name is taken";
                break;
            case CheckUserNameStatus.Unknown:
                ltlResult.Text = "Status Unknown";
                break;
        }

    }
}