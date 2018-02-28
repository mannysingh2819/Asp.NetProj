<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="APFProductUpdateProj.WebForm1" Async="true"  validateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>APF Orphan Product Cancellation</title>
    <script src="jquery/jquery-1.12.4.js"></script>
    <link href="Css/main.css" rel="stylesheet" />
    <link rel="shortcut icon" href="images/fav.ico" type="image/icon" />
    <link rel="icon" href="images/fav.ico" type="image/icon" />
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
            padding-top:3px;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid beige;
            width: 250px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
            -moz-border-radius: 6px;
            -o-border-radius: 6px;
            border-radius: 6px;
        }
        </style>
     <script type="text/javascript">
        $(document).ready(function () {
            $('#btnsearch,#btnsend').click(function (e) {
                 $('input[type="text"]').each(function () {
                    if ($.trim($(this).val()) == '') {
                         $(this).css('background-color', 'Red');
                        e.preventDefault();
                    } else {
                        $(this).css({
                            "border": "",
                            "background-color": ""
                     });
                   }
                });
             });
          });
         
    </script>
    <script type="text/javascript">
        function getsubstring() {
            var val = document.getElementById('txtlistingId').value;
            var instr = val.indexOf('_');
            var result = val.substr(0, instr).trim();
            document.getElementById('txtlistId').value = result;
        }

    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <section>
            <div>
            </div>
            <div class="centerFieldset">
                <fieldset>
                    <table style="width:100%;">
                        <tr>
                            <td colspan="2" style="text-align: center; font-weight: bold; color: saddlebrown;">APF ORPHAN PRODUCT CANCELLATION
                            </td>
                            <td style="float:right;">
                                <a href ="ExecuteAnonymous.aspx">Execute Apex</a>
                                <a href ="bulkload.aspx">Cancel APF Product</a>
                                <a href ="CancelApfListing.aspx" >Cancel APF Listing</a>
                            </td>
                           
                        </tr>
                        <tr>
                           <td></td> 
                        </tr>
                        <tr style="float: left">
                            <td>Enter the Bc Product Id
                            </td>
                            <td style="float: left; padding-top: 5px;">
                                <asp:TextBox ID="txtproductId" runat="server" CssClass="text_align"></asp:TextBox>
                            </td>
                            <td style="float: left; padding-left: 5px;padding-top: 8px;">
                                <asp:Button ID="btnsearch" runat="server" Text="Search" CssClass="button" OnClick="btnsearch_Click" /></td>
                        </tr>
                        <tr style="float: left">
                           
                            <td style="float: left; padding-left: 5px;padding-top: 8px;">
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    <div class="loading" >
                        Processing.Please wait ..
                        <img src="images/loader.gif" alt="" />
                    </div>
                    <div runat="server" class="Table" id="theDiv">
                        <div class="Row">
                            <div class="Cell">
                                Advertiser Id
            <asp:TextBox ID="txtadvertiserId" runat="server" CssClass="text_align"></asp:TextBox>
                             </div>
                            <div class="Cell">
                                Current Status
                      <asp:TextBox ID="txtcurrentStatus" runat="server" CssClass="text_align" ReadOnly="True"></asp:TextBox>        
                            </div>
                        </div>
                        <div class="Row">
                            <div class="Cell">
                                Product Code 
            <asp:TextBox ID="txtproductcode" runat="server" CssClass="text_align"></asp:TextBox>
                            </div>
                        
                              
                            <div class="Cell">
                               New Status
                                <asp:DropDownList ID="drpStatus" runat="server">
                                    <asp:ListItem Value="C">Cancel</asp:ListItem>
                                    <asp:ListItem Value="I">In Progress</asp:ListItem>
                                    <asp:ListItem>D</asp:ListItem>
                                    <asp:ListItem>X</asp:ListItem>
                                    <asp:ListItem Value="N">New</asp:ListItem>
                                    <asp:ListItem Value="L">LIVE</asp:ListItem>
                                    <asp:ListItem Value="E">Expired</asp:ListItem>
                                    <asp:ListItem Value="S">Suspend</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="Row">
                        <div class="Cell">
                                Product Version 
            <asp:TextBox ID="txtProdversion" runat="server" CssClass="text_align"></asp:TextBox>
                            </div> 
                            <div class="Cell">
                                Product Sub-Version 
            <asp:TextBox ID="txtProdsubversion" runat="server" CssClass="text_align"></asp:TextBox>
                            </div> 
                            </div>
                        <div class="Row">
                            <div class="Cell">
                                Publish Indicator
            <asp:DropDownList ID="drppublishInd" runat="server" AutoPostBack="True" CssClass="text_align">
                <asp:ListItem Value="Y"></asp:ListItem>
                <asp:ListItem>N</asp:ListItem>
            </asp:DropDownList>
                            </div>
                             
                            <div class="Cell">
                                Attributes
            <asp:TextBox ID="txtattributeId" runat="server" CssClass="text_align" onblur="getsubstring()" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="Cell">
                                Appearances
            <asp:TextBox ID="txtappearanceId" runat="server" CssClass="text_align"  TextMode="MultiLine"></asp:TextBox>
                            </div>
                        <div class="Cell">
                                Content
            <asp:TextBox ID="txtcontent" runat="server" CssClass="text_align"  TextMode="MultiLine"></asp:TextBox>
                            </div>
                       
                        <div class="Row">
                            <div class="Cell">
                                Effective Version Indicator
                        <asp:DropDownList ID="drpeffectiveversionInd" runat="server" AutoPostBack="True" CssClass="text_align">
                            <asp:ListItem>Y</asp:ListItem>
                            <asp:ListItem>N</asp:ListItem>
                        </asp:DropDownList>
                            </div>
                            <div class="Cell">
                                Heading Code
                                <asp:TextBox ID="txtheadingcode" runat="server" Text="" CssClass="text_align" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="Row">
                            <div class="Cell">
                                Listing Id
            <asp:TextBox ID="txtlistId" runat="server" CssClass="text_align"></asp:TextBox>
                            </div>
                            <div class="Cell">
                                Directory Code
            <asp:TextBox ID="txtdircode" runat="server" CssClass="text_align"></asp:TextBox>
                            </div>
                        </div>
                        <div class="Row">
                            <div class="Cell">
                                Directory Issue
            <asp:TextBox ID="txtdirissue" runat="server" CssClass="text_align"></asp:TextBox>
                            </div>
                            <div class="Cell">
                                Main product Id
            <asp:TextBox ID="txtmainprodid" runat="server" CssClass="text_align"></asp:TextBox>
                            </div>
                             <div class="Cell">
                                Directory Type
            <asp:TextBox ID="txtdirtype" runat="server" CssClass="text_align"></asp:TextBox>
                            </div>
                        </div>
                         
                        <div class="Row">
                            <div class="Cell">
                                <div style="float: right">
                                    <asp:Button ID="btnsend" runat="server" Text="Send" OnClick="btnsend_Click" CssClass="button" OnClientClick="return confirm('Are you sure you want to set the Status to ' + document.getElementById('drpStatus').options[document.getElementById('drpStatus').selectedIndex].text + '?');" />
                                    <asp:Button ID="btnreset" runat="server" Text="Reset" OnClick="btnreset_Click" CssClass="button" />
                                </div>
                            </div>
                            
                        </div>
                 </div>
                    <div class="Table" id="divMsg" runat="server">
                        <div class="Row">
                                <div class="Cell">
                                    <asp:Label ID="lblmsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="Row">
                                <div class="Cell">
                                    <asp:HyperLink ID="sflink" runat="server"></asp:HyperLink>
                                </div>
                            </div>
                    </div>
                    <div class="Table" id="gridview" runat="server">
                        <div class="Row">
                                <div class="Cell">
                                    <asp:GridView ID="grdview" runat="server" CellPadding="3" OnSelectedIndexChanging="grdview_SelectedIndexChanging" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px">
                                        <FooterStyle BackColor="White" ForeColor="#000066" />
                                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
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
                </fieldset>
           </div>
        </section>
    </form>
</body>
</html>
