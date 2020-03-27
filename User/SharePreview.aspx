<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" Culture="en-US" AutoEventWireup="true" CodeFile="SharePreview.aspx.cs" Inherits="User_SharePreview" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
    <script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "lightgrey";
                

            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "white";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
          //  var GridView = row.parentNode;
            var GridView = objRef.parentNode.parentNode.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                var row = inputList[i].parentNode.parentNode;
                if (!inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                   
                    if (objRef.checked) {
                        inputList[i].checked = true;
                        headerCheckBox.checked = false;
                       // break;
                    }
                    else {
                        inputList[i].checked = false;
                        headerCheckBox.checked = checked;
                    }
                }
            }
          

        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        row.style.backgroundColor = "lightgrey";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "white";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }


        //function checkAll1(objRef) {
        //    var GridView = objRef.parentNode.parentNode.parentNode;
        //    var inputList = GridView.getElementsByTagName("input");
        //    for (var i = 0; i < inputList.length; i++) {
        //        //Get the Cell To find out ColumnIndex
        //        var row = inputList[i].parentNode.parentNode;
        //        if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
        //            if (objRef.checked) {
        //                //If the header checkbox is checked
        //                //check all checkboxes
        //                //and highlight all rows
        //                row.style.backgroundColor = "lightgrey";
        //                inputList[i].checked = true;
        //            }
        //            else {
        //                //If the header checkbox is checked
        //                //uncheck all checkboxes
        //                //and change rowcolor back to original
        //                if (row.rowIndex % 2 == 0) {
        //                    //Alternating Row Color
        //                    row.style.backgroundColor = "white";
        //                }
        //                else {
        //                    row.style.backgroundColor = "white";
        //                }
        //                inputList[i].checked = false;
        //            }
        //        }
        //    }
        //}
    </script>

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

   <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
      <section class="content" style="margin-top:1.5cm;margin-bottom:1cm;">
    <div class="container-fluid">
        <div style="margin-top: 2%">
            <div class="row">
                <div class="col-md-12">
                    <div class="box box-primary">
                        <div class="box-header">
                            <h3 class="box-title">
                                <asp:Label ID="Label1" runat="server" Text="Share your records and history with doctor" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                        </div>
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row">

                                                <div class="form-group ">
                                                    <div class="form-group">
                                                        <h4 class="box-title" style="color: #4aa9af;">
                                                            <asp:Label ID="Label2" runat="server" Text="Your records" meta:resourcekey="Label2Resource1"></asp:Label></h4>
                                                        <asp:Label ID="lblError" runat="server" Text="* To date must be greater than or equal to from date." Visible="False" ForeColor="Red" meta:resourcekey="lblErrorResource1"></asp:Label>
                                                    </div>


                                                    <div class="col-md-6">
                                                        <label>
                                                            <asp:Label ID="Label3" runat="server" Text=" From Date" meta:resourcekey="Label3Resource1"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please choose a date" ValidationGroup="a" ControlToValidate="TxtFromDate" ForeColor="Red" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator></label><asp:TextBox ID="TxtFromDate" runat="server" CssClass="form-control datepicker" placeholder="yyyy-mm-dd" onkeydown="return false" onpaste="return false" CausesValidation="True" meta:resourcekey="TxtFromDateResource1"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>
                                                            <asp:Label ID="Label4" runat="server" Text="To Date" meta:resourcekey="Label4Resource1"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please choose a date" ValidationGroup="a" ControlToValidate="TxtToDate" ForeColor="Red" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator></label>
                                                        <asp:TextBox ID="TxtToDate" runat="server" CssClass="form-control datepicker" placeholder="yyyy-mm-dd" onkeydown="return false" onpaste="return false" CausesValidation="True" AutoPostBack="True" OnTextChanged="TxtToDate_TextChanged" meta:resourcekey="TxtToDateResource1"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>
                                        <div class="row">
                                            <asp:Panel ID="PnlRecords" runat="server" Visible="False" meta:resourcekey="PnlRecordsResource1">
                                                <div class="col-md-9" style="margin-top: 10px;">
                                                    <asp:GridView ID="GrvShareRecords" runat="server" AutoGenerateColumns="False" CssClass="table-bordered table-hover table" meta:resourcekey="GrvShareRecordsResource1">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource1">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblRecordId" runat="server" Text='<%# Eval("id") %>' Visible="False" meta:resourcekey="LblRecordIdResource1"></asp:Label>
                                                                    <asp:Label ID="LblHistoryDate" runat="server" meta:resourcekey="LblHistoryDateResource1"></asp:Label>
                                                                    <asp:Label ID="LblDate" runat="server" Text='<%# Eval("date") %>' Visible="False" meta:resourcekey="LblDateResource1"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Blood Test" meta:resourcekey="TemplateFieldResource2">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblBlood" runat="server" Text='<%# Eval("blood_test_report") %>' Visible="False" meta:resourcekey="LblBloodResource1"></asp:Label>
                                                                    <asp:CheckBox ID="CkbBlood" runat="server" Text="select" onclick="CheckColum(this)" meta:resourcekey="CkbBloodResource1" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Urine Test" meta:resourcekey="TemplateFieldResource3">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Label1Urine" runat="server" Text='<%# Eval("urine_test_report") %>' Visible="False" meta:resourcekey="Label1UrineResource1"></asp:Label>
                                                                    <asp:CheckBox ID="CkbUrine" runat="server" Text="select" onclick="CheckColum(this)" meta:resourcekey="CkbUrineResource1" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Scan Report" meta:resourcekey="TemplateFieldResource4">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblScan" runat="server" Text='<%# Eval("scan_test_report") %>' Visible="False" meta:resourcekey="LblScanResource1"></asp:Label>
                                                                    <asp:CheckBox ID="CkbScan" runat="server" Text="select" meta:resourcekey="CkbScanResource1" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="X-ray Report" meta:resourcekey="TemplateFieldResource5">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblXRay" runat="server" Text='<%# Eval("xray_test_report") %>' Visible="False" meta:resourcekey="LblXRayResource1"></asp:Label>
                                                                    <asp:CheckBox ID="CkbXray" runat="server" Text="select" meta:resourcekey="CkbXrayResource1" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Other" meta:resourcekey="TemplateFieldResource6">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CkbOther" runat="server" meta:resourcekey="CkbOtherResource1" />
                                                                    <asp:Label ID="LblOther" runat="server" Text='<%# Eval("other_test_name") %>' meta:resourcekey="LblOtherResource1"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField meta:resourcekey="TemplateFieldResource7">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="CkbAllSelect" runat="server" Text="All" onclick="checkAll(this)" meta:resourcekey="CkbAllSelectResource1" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="CkbAll" runat="server" Text="select all " onclick="Check_Click(this)" meta:resourcekey="CkbAllResource1" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#4AA9AF" />
                                                    </asp:GridView>
                                                    <div class="form-group ">
                                                        <div class="col-md-12">
                                                            <asp:Button ID="BtnShare" runat="server" Text="Share records" CssClass="btn btn-sm btn-success pull-right" ValidationGroup="a" OnClick="BtnShare_Click" meta:resourcekey="BtnShareResource1" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row">
                                                <div class="form-group">
                                                    <div class="form-group">
                                                        <h4 class="box-title" style="color: #4aa9af;">
                                                            <asp:Label ID="Label5" runat="server" Text="Your History" meta:resourcekey="Label5Resource1"></asp:Label></h4>
                                                        <asp:Label ID="LblError1" runat="server" Text="* To date must be greater than or equal to from date." Visible="False" ForeColor="Red" meta:resourcekey="LblError1Resource1"></asp:Label>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>
                                                            <asp:Label ID="Label7" runat="server" Text="From Date" meta:resourcekey="Label7Resource1"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="* Please choose a date" ValidationGroup="b" ControlToValidate="txtHisFromDate" ForeColor="Red" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator></label>
                                                        <asp:TextBox ID="txtHisFromDate" runat="server" CssClass="form-control datepicker" placeholder="yyyy-mm-dd" onkeydown="return false" onpaste="return false" meta:resourcekey="txtHisFromDateResource1"></asp:TextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <label>
                                                            <asp:Label ID="Label6" runat="server" Text="To Date" meta:resourcekey="Label6Resource1"></asp:Label>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="* Please choose a date" ValidationGroup="b" ControlToValidate="txtHisToDate" ForeColor="Red" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator></label>
                                                        <asp:TextBox ID="txtHisToDate" runat="server" CssClass="form-control datepicker" placeholder="yyyy-mm-dd" onkeydown="return false" onpaste="return false" AutoPostBack="True" OnTextChanged="txtHisToDate_TextChanged" meta:resourcekey="txtHisToDateResource1"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <div class="row">
                                        <asp:Panel ID="PnlHistory" runat="server" Visible="False" meta:resourcekey="PnlHistoryResource1">
                                            <div class="col-md-9" style="margin-top: 10px;">
                                                <div class="table-responsive">
                                                <asp:GridView ID="GrvShareHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" meta:resourcekey="GrvShareHistoryResource1">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Date" meta:resourcekey="TemplateFieldResource8">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblHisId" runat="server" Text='<%# Eval("id") %>' Visible="False" meta:resourcekey="LblHisIdResource1"></asp:Label>
                                                                <asp:Label ID="LblHosId" runat="server" Text='<%# Eval("h_id") %>' Visible="False" meta:resourcekey="LblHosIdResource1"></asp:Label>
                                                                <asp:Label ID="LblHistoryDate" runat="server" meta:resourcekey="LblHistoryDateResource2"></asp:Label>
                                                                <asp:Label ID="LblDate" runat="server" Text='<%# Eval("a_date") %>' Visible="False" meta:resourcekey="LblDateResource2"></asp:Label>
                                                                <asp:Label ID="LblDocId" runat="server" Text='<%# Eval("d_id") %>' Visible="False" meta:resourcekey="LblDocIdResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Hospital" meta:resourcekey="TemplateFieldResource9">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblHistoryHospital" runat="server" meta:resourcekey="LblHistoryHospitalResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Doctor" meta:resourcekey="TemplateFieldResource10">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblHistoryDoctor" runat="server" meta:resourcekey="LblHistoryDoctorResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Time" meta:resourcekey="TemplateFieldResource11">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblHistoryTime" runat="server" Text='<%# Eval("a_time") %>' meta:resourcekey="LblHistoryTimeResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Reason" meta:resourcekey="TemplateFieldResource12">
                                                            <ItemTemplate>
                                                                <asp:Label ID="LblHistoryReason" runat="server" Text='<%# Eval("a_reason") %>' meta:resourcekey="LblHistoryReasonResource1"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField meta:resourcekey="TemplateFieldResource13">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="CkbALLSelect" runat="server" Text="select all" onclick="checkAll(this)" meta:resourcekey="CkbALLSelectResource2" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="select" onclick="Check_Click(this)" meta:resourcekey="CheckBox1Resource1" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="#4AA9AF" />
                                                </asp:GridView>
                                                    </div>
                                                <div class="form-group ">
                                                    <div class="col-md-12">
                                                        <asp:Button ID="BtnShareHistory" runat="server" Text="Share history" CssClass="btn btn-sm btn-success pull-right" ValidationGroup="b" OnClick="BtnShareHistory_Click" meta:resourcekey="BtnShareHistoryResource1" />
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>
                                    </div>

                                    <div class="row">
                                        <div class="form-group" style="margin-top: 15px;">
                                            <div class="col-md-6">
                                                <asp:Button ID="btnFinish" runat="server" Text="Finish" CssClass="btn btn-success pull-righ" OnClick="btnFinish_Click" meta:resourcekey="btnFinishResource1" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
        <!--//model popup for alert-->

        <%--<asp:Button ID="btnForAjax2" runat="server" Text="" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg2"
            TargetControlID="btnForAjax2" CancelControlID="btnclose2">
        </ajaxToolkit:ModalPopupExtender>

        <asp:Panel ID="pnlPopupMsg2" runat="server" CssClass="modalPopup">
            <div class="col-md-12">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <h3 class="box-title" style="text-transform: capitalize">Hakkeem</h3>
                        <div class="box-tools pull-right">
                            <button class="btn btn-box-tool" id="btnclose2" data-widget="remove"><i class="fa fa-remove"></i></button>
                        </div>
                    </div>
                    <div class="box-body">

                        <div class="form-group">

                            <span style="text-transform: initial">
                                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                        </div>
                        <div class="form-group">
                            <span style="margin-left: 45%">
                                <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" /></span>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>--%>
        <!--//modal popup-->


    </div>
          </section>
     <%-- <style type="text/css">
        #copy {
            width: 100%;
            padding: 20px 0;
            position: absolute;
            z-index: 1000000;
            color: #fff;
            background: #313131;
        
            bottom: 0;
        }
    </style>--%>
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>


    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />
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
                //todayHighlight: true,
                autoclose: true,

            });

        });
    </script>
</asp:Content>

