<%@ Page Title="" Language="C#" MasterPageFile="~/Hospital/Hospital master.master" AutoEventWireup="true" CodeFile="Create hospital doctor.aspx.cs" Inherits="Hospital_Create_hospital_doctor" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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

        #a1:hover{
            color:#18bc9c;
        }

    </style>
   
    <script src="../js/bootstrap-datepicker.js"></script>
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
   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
     <%-- <% if (Session["Language"].ToString() == "Auto")
          {%>--%>
    <div class="container-fluid">
      <%--  <%}
    else
    { %>
         <div class="container-fluid" dir="rtl">
        <%} %>--%>
        <div style="margin-top:2%">

       
         <div class="row">
            <div class="col-md-12">

              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">
                      <asp:Label ID="Label2" runat="server" Text="Create doctors" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                </div>
                <div class="box-body">
                  <!-- Date dd/mm/yyyy -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label3" runat="server" Text="Doctor name" meta:resourcekey="Label3Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="* Enter first name" ControlToValidate="fname" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" ForeColor="Red" runat="server" ValidationGroup="a" ControlToValidate="fname" ValidationExpression="^[a-zA-Z]*$" ErrorMessage="*Valid characters: Alphabets only." meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                    <div class="input-group">
                       
                      <div class="input-group-addon">
                        <i class="fa fa-user"></i>
                      </div>
                         <asp:TextBox ID="Fname" CssClass="form-control" onkeyup="javascript:capitalize(this.id, this.value);" placeholder="First name" ValidationGroup="a" runat="server" meta:resourcekey="FnameResource1"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                  <!-- Date mm/dd/yyyy -->
                  <div class="form-group">
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-user"></i>
                      </div>
                        <asp:TextBox ID="Lname" CssClass="form-control" onkeyup="javascript:capitalize(this.id, this.value);" placeholder="Last name" ValidationGroup="a" runat="server" meta:resourcekey="LnameResource1"></asp:TextBox>
                     <%-- <input type="text" class="form-control" data-inputmask="'alias': 'mm/dd/yyyy'" data-mask>--%>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                  <!-- phone mask -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Doctor email" meta:resourcekey="Label4Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" Display="Dynamic" ErrorMessage="* Enter email adress" ValidationGroup="a" ForeColor="Red" ControlToValidate="email" meta:resourcekey="RequiredFieldValidator4Resource1" ></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="* Enter valid email" Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" ValidationGroup="a" ControlToValidate="email" meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-at"></i>
                      </div>
                        <asp:TextBox ID="email" CssClass="form-control" TextMode="Email" placeholder="abc@gmail.com" runat="server" meta:resourcekey="emailResource1"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                  <!-- phone mask -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Specialty" meta:resourcekey="Label5Resource1"></asp:Label></label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-dot-circle-o"></i>
                      </div>
                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" meta:resourcekey="DropDownList1Resource1">
                        </asp:DropDownList>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                  <!-- IP mask -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Contact phone number" meta:resourcekey="Label6Resource1"></asp:Label></label>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="a" Display="Dynamic" ControlToValidate="phone" runat="server" ErrorMessage="* Enter contact number" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ID="RegularExpressionValidator8" ValidationGroup="a" ControlToValidate="phone" runat="server"   Display="Dynamic" ForeColor="Red" ValidationExpression="^((?!(0))(?!(1))(?!(2))(?!(3))(?!(4))(?!(6))(?!(7))(?!(8))(?!(9))[0-9]{9})$"  ErrorMessage="* Enter a valid number start with 5" meta:resourcekey="RegularExpressionValidator8Resource1"></asp:RegularExpressionValidator>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa">+966</i>
                      </div>
                        <asp:TextBox ID="phone" onkeyup="myFunction()" placeholder="start number with 5"  CssClass="form-control" ValidationGroup="a" runat="server" meta:resourcekey="phoneResource1" MaxLength="9"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
                    <div class="form-group">
                    <label><asp:Label ID="Label7" runat="server" Text="Doctor Location " meta:resourcekey="Label7Resource1"></asp:Label></label>
                        <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-map"></i>
                      </div>
                    <%--    <asp:TextBox ID="zipcode" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="zipcodeResource1"></asp:TextBox>--%>
              <asp:DropDownList ID="DdlCity" runat="server" CssClass="form-control"  meta:resourcekey="zipcodeResource1"></asp:DropDownList>
                                  </div><!-- /.input group -->
                  </div><!-- /.form group -->
                    <div class="form-group">
                    <label>
                        <asp:Label ID="Label9" runat="server" Text="Doctor identification number" meta:resourcekey="Label9Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ValidationGroup="a" ControlToValidate="dnumber" runat="server" ErrorMessage="* Doctor identifiaction number" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-sellsy"></i>
                      </div>
                        <asp:TextBox ID="dnumber" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="dnumberResource1"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
                     <div class="form-group">
                    <label>
                        <asp:Label ID="Label10" runat="server" Text="Expire date of identification number" meta:resourcekey="Label10Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator5" ForeColor="Red" ValidationGroup="a" ControlToValidate="dexpire" runat="server" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator5Resource1"></asp:RequiredFieldValidator>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-calendar-times-o"></i>
                      </div>
                        <asp:TextBox ID="dexpire" ValidationGroup="a" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" runat="server" meta:resourcekey="dexpireResource1"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
                   
                    <div class="form-group">
                     <%--   <% if (Session["Language"].ToString() == "Auto")
                            {%>--%>
                        <div>
                            <%--<%}
    else
    { %>
                            <div class="pull-left">
                            <%} %>--%>
                        <asp:Button ID="Button1" runat="server" ValidationGroup="a" CssClass="btn btn-sm btn-primary pull-right" Text="Create doctor" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
                    </div>
                        </div>
                </div><!-- /.box-body -->
              </div><!-- /.box -->

             
            </div><!-- /.col (left) -->
           
          </div><!-- /.row -->
             </div>

         <!--//model popup for alert-->

       <%-- <asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label11" runat="server" Text="Hakkeem" meta:resourcekey="Label11Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" meta:resourcekey="BtnSubmitOTPResource1" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <!--//modal popup-->
       <%--   <asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">
               
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label12" runat="server" Text="Hakkeem" meta:resourcekey="Label12Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label8" runat="server" Text="Label" meta:resourcekey="Label8Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-primary btn-xs" OnClick="Button2_Click" meta:resourcekey="Button2Resource1"/></span>
                            </div>
                        </div>
                    </div>

              
            </asp:Panel>--%>

         <!-- Bootstrap Modal Dialog -->
            <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h4 class="modal-title">
                                        <asp:Label ID="lblModalTitle" runat="server" Text="Hakkeem" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                                </div>
                                <div class="modal-body">
                                    <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button2" runat="server" Text="Close" cssclass="btn btn-primary" OnClick="Button2_Click" UseSubmitBehavior="False" meta:resourcekey="Button2Resource1" />
                                </div>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>


    </div>
  <script type="text/javascript">
  function capitalize(textboxid, str) {
      // string with alteast one character
      if (str && str.length >= 1)
      {       
          var firstChar = str.charAt(0);
          var remainingStr = str.slice(1);
          str = firstChar.toUpperCase() + remainingStr;
      }
      document.getElementById(textboxid).value = str;
  }
  </script>
              
                 <script src="../js/sweetalert-dev.js"></script>
        <script src="../js/sweetalert.min.js"></script>
                      <script>
            $("#phone").keyup(function (e) {
                $("#mypopup").html('');

                var validstr = '';
                var dInput = $(this).val();
                var numpattern = /^\d+$/;




                for (var i = 0; i < dInput.length; i++) {

                    if ((i == 0)) {
                        if (numpattern.test(dInput[i])) {
                            console.log('validnum' + dInput[i]);
                            validstr += dInput[i];
                            if (+dInput[i] == 5) {

                            }
                            else {

                                swal("Start No. with 5");
                                $(this).val('');
                                return false;

                            }

                        }
                        else {
                            //$("#mypopup").html("Digits Only").show();
                            swal("***Enter a number***");

                        }
                    }

                    if ((i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 8 || i == 9)) {
                        if (numpattern.test(dInput[i])) {
                            console.log('validnum' + dInput[i]);
                            validstr += dInput[i];
                        } else {
                            $("#mypopup").html("Digits Only").show();
                            swal("**Enter a number***");


                        }
                    }

                }

                $(this).val(validstr);
                return false;

            });
        </script>
</asp:Content>

