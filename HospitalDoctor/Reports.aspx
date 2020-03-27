<%@ Page Title="" Language="C#" MasterPageFile="~/HospitalDoctor/HospitalDoctorMaster.master" AutoEventWireup="true" CodeFile="Reports.aspx.cs" Inherits="HospitalDoctor_Reports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <script type="text/javascript">
           $(function () {
               $('#Div1').slimScroll({
                   height: '400px'

               });
           });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <%--  <script type="text/javascript" lang="javascript">
        function PrintGridData() {
            var prtGrid = document.getElementById('<%=GridView1.ClientID %>');
             
    prtGrid.border = 0;
    var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
    prtwin.document.write(prtGrid.outerHTML);
    prtwin.document.close();
    prtwin.focus();
    prtwin.print();
    prtwin.close();
        }
        </script>--%>
    
    <div class="container-fluid">
       <%-- <%if (Session["Language"].ToString() == "Auto")
            {%>--%>
          <div class="col-md-12">
             <%-- <%}
    else
    { %>
              <div class="col-md-12" dir="rtl">
              <%} %>--%>
              <div style="margin-top:10px;">
                  <div class="box box-primary">
                <div class="box-header with-border">
                 <%--    <%if (Session["Language"].ToString() == "Auto")
                         {%>--%>
                    <h3 class="box-title">History</h3>
                 <%--   <%}
    else
    { %>
                     <h3 class="box-title pull-right">التاريخ</h3>
                    <%} %>--%>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <div class="row">
                    <div class="form-group">

                       <%-- <div class="col-md-5 col-sm-5">
                            <asp:RadioButtonList ID="rdpSelect" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" CssClass="table" RepeatLayout="Flow" OnSelectedIndexChanged="rdpSelect_SelectedIndexChanged">
                                <asp:ListItem>Date</asp:ListItem>
                                <asp:ListItem>BookDoc Id</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>

                    </div>
                        </div>
                    <div class="row">
                    <div class="form-group">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="col-lg-3 col-md-3 ">
                            <label>From Date</label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Please select a date" ControlToValidate="TxtFrom" ValidationGroup="s" ForeColor="Red"></asp:RequiredFieldValidator>
                        <asp:TextBox ID="TxtFrom" runat="server" TextMode="Date" CssClass="form-control" ValidationGroup="a" AutoPostBack="true" OnTextChanged="TxtFrom_TextChanged"></asp:TextBox>
                        </div>
                            <div class="col-lg-3 col-md-3 ">
                            <label>To Date</label>
                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please select a date" ControlToValidate="TxtTo" ValidationGroup="s" ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <asp:TextBox ID="TxtTo" runat="server" TextMode="Date" CssClass="form-control" ValidationGroup="a" AutoPostBack="True" Enabled="false" OnTextChanged="TxtTo_TextChanged"></asp:TextBox>
                        </div>
                            <div class="col-lg-2 col-md-2 ">
                                <label>Diagnosis</label>
                                <asp:DropDownList ID="DdlDiagnose" runat="server" CssClass="form-control"></asp:DropDownList>
                             </div>
                            <div class="col-lg-2 col-md-2 ">
                        <div style="margin-top:28px;">
                            <asp:Button ID="BtnViewReport" runat="server" Text="View/Download" CssClass="btn btn-sm btn-success"  ValidationGroup="a" OnClick="BtnViewReport_Click" />
                            <%--<asp:Button ID="BtnDownload" runat="server" Text="Download" CssClass="btn btn-sm btn-primary" ValidationGroup="a" OnClick="BtnDownload_Click"/>--%>
                            </div>
                    </div>
                            <div class="col-lg-2 col-md-2">
                                <div style="margin-top:27px;">
                                    <asp:Button ID="BtnMostUser" runat="server" Text="Most Patient" ForeColor="#18bc9c" CssClass="btn btn-default " ToolTip="Click to view most visited patients" OnClick="BtnMostUser_Click1" ValidationGroup="s" Width="102px" CommandName="MostPatient" />
                                    <asp:Button ID="BtnMostDisease" runat="server" Text="Most Disease" ForeColor="#18bc9c" CssClass="btn btn-default " ToolTip="Click to view most Disease the patients consulted" ValidationGroup="s" CommandName="MostDisease" OnClick="BtnMostDisease_Click"/>
                                </div>
                            </div>
                        </asp:Panel>
                         </div>
                        
                        </div>
                    <div class="row">
                    <div class="form-group">
                        <asp:Panel ID="Panel2" runat="server">
                         <div class="col-lg-4 col-md-4 ">
                            <label>BookDoc Id</label>  
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter BookDoc Id" ValidationGroup="b" ForeColor="Red" ControlToValidate="TxtBookDocId"></asp:RequiredFieldValidator>                      
                        <asp:TextBox ID="TxtBookDocId" runat="server" CssClass="form-control" ValidationGroup="b" placeholder="Patient BookDoc Id"></asp:TextBox>
                        </div>
                    <div class="col-lg-2 col-md-2 ">
                        <div style="margin-top:28px;">
                            <asp:Button ID="BtnViewReportId" runat="server" Text="View/Download" CssClass="btn btn-sm btn-success"  ValidationGroup="b" OnClick="BtnViewReportId_Click"/>
                            <%--<asp:Button ID="BtnDownloadId" runat="server" Text="Download" ValidationGroup="b" CssClass="btn btn-sm btn-primary" OnClick="BtnDownloadId_Click" />--%>
                            </div>
                    </div>
                        </asp:Panel>
                </div>
                        </div>
            </div>
           </div>
              <asp:Panel ID="Panel3" runat="server" Visible="false">
               <div class="col-md-12">
                 
                    <div class="box box-primary">
                        <div class="box-header with-border">
                             <h3 class="box-title">Details</h3>
                             <asp:Button ID="BtnPrintDownload" runat="server" Text="Download"  CssClass="btn btn-sm btn-danger pull-right" OnClick="BtnPrintDownload_Click"/>
                        </div>
                        <div class="box-body">
                           
                            <div class="form-group table-responsive">
                                <div id="Div1">
                                <asp:GridView ID="GridView1" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"  OnPageIndexChanging="GridView1_PageIndexChanging" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDate" runat="server" Text='<%#Eval("a_date") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Start Time">
                                            <ItemTemplate>
                                                <asp:Label ID="LblTime" runat="server" Text='<%# Eval("a_time") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="End Time">
                                            <ItemTemplate>
                                                <asp:Label ID="LblEndTime" runat="server" Text='<%# Eval("a_end_time") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Patient Name">
                                            <ItemTemplate>
                                                <asp:Label ID="LblPatientName" runat="server" Text='<%#Eval("name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Diagnose">
                                            <ItemTemplate>
                                                <asp:Label ID="LblDiagnose" runat="server" Text='<%# Eval("a_doc_daignose")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Prescription">
                                            <ItemTemplate>
                                                <asp:Label ID="LblPrescription" runat="server" Text='<%# Eval("a_doc_prescriptions")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                     <FooterStyle BackColor="White" ForeColor="#000066" />
                                    <HeaderStyle BackColor="#dddddd" Font-Bold="True" ForeColor="#18bc9c" />
                                    <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                    <RowStyle ForeColor="#000066" />
                                    <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#007DBB" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#00547E" />
                                </asp:GridView>
                                </div>
                            </div>
                           
                        </div>
                    </div>
             
            </div>
                  </asp:Panel>
             </div>
          </div>
        </div>
   
</asp:Content>

