using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EveryTrailNET.Core;
using EveryTrailNET.Objects.Users;

public partial class User_UsersFollowers : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGet_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(txtUserId.Text);
        List<User> users = Actions.UserFollowers(userId);

        grdFollowers.DataSource = users;
        grdFollowers.DataBind();
    }
}