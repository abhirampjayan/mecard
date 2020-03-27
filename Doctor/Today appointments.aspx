<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="Today appointments.aspx.cs" Inherits="Doctor_Today_appointments" Culture="en-US" meta:resourcekey="PageResource1" uiCulture="en-US" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
       <link href="../css/sweetalert.css" rel="stylesheet" />
    <script src="../js/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <div class="box box-primary">
        <div class="box-header">
            <h3 class="box-title">
                <asp:Label ID="Label1" runat="server" Text="Today appointments" meta:resourcekey="Label1Resource1"></asp:Label></h3>
        </div>
        <div class="box-body">
            <div class="form-group">
                <div class="table-responsive">
                    <%-- <%if (Session["Language"].ToString() == "Auto")
                         { %>--%>
                    <div>
                       <%-- <%}
    else
    { %>
                        <div dir="rtl">
                        <%} %>--%>


                             <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Timer ID="Timer1" runat="server" Interval="500" OnTick="Timer1_Tick" Enabled="true"></asp:Timer>--%>
                                    <asp:GridView ID="GridView1" CssClass="table table-hover table-bordered" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnRowCommand="GridView1_RowCommand" meta:resourcekey="GridView1Resource1">
                    <Columns>
                        <asp:TemplateField HeaderText="Patient name" meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <asp:Label ID="Label4" Visible="False" runat="server" Text='<%# Bind("c_id") %>' meta:resourcekey="Label4Resource1"></asp:Label>
                                <asp:Label ID="Label5" runat="server" Text="Label" meta:resourcekey="Label5Resource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appointment date" meta:resourcekey="TemplateFieldResource2">
                            <ItemTemplate>
                                <asp:Label ID="Label6" runat="server" Text='<%# Bind("a_date") %>' meta:resourcekey="Label6Resource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Appointment time" meta:resourcekey="TemplateFieldResource3">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# Bind("a_time") %>' meta:resourcekey="Label7Resource1"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource4">
                            <ItemTemplate>
                                <asp:Label ID="Label8" Visible="False" runat="server" Text='<%# Bind("a_status") %>' meta:resourcekey="Label8Resource1"></asp:Label>
                                <asp:Label ID="Label9" Visible="true" runat="server" Text=""></asp:Label>
                                 <asp:Label ID="Label10" Visible="False" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource5">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" CommandName="cnslt" runat="server" CommandArgument='<%# Bind("id") %>' meta:resourcekey="LinkButton1Resource1">Consult</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="###" meta:resourcekey="TemplateFieldResource5">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton11" CommandName="cnslt1" runat="server" CommandArgument='<%# Bind("id") %>' meta:resourcekey="LinkButton1Resource11">Not Consult</asp:LinkButton>
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
                             <%--   </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
                                </Triggers>
                            </asp:UpdatePanel>--%>


              
                    </div>
                    </div>
            </div>
        </div>

     </div>
     <script src="../js/app.min.js"></script>
</asp:Content>

