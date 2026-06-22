using System;
using System.Data;
using System.Data.OracleClient;

public partial class ViewChecklist : System.Web.UI.Page
{
    string conStr =
        System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString6"].ConnectionString;

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string rrno = txtRRSearch.Text.Trim();

        OracleConnection con = new OracleConnection(conStr);

        try
        {
            con.Open();

            OracleCommand cmd = new OracleCommand(
                "SELECT * FROM QC_CHK WHERE RRNO = :RRNO", con);

            cmd.Parameters.Add(":RRNO", OracleType.VarChar).Value = rrno;

            OracleDataAdapter da = new OracleDataAdapter(cmd);

            DataTable dt = new DataTable();

            da.Fill(dt);

            if (dt.Rows.Count > 0)
            {
                Session["RRDATA"] = dt;
                Response.Redirect("ViewResult.aspx");
            }
            else
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('No Record Found');",
                    true);
            }
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "msg",
                "alert('" + ex.Message + "');",
                true);
        }
        finally
        {
            con.Close();
        }
    }
}




