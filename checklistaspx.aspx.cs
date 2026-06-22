using System;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Checklist : System.Web.UI.Page
{
    string conStr =
        ConfigurationManager.ConnectionStrings["ConnectionString6"].ConnectionString;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadChecklist();

            txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");

            // Initialize file table in session
            DataTable dtFiles = new DataTable();
            dtFiles.Columns.Add("FILE_NAME");
            dtFiles.Columns.Add("FILE_PATH");

            Session["FILES"] = dtFiles;
        }
    }

    private void LoadChecklist()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("No");
        dt.Columns.Add("Checkpoint");
        dt.Columns.Add("DefaultText");

        dt.Rows.Add("1", "PO No", "");
        dt.Rows.Add("2", "Invoice and P.O Check", "Does the material size, length, form and quantity mentioned in DC/Invoice match with P.O :");
        dt.Rows.Add("3", "Specification", "");
        dt.Rows.Add("4", "Dia In MM", "");
        dt.Rows.Add("5", "Quantity", "");
        dt.Rows.Add("6", "Heat Number", "");
        dt.Rows.Add("7", "Batch Number", "");
        dt.Rows.Add("8", "Mill Test Certificate No", "");
        dt.Rows.Add("9", "Verify Ultrasonic Test", "");
        dt.Rows.Add("10", "Beta Transus Temp", "");
        dt.Rows.Add("11", "Gamma Temp", "");
        dt.Rows.Add("12", "Stenciling Status", "");
        dt.Rows.Add("13", "Colour Code", "");
        dt.Rows.Add("14", "Permanent Marking", "");
        dt.Rows.Add("15", "XRF Test Status", "");
        dt.Rows.Add("16", "Other Observations", "");

        gvChecklist.DataSource = dt;
        gvChecklist.DataBind();
    }

    // =========================
    // ADD FILE
    // =========================
    protected void btnAddFile_Click(object sender, EventArgs e)
    {
        DataTable dtFiles = (DataTable)Session["FILES"];

        if (dtFiles == null)
        {
            dtFiles = new DataTable();
            dtFiles.Columns.Add("FILE_NAME");
            dtFiles.Columns.Add("FILE_PATH");
        }

        if (fuUpload.HasFile)
        {
            string folder = Server.MapPath("~/TempUploads/");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string fileName = Guid.NewGuid() + "_" + fuUpload.FileName;
            string fullPath = Path.Combine(folder, fileName);

            fuUpload.SaveAs(fullPath);

            DataRow dr = dtFiles.NewRow();
            dr["FILE_NAME"] = fuUpload.FileName;
            dr["FILE_PATH"] = fullPath;

            dtFiles.Rows.Add(dr);
        }

        Session["FILES"] = dtFiles;

        gvFiles.DataSource = dtFiles;
        gvFiles.DataBind();
    }

    // =========================
    // REMOVE FILE
    // =========================
    protected void gvFiles_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "REMOVE")
        {
            int index = Convert.ToInt32(e.CommandArgument);

            DataTable dtFiles = (DataTable)Session["FILES"];

            if (dtFiles != null && index >= 0 && index < dtFiles.Rows.Count)
            {
                dtFiles.Rows.RemoveAt(index);
            }

            Session["FILES"] = dtFiles;

            gvFiles.DataSource = dtFiles;
            gvFiles.DataBind();
        }
    }

    // =========================
    // SAVE
    // =========================
    protected void btnSave_Click(object sender, EventArgs e)
    {
        OracleConnection con = new OracleConnection(conStr);

        try
        {
            con.Open();

            txtRR.Text = txtRR.Text.Trim().ToUpper();

            // CHECK RR EXISTS
            OracleCommand checkCmd = new OracleCommand(
                "SELECT COUNT(*) FROM QC_CHK WHERE RRNO = :RRNO", con);

            checkCmd.Parameters.Add(":RRNO", OracleType.VarChar).Value = txtRR.Text;

            int count = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (count > 0)
            {
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "msg",
                    "alert('RR No already exists. Please enter a unique RR No.');",
                    true);

                return;
            }

            // GET NEW ID
            int checklistId =
                Convert.ToInt32(
                    new OracleCommand(
                        "SELECT QC_CHK_SEQ.NEXTVAL FROM DUAL", con)
                    .ExecuteScalar());

            // READ GRID VALUES
            string[] val = new string[16];
            int i = 0;

            foreach (GridViewRow row in gvChecklist.Rows)
            {
                TextBox txt = (TextBox)row.FindControl("txtDetails");
                val[i] = txt.Text.Trim();
                i++;
            }

            // INSERT MAIN TABLE
            string sql =
            @"INSERT INTO QC_CHK
            (
                ID, RRNO, DATEOFINSPEC,
                PONO, INVOICE, SPECIFIC, DIAINMM,
                QTY, HEATNO, BATCHNO, MILLTEST,
                VERFITYULTRA, CHECKTEMP, GAMA,
                STENCILING, COLURE, PERMANET,
                XRF, OTHER, ENTRY_TYPE
            )
            VALUES
            (
                :ID, :RRNO, TO_DATE(:DATEOFINSPEC,'YYYY-MM-DD'),
                :PONO, :INVOICE, :SPECIFIC, :DIAINMM,
                :QTY, :HEATNO, :BATCHNO, :MILLTEST,
                :VERFITYULTRA, :CHECKTEMP, :GAMA,
                :STENCILING, :COLURE, :PERMANET,
                :XRF, :OTHER, 'D'
            )";

            OracleCommand cmd = new OracleCommand(sql, con);

            cmd.Parameters.Add(":ID", OracleType.Number).Value = checklistId;
            cmd.Parameters.Add(":RRNO", OracleType.VarChar).Value = txtRR.Text;
            cmd.Parameters.Add(":DATEOFINSPEC", OracleType.VarChar).Value = txtDate.Text;

            for (int x = 0; x < 16; x++)
            {
                cmd.Parameters.Add(":" + GetParamName(x), OracleType.VarChar).Value = val[x];
            }

            cmd.ExecuteNonQuery();

            // =========================
            // SAVE FILES
            // =========================
            DataTable dtFiles = (DataTable)Session["FILES"];

            if (dtFiles != null)
            {
                foreach (DataRow row in dtFiles.Rows)
                {
                    string fullPath = row["FILE_PATH"].ToString();
                    string originalName = row["FILE_NAME"].ToString();

                    string ext = Path.GetExtension(originalName);

                    string storedFileName =
                        checklistId + "_" + Guid.NewGuid() + ext;

                    string finalFolder = Server.MapPath("~/Uploads/");

                    if (!Directory.Exists(finalFolder))
                    {
                        Directory.CreateDirectory(finalFolder);
                    }

                    File.Copy(fullPath, Path.Combine(finalFolder, storedFileName), true);

                    OracleCommand fileCmd = new OracleCommand(
                        @"INSERT INTO QC_CHK_UPLOADS
                        (
                            FILE_ID,
                            CHECKLIST_ID,
                            ATTACHMENT_NAME,
                            REMARKS,
                            FILE_NAME,
                            FILE_EXTENSION,
                            CREATED_BY,
                            CREATED_DATE
                        )
                        VALUES
                        (
                            QC_CHK_UPLOADS_SEQ.NEXTVAL,
                            :CHECKLIST_ID,
                            :ATTACHMENT_NAME,
                            :REMARKS,
                            :FILE_NAME,
                            :FILE_EXTENSION,
                            :CREATED_BY,
                            SYSDATE
                        )", con);

                    fileCmd.Parameters.Add(":CHECKLIST_ID", OracleType.Number).Value = checklistId;
                    fileCmd.Parameters.Add(":ATTACHMENT_NAME", OracleType.VarChar).Value = originalName;
                    fileCmd.Parameters.Add(":REMARKS", OracleType.VarChar).Value = txtRemarks.Text.Trim();
                    fileCmd.Parameters.Add(":FILE_NAME", OracleType.VarChar).Value = storedFileName;
                    fileCmd.Parameters.Add(":FILE_EXTENSION", OracleType.VarChar).Value = ext;
                    fileCmd.Parameters.Add(":CREATED_BY", OracleType.VarChar).Value = Environment.UserName;

                    fileCmd.ExecuteNonQuery();
                }
            }

            // RESET SESSION
            DataTable newDt = new DataTable();
            newDt.Columns.Add("FILE_NAME");
            newDt.Columns.Add("FILE_PATH");
            Session["FILES"] = newDt;

            ClientScript.RegisterStartupScript(
                this.GetType(),
                "msg",
                "alert('Saved Successfully. ID : " + checklistId + "');",
                true);
        }
        catch (Exception ex)
        {
            ClientScript.RegisterStartupScript(
                this.GetType(),
                "msg",
                "alert('" + ex.Message.Replace("'", "") + "');",
                true);
        }
        finally
        {
            con.Close();
        }
    }

    private string GetParamName(int i)
    {
        string[] p =
        {
            "PONO","INVOICE","SPECIFIC","DIAINMM","QTY",
            "HEATNO","BATCHNO","MILLTEST","VERFITYULTRA",
            "CHECKTEMP","GAMA","STENCILING","COLURE",
            "PERMANET","XRF","OTHER"
        };

        return p[i];
    }
}


