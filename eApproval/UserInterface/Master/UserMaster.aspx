<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserMaster.aspx.cs" Inherits="eApproval.UserInterface.Master.UserMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">


    <script type="text/javascript">



        function ListDivClicked() {



            var btnList = $('#<%= btnList.ClientID %>');
        if (btnList != null) {



           // btnList.click();



        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    

    <table style="margin-left: 30px; margin-top: 20px;">
        <tr>
            <td>
                <asp:Label ID="lblName" runat="server" Text="User Master" Font-Names="Arial" Font-Bold="true" ForeColor="#0f4069" Font-Size="25px"></asp:Label>
            </td>
        </tr>
    </table>
    <br />


    <table style="margin-left:30px;">
        <tr>
            <td>
                <asp:TabContainer ID="TabContainerItem" runat="server" ActiveTabIndex="0" Width="1280px" Height="430px">

                    <asp:TabPanel ID="tabNew" runat="server">

                        <HeaderTemplate>
                            <div style="width: 80px;">

                                <asp:Label ID="lblNewTab" runat="server"></asp:Label>

                            </div>


                        </HeaderTemplate>
                        <ContentTemplate>


                            <div style="height: 425px; overflow: auto; width: 1253px;  margin-left: 5px; margin-top: 1px; 
	
	
background: -webkit-linear-gradient(-90deg, #d2def3, #bbbbc1); ">
                                <div style="height: 7px; overflow: auto; width: 1253px; background-color: #0f3d5b"></div>



                                <table style="margin-top: 40px; margin-left: 200px;">
                                    <tr>
                                        <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblUserID" runat="server" Text="User ID" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td style="width:320px;">
                                            <asp:TextBox ID="txtUserID" runat="server" CssClass="Colouredtextbox"></asp:TextBox>
                                        </td>

                                         <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblUserName" runat="server" Text="User Name" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUserName" runat="server" CssClass="Colouredtextbox uppercase"></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>

                                        <td style="width: 160px; height: 50px;">
                                            <asp:Label ID="lblPassword" runat="server" Text="Password" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassword" runat="server" CssClass="Colouredtextbox uppercase"></asp:TextBox>
                                        </td>


                                        <td style="width: 160px; height: 50px;">
                                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="Colouredtextbox uppercase"></asp:TextBox>
                                        </td>


                                    </tr>


                                      <tr>

                                        <td style="width: 160px; height: 50px;">
                                            <asp:Label ID="lblFullName" runat="server" Text="Full Name" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtFullName" runat="server" CssClass="Colouredtextbox uppercase"></asp:TextBox>
                                        </td>


                                        <td style="width: 160px; height: 50px;">
                                            <asp:Label ID="lblEmailID" runat="server" Text="Email ID" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmailID" runat="server" CssClass="Colouredtextbox uppercase"></asp:TextBox>
                                        </td>


                                    </tr>

                                    <tr>

                                         <td style="width: 160px; height: 50px;">
                                            <asp:Label ID="lblMobileNo" runat="server" Text="Mobile No" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="Colouredtextbox uppercase"></asp:TextBox>
                                        </td>




                                        <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblDept" runat="server" Text="Department" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlDept" CssClass="ColouredDropdown" runat="server"></asp:DropDownList>
                                        </td>

                                       




                                    </tr>
                                    <tr>
                                           <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblRole" runat="server" Text="Role" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>

                                        <td>
                                            <asp:DropDownList ID="ddlRole" CssClass="ColouredDropdown" runat="server"></asp:DropDownList>
                                        </td>

                                        <td style="width: 150px; height: 50px;">
                                            <asp:Label ID="lblSignature" runat="server" Text="Signature" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="15px"></asp:Label>

                                        </td>

                                        <td>
                                             <asp:FileUpload ID="FileUploadToServer" CssClass="FileUploadControl"  runat="server" />
                                        </td>
                                    </tr>


                                </table>

                                    <table style="margin-top:30px; margin-left:520px;">
                                      <tr >
                                      <td   style="width:180px;">
                                          <asp:Button ID="btnSave" runat="server"  CssClass="myButton" ></asp:Button>
                                      </td>
                                      <td >
                                          &nbsp;<asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="myButton" ></asp:Button>
                                      </td>
                                  </tr></table>



                            </div>
                        </ContentTemplate>

                    </asp:TabPanel>
                    <asp:TabPanel ID="tabList" runat="server">
                        <HeaderTemplate>
                            <div id="ListDiv" runat="server" style="width: 80px; height: 100px;" onclick="javascript:ListDivClicked(); return true;">List </div>
                        </HeaderTemplate>
                        <ContentTemplate>



                            <center>

        <div style="height: 400px; overflow: auto; width: 900px;" >

        
    
            </div>
    </center>




                        </ContentTemplate>
                    </asp:TabPanel>





                </asp:TabContainer>
            </td>
        </tr>
    </table>


     <asp:ImageButton ID="btnList"  style="display:none" runat="server" ></asp:ImageButton>

</asp:Content>
