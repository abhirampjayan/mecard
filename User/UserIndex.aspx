<%@ Page Title="" Language="C#" MasterPageFile="~/User/newusermaster.master" AutoEventWireup="true" CodeFile="UserIndex.aspx.cs" Inherits="User_UserIndex" %>

<%@ Register TagPrefix="ajaxToolkit" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
    <div class="container-fluid">

   <div style="margin-top:5%">
         <div class="col-md-12" role="search">
            <div class="row">
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtContactsSearch" ValidationGroup="cc" CssClass="form-control" placeholder="Doctor name or Specialty" runat="server"></asp:TextBox>

                        <ajaxToolkit:AutoCompleteExtender runat="server" ServiceMethod="SearchCustomers"
                            MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" BehaviorID="txtContactsSearch_AutoCompleteExtender" TargetControlID="txtContactsSearch" ID="txtContactsSearch_AutoCompleteExtender">
                        </ajaxToolkit:AutoCompleteExtender>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="form-group">
                        <asp:TextBox ID="txtZipCodeSearch" ValidationGroup="cc" CssClass="form-control" placeholder="Zipcode" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender runat="server" ServiceMethod="SearchZipCode" MinimumPrefixLength="2" CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" BehaviorID="txtZipCodeSearch_AutoCompleteExtender" TargetControlID="txtZipCodeSearch" ID="txtZipCodeSearch_AutoCompleteExtender"></ajaxToolkit:AutoCompleteExtender>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <asp:TextBox ID="txtLangSearch" CssClass="form-control" ValidationGroup="cc" placeholder="Language" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender runat="server" MinimumPrefixLength="2"
                            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" ServiceMethod="SearchLanguage" ServicePath="" DelimiterCharacters="" BehaviorID="txtLangSearch_AutoCompleteExtender" TargetControlID="txtLangSearch" ID="txtLangSearch_AutoCompleteExtender"></ajaxToolkit:AutoCompleteExtender>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="Button1" CssClass="btn btn-sm btn-flat btn-github" ValidationGroup="cc" runat="server" Text="Find" OnClick="Button1_Click" />
                </div>
            </div>

        </div>
   </div>
      
  
     </div>
   
            <section>
                <img src="../Images/Zocdoc.jpg" />
            </section>
        
</asp:Content>

