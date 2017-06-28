<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="BOTS_WL._default" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CIT</title>
    <link href="main.css" rel="stylesheet" />
    <link rel="shortcut icon" href="images/fav.ico" type="image/icon"/>
    <link rel="icon" href="images/fav.ico" type="image/icon"/>


</head>
<body>
    <form id="form1" defaultbutton="btn_search" runat="server">

        <section>
            <div>
                
                
            </div>
            <div class="centerFieldset">
                <fieldset>

                    <table class="tableheader">
                        <tr>
                            <td style="text-align: left; font-size: 35px;">CUSTOMER INTERACTION TOOL
                                <img src="images/DASH_TRANSP.png" style="height: 29px; width: 42px" /></td>

                            <td style="text-align: right;">Welcome&nbsp;<asp:Label ID="Logged_User" runat="server" Text="Label" Style="font-weight: 700"></asp:Label>!<span>
                                <asp:Label ID="lbluserid" runat="server"></asp:Label>
                                <asp:Button ID="btn_logout" runat="server" Text="LOG OUT" Width="85px" OnClick="btn_logout_Click" CssClass="button" /></span></td>
                        </tr>

                    </table>


                    <div class="header_line"></div>

                    <table class="sub_header_line">
                        <tr>
                            <td style="text-align: left; font-family: Dax-Regular; font-size: 12px; color: red; font-weight: bold;">
                                JUMP TO&nbsp;
                                <asp:DropDownList ID="drpjumpto" runat="server" AutoPostBack="True" Width="55px" OnSelectedIndexChanged="drpjumpto_SelectedIndexChanged">
                                </asp:DropDownList>
                                 &nbsp;&nbsp;&nbsp;RECORD COUNTER
                                <asp:Label ID="lblcounter" runat="server" Font-Bold="True" Font-Size="Medium"></asp:Label>
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>



                            </td>
                            <td style="text-align: center; font-family: Dax-Regular; font-size: 12px; color: blue; font-weight: bold;">
                                <asp:ImageButton ID="Img_feedback" runat="server"  src="images/feedback1.png" Height="40px" Width="122px"  /></td>
                            <td style="text-align: right; font-family: Dax Dax-Regular; font-size: 12px; color: blue; font-weight: bold;">

                                <span id="error" style="color: Red; display: none">* Input digits (0 - 9)</span>
                                <asp:TextBox ID="txtsearch" runat="server" value="Enter Advertiser Id" onBlur="if(this.value=='')this.value=' Enter Advertiser id'" onFocus="this.value=''" MaxLength="9" Height="22px"></asp:TextBox><asp:Button ID="btn_search" runat="server" Text="SEARCH" CssClass="s_nextbutton" OnClick="btn_search_Click" Height="23px" />

                            </td>
                        </tr>
                    </table>

                    <div class="header_line"></div>

                    <div class="Table">
                        <div class="Row">
                            <div class="Cell">
                                RECORDS<br />
                                <asp:TextBox ID="txtreccnt" runat="server" ReadOnly="True" Width="77px" EnableViewState="True" CssClass="text_align"></asp:TextBox>
                            </div>
                            <div class="Cell">
                                RECORD STATUS<br />
                                <asp:DropDownList ID="drp_rec_status" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drp_rec_status_SelectedIndexChanged" Width="135px" CssClass="text_align"></asp:DropDownList>
                            </div>
                            <div class="Cell">
                                IGEN RETIREMENT STATUS<br />
                                <asp:DropDownList ID="drp_ret_status" runat="server" AutoPostBack="True" Width="135px" OnSelectedIndexChanged="drp_ret_status_SelectedIndexChanged" CssClass="text_align"></asp:DropDownList>
                            </div>
                            <div class="sortcell">
                                SORT BY<br />
                                <asp:DropDownList ID="drp_sort" runat="server" AutoPostBack="True" Width="150px" OnSelectedIndexChanged="drp_sort_SelectedIndexChanged" CssClass="text_align">
                                </asp:DropDownList>

                            </div>
                            <div class="sortcell">
                                <asp:RadioButtonList ID="rdosort" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdosort_SelectedIndexChanged" CssClass="drp_size" Height="1px" Width="95px">
                                    <asp:ListItem Value="ASC" Text="ASC" Selected="True">ASCENDING</asp:ListItem>
                                    <asp:ListItem Value="DESC" Text="DESC">DESCENDING</asp:ListItem>
                                </asp:RadioButtonList>
                                
                            </div>
                            <div class="Cell">

                                <asp:Button ID="btn_callback" runat="server" Text="CALL BACK" Width="80px" OnClick="btn_callback_Click" CssClass="button" />
                            </div>
                            <div class="Cell">

                                <asp:Button ID="btn_finalise" runat="server" Text="FINALISED" Width="80px" OnClick="btn_finalise_Click" CssClass="button" />
                            </div>
                             <div class="Cell">
                                <asp:Button ID="btnback" runat="server" Text="BACK" Width="80px" OnClick="btn_back" CssClass="nextbutton" />

                            </div>
                            <div class="Cell">
                                <asp:Button ID="btnnext" runat="server" Text="NEXT" Width="80px" OnClick="btn_next" CssClass="nextbutton" />

                            </div>
                        </div>
                    </div>

                    <table>
                        <thead>
                            <tr>

                                <th colspan="3">NOTES<br />

                                </th>

                            </tr>
                        </thead>

                        <tr>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtnotes" ReadOnly="false" runat="server" CssClass="textbox_align" TextMode="MultiLine" Width="618px" MaxLength="255" onKeyUp="textCounter(this,'count_display',255);" onBlur="textCounter(this,'count_display',255);"  Height="39px" ToolTip="Click to Copy"></asp:TextBox>
                                <asp:Label ID="lblnotesdt" runat="server" Text=""></asp:Label>
                            </td>
                            <td>&nbsp;<span><asp:Label ID="count_display" runat="server" ForeColor="black"></asp:Label></span>

                            </td>
                            <td>&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btn_save_notes" runat="server" Text="SAVE NOTES" CssClass="button" OnClick="btn_save_notes_Click" Width="85px" OnClientClick="chkblanktxt();" />
                            </td>

                            <td style="text-align: left;"></td>

                        </tr>
                    </table>


                    <table>
                        <thead>
                            <tr>
                                <th colspan="3">ADVERTISER INFORMATION</th>
                                <th>CONTACT INFORMATION</th>
                            </tr>
                        </thead>
                        <tr>
                            <td>ADVERTISER ID<br />
                                <asp:TextBox ID="txtadvid" ReadOnly="false" runat="server" ToolTip="Single click to copy" CssClass="clstd" onclick="copytoclipbrd('txtadvid');"></asp:TextBox>

                            </td>
                            <td>HIERARCHY IND<br />
                                <asp:TextBox ID="txthryind" runat="server" Width="25px" CssClass="text_align"></asp:TextBox>
                            </td>
                            <td>MEMBERS COUNT<br />
                                <asp:TextBox ID="txtmemcnt" runat="server" Width="25px" CssClass="text_align"></asp:TextBox></td>

                            <td>CONTACT NUMBER<br />
                                <asp:TextBox ID="txtcnbr" ReadOnly="True" runat="server" ToolTip="Single click to copy" CssClass="clstd" onclick="copytoclipbrd('txtcnbr');" Width="138px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>ADVERTISER NAME<br />
                                <asp:TextBox ID="txtadvname" ReadOnly="True" runat="server" CssClass="textbox_align" Width="321px"></asp:TextBox>
                            </td>
                            <td></td>
                            <td></td>
                            <td>CONTACT NAME<br />
                                <asp:TextBox ID="txtcn" ReadOnly="True" runat="server" CssClass="textbox_align" Width="340px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="width: 110px;">SUBURB<br />
                                <asp:TextBox ID="txtsuburb" ReadOnly="True" runat="server" CssClass="textbox_align" Width="165px"></asp:TextBox>

                            </td>
                            <td style="width: 110px;">STATE<br />
                                <asp:TextBox ID="txtstate" ReadOnly="True" runat="server" CssClass="textbox_align" Width="43px"></asp:TextBox></td>
                            <td style="width: 110px;">POST CODE<br />
                                <asp:TextBox ID="txtpostcode" ReadOnly="True" runat="server" CssClass="textbox_align" Width="43px"></asp:TextBox></td>
                            <td style="width: 110px;">HBL<br />
                                <asp:TextBox ID="txthbl" ReadOnly="True" runat="server" CssClass="textbox_align" Width="38px"></asp:TextBox></td>
                            <td style="width: 110px;">EMAIL ADDRESS<br />
                                <asp:TextBox ID="txtemailadd" ReadOnly="True" runat="server" CssClass="textbox_align" Width="340px"></asp:TextBox></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>WEBSITE<br />
                                <asp:TextBox ID="txtweb" ReadOnly="True" runat="server" CssClass="textbox_align" Width="321px"></asp:TextBox></td>



                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>

                    </table>
                    <table>
                        <thead>
                            <tr>
                                <th colspan="5"><span>SPEND</span> <span>DETAILS</span></th>
                            </tr>
                        </thead>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style11">ASSIGNED PREVIOUS</td>
                            <td>COMMISSION PREVIOUS</td>

                            <td>COMMISSION CURRENT</td>
                            <td>RETIREMENT STATUS</td>
                        </tr>

                        <tr style="background-color: #EBE9C3;">
                            <td style="text-align: right;">CURRENT ISSUE</td>
                            <td>
                                <asp:TextBox ID="txtasscurramt" ReadOnly="True" runat="server" CssClass="textbox_align" Width="70px"></asp:TextBox>
                            </td>
                            <td>

                                <asp:TextBox ID="txtcomcur" ReadOnly="True" runat="server" CssClass="textbox_align" Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcurrspamt" ReadOnly="True" runat="server" CssClass="textbox_align" Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtretstatus" ReadOnly="True" runat="server" CssClass="textbox_align" Width="224px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">PREVIOUS ISSUE</td>
                            <td>
                                <asp:TextBox ID="txtassprevamt" ReadOnly="True" runat="server" CssClass="textbox_align" Width="70px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtcomprev" ReadOnly="True" runat="server" CssClass="textbox_align" Width="70px"></asp:TextBox>
                            </td>

                            <td>
                                <asp:TextBox ID="txtprevspamt" ReadOnly="True" runat="server" CssClass="textbox_align" Width="70px"></asp:TextBox>
                            </td>
                            <td>&nbsp;</td>

                        </tr>
                    </table>
                    <table>
                        <thead>
                            <tr>
                                <th colspan="4" class="auto-style2"><span>PRODUCT</span> <span>ATTRIBUTES</span></th>
                            </tr>
                        </thead>
                        <tr>
                            <td style="text-align:right;font-weight:bold;">PRIMARY CATEGORY NAME</td>
                            <td colspan="3" class="auto-style1">
                                <asp:TextBox ID="txtheading" runat="server" Width="384px" CssClass="clstd"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align:right;font-weight:bold;">LIVE PRODUCTS<br />

                            </td>
                            <td colspan="3" class="auto-style1">
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">STANDALONE PRODUCTS<br />

                            </td>
                            <td>
                                <asp:TextBox ID="txtstandprod" ReadOnly="True" runat="server" CssClass="textbox_align" Width="251px"></asp:TextBox><br />
                            </td>
                            <td>SALES SERVICE MONTH
                                <br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtservmonth" runat="server" CssClass="textbox_align" Width="85px"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td style="text-align: right" class="auto-style14">VALUE PACKAGE PRODUCTS<br />
                            </td>
                            <td colspan="1">
                                <asp:TextBox ID="txtvalpkgprod" ReadOnly="True" runat="server" CssClass="textbox_align" Width="251px"></asp:TextBox><br />
                            </td>
                            <td>EARLY VP ANNIVERSARY DATE<br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtevpanndate" runat="server" CssClass="textbox_align" Width="85px"></asp:TextBox></td>

                        </tr>
                        <tr>
                            <td style="text-align: right" class="auto-style14">NETWORK PRODUCTS</td>
                            <td>

                                <asp:TextBox ID="txtnetwprod" ReadOnly="True" runat="server" CssClass="textbox_align" Width="251px"></asp:TextBox><br />
                            </td>
                            <td>EARLY LIBT REBOOKING DATE
                            </td>
                            <td>
                                <asp:TextBox ID="txtlibtrdate" runat="server" CssClass="textbox_align" Width="85px"></asp:TextBox></td>

                        </tr>
                        <tr>
                            <td style="text-align: right" class="auto-style14">DIRECTORY COUNT
                            </td>
                            <td>
                                <asp:TextBox ID="txtdircnt" ReadOnly="True" runat="server" CssClass="textbox_align" Width="25px"></asp:TextBox>&nbsp;
                      DOMINANT UDAC
                                <asp:TextBox ID="txtdominantudac" runat="server" Width="45px"></asp:TextBox>
                                &nbsp;&nbsp; EARLY CLOSING DIR
                                <asp:TextBox ID="txtearlycldir" runat="server" Width="45px"></asp:TextBox>
                            </td>

                            <td>EARLY CLOSING DIR DATE
                            </td>
                            <td>
                                <asp:TextBox ID="txtecldirdate" runat="server" CssClass="textbox_align" Width="85px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <br />
                            </td>

                        </tr>

                    </table>

                    <table>
                        <thead>
                            <tr>
                                <th colspan="7">ADDITIONAL INFORMATION</th>
                            </tr>
                        </thead>
                        <tr>
                            <td colspan="2">CREDIT CLASS<br />
                                <asp:TextBox ID="txtcrcl" ReadOnly="True" runat="server" CssClass="textbox_align" Width="45px"></asp:TextBox>
                            </td>
                            <td>OVERDUE AMOUNT<br />
                                <asp:TextBox ID="txtoverdamt" runat="server" Width="45px"></asp:TextBox>
                            </td>
                            <td>OVERDUE DAYS<br />
                                <asp:TextBox ID="txtoverddays" runat="server" Width="45px"></asp:TextBox>
                            </td>
                            <td>CLAIMS COUNT<br />
                                <asp:TextBox ID="txtclaimscnt" ReadOnly="True" runat="server" CssClass="textbox_align" Width="45px"></asp:TextBox>
                            </td>
                            <td>SELF SERVICE INDICATOR<br />
                                <asp:TextBox ID="txtselfservind" ReadOnly="True" runat="server" CssClass="textbox_align" Width="25px"></asp:TextBox>
                            </td>
                            <td>FIRST YEAR ADVERTISER<br />
                                <asp:TextBox ID="txtfirstyradv" ReadOnly="True" runat="server" CssClass="textbox_align" Width="25px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </fieldset>

            </div>
        </section>
        <br />

        <footer>&copy;Copyright Sensis Pty Ltd</footer>
    </form>
    <script src="Js_funcs.js"></script>
</body>
</html>
