<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Mail.aspx.cs" Inherits="Index_Mail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
    </div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Mail" />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="OUTLOOK" />
        <br />
        <br />
        <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Test Mail" />
        <p>
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Activation Mail" />
        </p>
        <asp:Button ID="Button5" runat="server" OnClick="Button5_Click" Text="APPOINMENT" />
    </form>
</body>
</html>
