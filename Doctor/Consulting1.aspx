<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Consulting1.aspx.cs" Inherits="Doctor_Consulting" Culture="en-US" UICulture="en-US" meta:resourcekey="PageResource1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script src="../js/jquery.slimscroll.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '400px'

            });
        });
    </script>
    <style type="text/css">
        #ContentPlaceHolder1_Button2 {
            margin-top: 10px;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.4;
        }

        .modalPopup {
            /*background-color: #FFFFFF;*/
            border-width: 0px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 3px;
            width: 500px;
            height: 300px;
        }
    </style>

    <%--  <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>--%>
    <link href="../css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <div class="container-fluid">

        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">


                            <div class="modal-header">

                                <h4 class="modal-title">
                                    <h4 class="modal-title" dir="rtl">
                                        <asp:Label ID="lblModalTitle" runat="server" Text="Patient history" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <div>
                                        <div dir="rtl">

                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="5" meta:resourcekey="GridView2Resource1">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource1"></asp:Label>
                                                            <asp:Label ID="LblHosId1" runat="server" Text='<%# Eval("h_id") %>' Visible="False" meta:resourcekey="LblHosId1Resource1"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("d_id") %>' Visible="False" meta:resourcekey="Label2Resource1"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Diagnosis" meta:resourcekey="TemplateFieldResource3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_doc_daignose") %>' meta:resourcekey="LblDiagnoseResource1"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prescription" meta:resourcekey="TemplateFieldResource4">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblPrescription" runat="server" Text='<%# Eval("a_doc_prescriptions") %>' meta:resourcekey="LblPrescriptionResource1"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <HeaderStyle BackColor="#DDDDDD" Font-Bold="True" ForeColor="#18BC9C" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <RowStyle ForeColor="#000066" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                <SortedDescendingHeaderStyle BackColor="#00547E" />
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal-footer">
                                    <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>
                                    <button class="btn btn-primary pull-left" data-dismiss="modal" aria-hidden="true">قريب</button>

                                </div>
                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="GridView2" EventName="PageIndexChanging" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <!-- Bootstrap Modal Dialog -->
        <div class="modal fade" id="myModal1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">

                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title">
                                    <h4 class="modal-title pull-right">

                                        <asp:Label ID="Label3" runat="server" Text="Hakkeem" meta:resourcekey="Label3Resource1"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="modal-body" dir="rtl">
                                    <asp:Label ID="lblModalBody" runat="server" meta:resourcekey="lblModalBodyResource1"></asp:Label>

                                </div>

                                <div class="modal-footer">
                                    <div>
                                        <div class="pull-left">
                                            <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-primary" OnClick="Button2_Click" UseSubmitBehavior="False" meta:resourcekey="Button2Resource1" />
                                        </div>
                                    </div>
                                </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row">
            <div class="col-md-7">
              <%--  <%if (Session["Language"].ToString() == "Auto")
                    { %>--%>
                <div class="box box-primary">
                  <%--  <%}
                    else
                    { %>
                    <div class="box box-primary" dir="rtl">
                        <%} %>--%>
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="Label7" runat="server" Text="Consulting" meta:resourcekey="Label7Resource1"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label8" runat="server" Text="Appointment time" meta:resourcekey="Label8Resource1"></asp:Label></label>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" ReadOnly="True" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label9" runat="server" Text="Reason to visit" meta:resourcekey="Label9Resource1"></asp:Label></label>
                                <asp:TextBox ID="TextBox2" CssClass="form-control" ReadOnly="True" runat="server" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label10" runat="server" Text="Dignosis" meta:resourcekey="Label10Resource1"></asp:Label></label>
                                <asp:TextBox ID="TextBox3" CssClass="form-control" TextMode="MultiLine" Rows="2" runat="server" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>
                                    <asp:Label ID="Label11" runat="server" Text="Prescription" meta:resourcekey="Label11Resource1"></asp:Label></label>
                                <asp:TextBox ID="TextBox4" CssClass="form-control" TextMode="MultiLine" Rows="5" runat="server" meta:resourcekey="TextBox4Resource1"></asp:TextBox>
                            </div>

                        </div>
                        <div class="box-footer">
                           <%-- <%if (Session["Language"].ToString() == "Auto")
                                { %>--%>
                            <div>
                               <%-- <%}
                                else
                                { %>
                                <div class="pull-left">
                                    <%} %>--%>
                                    <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary pull-right" runat="server" Text="Submit" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-5">
                     <%--   <%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <div class="box box-primary">
                          <%--  <%}
                            else
                            { %>
                            <div class="box box-primary" dir="rtl">

                                <%} %>--%>
                                <div class="box-header">
                                    <h3 class="box-title">
                                        <asp:Label ID="Label12" runat="server" Text="Patient details" meta:resourcekey="Label12Resource1"></asp:Label></h3>
                                </div>
                                <div class="box-body">

                                    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered" AutoGenerateRows="False" OnItemCommand="DetailsView1_ItemCommand" meta:resourcekey="DetailsView1Resource1">
                                        <Fields>
                                            <asp:TemplateField HeaderText="User photo" meta:resourcekey="TemplateFieldResource5">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image3" runat="server" CssClass="img-lg img-responsive" ImageUrl='<%# Bind("photo") %>' AlternateText="Photo" meta:resourcekey="Image3Resource1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource6">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("name") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Age" meta:resourcekey="TemplateFieldResource7">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("dob") %>' meta:resourcekey="Label5Resource1"></asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Text="Label" meta:resourcekey="Label6Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gender" meta:resourcekey="TemplateFieldResource8">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("gender") %>' meta:resourcekey="Label7Resource2"></asp:Label>
                                                    <asp:Label ID="Label8" runat="server" meta:resourcekey="Label8Resource2"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource9">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="history" ForeColor="#98120F" Font-Bold="True" Text="Previous history" meta:resourcekey="LinkButton2Resource1"></asp:LinkButton>
                                                    |
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="TestReports" CommandArgument='<%# Eval("email") %>' ForeColor="#98120F" Font-Bold="True" Text="Test Reports" meta:resourcekey="LinkButton1Resource1"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Fields>
                                    </asp:DetailsView>

                                </div>
                                <div class="box-footer"></div>
                            </div>
                        </div>
                    </div>
                    <div>
                    </div>




                </div>
                <script src="../js/app.min.js"></script>
</asp:Content>

