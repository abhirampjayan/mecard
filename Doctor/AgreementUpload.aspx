<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="AgreementUpload.aspx.cs" Inherits="Doctor_AgreementUpload" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../css/sweetalert.css" rel="stylesheet" />
       <link rel="stylesheet" href="../css/AdminLTE.min.css" />
        <link rel="stylesheet" href="../css/_all-skins.min.css" />
          <link rel="stylesheet" href="../css/bootstrap.min.css" />
    <link href="../css/intlTelInput.css" rel="stylesheet" />
    <script src="../js/sweetalert-dev.js"></script>
    <script src="../js/sweetalert.min.js"></script>
    <link href="//fonts.googleapis.com/css?family=Montserrat:400,700" rel="stylesheet">
    <link href="//fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet">

     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <!-- Theme style -->
 
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

        #a1:hover{
            color:#18bc9c;
        }

    </style>
    <script>
        document.querySelector('#BtnUpload').addEventListener('submit', function (e) {
            var form = this;
            e.preventDefault();
            swal({
                title: "Are you sure?",
                text: "You will not be able to recover this imaginary file!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, I am sure!',
                cancelButtonText: "No, cancel it!",
                closeOnConfirm: false,
                closeOnCancel: false,
               
                
            },
            function(isConfirm) {
                if (isConfirm) {
                    swal({
                        title: 'Shortlisted!',
                        text: 'Candidates are successfully shortlisted!',
                        type: 'success',
                        
                        
                    }, function() {
                        //form.submit();
                        document.getElementById("HiddenField1").value = 1;
                        return true;
                    });
                    
                } else {
                    swal("Cancelled", "Your imaginary file is safe :)", "error");
                    document.getElementById("HiddenField1").value = 0;
                    return false;
                }
            });
        });

    </script>
     <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js"></script>
    <link href="../css/AdminLTE.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="container-fluid">

          <!-- Bootstrap Modal Dialog -->
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" >
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                               <%--   <%if (Session["Language"].ToString() == "Auto")
                                      { %>--%>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                              <%--  <%}
    else
    { %>
                                  <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>

                                <%} %>--%>
                                 <%--  <%if (Session["Language"].ToString() == "Auto")
                                       { %>--%>
                                <h4 class="modal-title">
                                   <%-- <%}
    else
    { %>
                                        <h4 class="modal-title pull-right">
                                    <%} %>--%>
                                    <asp:Label ID="lblModalTitle" runat="server" Text="Hakkeem" meta:resourcekey="lblModalTitleResource1"></asp:Label></h4>
                            </div>
                            <%-- <%if (Session["Language"].ToString() == "Auto")
                                 { %>--%>
                            <div class="modal-body">
                               <%-- <%}
    else
    { %>
                                <div class="modal-body" dir="rtl">
                                <%} %>--%>
                                <asp:Label ID="lblModalBody" runat="server" meta:resourcekey="lblModalBodyResource1"></asp:Label>
                             
                            </div>
                                <%--  <%if (Session["Language"].ToString() == "Auto")
                                      { %>--%>
                            <div class="modal-footer">
                               <%-- <%}
    else
    { %>
                                 <div class="modal-footer" dir="rtl">
                                <%} %>--%>
                                <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-success" OnClick="Button2_Click" UseSubmitBehavior="False" meta:resourcekey="Button2Resource1" />
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button2" EventName="Click" />
                      
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>



        <div class="col-md-12">
            <div class="row">
                <div class="box box-primary">
                    <div class="box-header">
                        <%--  <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                         <h3 class="box-title">
                          <%--   <%}
    else
    { %>
                              <h3 class="box-title pull-right">
                             <%} %>--%>
                             <asp:Label ID="Label3" runat="server" Text="Upload Signed Agreement" meta:resourcekey="Label3Resource1"></asp:Label>
                        </h3>
                        <asp:Label ID="LblDocId" runat="server" Text="Label" Visible="False" meta:resourcekey="LblDocIdResource1"></asp:Label>
                    </div>
                    <div class="box-body">
                        <div class="form-group">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a" ErrorMessage="* Please choose a file" ControlToValidate="FileUpload1" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                            <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" ValidationGroup="a" meta:resourcekey="FileUpload1Resource1" />

                        </div>
                           <%-- <%if ( Session["Language"].ToString() == "Auto")
                                { %>--%>
                        <div class="form-group pull-right">
                         <%--   <%}
    else
    { %>
                             <div class="form-group pull-left">
                            <%} %>--%>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                            <asp:Button ID="BtnUpload" runat="server" Text="Upload" CssClass="btn  btn-primary"  ValidationGroup="a" meta:resourcekey="BtnUploadResource1" OnClick="BtnUpload_Click" />

                        </div>

                    </div>

                </div>
            </div>

        </div>
          <%--<asp:Button ID="btnForAjax" runat="server" Style="display: none" meta:resourcekey="btnForAjaxResource1" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg"
                TargetControlID="btnForAjax" CancelControlID="btnclose" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsgResource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label4" runat="server" Text="Hakkeem" meta:resourcekey="Label4Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                               <span style="margin-left:45%"><asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" OnClick="BtnSubmitOTP_Click" meta:resourcekey="BtnSubmitOTPResource1" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

        <%-- //////////////////////////////////////////////////////--%>
          <%--<asp:Button ID="btnForAjax1" runat="server" Style="display: none" meta:resourcekey="btnForAjax1Resource1" />
          <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg1"
                TargetControlID="btnForAjax1" CancelControlID="btnclose1" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxToolkit:ModalPopupExtender>

            <asp:Panel ID="pnlPopupMsg1" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg1Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label5" runat="server" Text="Hakkeem" meta:resourcekey="Label5Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose1" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial;"> <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                                <span style="margin-left:45%"><asp:Button ID="Button2" runat="server" Text="OK" CssClass="btn btn-success btn-xs" meta:resourcekey="Button2Resource1"/></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

       <%--  //////////////////////////////////////////////////////--%>
    </div> 
         </div>
     <script src="../js/app.min.js"></script>
</asp:Content>

