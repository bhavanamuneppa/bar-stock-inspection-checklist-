<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewResult.aspx.cs" Inherits="ViewResult" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Checklist View</title>

    <style>
        body {
            font-family: Arial;
        }

        h2 {
            text-align: center;
            color: dimgrey;
        }

        .table-style {
            width: 100%;
            border-collapse: collapse;
        }

        .table-style th {
            background: #4682B4;
            color: white;
            border: 1px solid black;
            padding: 6px;
        }

        .table-style td {
            border: 1px solid black;
            padding: 6px;
        }

        .txtView {
            width: 98%;
            border: none;
            background: #f5f5f5;
        }

        .section {
            margin: 20px;
        }
    </style>
</head>

<body>
<form id="form1" runat="server">

<h2>CHECKLIST DETAILS (VIEW)</h2>

<div class="section">

    <!-- CHECKLIST DATA -->
    <asp:GridView ID="gvView" runat="server"
        AutoGenerateColumns="False"
        CssClass="table-style">

        <Columns>
            <asp:BoundField HeaderText="S.No" DataField="NO" />
            <asp:BoundField HeaderText="Checkpoint" DataField="CHECKPOINT" />

            <asp:TemplateField HeaderText="Value">
                <ItemTemplate>
                    <asp:TextBox ID="txtValue"
                        runat="server"
                        CssClass="txtView"
                        Text='<%# Eval("VALUE") %>'
                        ReadOnly="true" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <br />

    <h3>Attachments</h3>

    <!-- FILES GRID -->
    <asp:GridView ID="gvFiles" runat="server"
        AutoGenerateColumns="False"
        CssClass="table-style">

        <Columns>

            <asp:TemplateField HeaderText="File Name (Click to Open)">
                <ItemTemplate>
                    <a href='<%# "openfile.aspx?file=" + Eval("FILE_NAME") %>' target="_blank">
                        <%# Eval("ATTACHMENT_NAME") %>
                    </a>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Download">
                <ItemTemplate>
                    <a href='<%# "DownloadFile.aspx?file=" + Eval("FILE_NAME") %>'>
                        Download
                    </a>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

</div>

</form>
</body>
</html>
