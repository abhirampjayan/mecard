<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Doctor profile.aspx.cs" Inherits="Doctor_Doctor_profile" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>
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
    </style>
   <%-- <link href="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/css/jquery-countryselector.css" rel="stylesheet" />
    <link href="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/css/jquery-countryselector.min.css" rel="stylesheet" />--%>
    <link href="http://www.jqueryscript.net/css/jquerysctipttop.css" rel="stylesheet" type="text/css">
  <%--  <script src="../Simple-Flexible-jQuery-Country-Select-Box-Plugin-countrySelector/src/js/jquery.countrySelector.js"></script>--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="Label3" runat="server" Text="Profile" meta:resourcekey="Label3Resource1"></asp:Label></h3>
        </div>
        <div class="box-body">
            <div class="row">
                <div class="col-md-7">
                    <div class="form-group">
                      <%--  <asp:DropDownList ID="drpDemo" runat="server" multiple="multiple" CssClass="form-control select2" data-placeholder="Select a State" >
                            <asp:ListItem>English</asp:ListItem>
                            <asp:ListItem>Arabic</asp:ListItem>
                            <asp:ListItem>Malayalam</asp:ListItem>
                            <asp:ListItem>Hindi</asp:ListItem>
                        </asp:DropDownList>--%>
                        <%--<asp:ListBox ID="drpDemo" runat="server" SelectionMode="Multiple" CssClass="tokenize-demo">
                             <asp:ListItem>English</asp:ListItem>
                            <asp:ListItem>Arabic</asp:ListItem>
                            <asp:ListItem>Malayalam</asp:ListItem>
                            <asp:ListItem>Hindi</asp:ListItem>
                        </asp:ListBox>
                        <script>$('.tokenize-demo').tokenize2();</script>--%>      
                        <asp:Label ID="LblDocPId" runat="server" Text="Label" Visible="False" meta:resourcekey="LblDocPIdResource1"></asp:Label>
                        <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-hover table-bordered" AutoGenerateRows="False" OnModeChanging="DetailsView1_ModeChanging" OnItemUpdating="DetailsView1_ItemUpdating" OnDataBound="DetailsView1_DataBound1" meta:resourcekey="DetailsView1Resource1" >
                            <Fields>
                                <asp:TemplateField HeaderText="Name" meta:resourcekey="TemplateFieldResource1">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxname" CssClass="form-control" Text='<%# Bind("d_name") %>' ValidationGroup="a" placeholder="Full name" runat="server" meta:resourcekey="TextBoxnameResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="name" runat="server" Text='<%# Bind("d_name") %>' meta:resourcekey="nameResource2"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Education" meta:resourcekey="TemplateFieldResource2">
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
                                <asp:TemplateField HeaderText="Location" meta:resourcekey="TemplateFieldResource3">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxLoc" CssClass="form-control" Text='<%# Bind("d_location") %>' ValidationGroup="a" placeholder="Zipcode" runat="server" meta:resourcekey="TextBoxLocResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Location" runat="server" Text='<%# Bind("d_location") %>' meta:resourcekey="LocationResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Experience" meta:resourcekey="TemplateFieldResource4">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxExp" CssClass="form-control" Text='<%# Bind("d_experience") %>' ValidationGroup="a" placeholder="Work experience" runat="server" meta:resourcekey="TextBoxExpResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="experience" runat="server" Text='<%# Bind("d_experience") %>' meta:resourcekey="experienceResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Specialties" meta:resourcekey="TemplateFieldResource5">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DropDownListSpec" ValidationGroup="a" CssClass="form-control" runat="server" meta:resourcekey="DropDownListSpecResource1" >
                                            <asp:ListItem meta:resourcekey="ListItemResource1">General</asp:ListItem>
                                              <asp:ListItem meta:resourcekey="ListItemResource2">Primary Care</asp:ListItem>
                                              <asp:ListItem meta:resourcekey="ListItemResource3">Childrens</asp:ListItem>
                                            
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="specialty" runat="server" Text='<%# Bind("d_specialties") %>' meta:resourcekey="specialtyResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Languages Spoken" meta:resourcekey="TemplateFieldResource6">
                                    <EditItemTemplate>
                                        <asp:CheckBoxList ID="Ckblanguages" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="form-control" meta:resourcekey="CkblanguagesResource1"></asp:CheckBoxList>
                                        <asp:ListBox ID="LsbLanguages" runat="server" SelectionMode="Multiple" CssClas="form-control select2" data-placeholder="Select a State" meta:resourcekey="LsbLanguagesResource1">
                                            <asp:ListItem meta:resourcekey="ListItemResource4">English</asp:ListItem>
                                             <asp:ListItem meta:resourcekey="ListItemResource5">Arabic</asp:ListItem>
                                             <asp:ListItem meta:resourcekey="ListItemResource6">Malayalam</asp:ListItem>
                                                <asp:ListItem meta:resourcekey="ListItemResource7">Hindi</asp:ListItem>
                                        </asp:ListBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Language" runat="server" meta:resourcekey="LanguageResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hospital Affiliations" meta:resourcekey="TemplateFieldResource7">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxHosAfl" CssClass="form-control" Text='<%# Bind("d_hospital_afili") %>' ValidationGroup="a" placeholder="Hospital affiliations" runat="server" meta:resourcekey="TextBoxHosAflResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="hospafili" runat="server" Text='<%# Bind("d_hospital_afili") %>' meta:resourcekey="hospafiliResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Permanant address" meta:resourcekey="TemplateFieldResource8">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxAdrs1" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Bind("d_address") %>' ValidationGroup="a" placeholder="Doctor address" runat="server" meta:resourcekey="TextBoxAdrs1Resource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="adrs1" runat="server" Text='<%# Bind("d_address") %>' meta:resourcekey="adrs1Resource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Hospital address" meta:resourcekey="TemplateFieldResource9">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxAdrs2" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Bind("d_address2") %>' ValidationGroup="a" placeholder="Hospital address" runat="server" meta:resourcekey="TextBoxAdrs2Resource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="adrs2" runat="server" Text='<%# Bind("d_address2") %>' meta:resourcekey="adrs2Resource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date of birth" meta:resourcekey="TemplateFieldResource10">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxDob" CssClass="form-control" Text='<%# Bind("d_dob") %>' ValidationGroup="a" TextMode="Date" runat="server" meta:resourcekey="TextBoxDobResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="dob" runat="server" Text='<%# Bind("d_dob") %>' meta:resourcekey="dobResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nationality" meta:resourcekey="TemplateFieldResource11">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="DdlNationality" runat="server"  CssClass="form-control" placeholder="choose to change country" meta:resourcekey="DdlNationalityResource1"></asp:DropDownList>
                                      
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Nationality" runat="server" Text='<%# Bind("d_country") %>' meta:resourcekey="NationalityResource2"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="About you" meta:resourcekey="TemplateFieldResource12">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBoxAbout" TextMode="MultiLine" Rows="3" CssClass="form-control" Text='<%# Bind("d_about_you") %>' ValidationGroup="a" placeholder="About you" runat="server" meta:resourcekey="TextBoxAboutResource1"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="aboutyou" runat="server" Text='<%# Bind("d_about_you") %>' meta:resourcekey="aboutyouResource1"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField meta:resourcekey="TemplateFieldResource13">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="update" meta:resourcekey="LinkButton2Resource1">Update</asp:LinkButton>
                                        &nbsp;
                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="cancel" meta:resourcekey="LinkButton3Resource1">Cancel</asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="edit" meta:resourcekey="LinkButton1Resource1">Edit profile</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Fields>
                        </asp:DetailsView>
                    </div>
                </div>
                <div class="col-md-5">
                    <div class="col-md-5">
                        <div class="form-group">
                            <%--<asp:TextBox ID="TxtDemoTag" runat="server" multiple class="multipleInputDynamic" data-url="../fastselect-master/demo/data.json" ></asp:TextBox>--%>
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

        </div>
       <%-- Profile successfully updated--%>

        <%--<asp:Button ID="btnForAjax3" runat="server" Style="display: none" meta:resourcekey="btnForAjax3Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender4" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3" BehaviorID="ModalPopupExtender4" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg3Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label4" runat="server" Text="Hakkeem" meta:resourcekey="Label4Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label7" runat="server" Text="Label" meta:resourcekey="Label7Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button4" runat="server" Text="OK" CssClass="btn btn-success btn-xs" meta:resourcekey="Button4Resource1"/></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

        <%-- Profile successfully updated--%>



        <%--Profile2 successfully updated--%>

       <%-- <asp:Button ID="btnForAjax4" runat="server" Style="display: none" meta:resourcekey="btnForAjax4Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg4"
                TargetControlID="btnForAjax4" CancelControlID="btnclose4" BehaviorID="ModalPopupExtender1" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg4" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg4Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label5" runat="server" Text="Hakkeem" meta:resourcekey="Label5Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose4" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label1" runat="server" Text="Label" meta:resourcekey="Label1Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button3" runat="server" Text="OK" CssClass="btn btn-success btn-xs" meta:resourcekey="Button3Resource2"/></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

        <%--Profile2 successfully updated--%>

       <%-- Profile photo updated--%>

       <%--  <asp:Button ID="btnForAjax5" runat="server" Style="display: none" meta:resourcekey="btnForAjax5Resource1" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg5"
                TargetControlID="btnForAjax5" CancelControlID="btnclose5" BehaviorID="ModalPopupExtender2" DynamicServicePath="">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg5" runat="server" CssClass="modalPopup" meta:resourcekey="pnlPopupMsg5Resource1">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">
                                <asp:Label ID="Label6" runat="server" Text="Hakkeem" meta:resourcekey="Label6Resource1"></asp:Label></h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose5" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label" meta:resourcekey="Label2Resource1"></asp:Label></span>

                            </div>
                            <div class="form-group">
                             <span style="margin-left:45%"><asp:Button ID="Button5" runat="server" Text="OK" CssClass="btn btn-success btn-xs" meta:resourcekey="Button5Resource1"/></span>
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>

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
</asp:Content>

