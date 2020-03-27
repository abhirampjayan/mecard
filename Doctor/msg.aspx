<%@ Page Title="" Language="C#" MasterPageFile="~/Doctor/DoctorMasterPage.master" AutoEventWireup="true" CodeFile="msg.aspx.cs" Inherits="Doctor_msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="modal fade" id="myModal1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upModal1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                              <%--  <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                  <asp:Button ID="Button3" CssClass="close" ValidationGroup="v" runat="server" Text="x" OnClick="Button3_Click"  ata-dismiss="modal"  />
                               <%-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true" >&times;</button>--%>
                               <%-- <%}
                                    else
                                    { %>
                                <button type="button" class="close pull-left" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <%} %>--%>
                               <%-- <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <h4 class="modal-title">
                                   <%-- <%}
                                    else
                                    { %>
                                    <h4 class="modal-title pull-right">
                                        <%} %>--%>
                                        <asp:Label ID="Label6" runat="server" Text="Upload Messages to Admin" ></asp:Label>
                                    </h4>
                            <%--    <%if (Session["Speciality"].ToString() == "Auto")
                                    { %>--%>
                                <div class="modal-body">
                                    <%--<%}
                                    else
                                    { %>
                                    <div class="modal-body" dir="rtl">
                                        <%} %>--%>
                                        <div class="form-group">
                                            <label>
                                                <asp:Label ID="Label8" runat="server" Text="Message" meta:resourcekey="Label5Resource1"></asp:Label></label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="bb" runat="server" ErrorMessage="* Enter Message" ForeColor="Red" ControlToValidate="textbox1" ></asp:RequiredFieldValidator>
                                            <asp:TextBox ID="TextBox1" Rows="10" TextMode="MultiLine" CssClass="form-control" runat="server" ValidationGroup="bb"  meta:resourcekey="TextBox1Resource2"></asp:TextBox>
                                        </div>
                                        
                 
                                      
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Label ID="Label19" CssClass="pull-left" Font-Bold="True" runat="server" Text="" ></asp:Label>
                                        <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary pull-right" ValidationGroup="v" runat="server" Text="Upload" OnClick="Button1_Click"   />
                                    </div>
                                </div>
                            </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                           <asp:AsyncPostBackTrigger ControlID="Button3" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
</asp:Content>

