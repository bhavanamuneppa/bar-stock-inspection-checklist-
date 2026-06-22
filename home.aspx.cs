using System;

public partial class Home : System.Web.UI.Page
{
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        Response.Redirect("Checklistaspx.aspx");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        Response.Redirect("ViewChecklist.aspx");
    }
}
