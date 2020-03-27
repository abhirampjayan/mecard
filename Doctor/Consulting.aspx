<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Consulting.aspx.cs" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" Inherits="Doctor_Consulting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link href="//fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet">
    <link href="//fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet">
    <%-- <link rel="stylesheet" href="../css/bootstrap.min.css" />--%>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
    <%--<link rel="stylesheet" href="../css/AdminLTE.min.css" />--%>

    <%--<link rel="stylesheet" href="../css/_all-skins.min.css" />--%>

    <link href="../css/intlTelInput.css" rel="stylesheet" />
    <!-- //web font -->
    <style type="text/css">
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

        #a1:hover {
            color: #4aa9af;
        }
    </style>
    <%--  <link href="../css/datepicker3.css" rel="stylesheet" />
    <link href="../css/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../js/bootstrap-datepicker.js"></script>--%>
    <script>
        $(function () {

            //Timepicker
            //$(".timepicker").timepicker({
            //    showInputs: false
            //});

            //Date range picker
            $('.datepicker').datepicker({
                format: 'yyyy-mm-dd',
                //format: 'dd/mm/yyyy',
                todayHighlight: true,
                autoclose: true,

            });

        });
    </script>
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

                              <%--  <%if (Session["Language"].ToString() == "Auto")
                                     { %>--%>
                                <h4 class="modal-title">
                                  <%--  <%}
    else
    { %>
                                    <h4 class="modal-title" dir="rtl">
                                        <%} %>--%>
                                        <asp:Label ID="lblModalTitle" runat="server" Text="Patient history" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                  <%--  <%if (Session["Language"].ToString() == "Auto")
                                          { %>--%>
                                    <div>
                                      <%--  <%}
    else
    { %>
                                        <div dir="rtl">

                                            <%} %>--%>
                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="5" meta:resourcekey="GridView2Resource1">

                                                <Columns>
                                                    <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource5">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' meta:resourcekey="LblDateResource1"></asp:Label>
                                                            <asp:Label ID="LblHosId1" runat="server" Text='<%# Eval("h_id") %>' Visible="False" meta:resourcekey="LblHosId1Resource1"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource6">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# Eval("d_id") %>' Visible="False" meta:resourcekey="Label2Resource1"></asp:Label>

                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Diagnosis" meta:resourcekey="TemplateFieldResource7">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_doc_daignose") %>' meta:resourcekey="LblDiagnoseResource1"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Prescription" meta:resourcekey="TemplateFieldResource8">
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
                                 <%--   <%if (Session["Language"].ToString() == "Auto")
                                           { %>--%>
                                    <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>
                                   <%-- <%}
    else
    { %>
                                    <button class="btn btn-primary pull-left" data-dismiss="modal" aria-hidden="true">قريب</button>

                                    <%} %>--%>
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
                            <%--    <%if (Session["Language"].ToString() == "Auto")
                                     { %>--%>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                               <%-- <%}
    else
    { %>
                                <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <%} %>--%>

                             <%--   <%if (Session["Language"].ToString() == "Auto")
                                         { %>--%>
                                <h4 class="modal-title">
                                <%--    <%}
    else
    { %>
                                    <h4 class="modal-title pull-right">

                                        <%} %>--%>

                                        <asp:Label ID="Label3" runat="server" Text="Hakkeem" meta:resourcekey="Label3Resource1"></asp:Label></h4>
                            </div>
                          <%--  <%if (Session["Language"].ToString() == "Auto")
                                  { %>--%>
                            <div class="modal-body">
                               <%-- <%}
    else
    { %>
                                <div class="modal-body" dir="rtl">
                                    <%} %>--%>
                                    <asp:Label ID="lblModalBody" runat="server" meta:resourcekey="lblModalBodyResource1"></asp:Label>

                                </div>

                                <div class="modal-footer">
                                  <%--  <%if (Session["Language"].ToString() == "Auto")
                                               { %>--%>
                                    <div>
                                       <%-- <%}
    else
    { %>
                                        <div class="pull-left">
                                            <%} %>--%>
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
         <%--    <%if (Session["Language"].ToString() == "Auto")
                    { %>--%>
            <div class="col-md-7">
              <%--  <%}else{ %>
                 <div class="col-md-7 pull-right">
                <%} %>--%>
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
                              <%--  <%}
                                else
                                { %>
                                <div class="pull-left">
                                    <%} %>--%>
                                    <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary pull-right" runat="server" Text="Submit" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                                </div>
                            </div>
                        </div>
                    </div>
                     <%-- <%if (Session["Language"].ToString() == "Auto")
                    { %>--%>
                    <div class="col-md-5">
                      <%--  <%}else{ %>
                         <div class="col-md-5 pull-left">
                        <%} %>--%>
                       <%-- <%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <div class="box box-primary">
                           <%-- <%}
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
                                            <asp:TemplateField HeaderText="User photo" meta:resourcekey="TemplateFieldResource1">
                                                <ItemTemplate>
                                                    <asp:Image ID="Image3" runat="server" CssClass="img-lg img-responsive" AlternateText="Photo" meta:resourcekey="Image3Resource1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource2">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("name") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Age" meta:resourcekey="TemplateFieldResource3">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("dob") %>' meta:resourcekey="Label5Resource1"></asp:Label>
                                                    <asp:Label ID="Label6" runat="server" Text="Label" meta:resourcekey="Label6Resource1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Gender">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("gender") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource4">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="history" ForeColor="#98120F" Font-Bold="True" meta:resourcekey="LinkButton2Resource1">Previous history</asp:LinkButton>
                                                    |
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="TestReports" CommandArgument='<%# Eval("u_hakkimid") %>' ForeColor="#98120F" Font-Bold="True" meta:resourcekey="LinkButton1Resource1">Test Reports</asp:LinkButton>
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


