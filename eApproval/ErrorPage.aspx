<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="eApproval.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Error Page</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../Css/IndexStyle.css" rel="stylesheet" />
    <link runat="server" rel="shortcut icon" href="~/Images/E Logo.png" type="image/x-icon" />

    <style type="text/css">
        .ks-swing {
            width: 800px;
            height: 250px;
            background-color: white;
            margin-left: 270px;
            margin-top: 130px;
            -webkit-animation: swinging 5s ease-in-out 0s infinite;
            -moz-animation: swinging 5s ease-in-out 0s infinite;
            animation: swinging 3s ease-in-out 0s 1;
            -webkit-transform-origin: 50% 0;
            -moz-transform-origin: 50% 0;
            transform-origin: 50% 0;
            box-shadow: 10px 10px 5px grey;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div style="height: 50px; width: 1350px; background-color: #0f4069; margin-top: -5px; margin-left: -5px">
            <table style="margin-left: 20px; margin-top: -2px;">

                <tr>
                    <td>
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/E logo.png" Width="50px" Height="50px" />
                    </td>
                    <td style="margin-left: -10px; width: 600px;">
                        <asp:Label runat="server" Text="- Approval" Font-Bold="true" ForeColor="WhiteSmoke" Height="30px" Font-Size="22px" Width="197px"></asp:Label>

                    </td>


                    <td></td>
                    <td style="width: 50px; padding-left: 550px;"></td>

                    <td style="padding-left: 1px;">
                        <asp:LinkButton ID="lnkbtnLogin" runat="server" Font-Bold="true" Text="LOG IN" CssClass="lnkButtons" Font-Size="12px" PostBackUrl="~/Portal/Index.aspx"></asp:LinkButton>

                    </td>

                </tr>

            </table>


        </div>


        <div class="ks-swing">

            <div style="width: 800px; height: 30px; background-color: #0f4069;">

                <table>

                    <tr>
                        <td style="width: 780px">
                            <div style="margin-left: 15px; margin-top: 0px; padding-top: -3px;">
                                <asp:Label Font-Bold="false" Text="ERROR" ForeColor="WhiteSmoke" Font-Names="Times New Roman" Font-Size="18px" runat="server"></asp:Label></div>

                        </td>

                        <td>
                            <div style="padding-top: 3px">
                                <asp:Image ID="Image1" ImageUrl="~/Images/closebtn.png" Height="20px" Width="20px" runat="server" /></div>

                        </td>
                    </tr>
                </table>


            </div>

            <br />
            <br />
            <br />
            <br />

            <center>
    
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
    
</center>

        </div>





        <div style="height: 30px; margin-top: 205px; width: 1350px; margin-left: -10px; background-color: #0f4069;">
            <br />
            <center>
         <table style="margin-left: 20px; margin-top: -13px;align-content:center">
             <tr>
                 <td  style="padding-left:25px;color:white;">
                     @ Copyright from 2016
                 </td>
                  <td  style="padding-left:25px;color:white;">
                        Designed By - 
                 </td>
                   <td  style="padding-left:25px;color:white;">
                       Vijay Gupta 
                 </td>
             </tr>
</table>
         </center>

        </div>

    </form>
</body>
</html>
