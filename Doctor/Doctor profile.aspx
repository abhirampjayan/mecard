<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Doctor profile.aspx.cs" Inherits="Doctor_Doctor_profile" Culture="en-US" meta:resourcekey="PageResource1" UICulture="en-US" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
    <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert-dev.js"></script>
    <script src="../js/sweetalert.min.js"></script>
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

        #fileup2 {
            padding-top: 2%;
            padding-left: 2%;
            width: 98%;
            height: 37px;
            border: solid 0.5px #d2d6de;
        }
    </style>
    <script src="../js/jquery-1.3.2.js" type="text/javascript"></script>
    <script src="../js/jquery.MultiFile.js" type="text/javascript"></script>
    <%-- <link href="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/css/jquery-countryselector.css" rel="stylesheet" />
    <link href="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/css/jquery-countryselector.min.css" rel="stylesheet" />--%>
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css">
    <%--  <script src="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/js/jquery.countrySelector.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>



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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <%-- <%if (Session["Language"].ToString() == "Auto")
         { %>--%>
    
     <div>

        <%-- <%}
    else
    { %>
           <div dir="rtl">
         <%} %>--%>


        <div class="row">
          <%--   <%if (Session["Language"].ToString() == "Auto")
                 { %>--%>
            <div class="col-md-7">
               <%-- <%}
    else
    { %>
                  <div class="col-md-7 pull-right">

                <%} %>--%>
                <div class="box box-primary">
                    <div class="form-group">

                        <asp:Label ID="LblDocPId" runat="server" Text="Label" Visible="False" meta:resourcekey="LblDocPIdResource1"></asp:Label>
                        <div class="table-responsive">
                            <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered" AutoGenerateRows="False" OnModeChanging="DetailsView1_ModeChanging" OnItemUpdating="DetailsView1_ItemUpdating" OnDataBound="DetailsView1_DataBound1" meta:resourcekey="DetailsView1Resource1">
                                <Fields>
                                    <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource1">
                                        <ItemTemplate>
                                            <span style="margin-left: 45%; font-weight: bolder; font-size: 16px; color: #4aa9af">
                                                <asp:Label ID="Label3" runat="server" Text="Personal" meta:resourcekey="Label3Resource1"></asp:Label></span>
                                        </ItemTemplate>
                                        <ItemStyle BackColor="#ecf0f5" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource2">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxname" CssClass="form-control" Text='<%# Bind("d_name") %>' ValidationGroup="a" placeholder="Full name" runat="server" meta:resourcekey="TextBoxnameResource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="name" runat="server" Text='<%# Bind("d_name") %>' meta:resourcekey="nameResource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Education" meta:resourcekey="TemplateFieldResource3">
                                        <EditItemTemplate>
                                            <div class="form-group">
                                                <asp:TextBox ID="TextBoxClg" CssClass="form-control" TextMode="MultiLine" Rows="3" Text='<%# Bind("d_college") %>' ValidationGroup="a" placeholder="College name" runat="server" meta:resourcekey="TextBoxClgResource1"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <asp:TextBox ID="TextBoxEdu" CssClass="form-control" TextMode="MultiLine" Rows="3" Text='<%# Bind("d_education") %>' ValidationGroup="a" placeholder="Qualifications" runat="server" meta:resourcekey="TextBoxEduResource1"></asp:TextBox>
                                            </div>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <div class="form-group">
                                                <asp:Label ID="college" runat="server" Text='<%# Bind("d_college") %>' meta:resourcekey="collegeResource1"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="education" runat="server" Text='<%# Bind("d_education") %>' meta:resourcekey="educationResource1"></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Languages Spoken" meta:resourcekey="TemplateFieldResource4">
                                        <EditItemTemplate>
                                            <asp:CheckBoxList ID="Ckblanguages" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="form-control" meta:resourcekey="CkblanguagesResource1"></asp:CheckBoxList>
                                            <asp:ListBox ID="LsbLanguages" runat="server" SelectionMode="Multiple" CssClas="form-control select2" data-placeholder="Select a State" meta:resourcekey="LsbLanguagesResource1">
                                                <asp:ListItem meta:resourcekey="ListItemResource1" Text="English"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource2" Text="Arabic"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource3" Text="Malayalam"></asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource4" Text="Hindi"></asp:ListItem>
                                            </asp:ListBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Language" runat="server" meta:resourcekey="LanguageResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Location" meta:resourcekey="TemplateFieldResource5">
                                        <EditItemTemplate>
                                              <asp:DropDownList ID="dl_city" CssClass="form-control"  runat="server" meta:resourcekey="TextBoxLocResource1">
                                    </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Location" runat="server" Text='<%# Bind("d_location") %>' meta:resourcekey="LocationResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Permanant address" meta:resourcekey="TemplateFieldResource6">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxAdrs1" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Bind("d_address") %>' ValidationGroup="a" placeholder="Doctor address" runat="server" meta:resourcekey="TextBoxAdrs1Resource1" ReadOnly="true"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="adrs1" runat="server" Text='<%# Bind("d_address") %>' meta:resourcekey="adrs1Resource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date of birth" meta:resourcekey="TemplateFieldResource7">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxDob" CssClass="form-control datepicker" onkeydown="return false" onpaste="return false" Text='<%# Bind("d_dob") %>' ValidationGroup="a" runat="server" meta:resourcekey="TextBoxDobResource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="dob" runat="server" Text='<%# Bind("d_dob") %>' meta:resourcekey="dobResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Nationality" meta:resourcekey="TemplateFieldResource8">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DdlNationality" runat="server" CssClass="form-control" placeholder="choose to change country" meta:resourcekey="DdlNationalityResource1"></asp:DropDownList>

                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Nationality" runat="server" Text='<%# Bind("d_country") %>' meta:resourcekey="NationalityResource2"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="About you" meta:resourcekey="TemplateFieldResource9">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxAbout" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Bind("d_about_you") %>' ValidationGroup="a" placeholder="About you" runat="server" meta:resourcekey="TextBoxAboutResource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="aboutyou" runat="server" Text='<%# Bind("d_about_you") %>' meta:resourcekey="aboutyouResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="False" meta:resourcekey="TemplateFieldResource10">
                                        <ItemTemplate>
                                            <span style="margin-left: 45%; font-weight: bolder; font-size: 16px; color: #4aa9af">
                                                <asp:Label ID="Label4" runat="server" Text="Professional" meta:resourcekey="Label4Resource1"></asp:Label></span>
                                        </ItemTemplate>
                                        <ItemStyle BackColor="#ecf0f5" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specialties" meta:resourcekey="TemplateFieldResource11">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="DropDownListSpec" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="DropDownListSpecResource1"  >
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="specialty" runat="server" Text='<%# Bind("d_specialties") %>' meta:resourcekey="specialtyResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Experience" meta:resourcekey="TemplateFieldResource12">
                                        <EditItemTemplate>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="a" ControlToValidate="TextBoxExp" runat="server" ForeColor="Red" ErrorMessage="* please enter your experience" meta:resourcekey="RequiredFieldValidator3Resource1"></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextBoxExp" CssClass="form-control" Text='<%# Bind("d_experience") %>' ValidationGroup="a" placeholder="Work experience" runat="server" meta:resourcekey="TextBoxExpResource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="experience" runat="server" Text='<%# Bind("d_experience") %>' meta:resourcekey="experienceResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Hospital Affiliations" meta:resourcekey="TemplateFieldResource13">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxHosAfl" CssClass="form-control" Text='<%# Bind("d_hospital_afili") %>' ValidationGroup="a" placeholder="Hospital affiliations" runat="server" meta:resourcekey="TextBoxHosAflResource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="hospafili" runat="server" Text='<%# Bind("d_hospital_afili") %>' meta:resourcekey="hospafiliResource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="Hospital Name &address" meta:resourcekey="TemplateFieldResource14">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxAdrs2" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Bind("d_address2") %>' ValidationGroup="a" placeholder="Hospital address" runat="server" meta:resourcekey="TextBoxAdrs2Resource1"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="adrs2" runat="server" Text='<%# Bind("d_address2") %>' meta:resourcekey="adrs2Resource1"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:TemplateField meta:resourcekey="TemplateFieldResource15">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton2" ValidationGroup="a" runat="server" CommandName="update" meta:resourcekey="LinkButton2Resource1" Text="Update"></asp:LinkButton>
                                            &nbsp;
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="cancel" meta:resourcekey="LinkButton3Resource1" Text="Cancel"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="edit" meta:resourcekey="LinkButton1Resource1" Text="Edit profile"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Fields>
                            </asp:DetailsView>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-5 connectedSortable">
                <div class="box box-primary box-solid">
                    <div class="box-header">
                        <asp:Label ID="Label1" runat="server" Text="Consultation fee" meta:resourcekey="Label1Resource1"></asp:Label>
                    </div>
                    <div class="box-body">
                        
                            <div class="input-group">
                                <asp:TextBox ID="fee" CssClass="form-control" placeholder="Enter your consultation fee" ValidationGroup="ff" MaxLength="3" runat="server" meta:resourcekey="feeResource1"></asp:TextBox>
                                
                                <span class="input-group-btn">
                                    <asp:DropDownList ID="DropDownList1" CssClass="btn btn-default" runat="server" meta:resourcekey="DropDownList1Resource1">
                                        <asp:ListItem meta:resourcekey="ListItemResource5">SAR</asp:ListItem>
                                        <asp:ListItem meta:resourcekey="ListItemResource6">USD</asp:ListItem>
                                        
                                    </asp:DropDownList>
                                </span>
                            </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="ff" ForeColor="Red" ControlToValidate="fee" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator4Resource1"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="ff" ForeColor="Red" ControlToValidate="fee" ValidationExpression="\d+" ErrorMessage="Enter only Numbers" meta:resourcekey="RegularExpressionValidator2Resource1"></asp:RegularExpressionValidator>
                        <div class="row">
                            <div class="col-md-6">
                                 <p style="margin-top:3%;font-size:small">
                                     <asp:Label ID="Label7" runat="server" Text="Current consultation fee" meta:resourcekey="Label7Resource1"></asp:Label></p>
                           <p style="font-size:200%;font-weight:bold"><asp:Label ID="Label2" runat="server" meta:resourcekey="Label2Resource1"></asp:Label></p> 
                            </div>
                            <div class="col-md-6">
                                <p style="margin-top:10%"><asp:Button ID="Button3" ValidationGroup="ff" CssClass="btn btn-primary btn-sm pull-right" runat="server" Text="Set your consultation fee" OnClick="Button3_Click" meta:resourcekey="Button3Resource2" /></p> 
                            </div>
                        </div>
                     

                    </div>
                </div>
            </div>
            <div class="col-md-5 connectedSortable">
                <div class="box box-solid box-primary">
                    <div class="box-header">
                        <asp:Label ID="Label5" runat="server" Text="Profile photo" meta:resourcekey="Label5Resource1"></asp:Label>

                    </div>
                    <div class="box-body">
                        <div class="col-md-3">
                            <div class="form-group">
                                <%--</div>--%>
                                <asp:Image ID="Image1" AlternateText="Doctor image" CssClass="img-responsive img-bordered" runat="server" meta:resourcekey="Image1Resource1" />
                            </div>
                        </div>
                        <div class="form-group">
                            <asp:FileUpload ID="FileUpload1" CssClass="form-control" runat="server" meta:resourcekey="FileUpload1Resource1" />
                        </div>
                        <div class="form-group">
                            <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary pull-right" runat="server" Text="Change photo" OnClick="Button1_Click" meta:resourcekey="Button1Resource2" />
                        </div>
                    </div>
                </div>
                <div class="box box-solid box-primary">
                    <div class="box-header">
                        <asp:Label ID="Label6" runat="server" Text="Update Certificate" meta:resourcekey="Label6Resource1"></asp:Label>

                    </div>
                    <div class="box-body">



                        <div class="form-group">

                            <%--<asp:FileUpload ID="FileUpload2" runat="server" class="multi" AllowMultiple="True" meta:resourcekey="FileUpload2Resource1" />--%>
                            <ajaxToolkit:AjaxFileUpload ID="AjaxFileUpload1" runat="server" OnUploadComplete="AjaxFileUpload1_UploadComplete" MaximumNumberOfFiles="0" meta:resourcekey="AjaxFileUpload1Resource1" AllowedFileTypes="pdf,jpg,jpeg,doc,png" />
                        </div>
                        <div class="form-group">
                            <%--</div>--%>


                            <asp:LinkButton ID="LinkButton5" CssClass="pull-right btn btn-xs btn-default" runat="server" OnClick="LinkButton5_Click" meta:resourcekey="LinkButton5Resource1" Text="Cancel"></asp:LinkButton>
                        </div>
                        <div class="form-group">

                            <%--<asp:Button ID="btnUpload" runat="server" CssClass="btn btn-sm btn-primary pull-right" OnClick="btnUpload_Click" Text="Upload All" meta:resourcekey="btnUploadResource1" />--%>
                            <asp:DataList ID="DataList1" runat="server" CssClass="table table-bordered" OnItemCommand="DataList1_ItemCommand" meta:resourcekey="DataList1Resource1">
                                <ItemTemplate>
                                    <table class="datepicker-inline">
                                        <tr>
                                            <td>
                                                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument='<%# Bind("certi") %>' CommandName="view" Text='<%# Bind("certi") %>' meta:resourcekey="LinkButton4Resource1"></asp:LinkButton>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="ImageButton1" CssClass="pull-right" runat="server" CommandArgument='<%# Bind("id") %>' CommandName="del" Height="20px" ImageUrl="../images/del.jpg" Width="20px" meta:resourcekey="ImageButton1Resource1" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>

                        </div>
                    </div>
                </div>
                <div class="box box-solid box-primary">
                    <div class="box-header">
                        <asp:Label ID="Label8" runat="server" Text="Change password" meta:resourcekey="Label8Resource1"></asp:Label>
                    </div>
                    <div class="box-body">
                        <div class="form-group">

                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="aaa" ControlToValidate="textbox1" ForeColor="Red" runat="server" ErrorMessage="* Required" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="TextBox1" CssClass="form-control" ValidationGroup="aaa" placeholder="Current password" TextMode="Password" runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="textbox2" ForeColor="Red" ValidationGroup="aaa" runat="server" ErrorMessage="* Required" Display="Dynamic" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="* Minimum 6 characters required" ControlToValidate="TextBox2" ForeColor="Red" ValidationGroup="aaa" Display="Dynamic" ValidationExpression="^[\s\S]{6,}$" meta:resourcekey="RegularExpressionValidator1Resource1"></asp:RegularExpressionValidator>
                                <asp:TextBox ID="TextBox2" CssClass="form-control" ValidationGroup="aaa" placeholder="New password" TextMode="Password" runat="server" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ControlToValidate="textbox3" ControlToCompare="textbox2" ValidationGroup="aaa" ErrorMessage="* Re-enter password" meta:resourcekey="CompareValidator1Resource1"></asp:CompareValidator>
                                <asp:TextBox ID="TextBox3" CssClass="form-control" placeholder="Re-enter password" TextMode="Password" runat="server" ValidationGroup="aaa" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="Button2" CssClass="btn btn-sm btn-primary pull-right" runat="server" Text="Change password" OnClick="Button2_Click" ValidationGroup="aaa" meta:resourcekey="Button2Resource2" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<asp:TextBox ID="TextBoxLang" Text="" ValidationGroup="a"  runat="server" multiple="multiple" CssClass="form-control select2" data-url="src/data.json"></asp:TextBox>--%>
    
        <%-- Profile photo updated--%>
    </div>
    <script src="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/js/jquery.countrySelector.js"></script>
    <script>$('.tokenize-demo').tokenize2();</script>
    <script type="text/javascript">
        //$(document).ready(function () {
        //    $("#DdlNationality").countrySelector({ value: 'FRA' });
        $('.multipleInputDynamic').fastselect();
        //});


    </script>
    <script src="../js/app.min.js"></script>
</asp:Content>

