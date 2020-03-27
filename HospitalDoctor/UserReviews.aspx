<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalDoctor/HospitalDoctorMaster.master" AutoEventWireup="true" CodeFile="UserReviews.aspx.cs" Inherits="HospitalDoctor_UserReviews" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.9.0/jquery.min.js"></script>
    <script src="../Design/plugins/slimScroll/jquery.slimscroll.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#Div1').slimScroll({
                height: '600px'

            });
        });
    </script>
    <div class="container-fluid">
        <div style="margin-top: 2%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Timer ID="Timer1" runat="server" Interval="1000"></asp:Timer>
                </ContentTemplate>
            </asp:UpdatePanel>




            <div class="table-responsive">
                <div id="Div1">
                  
                    <asp:DataList ID="DataList1" CssClass="table" runat="server" meta:resourcekey="DataList1Resource1">
                        <ItemTemplate>
                            
                            <div class="box box-primary">

                                <div class="box-body">


                                    <div class="row">
                                       <%-- <%if (Session["Language"].ToString() == "Auto")
                                            {%>--%>
                                        <div class="col-md-3">
                                           <%-- <%}
    else
    { %>
                                            <div class="col-md-3 pull-right">
                                                <%} %>--%>
                                            <asp:Label ID="Label6" Visible="False" runat="server" Text='<%# Bind("u_email") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                            <asp:Label ID="Label2" Font-Size="Medium" Font-Bold="True" runat="server" Text='<%# Eval("name") %>' meta:resourcekey="Label2Resource1"></asp:Label><br />
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("date") %>' meta:resourcekey="Label3Resource1"></asp:Label><br />
                                            <asp:Label ID="Label4" runat="server" Text='<%# Eval("time") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                        </div>
                                        <div class="col-md-9">
                                            <p>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("u_review") %>' meta:resourcekey="Label5Resource1"></asp:Label>

                                            </p>
                                        </div>
                                    </div>
                                </div>
                                <div class="box-footer">
                                     <%--<%if (Session["Language"].ToString() == "Auto")
                                         {%>--%>
                                    <div class="row">
                                       <%-- <%}
    else
    { %>
                                        <div class="row" dir="rtl">
                                        <%} %>--%>
                                        <div class="col-md-4">
                                            <span class="btn btn-default btn-sm">
                                                <asp:Label ID="Label1" runat="server" Text="Bedside manner" meta:resourcekey="Label1Resource2"></asp:Label> <asp:Literal ID="Literal1" runat="server" meta:resourcekey="Literal1Resource1"></asp:Literal></span>
                                        </div>
                                         <div class="col-md-4">
                                             <span class="btn btn-default btn-sm"><asp:Label ID="Label7" runat="server" Text=" Waiting time" meta:resourcekey="Label7Resource2"></asp:Label> <asp:Literal ID="Literal2" runat="server" meta:resourcekey="Literal2Resource1"></asp:Literal></span> 
                                         </div>
                                        <div class="col-md-4">
                                           <span class="btn btn-default btn-sm"><asp:Label ID="Label8" runat="server" Text=" Service" meta:resourcekey="Label8Resource1"></asp:Label> <asp:Literal ID="Literal3" runat="server" meta:resourcekey="Literal3Resource1"></asp:Literal></span> 
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>

                </div>
            </div>


        </div>
    </div>
    <div>
       <%-- <asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
            TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" Width="40%" meta:resourcekey="pnlPopupMsg1Resource1">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label7" runat="server" Text="Hakkeem" meta:resourceKey="Label7Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label1" runat="server" Text="Label" meta:resourceKey="Label1Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="BtnOk" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="BtnOk_Click" meta:resourceKey="BtnOkResource1" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>
    </div>
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
            width: 1000px;
            height: 300px;
        }
    </style>
</asp:Content>

