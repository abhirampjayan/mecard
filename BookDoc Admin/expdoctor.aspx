<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="expdoctor.aspx.cs" Inherits="BookDoc_Admin_Create_Doctor" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../Design/plugins/datepicker/datepicker3.css" rel="stylesheet" />
    <script src="../Design/plugins/datepicker/bootstrap-datepicker.js"></script>
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
     <style type="text/css">
         #ContentPlaceHolder1_Label8{
             margin-left:-15px;
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
    <%--<link href="../css/AdminLTE.min.css" rel="stylesheet" />--%>
      <script src="../Design/dist/js/app.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <!-- SELECT2 EXAMPLE -->
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
                                  <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource1"></asp:Label>
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="Button2" runat="server" cssclass="btn btn-primary" Text="Close" OnClick="Button2_Click" UseSubmitBehavior="False" meta:resourcekey="Button2Resource1" />
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
            <div class="col-md-12">

              <div class="box box-primary">
                <div class="box-header">
                  <h3 class="box-title">
                      <asp:Label ID="Label3" runat="server" Text="Create doctors" meta:resourcekey="Label3Resource1"></asp:Label></h3>
                   <%--   <%if (Session["Language"].ToString() == "Auto")
                          { %>--%>
                 <div class="box-tools pull-right">
                     <%--<%}
    else
    { %>
                        <div class="pull-left">
                     <%} %>--%>
                                         <asp:Button ID="BtnNewDoc" runat="server" Text="New" CssClass="btn btn-primary btn-sm pull-right" OnClick="BtnNewDoc_Click" meta:resourcekey="BtnNewDocResource1" />

                     </div>
                </div>
                <div class="box-body">
                  <!-- Date dd/mm/yyyy -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label4" runat="server" Text="Doctor name" meta:resourcekey="Label4Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="* Enter first name" ControlToValidate="fname" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
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
                        <asp:TextBox ID="Lname" CssClass="form-control" placeholder="Last name" onkeyup="javascript:capitalize(this.id, this.value);" ValidationGroup="a" runat="server" meta:resourcekey="LnameResource1"></asp:TextBox>
                     <%-- <input type="text" class="form-control" data-inputmask="'alias': 'mm/dd/yyyy'" data-mask>--%>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                  <!-- phone mask -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label5" runat="server" Text="Doctor email" meta:resourcekey="Label5Resource1"></asp:Label></label><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="* Enter valid email" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red" ValidationGroup="a" ControlToValidate="email" Display="Dynamic" meta:resourcekey="RegularExpressionValidator3Resource1"></asp:RegularExpressionValidator>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="* Enter emai address" ForeColor="Red" ControlToValidate="email" Display="Dynamic" ValidationGroup="a" meta:resourcekey="RequiredFieldValidator7Resource1" ></asp:RequiredFieldValidator>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-at"></i>
                      </div>
                        <asp:TextBox ID="email" CssClass="form-control" TextMode="Email" placeholder="abc@gmail.com" runat="server" ValidationGroup="a" meta:resourcekey="emailResource1"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                  <!-- phone mask -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label6" runat="server" Text="Specialty" meta:resourcekey="Label6Resource1"></asp:Label></label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-dot-circle-o"></i>
                      </div>
                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server" meta:resourcekey="DropDownList1Resource1">
                        </asp:DropDownList>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
                     <div class="form-group">
                    <label>
                        <asp:Label ID="lbl2" runat="server" Text="City" meta:resourcekey="lbl2Resource1"></asp:Label></label>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-dot-circle-o"></i>
                      </div>
                        <asp:DropDownList ID="dl_city" CssClass="form-control" runat="server" meta:resourcekey="dl_cityResource1">
                        </asp:DropDownList>
                    </div><!-- /.input group -->
                  </div>
                  <!-- IP mask -->
                  <div class="form-group">
                    <label>
                        <asp:Label ID="Label7" runat="server" Text="Contact phone number" meta:resourcekey="Label7Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" ValidationGroup="a" ControlToValidate="phone" runat="server" ErrorMessage="* Enter contact number" Display="Dynamic" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                      <asp:RegularExpressionValidator ValidationExpression="^((?!(0))(?!(1))(?!(2))(?!(3))(?!(4))(?!(6))(?!(7))(?!(8))(?!(9))[0-9]{9})$" ID="RegularExpressionValidator2" ControlToValidate="phone" ForeColor="Red" ValidationGroup="a" runat="server" ErrorMessage="* Enter valid phone number" Display="Dynamic" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                    <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa">
                            <asp:Label ID="Label8" runat="server" Text="+966" meta:resourcekey="Label8Resource1"></asp:Label></i>
                      </div>
                        <asp:TextBox ID="phone" CssClass="form-control" ValidationGroup="a" runat="server" meta:resourcekey="phoneResource1" placeholder="start number with 5" MaxLength="9"></asp:TextBox><span id="mypopup" class="popuptext"></span>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->
                    <%-- <div class="form-group">
                    <label>Doctor City</label><asp:RequiredFieldValidator ID="RequiredFieldValidator6" ForeColor="Red" ValidationGroup="a" ControlToValidate="dcity" runat="server" ErrorMessage="* Doctor location"></asp:RequiredFieldValidator>
                        
                        <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-map-marker"></i>
                      </div>
                        <asp:TextBox ID="dcity" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->--%>
                    <%--<div class="form-group">
                    <label>Doctor Location (Zipcode)</label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="\d{5}(-\d{4})?$" ControlToValidate="zipcode" ForeColor="Red" ErrorMessage="* Enter valid zipcode"></asp:RegularExpressionValidator>
                        <div class="input-group">
                      <div class="input-group-addon">
                        <i class="fa fa-map"></i>
                      </div>
                        <asp:TextBox ID="zipcode" ValidationGroup="a" CssClass="form-control" runat="server"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->--%>
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
                        <asp:TextBox ID="dexpire" ValidationGroup="a" CssClass="form-control datepicker"  onkeydown="return false" onpaste="return false" runat="server" meta:resourcekey="dexpireResource1"></asp:TextBox>
                    </div><!-- /.input group -->
                  </div><!-- /.form group -->

                    <div class="form-group">
                         <asp:HyperLink ID="HyperLink1" Visible="false" runat="server" Text="Agreement" Target="_blank" meta:resourcekey="HyperLink1Resource1"></asp:HyperLink>
                        <asp:Panel ID="Panel1" runat="server" Visible="False" meta:resourcekey="Panel1Resource1">
                            <label>
                                <asp:Label ID="Label11" runat="server" Text="Agreement File" meta:resourcekey="Label11Resource1" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Please upload agreement file" ForeColor="Red" ControlToValidate="FileUpload2"  ValidationGroup="a" meta:resourcekey="RequiredFieldValidator4Resource1" Visible="False"></asp:RequiredFieldValidator></label>
                            <asp:FileUpload ID="FileUpload2" runat="server"  CssClass="form-control"  ValidationGroup="a" meta:resourcekey="FileUpload2Resource1" Visible="False"/>
                        </asp:Panel>
                    </div>

                    <div class="form-group">
                         <%-- <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <div>
                            <%--<%}
    else
    { %> <div class="pull-left">

                            <%} %>--%>
         <asp:Button ID="btn_cancel" runat="server"  CssClass="btn btn-sm btn-primary pull-right" Style="margin:5px;" Text="Cancel" meta:resourcekey="btn_cancelResource1" OnClick="btn_cancel_Click"  />
                        <asp:Button ID="Button1" runat="server" ValidationGroup="a"  CssClass="btn btn-sm btn-primary pull-right" Style="margin:5px;" Text="Update doctor" OnClick="Button1_Click" meta:resourcekey="Button1Resource1" />
       
                    </div>
                        </div>
                </div><!-- /.box-body -->
              </div><!-- /.box -->

             
            </div><!-- /.col (left) -->
           


               <!--//model popup for alert-->

       <%-- <asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg1Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" meta:resourcekey="BtnSubmitOTPResource1"/></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
        <!--//modal popup-->

                <%--<asp:Button ID="btnForAjax2" runat="server" Style="display: none" meta:resourcekey="btnForAjax2Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg2"
                TargetControlID="btnForAjax2" CancelControlID="btnclose2" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg2" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg2Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose2" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> </span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="Button3_Click" meta:resourcekey="Button3Resource1"/></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

          </div><!-- /.row -->
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
                 <script>
            $("#phone").focus(function () {
             //   $(this).attr('placeholder', 'أدخل رقم يبدأ ب 5')
            }).blur(function () {
                $(this).attr('placeholder', '987654321')
            })
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

