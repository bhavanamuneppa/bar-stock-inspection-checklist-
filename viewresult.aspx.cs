using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;

public partial class ViewResult : System.Web.UI.Page
{
    string conStr =
        ConfigurationManager.ConnectionStrings["ConnectionString6"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable db = (DataTable)Session["RRDATA"];

            if (db != null && db.Rows.Count > 0)
            {
                LoadChecklist(db);
                LoadFiles(db.Rows[0]["RRNO"].ToString());
            }
        }
    }

    private void LoadChecklist(DataTable db)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("NO");
        dt.Columns.Add("CHECKPOINT");
        dt.Columns.Add("VALUE");

        dt.Rows.Add("1", "PO No", db.Rows[0]["PONO"].ToString());
        dt.Rows.Add("2", "Invoice", db.Rows[0]["INVOICE"].ToString());
        dt.Rows.Add("3", "Specification", db.Rows[0]["SPECIFIC"].ToString());
        dt.Rows.Add("4", "Dia In MM", db.Rows[0]["DIAINMM"].ToString());
        dt.Rows.Add("5", "Quantity", db.Rows[0]["QTY"].ToString());
        dt.Rows.Add("6", "Heat Number", db.Rows[0]["HEATNO"].ToString());
        dt.Rows.Add("7", "Batch Number", db.Rows[0]["BATCHNO"].ToString());
        dt.Rows.Add("8", "Mill Test Certificate", db.Rows[0]["MILLTEST"].ToString());
        dt.Rows.Add("9", "Ultrasonic Test", db.Rows[0]["VERFITYULTRA"].ToString());
        dt.Rows.Add("10", "Beta Transus Temp", db.Rows[0]["CHECKTEMP"].ToString());
        dt.Rows.Add("11", "Gamma Temp", db.Rows[0]["GAMA"].ToString());
        dt.Rows.Add("12", "Stenciling", db.Rows[0]["STENCILING"].ToString());
        dt.Rows.Add("13", "Colour Code", db.Rows[0]["COLURE"].ToString());
        dt.Rows.Add("14", "Permanent Marking", db.Rows[0]["PERMANET"].ToString());
        dt.Rows.Add("15", "XRF Test", db.Rows[0]["XRF"].ToString());
        dt.Rows.Add("16", "Other", db.Rows[0]["OTHER"].ToString());

        gvView.DataSource = dt;
        gvView.DataBind();
    }

    private void LoadFiles(string rrno)
    {
        OracleConnection con = new OracleConnection(conStr);
        con.Open();

        OracleCommand cmd = new OracleCommand(@"
            SELECT U.FILE_NAME, U.ATTACHMENT_NAME
            FROM QC_CHK_UPLOADS U
            INNER JOIN QC_CHK C ON C.ID = U.CHECKLIST_ID
            WHERE C.RRNO = :RRNO
        ", con);

        cmd.Parameters.Add(":RRNO", OracleType.VarChar).Value = rrno;

        OracleDataAdapter da = new OracleDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);

        gvFiles.DataSource = dt;
        gvFiles.DataBind();

        con.Close();
    }
}


