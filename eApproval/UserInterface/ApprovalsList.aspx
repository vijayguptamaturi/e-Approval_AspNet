<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ApprovalsList.aspx.cs" Inherits="eApproval.UserInterface.ApprovalsList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

       <asp:UpdatePanel ID="MainUpdatePannel" runat="server"><ContentTemplate>

            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="MainUpdatePannel">
                <ProgressTemplate>
                    <div class="modal">
                        <div class="center">
                            <img alt="" src="/Images/Loader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
     

     <table style="margin-left:20px; margin-top:30px;">
        
        <tr>
            <td>

                <asp:Label ID="lblName" runat="server" Text="Approvals" Font-Size="25px" Font-Bold="true" ForeColor="#0f4069"></asp:Label>
            </td>
        </tr>

         <tr>
            <td>

                <asp:Label ID="Label1" runat="server" Text="To Make Approvals" Font-Size="13px" Font-Bold="false" ForeColor="#0f4069"></asp:Label>
            </td>
        </tr>

      
    </table>


    <div style="width:1300px; margin-left:25px; margin-top:30px;height:30px; background-color:#0f4069">
        <table style="margin-left:10px; padding-top:4px;">
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblUProfile"  Text="List of Approvals" Font-Bold="false" ForeColor="White"></asp:Label>
                </td>
                <%--<td style="padding-left:1090px;">
                    <asp:LinkButton ID="lblAddNewProfile" runat="server" Text="Add New Profile" PostBackUrl="~/UserInterface/Master/UserProfile_New.aspx" Font-Size="12px" ForeColor="White"></asp:LinkButton>
                </td>--%>
            </tr>
        </table>

    </div>

    <div style="font-family:Arial;margin-left:25px;">

        <asp:GridView ID="grdApprovalList" runat="server" EmptyDataText="No Approvals Pending" EmptyDataRowStyle-CssClass="EmptyDataRowStyleGridview"  OnSelectedIndexChanged="grdApprovalList_SelectedIndexChanged"  AllowPaging="true" Width="1300px" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"  OnPageIndexChanging="grdApprovalList_PageIndexChanging" OnPreRender="grdApprovalList_PreRender" >

            
            <Columns>

                  <asp:TemplateField HeaderText="S.no">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
                <ItemStyle Width="50px" Font-Size="12px" HorizontalAlign="Center" />
                     <HeaderStyle Width="50px" />
            </asp:TemplateField>


                <asp:BoundField DataField="SRNO" HeaderText="SR no" HeaderStyle-Width="100px">
                    <ItemStyle Width="100px" Font-Size="12px" HorizontalAlign="Center"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="INITIATOR" HeaderText="Initiator" HeaderStyle-Width="150px" >
                    <ItemStyle Width="150px" Font-Size="12px" Height="25px"></ItemStyle>
                </asp:BoundField>

                 <asp:BoundField DataField="INITIATINGDEPT" HeaderText="Initiating Dept" HeaderStyle-Width="220px">
                    <ItemStyle Width="220px" Font-Size="12px"></ItemStyle>
                </asp:BoundField>

                 <asp:BoundField DataField="SRDATE" HeaderText="SR Date" HeaderStyle-Width="80px" >
                    <ItemStyle Width="80px" Font-Size="12px"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" HeaderStyle-Width="200px">
                    <ItemStyle Width="200px" Font-Size="12px"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="AMOUNT" HeaderText="Amount" HeaderStyle-Width="80px">
                    <ItemStyle Width="80px" Font-Size="12px" HorizontalAlign="Center" ></ItemStyle>
                    <HeaderStyle   />
                </asp:BoundField>

                <asp:BoundField DataField="PROCESSLEVEL" HeaderText="Process Level" HeaderStyle-Width="220px" >
                    <ItemStyle Width="220px" Font-Size="12px"></ItemStyle>
                </asp:BoundField>

                <asp:BoundField DataField="STATUS" HeaderText="Status" HeaderStyle-Width="60px">
                    <ItemStyle Width="60px" Font-Size="12px"></ItemStyle>
                    <HeaderStyle />
                </asp:BoundField>

               

                 <asp:TemplateField HeaderStyle-Width="50px" HeaderText="View" ItemStyle-HorizontalAlign="Center">
                     <ItemTemplate>
                         <asp:ImageButton ID="btnView" runat="server" ImageUrl="~/Images/View.png" Width="15px" Height="15px" ToolTip="View SR Request" CommandName="Select" />
                     </ItemTemplate>
                 </asp:TemplateField>
            
            </Columns>
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#bec3c5" Font-Bold="True" Height="40px" Font-Size="13px" ForeColor="black" />
            <PagerStyle BackColor="#dde2e7" ForeColor="#000066" HorizontalAlign="Center" Wrap="True" CssClass="GridPager" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />
        </asp:GridView>
    </div>

        </ContentTemplate></asp:UpdatePanel>

</asp:Content>
