<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="eApproval.Portal.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
      .Settingsdiv
      {
          
          width:250px;
          height:250px;
          
          
      }
      .Settingsdiv:hover
      {
          cursor:pointer;
      }

       

            /* hide tooltip */
            div.Settingsdiv span {
                display: none;
                
            }

            /* show and style tooltip */
            div.Settingsdiv:hover span {
                /* show tooltip */
                display: block;
                /* position relative to container div.tooltip */
                /*position:absolute;*/
                bottom: 1em;
              
               
                /* prettify */
                /*padding: 0.5em;*/
                color: #000000;
                background: #ebf4fb;
                border: 0.1em solid #b7ddf2;
                /* round the corners */
                border-radius: 0.5em;
                /* prevent too wide tooltip */
                max-width: 13em;

                margin-top:10px;
                margin-left:40px;

                /*margin-top:-250px;*/
                text-align:center;
            }



    </style>
    
    
 

    <script src="../Js/Jquery.js"></script>
    <script src="../Js/Notify.min.js"></script>

    <script>
   
       
	
        function WelcomeNote(UName)
        {
            $.notify('Welcome ' + UName, "success");
        }
        
    </script>
    <title>Home</title>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="MainUpdatePannel" runat="server">
        <ContentTemplate>

    <%-- <div style="padding-top:50px;">
        <div style="background-color:#d3d3d3;width:1330px;height:150px;margin-left:5px;">
            <div style="padding-top:5px;padding-left:5px;">
            <h3><font color="Blue" font-weight="true">ABOUT E - APPROVAL TOOL</font></h3>
                
            <p>This Tool can be used to Request the Applications as per your departments and get approval by higher authorities and above...</p>
            </div>
            </div>

        

    </div>--%>

      <table style="margin-left: 20px; margin-top: 20px;">

                <tr>
                    <td>

                        <asp:Label ID="lblName" runat="server" Text="e-Approval" Font-Size="25px" Font-Bold="true" ForeColor="#0f4069"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>

                        <%-- <asp:Label ID="Label1" runat="server" Text="Starts the Approval Process" Font-Size="13px" Font-Bold="false" ForeColor="#0f4069"></asp:Label>--%>
                    </td>
                </tr>


            </table>


     <div style="width:1300px; margin-left:25px; margin-top:20px;height:30px; background-color:#0f4069">
        <table style="margin-left:10px; padding-top:4px;">
            <tr>
                <td>
                    <asp:Label runat="server" ID="lblUProfile"  Text="View List" Font-Bold="false" ForeColor="White"></asp:Label>
                </td>
                <%--<td style="padding-left:1090px;">
                    <asp:LinkButton ID="lblAddNewProfile" runat="server" Text="Add New Profile" PostBackUrl="~/UserInterface/Master/UserProfile_New.aspx" Font-Size="12px" ForeColor="White"></asp:LinkButton>
                </td>--%>
            </tr>
        </table>

    </div>

    <div style="font-family:Arial;margin-left:25px;">

        <asp:GridView ID="grdApprovalViewList" runat="server" EmptyDataText="No Data Found" EmptyDataRowStyle-CssClass="EmptyDataRowStyleGridview"  OnSelectedIndexChanged="grdApprovalViewList_SelectedIndexChanged"  AllowPaging="true" Width="1300px" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3"  OnPageIndexChanging="grdApprovalViewList_PageIndexChanging" OnPreRender="grdApprovalViewList_PreRender" PageSize="5" >

            
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



    <br />
   <table style="width:1350px;margin-left:40px; margin-top:40px;"> 
       <tr>
           <td>
               <div class="Settingsdiv" id="RequsetsDiv" runat="server">
        <br />
       <center> <asp:ImageButton ID="ImgbtnInitiateRequest" runat="server" ImageUrl="~/Images/InitiateRequest.png" Width="150px" Height="150px" PostBackUrl="~/UserInterface/InitiateRequest.aspx"></asp:ImageButton>
           
        <br /><br />
        <b>Intiate Request</b></center>
        
        
        <span >Initiate your Request...</span>


    </div>
                  <div class="Settingsdiv" id="ApprovalDiv" runat="server">
        <br />
       <center> <asp:ImageButton ID="ImgbtnRequestApprove" runat="server" ImageUrl="~/Images/RequestApprove.png" Width="150px" Height="150px"  ></asp:ImageButton>
           
        <br /><br />
        <b>Approve Requests</b></center>
        
        
        <span>Approve request by verifying documents and details...</span>


    </div>


           </td>

           <td>
    <div class="Settingsdiv">
        <br />
       <center> <asp:ImageButton ID="ImgbtnReports" runat="server" ImageUrl="~/Images/Reports.png" Width="150px" Height="150px" ></asp:ImageButton>
        <br /><br />
        <b>Views</b></center>
        
        
        <span >You can check detalils of requests by selecting this option...</span>


    </div>

</td>




           <td>
    <div class="Settingsdiv">
        <br />
       <center> <asp:ImageButton ID="ImgbtnMyProfile" runat="server" ImageUrl="~/Images/MyProfile.png" Width="150px" Height="150px" ></asp:ImageButton>
           
        <br /><br />
        <b>Profile</b></center>
        
        
      <span>You Can view your full details on clicking on Profile...</span>


    </div>
               </td>




           <td>
               <div class="Settingsdiv">
        <br />
       <center> <asp:ImageButton ID="ImgbtnSettings" runat="server" ImageUrl="~/Images/Settings.png" Width="150px" Height="150px" ></asp:ImageButton>
        <br /><br />
        <b>Settings</b></center>
        
        
        <span>You Can Change your Password Details by selecting Settings Option...</span>


    </div>
           </td>

          
           </tr>
   </table>

   <br /><br />

                                    <button id="basicSuccessCustomDelay" class="btn btn-info" style="display:none">Info</button>



    </ContentTemplate> </asp:UpdatePanel>

</asp:Content>
