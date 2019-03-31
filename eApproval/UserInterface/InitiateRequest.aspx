<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="InitiateRequest.aspx.cs" Inherits="eApproval.UserInterface.InitiateRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        //Validations

        function EmptyControlValidations() {

            debugger;
            var Location = document.getElementById("<%=ddlLocation.ClientID%>").value;

            if (Location == "--Select--") {
                alert('Please Select Valid Location..');
                document.getElementById("<%=ddlLocation.ClientID%>").focus();
                return false;
            }
            var SanctionRequestFor = document.getElementById("<%=txtSectionRequestFor.ClientID%>").value;
            if (SanctionRequestFor == "") {
                alert('Sanction Request For is Required..');
                document.getElementById("<%=txtSectionRequestFor.ClientID%>").focus();
                return false;
            }

            var SrType = document.getElementById("<%=ddlSrType.ClientID%>").value;
            if (SrType == "0") {
                alert('Please Select SR Type..');
                document.getElementById("<%=ddlSrType.ClientID%>").focus();
                return false;
            }

            var AccountHead = document.getElementById("<%=ddlAccountHead.ClientID%>").value;

            if (AccountHead == "--Select--") {
                alert('Please Select Valid Account Head..');
                document.getElementById("<%=ddlAccountHead.ClientID%>").focus();
                return false;
            }


            var VendorRecomended = document.getElementById("<%=txtVendorRecommended.ClientID%>").value;
            if (VendorRecomended == "") {
                alert('Recomended Vendor Name is Required..');
                document.getElementById("<%=txtVendorRecommended.ClientID%>").focus();
                return false;
            }
            var JustificationDetails = document.getElementById("<%=txtJustification.ClientID%>").value;
            if (JustificationDetails == "") {
                alert('Justification Details is Required..');
                document.getElementById("<%=txtJustification.ClientID%>").focus();
                return false;
            }
            var ChkInitiate = document.getElementById("<%=chkInitiate.ClientID%>");
            if (!ChkInitiate.checked) {
                alert('Please Check the Initiate CheckBox..');
                document.getElementById("<%=chkInitiate.ClientID%>").focus();
                return false;
            }


        }

    </script>







    <script type="text/javascript">

        //checking Approval checkboxes mutually
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }

        //Delete Validation

        function DeleteValidation() {
            if (confirm('Do You Want To Delete the Document')) {
                return true;
            }
            else {
                return false;
            }

        }





    </script>


    <script type="text/javascript">


        function showFileNames() {
            debugger;

            document.getElementById("<%= lblItems.ClientID%>").innerHTML = "";

            var FileUploadedCount = document.getElementById("<%=FileUpload.ClientID%>").files.length;

            var HiddenFieldCount = parseInt(document.getElementById("<%=HiddenFieldCount.ClientID %>").value);

            var FinalFileLength = FileUploadedCount + HiddenFieldCount;

            if (FinalFileLength <= 5) {

                var res = "";

                for (var i = 0; i < FinalFileLength; i++) {

                    if (!GetExtension(document.getElementById("<%=FileUpload.ClientID%>").files[i].name)) {
                        document.getElementById("<%=FileUpload.ClientID%>").value = '';
                        return;
                    }

                    if (document.getElementById("<%=FileUpload.ClientID%>").files[i].size > 2097152) {
                        alert('Each File Size should be less than 2MB only');
                        document.getElementById("<%=FileUpload.ClientID%>").value = '';
                        return;
                    }




                    if (document.getElementById("<%=FileUpload.ClientID%>").files[i]) {
                        res += document.getElementById("<%=FileUpload.ClientID%>").files[i].name + "<br />";
                    }

                }

                document.getElementById("<%= lblItems.ClientID%>").innerHTML = res;
            }
            else {
                alert('Allows only 5 Documents');
                document.getElementById("<%=FileUpload.ClientID%>").value = '';
                return false;
            }
        }

        function GetExtension(UploadedFileName) {
            var LstIndex = UploadedFileName.lastIndexOf(".");


            var chkExn = UploadedFileName.substring(LstIndex);

            switch (chkExn.toLowerCase()) {
                case ".pdf":
                case ".jpg":
                case ".doc":
                case ".docx":
                case ".txt":
                case ".xls":
                case ".xlsx":
                case ".png":
                case ".bak":

                    return true;
                    break;

                default:
                    alert(UploadedFileName + "  is not valid Document \n Allows only Pdf,Text,Images,Word and Excel");
                    return false;
            }


        }

    </script>





    <style type="text/css">
        .hiddencol {
            display: none;
        }

        .modalBackgroundFileUpload {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
            border-radius: 8px;
        }

        .modalPopupFileUpload {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            padding-right: 0px;
            width: 700px;
            height: 250px;
            border-radius: 8px;
        }
    </style>


   


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="MainUpdatePannel" runat="server">
        <ContentTemplate>


           <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="MainUpdatePannel">
                <ProgressTemplate>
                    <div class="modal">
                        <div class="center">
                            <img alt="" src="/Images/Loader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>


            <asp:HiddenField ID="HiddenFieldCount" Value="0" runat="server" />


            <table style="margin-left: 20px; margin-top: 30px;">

                <tr>
                    <td>

                        <asp:Label ID="lblName" runat="server" Text="Sanction Request" Font-Size="25px" Font-Bold="true" ForeColor="#0f4069"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>

                        <%-- <asp:Label ID="Label1" runat="server" Text="Starts the Approval Process" Font-Size="13px" Font-Bold="false" ForeColor="#0f4069"></asp:Label>--%>
                    </td>
                </tr>


            </table>



            <div style="width: 1300px; margin-left: 30px; margin-top: -25px;">
                <center><div>
            <asp:Image runat="server" ImageUrl="~/Images/khazana logo Maroon.png" Width="200px" Height="80px" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="KHAZANA GOLD & DIAMOND DMCC" Font-Size="18px" Font-Bold="true" ForeColor="#0f4069"></asp:Label>
            
        </div></center>

                <div style="width: 1300px; margin-left: 0px; margin-top: 5px; height: 25px; background-color: #0f4069">
                    <table style="margin-left: 10px; padding-top: 0px;">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblInitiate" Text="Initiate" Font-Names="Arial" Font-Size="18px" Font-Bold="false" ForeColor="White"></asp:Label>
                            </td>
                            <td style="padding-left: 1140px;">
                                <asp:LinkButton ID="lnkbtnUploadFiles" runat="server" Text="Upload Doc" Font-Size="13px" ForeColor="White"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                </div>

                <asp:Panel ID="pnlInititate" runat="server" BorderStyle="Groove">
                    <center>
                <br />
            <table>
                <tr style="height:30px;">
                    <td style="width:180px">
                        <asp:Label ID="lblSrNo" runat="server" Text="SR No" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>
                    </td>
                    <td style="padding-right:80px; width:150px;">
                        <asp:TextBox ID="txtSRNo" runat="server" Width="120px"  CssClass="Colouredtextbox"></asp:TextBox>
                    </td>
                    <td style="padding-right:30px; text-align:right">
                       

                        <asp:Label ID="Label3" runat="server" Text="Location" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td style="padding-right:50px;">
                                                <asp:DropDownList ID="ddlLocation" Width="160px" CssClass="ColouredDropdown" runat="server"></asp:DropDownList>

                    </td>
                    <td style="padding-right:30px; text-align:right"">
                                                <asp:Label ID="Label4" runat="server" Text="Date" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td style="padding-right:50px;">
                               <asp:TextBox ID="txtDate" runat="server" Width="120px" CssClass="Colouredtextbox"></asp:TextBox>

                    </td>
                </tr>
                <tr style="height:30px;">
                    <td>
                       
                     <asp:Label ID="Label5" runat="server" Text="Originating Department" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtOriginatingDept" runat="server" Width="250px" CssClass="Colouredtextbox"></asp:TextBox>
                    </td>

                    <td style="padding-right:30px; text-align:right">
                       

                        <asp:Label ID="lblSrType" runat="server" Text="Sr Type" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td style="padding-right:50px;">
                                     <asp:DropDownList ID="ddlSrType" Width="160px" CssClass="ColouredDropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlSrType_SelectedIndexChanged" runat="server">
                                         <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                         <asp:ListItem Text="Regular SR" Value="1"></asp:ListItem>
                                         <asp:ListItem Text="Rate SR" Value="2"></asp:ListItem>
                                     </asp:DropDownList>

                    </td>
                    <td style="padding-right:30px; text-align:right"">
                                                <asp:Label ID="lblOldSRno" runat="server" Text="Old SR no" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td style="padding-right:50px;">
                                       <asp:DropDownList ID="ddloldSrNo" Width="125px" CssClass="ColouredDropdown" runat="server"></asp:DropDownList>


                    </td>





                   
                </tr>
                 <tr style="height:30px;">
                    <td>
                        
                     <asp:Label ID="Label6" runat="server" Text="Sanction Request For" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtSectionRequestFor" runat="server" Width="250px"  CssClass="Colouredtextbox"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr style="height:30px;">
                    <td>
                        
                     <asp:Label ID="Label7" runat="server" Text="Account Head Applicable" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAccountHead" Width="252px" CssClass="ColouredDropdown" runat="server"></asp:DropDownList>
                      
                    </td>
                   
                </tr>
                <tr style="height:30px;">
                    <td>
                        
                     <asp:Label ID="Label8" runat="server" Text="Amount In AED" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtAmountInAED" runat="server" Width="120px" OnTextChanged="txtAmountInAED_TextChanged" AutoPostBack="true"  CssClass="Colouredtextbox"></asp:TextBox>

                        <asp:FilteredTextBoxExtender ID="ftxtAmountInAED" runat="server" FilterType="Numbers,Custom" ValidChars="." TargetControlID="txtAmountInAED" />
                    </td>
                   <td style="width:130px">
                      
                     <asp:Label ID="Label9" runat="server" Text="Amount In Words" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtAmountInWords" runat="server" Width="350px"  CssClass="Colouredtextbox"></asp:TextBox>
                    </td>

                    <td></td>
                </tr>
                 <tr style="height:30px;">
                   
                   <td>
                       
                     <asp:Label ID="Label10" runat="server" Text="Vendor Recommended" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td>
                        <asp:TextBox ID="txtVendorRecommended" runat="server" Width="300px"  CssClass="Colouredtextbox"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr style="height:30px;">
                   
                   <td>
                        
                     <asp:Label ID="Label11" runat="server" Text="Justification Details" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                    </td>
                    <td colspan="3">
                        <asp:TextBox ID="txtJustification" runat="server" Width="500px" Height="80px" TextMode="MultiLine" CssClass="Colouredtextbox"></asp:TextBox>
                    </td>
                </tr>
                
                </table>

                <table style="margin-left:-20px;" >
                
                
                
                 <tr >

                     <td  style="width:200px; ">
                     <asp:Label ID="Label1" runat="server" Text="Uploaded Documents" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>

                     </td>

                     <td style="padding-left:0;">

                          <div style="font-family:Arial;">

        <asp:GridView ID="grdUploadData" runat="server" Width="400px" ShowHeaderWhenEmpty="true" EmptyDataRowStyle-CssClass="EmptyDataRowStyleGridview" OnRowCreated="grdUploadData_RowCreated" EmptyDataText="No Documents Uploaded" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">

            
            <Columns>
               <%-- <asp:BoundField DataField="SNO" HeaderText="S.No" HeaderStyle-Width="80px">
                    <ItemStyle Font-Size="12px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>--%>

                 <asp:TemplateField HeaderText="S No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="80px" Font-Size="12px" HorizontalAlign="Center" />
                     <HeaderStyle Width="80px" />
            </asp:TemplateField>

                <asp:BoundField DataField="FileName" HeaderText="Doc Name" HeaderStyle-Width="300px" >
                    <ItemStyle Width="300px" Font-Size="12px" Height="25px"></ItemStyle>
                </asp:BoundField>
                 <asp:TemplateField HeaderStyle-Width="80px" HeaderText="View" ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnView"  runat="server" OnCommand="btn_Command" ImageUrl="~/Images/View.png"  Width="15px" Height="15px" ToolTip="View Doc" CommandName="View" />
                     </ItemTemplate>
                 </asp:TemplateField>
                  <asp:TemplateField HeaderStyle-Width="80px" HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnDelete" runat="server" OnClientClick="return DeleteValidation()"  OnCommand="btn_Command" ImageUrl="~/Images/Delete.png" Width="13px" Height="13px" ToolTip="Delete Doc" CommandName="Delete" />
                     </ItemTemplate>
                 </asp:TemplateField>
     
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#bec3c5" Font-Bold="True" Height="30px" Font-Size="13px" ForeColor="black" />
            <PagerStyle BackColor="#dde2e7" ForeColor="#000066" HorizontalAlign="Center" Wrap="True" CssClass="GridPager" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>

                              <br />
    </div>


                     </td>


                   
                   <td  style="padding-left:100px;">
                       <br /><br />
                        <%--<center><asp:CheckBox ID="chkInitiate1" runat="server" Text="   Initiate"  Font-Size="14pt" ForeColor="Black"  Font-Names="Times New Roman" Width="120px"/></center>--%>

                       <asp:CheckBox  runat="server" Text="   Initiate" ID="chkInitiate" OnCheckedChanged="chkInitiate_CheckedChanged" Font-Size="14pt" ForeColor="Black"  Font-Names="Times New Roman" Width="120px" AutoPostBack="true" />
                    </td>
                    
                     <td  style="text-align:center; padding-left:10px;">
                         <br /><br />
                         <asp:Image ID="ImgInitiaterSignature" runat="server" Width="200px" Height="50px"/>
                         <br />

                     <asp:Label ID="Label12" runat="server" Text="Initiator /Head of the Dept" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>



                    </td>

                     <td style="padding-left:20px;">
                         <br />
                         <asp:Button ID="btninitiateSubmit" runat="server" Text="Submit" OnClick="btninitiateSubmit_Click"  OnClientClick="return EmptyControlValidations()" CssClass="myButton"></asp:Button>
                     </td>
                    
                </tr>
            </table>
                </center>
                </asp:Panel>
                <div style="width: 1300px; margin-left: 0px; margin-top: 5px; height: 25px; background-color: #0f4069">
                    <table style="margin-left: 10px; padding-top: 0px;">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="Label13" Text="Routing Department :<b> Finance / Accounts</b>" Font-Names="Arial" Font-Size="15px" Font-Bold="false" ForeColor="White"></asp:Label>
                            </td>

                        </tr>
                    </table>

                </div>
                <asp:Panel ID="pnlFinanceRoutingDept" CssClass="pnlDisabledClass" ToolTip="Access is Disabled" runat="server" BorderStyle="Groove">



                    <table style="margin-left: 130px;">

                        <tr>
                            <td style="width: 120px">

                                <asp:Label ID="Label14" runat="server" Text="Comments" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>


                            </td>
                            <td>
                                <br />

                                <asp:TextBox ID="txtFinanceComments" runat="server" Width="500px" Height="80px" TextMode="MultiLine" CssClass="Colouredtextbox"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>

                            <td colspan="4" style="padding-left: 610px;">

                                <center><asp:CheckBox ID="chkFinanceApprove" runat="server" Text="   Approve" Font-Size="14pt" ForeColor="Black"  Font-Names="Times New Roman" Width="120px"/></center>
                            </td>

                            <td colspan="2" style="text-align: center; padding-left: 26px;">

                                <asp:Image ID="Image1" runat="server" Width="200px" Height="50px" />
                                <br />

                                <asp:Label ID="Label15" runat="server" Text="Finance Controller" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>


                            </td>
                            <td style="padding-left: 20px;">

                                <asp:Button ID="btnFinanceSubmit" runat="server" Text="Submit" CssClass="myButton"></asp:Button>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>

                <div style="width: 1300px; margin-left: 0px; margin-top: 5px; height: 25px; background-color: #0f4069">
                    <table style="margin-left: 10px; padding-top: 0px;">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="Label16" Text="Forwarded for Approval to : <b>CEO</b>" Font-Names="Arial" Font-Size="15px" Font-Bold="false" ForeColor="White"></asp:Label>
                            </td>

                        </tr>
                    </table>

                </div>
                <asp:Panel ID="pnlCEOApproval" CssClass="pnlDisabledClass" ToolTip="Access is Disabled" runat="server" BorderStyle="Groove">



                    <table style="margin-left: 130px;">

                        <tr>
                            <td style="width: 120px">

                                <asp:Label ID="Label17" runat="server" Text="Comments" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>


                            </td>
                            <td>
                                <br />

                                <asp:TextBox ID="txtCEOComments" runat="server" Width="500px" Height="80px" TextMode="MultiLine" CssClass="Colouredtextbox"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>

                            <td colspan="4" style="padding-left: 490px;">

                                <%--<center><asp:CheckBox ID="chkCEOApprove" runat="server" Text="   Approve" Font-Size="14pt" ForeColor="Black"  Font-Names="Times New Roman" Width="120px"/></center>--%>

                                <asp:CheckBoxList runat="server" ID="chkCEOApprove" Font-Size="14pt" ForeColor="Black" Font-Names="Times New Roman" Width="260px" AutoPostBack="True" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Approve" Value="Approve" onclick="MutExChkList(this);"> </asp:ListItem>
                                    <asp:ListItem Text="DisApprove" Value="Disapprove" onclick="MutExChkList(this);"> </asp:ListItem>

                                </asp:CheckBoxList>
                            </td>

                            <td colspan="2" style="text-align: center; padding-left: 10px;">

                                <asp:Image ID="imgCEO" runat="server" Width="200px" Height="50px" />
                                <br />

                                <asp:Label ID="Label18" runat="server" Text="CEO" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>


                            </td>
                            <td style="padding-left: 20px;">

                                <asp:Button ID="btnCEOSubmit" runat="server" Text="Submit" CssClass="myButton"></asp:Button>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>


                <div style="width: 1300px; margin-left: 0px; margin-top: 5px; height: 25px; background-color: #0f4069">
                    <table style="margin-left: 10px; padding-top: 0px;">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="Label19" Text="Forwarded for Approval to : <b>Managing Director</b>" Font-Names="Arial" Font-Size="15px" Font-Bold="false" ForeColor="White"></asp:Label>
                            </td>

                        </tr>
                    </table>

                </div>
                <asp:Panel ID="pnlMDApproval" Visible="false" CssClass="pnlDisabledClass" ToolTip="Access is Disabled" runat="server" BorderStyle="Groove">



                    <table style="margin-left: 130px;">

                        <tr>
                            <td style="width: 120px">

                                <asp:Label ID="Label20" runat="server" Text="Comments" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="14px"></asp:Label>


                            </td>
                            <td>
                                <br />

                                <asp:TextBox ID="txtMDComments" runat="server" Width="500px" Height="80px" TextMode="MultiLine" CssClass="Colouredtextbox"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>

                            <td colspan="4" style="padding-left: 490px;">

                                <%--<center><asp:CheckBox ID="chkCEOApprove" runat="server" Text="   Approve" Font-Size="14pt" ForeColor="Black"  Font-Names="Times New Roman" Width="120px"/></center>--%>

                                <asp:CheckBoxList runat="server" ID="chkMDApprove" Font-Size="14pt" ForeColor="Black" Font-Names="Times New Roman" Width="260px" AutoPostBack="True" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Approve" Value="Approve" onclick="MutExChkList(this);"> </asp:ListItem>
                                    <asp:ListItem Text="DisApprove" Value="Disapprove" onclick="MutExChkList(this);"> </asp:ListItem>

                                </asp:CheckBoxList>
                            </td>

                            <td colspan="2" style="text-align: center; padding-left: 10px;">

                                <asp:Image ID="imgMD" runat="server" Width="200px" Height="50px" />
                                <br />

                                <asp:Label ID="Label21" runat="server" Text="Managing Director" ForeColor="Black" Font-Bold="True" Font-Names="Times New Roman" Font-Size="16px"></asp:Label>


                            </td>
                            <td style="padding-left: 20px;">

                                <asp:Button ID="Button1" runat="server" Text="Submit" CssClass="myButton"></asp:Button>
                            </td>
                        </tr>
                    </table>

                </asp:Panel>

            </div>



            <asp:ModalPopupExtender ID="ModalPopupExtender" BehaviorID="ModalPopupExtenderBID" runat="server" PopupControlID="pnlPopup" TargetControlID="lnkbtnUploadFiles" CancelControlID="btnItemMasterClose" BackgroundCssClass="modalBackgroundFileUpload"></asp:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopupFileUpload" align="center" Style="margin-top: 30px; display: none">

                <div style="width: 690px; align-items: flex-end;">
                    <asp:ImageButton ID="btnItemMasterClose" ImageUrl="~/Images/closebtn.png" ImageAlign="Right" Height="18px" Width="18px" runat="server" />
                </div>
                <br />

                <fieldset>
                    <legend>
                        <h3><font color="Black" style="font-family: 'Times New Roman'">Upload Documents</font></h3>
                    </legend>

                    <table style="margin-top: 10px;">



                        <tr>

                            <td style="width: 300px">
                                <asp:FileUpload ID="FileUpload" runat="server" CssClass="FileUploadControl" AllowMultiple="true" onchange="return showFileNames()" />
                            </td>


                            <td style="padding-right: 30px;">

                                <asp:Button Text="Add" runat="server" Width="80px" ID="btnOk" OnClick="btnOk_Click" CssClass="myButton" />
                            </td>

                            <td>

                                <%--<asp:Button Text="Clear" runat="server" ID="btnClear" CssClass="myButton" />--%>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="2">

                                <div style="width: 300px; height: 130px;">
                                    <asp:Label ID="lblItems" Font-Size="14px" Font-Names="Arial" ForeColor="Black" runat="server"></asp:Label>
                                </div>


                            </td>

                            <td></td>
                        </tr>
                    </table>




                </fieldset>
            </asp:Panel>




        </ContentTemplate>

        <Triggers>

            <asp:PostBackTrigger ControlID="btnOk" />


        </Triggers>
    </asp:UpdatePanel>








</asp:Content>
