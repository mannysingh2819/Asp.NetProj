<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bulkload.aspx.cs" Inherits="APFProductUpdateProj.bulkload" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>APF Orphan Product Bulk Cancellation</title>
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
            padding-top: 3px;
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
            <div class="loading">
                Processing.Please wait ..
                        <img src="images/loader.gif" alt="" />
            </div>
            <div class="centerFieldset">
                <fieldset>
                    <table style="width:100%;">
                        <tr>
                            <td colspan="2" style="text-align: center; font-weight: bold; color: saddlebrown;">APF ORPHAN PRODUCT BULK CANCELLATION
                            </td>
                            </tr>
                    </table>
                   
                    <br />
                    <div class="Table">
                        <div class="Row">
                            <div class="Cell">
                               Please select a csv file to upload &nbsp;&nbsp;
                                <asp:FileUpload ID="fileUpload" runat="server" />
                               <asp:Button ID="btnUpload" runat="server" Text="Submit" OnClick="btnUpload_Click" OnClientClick="return confirm('Are you sure you want to send cancellation?');" />
                            </div>
                        </div>
                        <div class="Row">
                            <div class="Cell">
                                </div>
                            </div>
                        <div class="Row">
                            <div class="Cell">
                             <asp:Label ID="lblmg" runat="server" Text="" Width="450px" Height="10px"></asp:Label>
                            </div>
                            <div class="Row">
                                <div id="flname" class="Cell" runat="server">
                                </div>
                                <div id="flrecordcount" class="Cell" runat="server">
                                </div>
                                <div id="flsize" class="Cell" runat="server">
                                </div>
                            </div>
                        </div>
                    </div>

                </fieldset>
            </div>


            <div class="centerFieldset" runat="server" id="divGrid">
                <fieldset>
                    <p>List of all products cancelled along with the status</p> <br />
                    <div class="Table">

                        <div class="Row">
                            <div class="Cell">
                                <asp:GridView ID="grdCsv" runat="server" OnSelectedIndexChanging="grdCsv_SelectedIndexChanging" PageSize="5" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
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
