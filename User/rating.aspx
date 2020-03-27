<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="rating.aspx.cs" Inherits="User_rating" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
<!-- jQuery library -->
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>

<!-- Latest compiled JavaScript -->
<script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <link href="../Design/dist/css/skins/_all-skins.min.css" rel="stylesheet" />

    <link href="../Design/dist/css/AdminLTE.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div style="margin-top: 8%">
            <div class="col-md-12">
                <div class="box box-solid box-primary">
                    <div class="box-header">
                        <h3 class="box-title">Doctor profile</h3><div class="pull-right">
                             Average rating<asp:Rating ID="Rating4" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" runat="server"></asp:Rating>
                                                                 </div>
                       
                    </div>
                    <div class="box-body">

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:Image ID="Image1" CssClass="img-responsive img-bordered img-thumbnail" runat="server" />
                                </div>

                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <h1>Dr.<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h1>
                                    
                                </div>
                               
                                         <div class="form-group">

                                   Waiting time: <asp:rating ID="Rating1" AutoPostBack="true" runat="server" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" OnClick="Rating1_Click"></asp:rating>

                                </div>
                                <br />
                                <div class="form-group">

                                 Beside manner: <asp:rating ID="Rating2" AutoPostBack="true" runat="server" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" OnClick="Rating2_Click"></asp:rating>

                                </div>
                                <br />
                                <div class="form-group">

                                 Services: <asp:rating ID="Rating3" runat="server" AutoPostBack="true" StarCssClass="blankstar" WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" EmptyStarCssClass="blankstar" OnClick="Rating3_Click"></asp:rating>

                                </div>
                                
                                   
                                <div>
                               
                                    </div>

                                <%--<div class="form-group">
                                    <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
                                </div>--%>
                                <div class="form-group">
                                    <label>About doctor</label><br />
                                    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="box-footer"></div>
                </div>
            </div>
        </div>

    </div>


</asp:Content>

