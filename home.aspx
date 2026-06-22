<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>QC Menu</title>

    <style>
        body {
            font-family: Arial;
            background: #ffffff;
            margin: 0;
        }

        h1 {
            text-align: center;
            color: #151518;
            padding: 15px;
            margin: 5px auto;
            font-size: 40px;
            letter-spacing: 1px;
        }

        .sidebar {
            position: fixed;
            left: 0;
            top: 0;
            width: 170px;
            height: 100%;
            background: #151518;
            text-align: center;
            padding-top: 20px;
        }

        .homeLink {
            color: white;
            text-decoration: none;
            font-size: 25px;
            display: block;

        }

        .content {
            margin-left: 90px;
        }

        .box {
            width: 300px;
            margin: 200px auto;
            text-align: center;
            border-radius: 15px;
            padding: 40px 20px;
            background: white;
            box-shadow: 0px 5px 20px rgba(0,0,0,0.15);
        }

        .btn {
            width: 150px;
            padding: 12px;
            margin: 12px;
            background: rgb(128, 128, 128);
            border: none;
            color: white;
            font-weight: bold;
            cursor: pointer;
            border-radius: 25px;
        }

        .btn:hover {
            background: linear-gradient(90deg,#FF8C00,#4682B4);
        }
    </style>
</head>

<body>

<form id="form1" runat="server">

    <div class="sidebar">
        <a href="Home.aspx" class="homeLink">?? Home</a>
    </div>

    <div class="content">

        <h1>Bar Stock Inspection  Checklist System</h1>

        <div class="box">

            <asp:Button ID="btnCreate"
                runat="server"
                Text="CREATE"
                CssClass="btn"
                OnClick="btnCreate_Click" />

            <br />

            <asp:Button ID="btnView"
                runat="server"
                Text="VIEW"
                CssClass="btn"
                OnClick="btnView_Click" />

        </div>

    </div>

</form>

</body>
</html>


