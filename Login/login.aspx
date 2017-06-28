<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="BOTS_WL._login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIT</title>
    <link href="../divtable.css" rel="stylesheet" />
    <link rel="shortcut icon" href="../images/fav.ico" type="image/icon"/> 
    <link rel="icon" href="../images/fav.ico" type="image/icon"/>

    <script type="text/javascript">
        function changeHashOnLoad() {
            window.location.href += "#";
            setTimeout(changeHashAgain, 50);
        }

        function changeHashAgain() {
            window.location.href += "1";
        }

        var storedHash = window.location.hash;
        window.setInterval(function () {
            if (window.location.hash != storedHash) {
                window.location.hash = storedHash;
            }
        }, 50);

    </script>



</head>
<body onload="changeHashOnLoad();">
    <form id="form1" runat="server">
        <section>

            <div class="shorty_Table">
                <div class="shorty_row_neck">
                    <div class="shorty_cell_head">
                    </div>
                </div>
            </div>
            <div class="Table">
                <div class="Heading">
                    <div class="Cell">
                        Customer Interaction Tool Login
                    </div>
                </div>
                <div class="Row">
                    <div class="sub_Cell">
                        <p>Enter your user information, and click "Login". </p>
                    </div>

                </div>

                <div class="Row">
                    <div class="Cell-user">
                        
                            <asp:Label ID="Label1" runat="server" Text="Username:" AssociatedControlID="txtUserName"></asp:Label>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txt_f"></asp:TextBox>&nbsp;
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUserName" ErrorMessage="Enter Username" ForeColor="Red" Style="font-size: small"></asp:RequiredFieldValidator>
                       
                    </div>

                </div>
                <div class="Row">
                    <div class="Cell-pass">
                        
                            <asp:Label ID="Label2" runat="server" Text="Password:" AssociatedControlID="txtPassword" ></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="txt_f"></asp:TextBox>&nbsp;
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ErrorMessage="Enter Password" ForeColor="#FF3300" Style="font-size: small"></asp:RequiredFieldValidator>
                        
                    </div>
                </div>
                <div class="Row">
                    <div class="last-Cell">
                        <p>
                            <asp:Label ID="lblmsg" runat="server" Style="font-size: small" ForeColor="Red"></asp:Label>
                        </p>



                        <p>
                            <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass="button" />&nbsp;<asp:Button ID="btn_reset" runat="server" Text="Reset" OnClick="btn_reset_Click" CssClass="button" />
                        </p>


                    </div>
                </div>
            </div>
           
            <!--   <div class="f_class">&copy;Copyright Sensis Pty Ltd</div> -->

        </section>
    </form>
</body>
</html>
