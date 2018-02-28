<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="ExecuteAnonymous.aspx.cs" Inherits="APFProductUpdateProj.ExecuteAnonymous" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
        $(document).ready(function () {
            $('#btnsearch,#btnsend').click(function (e) {
                if ($.trim($('#txteanonymous').val()) == '') {
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

    </script>
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
            <div class="loading">
                Processing.Please wait ..
                        <img src="images/loader.gif" alt="" />
            </div>
            <div class="centerFieldset">
                <fieldset>
                    <div>
                        <h2>Apex Execute</h2>
                        <h3>Enter Apex code to be executed as an anonymous block:</h3>
                    </div>
                    <div>
                        <asp:TextBox ID="txteanonymous" runat="server" Height="184px" Width="851px" TextMode="MultiLine" Style="text-align: left;"></asp:TextBox>
                        <br />
                        <asp:Button ID="btnExecute" runat="server" OnClick="btnExecute_Click" Text="Execute" />&nbsp;
            <asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
                    </div>
                    <br />
                    <div id="divout" runat="server">
                        
                    </div>
                </fieldset>
            </div>
        </section>
    </form>
</body>
</html>
