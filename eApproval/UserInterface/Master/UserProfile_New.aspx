<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserProfile_New.aspx.cs" Inherits="eApproval.UserInterface.Master.UserProfile_New" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script type="text/javascript">

        function EmptyControlValidations()

        {

            var btnName = document.getElementById("<%=btnSave.ClientID%>").value;

            //Insert User

            if (btnName == "Save")
            {
                var Uname = document.getElementById("<%=txtUserName.ClientID%>").value;
                if (Uname == "")
                {
                    alert('User Name is Required..');
                    document.getElementById("<%=txtUserName.ClientID%>").focus();
                    return false;
                }
                var Password = document.getElementById("<%=txtPassword.ClientID%>").value;
                if (Password == "") {
                    alert('Password is Required..');
                    document.getElementById("<%=txtPassword.ClientID%>").focus();
                    return false;
                }

                var ConfirmPassword = document.getElementById("<%=txtConfirmPassword.ClientID%>").value;
                if (ConfirmPassword == "") {
                    alert('Confirm Password is Required..');
                    document.getElementById("<%=txtConfirmPassword.ClientID%>").focus();
                    return false;
                }
                var FullName = document.getElementById("<%=txtFullName.ClientID%>").value;
                if (FullName == "") {
                    alert('User Full Name is Required..');
                    document.getElementById("<%=txtFullName.ClientID%>").focus();
                    return false;
                }
                var Email = document.getElementById("<%=txtEmailID.ClientID%>").value;
                if (Email == "") {
                    alert('Email ID is Required..');
                    document.getElementById("<%=txtEmailID.ClientID%>").focus();
                    return false;
                }
                var MobileNo = document.getElementById("<%=txtMobileNo.ClientID%>").value;
                if (MobileNo == "") {
                    alert('Mobile No is Required..');
                    document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                    return false;
                }

                var DeptName = document.getElementById("<%=ddlDept.ClientID%>").value;
                if (DeptName == "--Select--") {
                    alert('Pl select Department');
                    document.getElementById("<%=ddlDept.ClientID%>").focus();
                    return false;
                }

                var RoleName = document.getElementById("<%=ddlRole.ClientID%>").value;
                if (RoleName == "--Select--") {
                    alert('Pl select Role');
                    document.getElementById("<%=ddlRole.ClientID%>").focus();
                    return false;
                }


            }
            else

                //Update User
            {
                var Uname = document.getElementById("<%=txtUserName.ClientID%>").value;
                if (Uname == "") {
                    alert('User Name is Required..');
                    document.getElementById("<%=txtUserName.ClientID%>").focus();
                    return false;
                }
                var FullName = document.getElementById("<%=txtFullName.ClientID%>").value;
                if (FullName == "") {
                    alert('User Full Name is Required..');
                    document.getElementById("<%=txtFullName.ClientID%>").focus();
                    return false;
                }
                var Email = document.getElementById("<%=txtEmailID.ClientID%>").value;
                if (Email == "") {
                    alert('Email ID is Required..');
                    document.getElementById("<%=txtEmailID.ClientID%>").focus();
                    return false;
                }
                var MobileNo = document.getElementById("<%=txtMobileNo.ClientID%>").value;
                if (MobileNo == "") {
                    alert('Mobile No is Required..');
                    document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                    return false;
                }

                var DeptName = document.getElementById("<%=ddlDept.ClientID%>").value;
                if (DeptName == "--Select--") {
                    alert('Pl select Department');
                    document.getElementById("<%=ddlDept.ClientID%>").focus();
                    return false;
                }

                var RoleName = document.getElementById("<%=ddlRole.ClientID%>").value;
                if (RoleName == "--Select--") {
                    alert('Pl select Role');
                    document.getElementById("<%=ddlRole.ClientID%>").focus();
                    return false;
                }

            }
        }

    </script>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <%--  <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>

     <table style="margin-left:20px; margin-top:30px;">
        
        <tr>
            <td>

                <asp:Label ID="lblName" runat="server" Text="User Profile" Font-Size="25px" Font-Bold="true" ForeColor="#0f4069"></asp:Label>
            </td>
        </tr>

         <tr>
            <td>

                <asp:Label ID="Label1" runat="server" Text="Create and View the Profiles" Font-Size="13px" Font-Bold="false" ForeColor="#0f4069"></asp:Label>
            </td>
        </tr>

      
    </table>


     <div style="width:1300px; margin-left:25px; margin-top:30px;height:30px; background-color:#0f4069">
        <table style="margin-left:10px; padding-top:4px;">
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblUProfile" Font-Bold="false" ForeColor="White"></asp:Label>
                </td>

                 <td style="padding-left:1140px;">
                    <asp:LinkButton runat="server" ID="lnkBack" Text="Back"  ForeColor="White" PostBackUrl="~/UserInterface/Master/UserProfile.aspx"></asp:LinkButton>
                </td>
                
            </tr>
        </table>

    </div>

    <div style="margin-left:25px">

    <asp:Panel ID="pnlcontent" runat="server" BorderColor="#999999" BorderWidth="1px" Width="1298px" Height="400px">

         <div style=" margin-top:10px; margin-right:30px; width:1290px; text-align:right " >

              <asp:RegularExpressionValidator ID="RegexpFileUpload" runat="server" ControlToValidate="FileUploadToServer"
     ErrorMessage="* Only .jpg, .png and .jpeg" Font-Size="14px" Font-Bold="true" ValidationGroup="Group1" Font-Names="Times New Roman" ForeColor="Red" Display="Dynamic"
     ValidationExpression="(.*\.([Jj][Pp][Gg])|.*\.([pP][nN][gG])$)"></asp:RegularExpressionValidator>



             <asp:RegularExpressionValidator ID="EmailIDValidator" runat="server" ControlToValidate="txtEmailID"
        ErrorMessage="* Pl Enter Valid Mail ID" Font-Size="14px" Font-Bold="true" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Group1" Font-Names="Times New Roman" ForeColor="Red"></asp:RegularExpressionValidator>



        <asp:CompareValidator ID="CmppasswordValidator" ControlToCompare="txtPassword" Font-Bold="true" Font-Size="12px" ControlToValidate="txtConfirmPassword" ValidationGroup="Group1" runat="server" ForeColor="Red" ErrorMessage="* Password & Confirm Passwords are not Matched.." Display="Dynamic"></asp:CompareValidator>
        </div>

     <table style="margin-top: 10px; margin-left: 230px;">
                                    <tr>
                                        <td style="width: 130px; height: 50px;">
                                            <asp:Label ID="lblUserID" runat="server" Text="User ID" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>
                                        <td style="width:320px;">
                                            <asp:TextBox ID="txtUserID" Enabled="false" runat="server" CssClass="Colouredtextbox"></asp:TextBox>
                                        </td>

                                         <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblUserName" runat="server" Text="User Name" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td >
                                        <td style="width: 300px;">
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="Colouredtextbox"></asp:TextBox>
                                        </td>

                                        </tr>

                                   
                                
                                      


                                  



                                    <tr>

                                        <td style="width: 130px; height: 50px;">
                                            <asp:Label ID="lblPassword" runat="server" Text="Password" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="Colouredtextbox" TextMode="Password"></asp:TextBox>
                                        </td>


                                        <td style="width: 160px; height: 50px;">
                                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="Colouredtextbox" TextMode="Password"></asp:TextBox>
                                           
                                        </td>

                                    </tr>

                                         
                                      <tr>

                                        <td style="width: 130px; height: 50px;">
                                            <asp:Label ID="lblFullName" runat="server" Text="Full Name" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFullName" runat="server" CssClass="Colouredtextbox"></asp:TextBox>
                                        </td>


                                        <td style="width: 160px; height: 50px;">
                                            <asp:Label ID="lblEmailID" runat="server" Text="Email ID" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="Colouredtextbox"></asp:TextBox>
                                        </td>


                                    </tr>

                                    <tr>

                                         <td style="width: 130px; height: 50px;">
                                            <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="Colouredtextbox"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="ftxtMobileNo" runat="server" FilterType="Numbers" TargetControlID="txtMobileNo" />
                                        </td>




                                        <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblDept" runat="server" Text="Department" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDept" CssClass="ColouredDropdown" runat="server"></asp:DropDownList>
                                        </td>

                                       




                                    </tr>
                                    <tr>
                                           <td style="width: 130px; height: 50px;">
                                            <asp:Label ID="lblRole" runat="server" Text="Role" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlRole" CssClass="ColouredDropdown" runat="server"></asp:DropDownList>
                                        </td>

                                        <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblSignature" runat="server" Text="Signature" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>

                                        </td>

                                        <td>
                                             <asp:FileUpload ID="FileUploadToServer" CssClass="FileUploadControl"  runat="server" />
                                        </td>
                                    </tr>


                                </table>

                                    <table style="margin-top:30px; margin-left:520px;">
                                      <tr >
                                      <td   style="width:180px;">
                                          <asp:Button ID="btnSave" runat="server" CssClass="myButton" OnClientClick="return EmptyControlValidations();" OnClick="btnSave_Click" ></asp:Button>
                                      </td>
                                      <td >
                                          &nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="myButton" OnClick="btnReset_Click" ></asp:Button>
                                      </td>
                                  </tr></table>
        </asp:Panel></div>

</asp:Content>
