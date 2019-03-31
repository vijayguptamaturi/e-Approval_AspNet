<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PasswordChange.aspx.cs" Inherits="eApproval.UserInterface.PasswordChange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script type="text/javascript">

        function EmptyControlValidations() {

            var OldPassword = document.getElementById("<%=txtOldPassword.ClientID%>").value;
               if (OldPassword == "") {
                   alert('Pls Enter Old Password..');
                   document.getElementById("<%=txtOldPassword.ClientID%>").focus();
                       return false;
                   }


                   var NewPassword = document.getElementById("<%=txtNewPassword.ClientID%>").value;
               if (NewPassword == "") {
                   alert('Pls Enter New Password..');
                   document.getElementById("<%=txtNewPassword.ClientID%>").focus();
                   return false;
               }

               var ConfirmPassword = document.getElementById("<%=txtConfirmPassword.ClientID%>").value;
               if (ConfirmPassword == "") {
                   alert('Pls Enter Confirm Password..');
                   document.getElementById("<%=txtConfirmPassword.ClientID%>").focus();
                       return false;
               }

            if (NewPassword != ConfirmPassword)
            {
               // alert('Pls Enter Confirm Password..');
                document.getElementById("<%=txtNewPassword.ClientID%>").focus();
                return false;
            }
                 

               }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel runat="server" ID="MainUpdatePannel">
        <ContentTemplate>

            <table style="margin-left: 20px; margin-top: 30px;">

                <tr>
                    <td>

                        <asp:Label ID="lblName" runat="server" Text="Change Password" Font-Size="25px" Font-Bold="true" ForeColor="#0f4069"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>

                        <%--<asp:Label ID="Label1" runat="server" Text="Create and View the Profiles" Font-Size="13px" Font-Bold="false" ForeColor="#0f4069"></asp:Label>--%>
                    </td>
                </tr>


            </table>


            <div style="width: 1300px; margin-left: 25px; margin-top: 30px; height: 30px; background-color: #0f4069">
                <table style="margin-left: 10px; padding-top: 4px;">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblChangePass" Text="Change Password" Font-Bold="false" ForeColor="White"></asp:Label>
                        </td>



                    </tr>
                </table>

            </div>

            <div style="margin-left: 25px">

                <asp:Panel ID="pnlcontent" runat="server" BorderColor="#999999" BorderWidth="1px" Width="1298px" Height="400px">

                    <div style="margin-top: 15px; margin-right: 30px; width: 1290px; text-align: right">



                        <asp:CompareValidator ID="CmppasswordValidator" ControlToCompare="txtNewPassword" Font-Bold="true" Font-Size="12px" ControlToValidate="txtConfirmPassword" ValidationGroup="GP" runat="server" ForeColor="Red" ErrorMessage="* Password & Confirm Passwords are not Matched.."></asp:CompareValidator>
                    </div>

                    <table style="margin-top: 10px; margin-left: 480px;">
                        <tr>
                            <td style="width: 130px; height: 50px;">
                                <asp:Label ID="lblUserID" runat="server" Text="User ID" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                            </td>

                            <td style="width: 320px;">
                                <asp:TextBox ID="txtUserID" Enabled="false" runat="server" CssClass="Colouredtextbox"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="width: 150px; height: 50px;">
                                <asp:Label ID="lblUserName" runat="server" Text="User Name" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                            </td>


                            <td style="width: 300px;">
                                <asp:TextBox ID="txtUserName" runat="server" Enabled="false" CssClass="Colouredtextbox"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>

                            <td style="width: 130px; height: 50px;">
                                <asp:Label ID="lblOldPassword" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px" ForeColor="Black" Text="Old Password"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldPassword" runat="server" CssClass="Colouredtextbox" TextMode="Password"></asp:TextBox>
                            </td>

                        </tr>

                        <tr>

                            <td style="width: 130px; height: 50px;">
                                <asp:Label ID="lblNewPassword" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px" ForeColor="Black" Text="New Password"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtNewPassword" runat="server" CssClass="Colouredtextbox" TextMode="Password"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td style="width: 160px; height: 50px;">
                                <asp:Label ID="lblConfirmPassword" runat="server" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px" ForeColor="Black" Text="Confirm Password"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="Colouredtextbox" TextMode="Password"></asp:TextBox>
                            </td>


                        </tr>

                    </table>

                    <table style="margin-top: 30px; margin-left: 520px;">
                        <tr>
                            <td style="width: 180px;">
                                <asp:Button ID="btnSave" Text="Save" runat="server" CssClass="myButton" ValidationGroup="GP" OnClientClick="return EmptyControlValidations();" OnClick="btnSave_Click" ></asp:Button>
                            </td>
                            <td>&nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="myButton" OnClick="btnReset_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
