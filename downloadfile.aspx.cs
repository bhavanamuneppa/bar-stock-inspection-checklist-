using System;
using System.IO;
using System.Web;

public partial class DownloadFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string file = Request.QueryString["file"];

        if (string.IsNullOrEmpty(file))
            return;

        string path = Server.MapPath("~/Uploads/" + file);

        if (File.Exists(path))
        {
            Response.Clear();
            Response.ContentType = "application/octet-stream";

            Response.AppendHeader(
                "Content-Disposition",
                "attachment; filename=" + file
            );

            Response.TransmitFile(path);
            Response.End();
        }
    }
}

