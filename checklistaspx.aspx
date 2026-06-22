<%@ Page Language="C#" AutoEventWireup="true"
    CodeFile="Checklistaspx.aspx.cs"
    Inherits="Checklist" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">

<title>Bar Stock Inspection Checklist</title>

<link href="style.css" rel="stylesheet" />

</head>

<body>

<form id="form1" runat="server">

    <!-- SIDEBAR -->

    <div class="sidebar">

        <a href="Home.aspx" class="homeLink">
            ??
        </a>

    </div>

    <!-- MAIN CONTENT -->

    <div class="container">

        <h2>BAR STOCK INSPECTION CHECKLIST</h2>

        <div class="headerRow">

            <div class="leftSection">

                RR No :

                <asp:TextBox
                    ID="txtRR"
                    runat="server"
                    CssClass="txtSmall" />

            </div>

            <div class="rightSection">

                Date of Inspection :

                <asp:TextBox
                    ID="txtDate"
                    runat="server"
                    TextMode="Date"
                    CssClass="txtDate" />

            </div>

        </div>

        <br />

        <!-- CHECKLIST GRID -->

        <asp:GridView
            ID="gvChecklist"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table-style">

            <Columns>

                <asp:BoundField
                    DataField="No"
                    HeaderText="S.No" />

                <asp:BoundField
                    DataField="Checkpoint"
                    HeaderText="Check Points" />

                <asp:TemplateField HeaderText="Details">

                    <ItemTemplate>

                        <asp:TextBox
                            ID="txtDetails"
                            runat="server"
                            CssClass="txtDetails"
                            Text='<%# Eval("DefaultText") %>' />

                    </ItemTemplate>

                </asp:TemplateField>

            </Columns>

        </asp:GridView>

        <br />

        <!-- FILE SECTION -->

        <div class="actionRow">

            <div class="leftAction">

                <b>Upload Files :</b>

                <br /><br />

                <asp:FileUpload
                    ID="fuUpload"
                    runat="server"
                    CssClass="fileUpload"
                    AllowMultiple="true" />

                <br /><br />

                <asp:Button
                    ID="btnAddFile"
                    runat="server"
                    Text="Add File"
                    CssClass="btnAddFile"
                    OnClick="btnAddFile_Click" />

                <br /><br />

                <!-- FILE GRID -->

                <asp:GridView
                    ID="gvFiles"
                    runat="server"
                    AutoGenerateColumns="False"
                    CssClass="fileGrid"
                    OnRowCommand="gvFiles_RowCommand">

                    <Columns>

                        <asp:BoundField
                            DataField="FILE_NAME"
                            HeaderText="File Name" />

                        <asp:TemplateField HeaderText="Remove">

                            <ItemTemplate>

                                <asp:Button
                                    ID="btnRemove"
                                    runat="server"
                                    Text="X"
                                    CssClass="btnRemove"
                                    CommandName="REMOVE"
                                    CommandArgument='<%# Container.DataItemIndex %>' />

                            </ItemTemplate>

                        </asp:TemplateField>

                    </Columns>

                </asp:GridView>

                <br />

                Remarks :

                <br /><br />

                <asp:TextBox
                    ID="txtRemarks"
                    runat="server"
                    CssClass="txtRemarks" />

            </div>

            <div class="rightAction">

                <asp:Button
                    ID="btnSave"
                    runat="server"
                    Text="SAVE"
                    CssClass="btnSave"
                    OnClick="btnSave_Click" />

            </div>

        </div>

    </div>

</form>

</body>

</html>


