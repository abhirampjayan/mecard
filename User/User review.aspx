<%@ Page Title="" Language="C#" MasterPageFile="~/User/newUserMaster.master" AutoEventWireup="true" UICulture="en-US" Culture="en-US" CodeFile="User review.aspx.cs" Inherits="User_User_review" meta:resourcekey="PageResource2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        body {
            /*margin: 0px auto;
            width: 980px;
            font-family: "Trebuchet MS", Arial, Helvetica, sans-serif;
            background: #C9C9C9;*/
        }

        .blankstar {
            background-image: url(../Images/blank_star.png);
            width: 16px;
            height: 16px;
        }

        .waitingstar {
            background-image: url(../images/half_star.png);
            width: 16px;
            height: 16px;
        }

        .shiningstar {
            background-image: url(../images/shining_star.png);
            width: 16px;
            height: 16px;
        }
    </style>
   <%--  <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content" style="margin-bottom:1cm;margin-top:1.5cm;">

    
    <div class="container">
        <div class="row">

            <div class="box box-primary" style="margin-top: 1%">
                <div class="box-header">
                   <%--  <%if (Session["Speciality"].ToString() == "Auto")
                             { %>--%>
                    <p class="pull-left" style="color:red;">Your rating and feedback is very important! So you should rate each doctors in consulted order.</p>
                  <%--  <%}
                        else { %>
                     <p class="pull-right" style="color:red;">تقييمك وردود الفعل مهم جدا! لذلك يجب أن معدل كل الأطباء في النظام التشاور.</p>
                    <%} %>--%>
                    <div class="form-group">
                     <%--    <%if (Session["Speciality"].ToString() == "Auto")
                             { %>--%>
                        <div class="pull-right"><%--<%}
    else
    { %>
<div class="pull-left">
                            <%} %>--%>
                           
                        <asp:HyperLink ID="HyperLink1" ForeColor="#4AA9AF" runat="server" meta:resourcekey="HyperLink1Resource2">Posted reviews</asp:HyperLink>
                       </div>
                             <%--<div class="input-group">
                            <asp:TextBox ID="TextBox1" CssClass="form-control" placeholder="Enter doctor name" runat="server"></asp:TextBox>
                            <span class="input-group-btn">
                                <asp:Button ID="Button1" CssClass="btn btn-warning" runat="server" Text="Search" OnClick="Button1_Click" />
                            </span>
                        </div>--%>
                    </div>
                </div>
                  <%--   <%if (Session["Speciality"].ToString() == "Auto")
                             { %>--%>
                <div class="box-body">
                   <%-- <%}else{ %>
                    <div class="box-body" dir="rtl">
                    <%} %>--%>
                    <div class="form-group">
                        
                    </div>
                    <div class="form-group">
                        <div class="table-responsive">
                            <asp:GridView ID="GridView1" CssClass="table table-hover table-bordered " runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" meta:resourcekey="GridView1Resource2">
                                <Columns>
                                    <asp:TemplateField HeaderText="Doctor details" meta:resourcekey="TemplateFieldResource5">
                                        <ItemTemplate>

                                        <%--    <%if (Session["Speciality"].ToString() == "Auto")
                             { %>--%>
                                            <div class="form-group">
                                              <%--  <%}else{ %>
                                                <div class="form-group pull-right">
                                                <%} %>--%>
                                                <asp:Image ID="Image1" runat="server" AlternateText="Photo" CssClass="img-circle img-responsive img-md" meta:resourcekey="Image1Resource2" />

                                            </div>
                                            <div class="form-group" style="margin-left:2%">
                                                Dr.
                                          <asp:Label ID="Label1" runat="server" meta:resourcekey="Label1Resource2"></asp:Label>
                                                <asp:Label ID="Label2" runat="server" Text='<%# Bind("d_id") %>' Visible="False" meta:resourcekey="Label2Resource2"></asp:Label>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("h_id") %>' Visible="False" meta:resourcekey="Label5Resource2"></asp:Label>
                                                <asp:Label ID="Label11" Visible="false" runat="server" Text='<%# Bind("u_id") %>'></asp:Label>
                                            </div>

                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#EEEEEE" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Consulting details" meta:resourcekey="TemplateFieldResource6">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label3Resource2"></asp:Label><br />
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("a_time") %>' meta:resourcekey="Label4Resource2"></asp:Label>
                                            <br />
                                            <span class="label" style="background-color: #4aa9af">
                                                <asp:Label ID="Label7" runat="server" Text="Average rating" meta:resourcekey="Label7Resource1"></asp:Label> </span><br />
                                           <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                                 { %>--%>
                                            <div class="pull-left"><%--<%}
    else
    { %><div class="pull-right">
                                                <%} %>--%>
                                            <ajaxToolkit:Rating ID="Rating1" ReadOnly="True" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" runat="server" BehaviorID="Rating1_RatingExtender" CurrentRating="0" meta:resourcekey="Rating1Resource2">
                                            </ajaxToolkit:Rating>
                                                </div>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#EEEEEE" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate this doctor" meta:resourcekey="TemplateFieldResource7">

                                        <ItemTemplate>
                                            <div style="color: #4aa9af">
                                                
                                                <asp:Label ID="Label8" runat="server" Text="Waiting time" meta:resourcekey="Label8Resource1"></asp:Label>
                                                    
                                        <ajaxToolkit:Rating ID="Rating2" AutoPostBack="True" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" runat="server" OnClick="Rating2_Click" BehaviorID="Rating2_RatingExtender" CurrentRating="0" meta:resourcekey="Rating2Resource2">
                                        </ajaxToolkit:Rating>
                                            </div>
                                          <br />
                                            <div style="color: #4aa9af">
                                                <asp:Label ID="Label9" runat="server" Text="Beside manner" meta:resourcekey="Label9Resource1"></asp:Label>
                                        <ajaxToolkit:Rating ID="Rating3" AutoPostBack="True" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" runat="server" OnClick="Rating3_Click" BehaviorID="Rating3_RatingExtender" CurrentRating="0" meta:resourcekey="Rating3Resource2">
                                        </ajaxToolkit:Rating>
                                            </div>
                                            <br />
                                            <div style="color: #4aa9af">
                                                <asp:Label ID="Label10" runat="server" Text=" Services " meta:resourcekey="Label10Resource1"></asp:Label>
                                        <ajaxToolkit:Rating ID="Rating4" AutoPostBack="True" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" runat="server" OnClick="Rating4_Click" BehaviorID="Rating4_RatingExtender" CurrentRating="0" meta:resourcekey="Rating4Resource2">
                                        </ajaxToolkit:Rating>
                                            </div>
                                            <br />
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#EEEEEE" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Post feedback" meta:resourcekey="TemplateFieldResource8">
                                        <ItemTemplate>
                                            <div class="form-group">

                                                <asp:TextBox ID="TextBox2" CssClass="form-control" Rows="3" TextMode="MultiLine" runat="server" meta:resourcekey="TextBox2Resource2"></asp:TextBox>
                                            </div>
                                            <asp:Label ID="Label6" runat="server" Visible="False" Text='<%# Bind("id") %>' meta:resourcekey="Label6Resource2"></asp:Label>
                                            <asp:LinkButton ID="LinkButton2" runat="server" ForeColor="#4AA9AF" CommandName="update" CommandArgument='<%# Bind("d_id") %>' meta:resourcekey="LinkButton2Resource2">Post Review</asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle BackColor="#EEEEEE" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

            </div>







            <!--//model popup for alert-->

            <%--        <asp:Button ID="btnForAjax3" runat="server" Text="" Style="display: none" />
          <ajaxtoolkit:modalpopupextender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="modalBackground" PopupControlID="pnlPopupMsg3"
                TargetControlID="btnForAjax3" CancelControlID="btnclose3">
            </ajaxtoolkit:modalpopupextender>

            <asp:Panel ID="pnlPopupMsg3" runat="server" CssClass="modalPopup">
                <div class="col-md-12">
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            <h3 class="box-title" style="text-transform:capitalize">Hakkeem</h3>
                            <div class="box-tools pull-right">
                                <button class="btn btn-box-tool" id="btnclose3" data-widget="remove"><i class="fa fa-remove"></i></button>
                            </div>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group">
                                
                                   <span style="text-transform:initial"> <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label></span>

                            </div>
                            <div class="form-group">
                              <span style="margin-left:45%"> <asp:Button ID="BtnSubmitOTP" runat="server" Text="OK" CssClass="btn btn-success btn-xs" ValidationGroup="cc" /></span> 
                            </div>
                        </div>
                    </div>

                </div>
            </asp:Panel>--%>
            <!--//modal popup-->



        </div>

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
            /* margin-top: 6cm; */
            bottom: 0;
        }
          .main-header ul li a {
    text-decoration: none;
    text-transform: uppercase;
    color: #000;
    font-size: 17px;
    font-weight: 500;
    text-shadow: none;
    padding: 9px 15px;
    display: block;
    outline: none;
    letter-spacing: 0.1px;
}
    </style>--%>

      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

    <!-- Latest compiled JavaScript -->
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
     <script src="../Design/dist/js/app.min.js"></script>
    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />

</asp:Content>

