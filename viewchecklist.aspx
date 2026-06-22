<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewChecklist.aspx.cs" Inherits="ViewChecklist" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
<title>View Checklist</title>

<style>

body{
    font-family:Arial;
    margin:0;
    background:white;
}

.sidebar{
    position:fixed;
    left:0;
    top:0;
    width:170px;
    height:100%;
    background:#151518;
    text-align:center;
    padding-top:20px;
}

.homeLink{
    color:white;
    text-decoration:none;
}

.content{
    margin-left:90px;
}

.box{
    width:300px;
    margin:100px auto;
    text-align:center;
    border-radius:15px;
    padding:40px 20px;
    background:white;
    box-shadow:0px 5px 20px rgba(0,0,0,0.15);
}

.txt{
    width:220px;
    height:28px;
}

.btn{
    width:150px;
    padding:12px;
    background:#808080;
    border:none;
    color:white;
    border-radius:25px;
    font-weight:bold;
}

</style>

</head>

<body>

<form id="form1" runat="server">

<div class="sidebar">
    <a href="Home.aspx" class="homeLink">?? Home</a>
</div>

<div class="content">

<div class="box">

<h3>VIEW CHECKLIST</h3>

RR No

<br /><br />

<asp:TextBox ID="txtRRSearch"
    runat="server"
    CssClass="txt"></asp:TextBox>

<br /><br />

<asp:Button ID="btnSearch"
    runat="server"
    Text="SEARCH"
    CssClass="btn"
    OnClick="btnSearch_Click" />

</div>

</div>

</form>

</body>
</html>
