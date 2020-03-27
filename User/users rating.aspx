<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="users rating.aspx.cs" Inherits="User_users_rating" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">
body
{
/*margin:0px auto;
width:980px;
font-family:"Trebuchet MS", Arial, Helvetica, sans-serif;	
background:#C9C9C9;*/
}
.blankstar
{
background-image: url(../images/blank_star.png);
width: 16px;
height: 16px;
}
.waitingstar
{
background-image: url(../images/half_star.png);
width: 16px;
height: 16px;
}
.shiningstar
{
background-image: url(../images/shining_star.png);
width: 16px;
height: 16px;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
    <ajaxToolkit:Rating ID="Rating1" AutoPostBack="true" runat="server" StarCssClass="blankstar" 
        WaitingStarCssClass="waitingstar" FilledStarCssClass="shiningstar" 
        EmptyStarCssClass="blankstar" OnClick="Rating1_Click"></ajaxToolkit:Rating>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
    <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
</asp:Content>

