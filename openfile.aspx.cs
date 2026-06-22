using System;
using System.IO;
using System.Web.UI;

public partial class openfile : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string file = Request.QueryString["file"];

        if (string.IsNullOrEmpty(file))
            return;

        string path = Server.MapPath("~/Uploads/" + file);

        if (File.Exists(path))
        {
            string ext = Path.GetExtension(path).ToLower();

            if (ext == ".pdf")
                Response.ContentType = "application/pdf";
            else if (ext == ".jpg" || ext == ".jpeg")
                Response.ContentType = "image/jpeg";
            else if (ext == ".png")
                Response.ContentType = "image/png";
            else
                Response.ContentType = "application/octet-stream";

            Response.TransmitFile(path);
            Response.End();
        }
    }
}

