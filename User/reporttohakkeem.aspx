<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="reporttohakkeem.aspx.cs" Inherits="User_reporttohakkeem" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%if (Session["Speciality"].ToString() == "Auto")
        { %>
    <section class="content" style="margin-top: 1.5cm; margin-bottom: 1cm">
        <%}
            else
            { %>
        <section class="content" dir="rtl" style="margin-top: 1.5cm; margin-bottom: 1cm">
            <%} %>
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <%if (Session["Speciality"].ToString() == "Auto")
                                { %>
                            <h3 class="box-title">Why you report this doctor ?</h3>
                            <%}
                            else
                            { %>
                            <h3 class="box-title">لماذا تبلغ عن هذا الطبيب؟</h3>
                            <%} %>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" meta:resourcekey="CheckBoxList1Resource1">
                                    <asp:ListItem meta:resourcekey="ListItemResource1">Not consult</asp:ListItem>
                                    <asp:ListItem meta:resourcekey="ListItemResource2">Misbehave</asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                            <div class="form-group">
                                <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>
                                <p>Description</p>
                                <%}
                                else
                                { %>
                                <p>وصف</p>
                                <%} %>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" TextMode="MultiLine" Rows="10" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-sm btn-danger" Text="Report" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                            </div>
                        </div>



                    </div>
                </div>
            </div>
        </section>
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

        <!-- Latest compiled JavaScript -->
        <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
         <script src="../Design/dist/js/app.min.js"></script>
        <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>

