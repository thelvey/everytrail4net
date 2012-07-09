using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EveryTrailNET.Core;
using EveryTrailNET.Core.QueryResponse;
using EveryTrailNET.Objects.Users;

public partial class User_UserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnUserInfo_Click(object sender, EventArgs e)
    {
        UserProfileResponse response = Actions.UserProfileInfo(Convert.ToInt32(txtUserId.Text));

        if (response.Status == UserProfileInfoStatus.Unknown)
        {
            ltlStatus.Text = "Status Unknown";
            grdInfo.DataSource = new List<string>();
            grdInfo.DataBind();
        }
        else if (response.Status == UserProfileInfoStatus.Success)
        {
            ltlStatus.Text = "User found successfully";

            List<User> user = new List<EveryTrailNET.Objects.Users.User>();
            user.Add(response.ResponseUser);

            grdInfo.DataSource = user;
            grdInfo.DataBind();
        }
    }
}