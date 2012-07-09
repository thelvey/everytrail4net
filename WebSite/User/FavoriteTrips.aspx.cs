using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EveryTrailNET.Core;
using EveryTrailNET.Objects;

public partial class User_FavoriteTrips : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int userId = Convert.ToInt32(txtUserId.Text);

        List<Trip> trips = Actions.FavoriteTrips(userId);

        grdFavoriteTrips.DataSource = trips;
        grdFavoriteTrips.DataBind();

    }
}