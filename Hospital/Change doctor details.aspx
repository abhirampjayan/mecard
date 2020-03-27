<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Change doctor details.aspx.cs" Inherits="Hospital_Today_appointments" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            color: #18bc9c;
        }
    </style>
  

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
      <script type="text/javascript">
        function getConfirmation(sender, title, message) {
            $("#spnTitle").text(title);
            $("#spnMsg").text(message);
            $('#modalPopUp').modal('show');
            $('#btnConfirm').attr('onclick', "$('#modalPopUp').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function Myconfirm() {
            //var reslt = confirm('Do yo Want to delete this doctor?');
            var reslt = mscConfirm(title, subtitle, okCallback);
            if (reslt) {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
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
      <%--<% if (Session["Language"].ToString() == "Auto")
          {%>--%>
    <div class="container-fluid">
      <%--  <%}
    else
    { %>
          <div class="container-fluid" dir="rtl">

        <%} %>--%>
        <div class="col-md-12">

              <div id="modalPopUp" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">
                            <span id="spnTitle"></span>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <span id="spnMsg"></span>.
                        </p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm" class="btn btn-primary">
                            Yes, please</button>
                    </div>
                </div>
            </div>
        </div>


            <div style="margin-top: 3%">
                <div class="row">

                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="Label13" runat="server" Text="Change doctors details" meta:resourcekey="Label13Resource1"></asp:Label>
                            </h3>
                            <asp:Label ID="LblDoctor" runat="server" Visible="False" Text="Label" meta:resourcekey="LblDoctorResource1"></asp:Label>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" meta:resourcekey="DropDownList1Resource1"></asp:DropDownList>
                            </div>
                            <div class="row">
                                 <%-- <% if (Session["Language"].ToString() == "Auto")
                                      {%>--%>
                                <div class="col-md-8">
                                   <%-- <%}
    else
    { %>
                                    <div class="col-md-8 pull-right">

                                    <%} %>--%>
                                    <div class="form-group">

                                        <asp:DetailsView ID="DetailsView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateRows="False" OnItemUpdating="DetailsView1_ItemUpdating" OnModeChanging="DetailsView1_ModeChanging" OnDataBound="DetailsView1_DataBound" meta:resourcekey="DetailsView1Resource1">
                                            <Fields>
                                                <asp:TemplateField HeaderText="Qualifications" meta:resourcekey="TemplateFieldResource1">
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="dqlfcn" CssClass="form-control" Text='<%# Bind("hd_education") %>' runat="server" meta:resourcekey="dqlfcnResource1"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("hd_education") %>' meta:resourcekey="Label2Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="College" meta:resourcekey="TemplateFieldResource2">
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="dclg" CssClass="form-control" Text='<%# Bind("hd_college") %>' runat="server" meta:resourcekey="dclgResource1"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("hd_college") %>' meta:resourcekey="Label3Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="About doctor" meta:resourcekey="TemplateFieldResource3">
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="dabout" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Bind("hd_about_you") %>' runat="server" meta:resourcekey="daboutResource1"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("hd_about_you") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specialties" meta:resourcekey="TemplateFieldResource4">
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="dspec" runat="server" CssClass="from-control" meta:resourcekey="dspecResource1">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("hd_specialties") %>' meta:resourcekey="Label5Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Doctor location" meta:resourcekey="TemplateFieldResource9" >
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:DropDownList ID="dl_city" runat="server" CssClass="from-control" meta:resourcekey="dl_cityResource1" >
                                                            </asp:DropDownList>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label15" runat="server" Text='<%# Bind("hd_location") %>' meta:resourcekey="Label15Resource1" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Contact number" meta:resourcekey="TemplateFieldResource5">
                                                    <EditItemTemplate>

                                                        <div class="form-group">

                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="a" Display="Dynamic" ControlToValidate="dcontact" runat="server" ErrorMessage="* Enter contact number" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="a" ControlToValidate="dcontact" runat="server"   Display="Dynamic" ForeColor="Red" ValidationExpression="^((?!(0))(?!(1))(?!(2))(?!(3))(?!(4))(?!(6))(?!(7))(?!(8))(?!(9))[0-9]{9})$"  ErrorMessage="* Enter a valid number start with 5" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                                        
                                                            <div class="input-group">
                                                                <div class="input-group-addon">
                                                                    <i class="fa">+966</i>
                                                                </div>
                                                                <asp:TextBox ID="dcontact" CssClass="form-control" runat="server" meta:resourcekey="dcontactResource1" MaxLength="9"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" runat="server" Text='<%# Bind("hd_contact") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Doctor address" meta:resourcekey="TemplateFieldResource6">
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="dpadrs" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Permanant address" Text='<%# Bind("hd_address") %>' runat="server" meta:resourcekey="dpadrsResource1"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%# Bind("hd_address") %>' meta:resourcekey="Label8Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hospital address" meta:resourcekey="TemplateFieldResource7">
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="dtadrs" TextMode="MultiLine" Rows="3" CssClass="form-control" placeholder="Hospital address" Text='<%# Bind("hd_address2") %>' runat="server" meta:resourcekey="dtadrsResource1"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("hd_address2") %>' meta:resourcekey="Label9Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of birth" meta:resourcekey="TemplateFieldResource8">
                                                    <EditItemTemplate>
                                                        <div class="form-group">
                                                            <asp:TextBox ID="ddob" onkeydown="return false" onpaste="return false" CssClass="form-control datepicker" Text='<%# Bind("hd_dob") %>' runat="server" meta:resourcekey="ddobResource1"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%# Bind("hd_dob") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField meta:resourcekey="TemplateFieldResource9">
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton2" ForeColor="#4AA9AF" runat="server" CommandName="update" meta:resourcekey="LinkButton2Resource1">Update</asp:LinkButton>
                                                        &nbsp;
                                            <asp:LinkButton ID="LinkButton3" ForeColor="#4AA9AF" runat="server" CommandName="cancel" meta:resourcekey="LinkButton3Resource1">Cancel</asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="#4AA9AF" CommandName="edit" meta:resourcekey="LinkButton1Resource1">Edit doctor profile</asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Fields>
                                        </asp:DetailsView>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:Image ID="Image1" AlternateText="Doctor photo" CssClass="img-thumbnail img-responsive" runat="server" meta:resourcekey="Image1Resource1" Height="15em" />
                                    </div>
                                    <div class="form-group">
                                        <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" meta:resourcekey="FileUpload1Resource1" />
                                    </div>
                                    <div class="form-group">
                                          <%--<% if (Session["Language"].ToString() == "Auto")
                                              {%>--%>
                                        <div>
                                           <%-- <%}
    else
    { %>
                                             <div class="pull-left">

                                            <%} %>--%>
                                        <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary pull-right" runat="server" Text="Change photo" OnClick="Button1_Click" meta:resourcekey="Button1Resource2" />
                                            </div>
                                        <%--<asp:Button ID="BtnDeleteDoc" runat="server" Text="Delete Doctor" CssClass="btn btn-sm btn-danger pull-left" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this selected availabilities?');"  OnClick="BtnDeleteDoc_Click" meta:resourcekey="BtnDeleteDocResource1" />--%>
                                        

                                        <p>
                                    </div>
                                </div>
                            </div>


                        </div>
                    </div>

                </div>
            </div>
        </div>

   
















        <!--//model popup for alert-->

        <%--<asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
            TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label16" runat="server" Text="Hakkeem" meta:resourcekey="Label16Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" OnClick="BtnSubmitOTP_Click" meta:resourcekey="BtnSubmitOTPResource1" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>
        <!--//modal popup-->

        <!--//model popup for alert-->

      <%--  <asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
            TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg1Resource1">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label17" runat="server" Text="Hakkeem" meta:resourcekey="Label17Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label10" runat="server" Text="Label" meta:resourcekey="Label10Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" OnClick="BtnSubmitOTP_Click" meta:resourcekey="Button3Resource2" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>
        <!--//modal popup-->


        <!--//model popup for alert-->

        <%--<asp:Button ID="btnForAjax2" runat="server" Style="display: none" meta:resourcekey="btnForAjax2Resource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
            TargetControlID="btnForAjax2" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender3" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label18" runat="server" Text="Hakkeem" meta:resourcekey="Label18Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose2" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label11" runat="server" Text="Label" meta:resourcekey="Label11Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button4" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button4_Click" meta:resourcekey="Button4Resource1" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>
        <!--//modal popup-->


       <%-- <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender5" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg4"
            TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender5" DynamicServicePath="">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg4" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg4Resource1">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">
                            <asp:Label ID="Label19" runat="server" Text="Hakkeem" meta:resourcekey="Label19Resource1"></asp:Label></h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label12" runat="server" Text="Label" meta:resourcekey="Label12Resource1"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="Button7" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button7_Click" meta:resourcekey="Button7Resource1" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>


    </div>

</asp:Content>

