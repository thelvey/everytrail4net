using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EveryTrailNET.Core;
using EveryTrailNET.Core.QueryResponse;

public partial class User_CheckEmail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCheck_Click(object sender, EventArgs e)
    {
        CheckUserEmailResponse response = Actions.CheckUserEmail(txtEmail.Text);

        switch (response.Status)
        {
            case CheckUserEmailStatus.EmailAddressTaken:
                ltlResponse.Text = "Email Address Taken";
                break;
            case CheckUserEmailStatus.IncorrectEmailFormat:
                ltlResponse.Text = "Incorrect Email Format";
                break;
            case CheckUserEmailStatus.Success:
                ltlResponse.Text = "Email available";
                break;
            case CheckUserEmailStatus.Unknown:
                ltlResponse.Text = "Status Unknown";
                break;

        }
    }
}