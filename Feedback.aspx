<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Feedback.aspx.cs" Inherits="BOTS_WL.Feedback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
   <style>
   fieldset 
{
   
    margin-top:18px;
    width:950px;
    height:800px;
    -webkit-border-radius:6px;
    -moz-border-radius:6px;
    -o-border-radius:6px;
    border-radius:6px;
    text-align:left;
    background-color:beige;
    box-shadow: 10px 10px 5px #888888;
}

   </style>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset>
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" style="font-weight: 700; color: #FF0000"
                    Text="FEEDBACK"></asp:Label>
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                User Id</td>
            <td>
                <asp:TextBox ID="txtuserid" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtname" runat="server" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Comments"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtcomments" runat="server" Height="112px" TextMode="MultiLine"
                    Width="284px" MaxLength="255"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="cmd_feedback" runat="server" onclick="cmd_feedback_Click"
                    Text="Send Feedback" />
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblmsg" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
            </fieldset>
    </form>
</body>
</html>
