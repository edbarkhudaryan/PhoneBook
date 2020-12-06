<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WCFApp.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridViewPhoneBook" runat="server" OnSelectedIndexChanged="GridViewPhoneBook_SelectedIndexChanged">
            </asp:GridView>
        </div>
        <p>
            PhoneBook Id&nbsp;
            <asp:TextBox ID="TextBoxId" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="ButtonSearch" runat="server" OnClick="ButtonSearch_Click" Text="Search" />
        </p>
        <p>
            Name <asp:Label ID="LabelName" runat="server"></asp:Label>
        </p>
        <p>
            Number<asp:Label ID="LabelNumber" runat="server"></asp:Label>
        </p>
    </form>
</body>
</html>
