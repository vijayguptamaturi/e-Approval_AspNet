<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="eApproval.UserInterface.Master.UserProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%-- Loding Image CSS--%>
    <style type="text/css">
        .modal {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }

        .center {
            z-index: 1000;
            margin-top: 300px;
            margin-left: 500px;
            padding: 10px;
            width: 80px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }

            .center img {
                height: 80px;
                width: 80px;
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


            <table style="margin-left: 20px; margin-top: 30px;">

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


            <div style="width: 1300px; margin-left: 25px; margin-top: 30px; height: 30px; background-color: #0f4069">
                <table style="margin-left: 10px; padding-top: 4px;">
                    <tr>
                        <td>
                            <asp:Label runat="server" ID="lblUProfile" Text="User Profile" Font-Bold="false" ForeColor="White"></asp:Label>
                        </td>
                        <td style="padding-left: 1090px;">
                            <asp:LinkButton ID="lblAddNewProfile" runat="server" Text="Add New Profile" PostBackUrl="~/UserInterface/Master/UserProfile_New.aspx" Font-Size="12px" ForeColor="White"></asp:LinkButton>
                        </td>
                    </tr>
                </table>

            </div>

            <div style="font-family: Arial; margin-left: 25px;">

                <asp:GridView ID="grdUserProfile" runat="server" OnSelectedIndexChanged="grdUserProfile_SelectedIndexChanged" AllowPaging="true" Width="1300px" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnPageIndexChanging="grdUserProfile_PageIndexChanging" OnPreRender="grdUserProfile_PreRender">


                    <Columns>
                        <asp:BoundField DataField="USERID" HeaderText="User ID" HeaderStyle-Width="100px">
                            <ItemStyle Width="100px" Font-Size="12px" HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="USERNAME" HeaderText="User Name" HeaderStyle-Width="300px">
                            <ItemStyle Width="300px" Font-Size="12px" Height="25px"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="UFULLNAME" HeaderText="Full Name" HeaderStyle-Width="400px">
                            <ItemStyle Width="400px" Font-Size="12px"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="EMAILID" HeaderText="Email ID" HeaderStyle-Width="300px">
                            <ItemStyle Width="300px" Font-Size="12px"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="MOBILENO" HeaderText="Mobile No" HeaderStyle-Width="200px">
                            <ItemStyle Width="200px" Font-Size="12px"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="DEPTID" HeaderText="Dept ID" HeaderStyle-Width="100px">
                            <ItemStyle Width="100px" Font-Size="12px" CssClass="HideGridcolumns"></ItemStyle>
                            <HeaderStyle CssClass="HideGridcolumns" />
                        </asp:BoundField>

                        <asp:BoundField DataField="DEPTNAME" HeaderText="Dept Name" HeaderStyle-Width="250px">
                            <ItemStyle Width="250px" Font-Size="12px"></ItemStyle>
                        </asp:BoundField>

                        <asp:BoundField DataField="ROLEID" HeaderText="Role ID" HeaderStyle-Width="100px">
                            <ItemStyle Width="100px" Font-Size="12px" CssClass="HideGridcolumns"></ItemStyle>
                            <HeaderStyle CssClass="HideGridcolumns" />
                        </asp:BoundField>

                        <asp:BoundField DataField="ROLENAME" HeaderText="Role" HeaderStyle-Width="300px">
                            <ItemStyle Width="300px" Font-Size="12px"></ItemStyle>
                        </asp:BoundField>

                        <asp:TemplateField HeaderStyle-Width="100px" HeaderText="Action" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/EditImage.png" Width="15px" Height="15px" ToolTip="Edit Profile" CommandName="Select" />
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

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
