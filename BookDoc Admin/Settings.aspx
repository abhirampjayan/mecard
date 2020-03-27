<%@ Page Title="" Language="C#" MasterPageFile="~/BookDoc Admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="BookDoc_Admin_Settings" Culture="auto" meta:resourcekey="PageResource1" UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .gridview {
            background-color: #fff;
            padding: 2px;
            margin: 2% auto;
        }

            .gridview a {
                margin: auto 1%;
                border-radius: 50%;
                background-color: #4aa9af;
                padding: 5px 10px 5px 10px;
                color: #fff;
                text-decoration: none;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
            }

                .gridview a:hover {
                    background-color: rgba(199, 198, 198, 0.28);
                    color: #4aa9af;
                }

            .gridview span {
                background-color: #fff;
                color: #4aa9af;
                -o-box-shadow: 1px 1px 1px #111;
                -moz-box-shadow: 1px 1px 1px #111;
                -webkit-box-shadow: 1px 1px 1px #111;
                box-shadow: 1px 1px 1px #111;
                border-radius: 50%;
                padding: 5px 10px 5px 10px;
            }
    </style>
    <script src="../user/js/jquery.min.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {

            $("#<%=TextBox4.ClientID %>").autocomplete({


                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("../Service.asmx/GetCityName") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        width: '5%',
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))

                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });

                },
                select: function (e, i) {
                    $("#<%=hfcityId.ClientID %>").val(i.item.val);
                    $("#<%=TextBox4.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

        });
    </script>

    <script type="text/javascript">

        //function CheckParts() {
        //    __doPostBack('', '');
        //};
        $(document).ready(function () {

            $("#<%=TextBox2.ClientID %>").autocomplete({


                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("../Service.asmx/GetSpecialityName") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item.split('-')[0],
                                    val: item.split('-')[1]
                                }
                            }))

                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });

                },
                select: function (e, i) {
                    $("#<%=hfspId.ClientID %>").val(i.item.val);
                    $("#<%=TextBox2.ClientID %>").val(i.item.label);
                    CheckParts();
                },
                minLength: 1
            });


            //

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

        function getConfirmation1(sender, title, message) {
            $("#spnTitle1").text(title);
            $("#spnMsg1").text(message);
            $('#modalPopUp1').modal('show');
            $('#btnConfirm1').attr('onclick', "$('#modalPopUp1').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }

        function getConfirmation2(sender, title, message) {
            $("#spnTitle2").text(title);
            $("#spnMsg2").text(message);
            $('#modalPopUp2').modal('show');
            $('#btnConfirm2').attr('onclick', "$('#modalPopUp2').modal('hide');setTimeout(function(){" + $(sender).prop('href') + "}, 50);");
            return false;
        }

    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    

      <div id="modalPopUp" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--  <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <%--  <%}
    else
    { %>
                         <button type="button" class="close pull-left" data-dismiss="modal">&times;</button>
                        <%} %>--%>
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
                      <%--  <%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm" class="btn btn-primary">
                            Yes, please</button>
                      <%--  <%}
    else
    { %>
                       <div class="pull-left">
                        <button type="button" id="btnConfirm" class="btn btn-primary">  نعم فعلا, رجاء</button>
                             <button type="button" class="btn btn-default" data-dismiss="modal">قريب</button>
                          </div>
                        <%} %>--%>
                    </div>
                </div>
            </div>
        </div>


      <div id="modalPopUp1" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <%--  <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <%--  <%}
    else
    { %>
                         <button type="button" class="close pull-left" data-dismiss="modal">&times;</button>
                        <%} %>--%>
                        <h4 class="modal-title">
                            <span id="spnTitle1"></span>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <span id="spnMsg1"></span>.
                        </p>
                    </div>
                    <div class="modal-footer">
                        <%--<%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm1" class="btn btn-primary">
                            Yes, please</button>
                      <%--  <%}
    else
    { %>
                       <div class="pull-left">
                        <button type="button" id="btnConfirm1" class="btn btn-primary">  نعم فعلا, رجاء</button>
                             <button type="button" class="btn btn-default" data-dismiss="modal">قريب</button>
                          </div>
                        <%} %>--%>
                    </div>
                </div>
            </div>
        </div>


      <div id="modalPopUp2" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                         <%-- <%if (Session["Language"].ToString() == "Auto")
                              { %>--%>
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                      <%--  <%}
    else
    { %>
                         <button type="button" class="close pull-left" data-dismiss="modal">&times;</button>
                        <%} %>--%>
                        <h4 class="modal-title">
                            <span id="spnTitle2"></span>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <p>
                            <span id="spnMsg2"></span>.
                        </p>
                    </div>
                    <div class="modal-footer">
                      <%--  <%if (Session["Language"].ToString() == "Auto")
                            { %>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="btnConfirm2" class="btn btn-primary">
                            Yes, please</button>
                       <%-- <%}
    else
    { %>
                       <div class="pull-left">
                        <button type="button" id="btnConfirm2" class="btn btn-primary">  نعم فعلا, رجاء</button>
                             <button type="button" class="btn btn-default" data-dismiss="modal">قريب</button>
                          </div>
                        <%} %>--%>
                    </div>
                </div>
            </div>
        </div>

    <div class="row">
        <div class="col-md-6">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="Label1" runat="server" Text="Add Cities" meta:resourcekey="Label1Resource1"></asp:Label></h3>
                    <br />

                    <div class="form-group">
                        <div class="input-group">
                            <asp:HiddenField ID="hfcityId" runat="server" />
                            <asp:TextBox ID="TextBox4" CssClass="form-control" ValidationGroup="vsc" placeholder="Search city" AutoPostBack="True" runat="server" meta:resourcekey="TextBox4Resource1" OnTextChanged="TextBox4_TextChanged"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" ValidationGroup="vsc" Text="Search" meta:resourcekey="Button3Resource1" OnClick="Button3_Click" />
                            </span>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="input-group">
                            <%--<asp:TextBox ID="TextBoxcity" CssClass="form-control" placeholder="Enter city" ValidationGroup="c" runat="server"></asp:TextBox>--%>
                            <asp:TextBox ID="TxtCity" runat="server" CssClass="form-control" placeholder="Enter city " ValidationGroup="vcity" meta:resourcekey="TxtCityResource1"></asp:TextBox>
                            <span class="input-group-btn">
                                <%--<asp:Button ID="Button4" runat="server" CssClass="btn btn-primary" ValidationGroup="c" Text="Add" OnClick="Button4_Click" />--%>
                                <asp:Button ID="BtnAddCity" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="BtnAddCity_Click" ValidationGroup="vcity" meta:resourcekey="BtnAddCityResource1" />
                            </span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="* Please enter a city" ForeColor="Red" ControlToValidate="TxtCity" ValidationGroup="vcity" meta:resourcekey="RequiredFieldValidator1Resource1"></asp:RequiredFieldValidator>
                    </div>








                </div>
                <div class="box-body">
                    <div class="form-group">

                        <asp:GridView ID="GrvCities" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="id" OnPageIndexChanging="GrvCities_PageIndexChanging" OnRowDeleting="GrvCities_RowDeleting" PageSize="15" meta:resourcekey="GrvCitiesResource1" OnRowEditing="GrvCities_RowEditing" OnRowUpdating="GrvCities_RowUpdating" OnRowCancelingEdit="GrvCities_RowCancelingEdit">
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource5"><ItemTemplate><%#Container.DisplayIndex+1 %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="#4aa9af" HeaderText="Cities" meta:resourcekey="TemplateFieldResource1">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Eval("City") %>' CssClass="form-control" meta:resourcekey="TextBox3Resource1"></asp:TextBox>
                                        <asp:Label ID="Label4" Visible="False" runat="server" Text='<%# Eval("id") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="LblCity" runat="server" Text='<%# Eval("City") %>' meta:resourcekey="LblCityResource1"></asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle ForeColor="#4aa9af"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-ForeColor="#4aa9af" HeaderText="###" meta:resourcekey="TemplateFieldResource2">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton7" runat="server" CssClass="btn btn-primary btn-xs" CommandName="update" OnClientClick="return getConfirmation1(this, 'Please confirm','Are you sure, you want to update this data?');" meta:resourcekey="LinkButton7Resource1" Text="Update"></asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton8" runat="server" CssClass="btn btn-primary btn-xs" CommandName="cancel" meta:resourcekey="LinkButton8Resource1" Text="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>

                                        <asp:Button ID="Button1" runat="server" CommandName="edit" CssClass="btn btn-primary btn-xs" Text="Edit" meta:resourcekey="Button1Resource1" />
                                        <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return getConfirmation2(this, 'Please confirm','Are you sure, you want to remove this data?');" CssClass="btn btn-primary btn-xs" CommandName="Delete" CommandArgument='<%# Eval("id") %>' meta:resourcekey="BtnDeleteCityResource1" Text="Remove"></asp:LinkButton>
                                    </ItemTemplate>

                                    <HeaderStyle ForeColor="#4aa9af"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle BackColor="#F0F0F0" />
                            <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                        </asp:GridView>

                    </div>
                </div>
                <div class="box-footer">
                </div>
            </div>
        </div>

        <div class="col-md-6">
            <div class="box box-primary">
                <div class="box-header">
                    <h3 class="box-title">
                        <asp:Label ID="Label2" runat="server" Text="Add Specialities" meta:resourcekey="Label2Resource1"></asp:Label></h3>
                    <div class="form-group">
                        <div class="input-group">
                            <asp:HiddenField ID="hfspId" runat="server" />
                            <asp:TextBox ID="TextBox2" CssClass="form-control" ValidationGroup="vss" placeholder="Search speciality" runat="server" meta:resourcekey="TextBox2Resource1"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" ValidationGroup="vss" Text="Search" OnClick="Button2_Click" meta:resourcekey="Button2Resource1" />
                            </span>
                        </div>
                    </div>

                    <div class="form-group">
                        <div class="input-group">
                            <asp:TextBox ID="TxtSpecialities" runat="server" CssClass="form-control" placeholder="Enter speciality " ValidationGroup="vspec" meta:resourcekey="TxtSpecialitiesResource1"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="BtnAddSpeciality" runat="server" Text="Add" CssClass="btn btn-primary" ValidationGroup="vspec" OnClick="BtnAddSpeciality_Click" meta:resourcekey="BtnAddSpecialityResource1" />

                            </span>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="* Please enter a speciality" ForeColor="Red" ControlToValidate="TxtSpecialities" ValidationGroup="vspec" meta:resourcekey="RequiredFieldValidator2Resource1"></asp:RequiredFieldValidator>
                    </div>






                </div>



                <div class="box-body">
                    <asp:GridView ID="GrvSpeciality" runat="server" CssClass="table table-hover table-bordered" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="id" PageSize="15" OnPageIndexChanging="GrvSpeciality_PageIndexChanging" OnRowDeleting="GrvSpeciality_RowDeleting" meta:resourcekey="GrvSpecialityResource1" OnRowEditing="GrvSpeciality_RowEditing" OnRowCancelingEdit="GrvSpeciality_RowCancelingEdit" OnRowUpdating="GrvSpeciality_RowUpdating">
                        <Columns>
                             <asp:TemplateField HeaderText="Sl.No" meta:resourcekey="TemplateFieldResource6"><ItemTemplate><%#Container.DisplayIndex+1 %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="#4aa9af" HeaderText="Specialities" meta:resourcekey="TemplateFieldResource3">
                                <EditItemTemplate>
                                    <asp:Label ID="Label3" Visible="False" runat="server" Text='<%# Eval("id") %>' meta:resourcekey="Label3Resource1"></asp:Label>

                                    <asp:TextBox ID="TextBox1" CssClass="form-control" Text='<%# Eval("Specialities") %>' runat="server" meta:resourcekey="TextBox1Resource1"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="LblSpecialities" runat="server" Text='<%# Eval("Specialities") %>' meta:resourcekey="LblSpecialitiesResource1"></asp:Label>
                                </ItemTemplate>

                                <HeaderStyle ForeColor="#4aa9af"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-ForeColor="#4aa9af" HeaderText="###" meta:resourcekey="TemplateFieldResource4">
                                <EditItemTemplate>
                                    <asp:LinkButton ID="LinkButton5" runat="server" CssClass="btn btn-primary btn-xs" OnClientClick="return getConfirmation1(this, 'Please confirm','Are you sure, you want to update this data?');" CommandName="update" meta:resourcekey="LinkButton5Resource1" Text="Update"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton6" runat="server" CssClass="btn btn-primary btn-xs" CommandName="cancel" meta:resourcekey="LinkButton6Resource1" Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton4" CssClass="btn btn-primary btn-xs" CommandName="edit" runat="server" meta:resourcekey="LinkButton4Resource1" Text="Edit"></asp:LinkButton>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-primary btn-xs" OnClientClick="return getConfirmation(this, 'Please confirm','Are you sure, you want to remove this data?');" CommandName="Delete" CommandArgument='<%# Eval("id") %>' meta:resourcekey="BtnDeleteSpecialitiesResource1" Text="Remove"></asp:LinkButton>
                                </ItemTemplate>

                                <HeaderStyle ForeColor="#4aa9af"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle BackColor="#F0F0F0" />
                        <PagerStyle CssClass="gridview" BackColor="#F0F0F0"></PagerStyle>
                    </asp:GridView>
                </div>
                <div class="box-footer">
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript" src="http://code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        $($('.ui-autocomplete-input')[0]).css('width', '10px')

    </script>
    <style>
        .ui-autocomplete {
            width: 33.6% !important;
            height:auto;
        }
    </style>
    <script src="../js/app.min.js"></script>
</asp:Content>

