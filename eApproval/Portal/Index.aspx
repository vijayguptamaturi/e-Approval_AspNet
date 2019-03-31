<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="eApproval.Portal.Index" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WELCOME TO E-APPROVAL APPLICATION</title>
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="../Css/IndexStyle.css" rel="stylesheet" />
    
    <link href="../Css/bootstrap.css" rel="stylesheet" />
    <script src="../Js/Jquery.js"></script>
    <script src="../Js/bootstrap.js"></script>

    <script type="text/javascript">

        function EmptyValidation() {
            var Uname = document.getElementById("<%=txtUserName.ClientID%>").value;
            var Password = document.getElementById("<%=txtPassword.ClientID%>").value;

            if (Uname == "") {
                alert('Please Enter User Name');
                return false;
            }

            if (Password == "") {
                alert('Please Enter Password ');
                return false;
            }
        }





    </script>

    <script type="text/javascript">
        function ShowPopup() {
            $("#lnkbtnLogin").click();
        }
    </script>  

     <link runat="server" rel="shortcut icon" href="~/Images/E Logo.png" type="image/x-icon"/>
 
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <div style="height: 50px; width: 1350px; background-color: #0f4069;">
        <table style="margin-left: 20px; margin-top: -2px;">

            <tr>
                <td >
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/E logo.png" Width="50px" Height="50px" />
                </td>
                <td style="margin-left: -10px;width:600px;">
                    <asp:Label runat="server" Text="- Approval" Font-Bold="true" ForeColor="WhiteSmoke" Height="30px" Font-Size="22px" Width="197px"></asp:Label>

                </td>


                <td>

                    
                   <%-- <section class="main">
                        <div class="wrapper-demo" >
                            <div id="dd" class="wrapper-dropdown-5" tabindex="1">
                                John Doe
					
                                <ul class="dropdown">
                                    <li><a href="#"><i class="icon-user"></i>Profile</a></li>
                                    <li><a href="#"><i class="icon-cog"></i>Settings</a></li>
                                    <li><a href="#"><i class="icon-remove"></i>Log out</a></li>
                                </ul>
                            </div>
                            ​
                        </div>
                    </section>
                     --%>

                </td>
                <td style="width:50px;padding-left:550px;">
                   <asp:LinkButton ID="lnkbtnHome" runat="server" Font-Bold="true" Text="HOME" CssClass="lnkButtons" Font-Size="12px" PostBackUrl="~/Portal/Index.aspx"></asp:LinkButton>

                </td>
                 
                 <td  style="padding-left:25px;"> 
                   <asp:LinkButton ID="lnkbtnLogin" runat="server" Font-Bold="true" Text="LOG IN" CssClass="lnkButtons" Font-Size="12px" PostBackUrl="#" data-toggle="modal" data-target="#exampleModal" data-whatever="@mdo"></asp:LinkButton>

                </td>

            </tr>

        </table>




    </div>

        <br />
        <br />

     

        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" style="margin-top:120px;">
  <div class="modal-dialog" role="document">
    <div class="modal-content">
      <div class="modal-header">
       
          
         <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
       <center> <h4 class="modal-title" id="exampleModalLabel"><strong>Log In</strong></h4></center>
      </div>
      <div class="modal-body">
        <form>
          <div class="form-group">
              <asp:Label ID="lblUName" Font-Bold="true" runat="server" CssClass="control-label">UserName :</asp:Label>
            <br />
             <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>

             
          </div>
          <div class="form-group">
           <asp:Label ID="lblPassword" Font-Bold="true" runat="server" CssClass="control-label">Password :</asp:Label>
              <br/>
              <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox> 
            
          </div>
        </form>
      </div>
      <div class="modal-footer">
          <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-default" data-dismiss="modal" />
         <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary" Text="Log In" OnClientClick="return EmptyValidation();" OnClick="btnLogin_Click"/>
       
      </div>
    </div>
  </div>
</div>
  
    </form>
    
 
    <center>
     <div style="padding-left:130px;padding-top:120px;">
            <div class="row">
                <div class="col-md-4">
                   
    	<div style="width:250px;"  class="service_box">
        <center><img src="../Images/Initiate.png" alt="Strategic Planing Icon" width="100px" height="100px"/>
        <h2>Initiate Sanction Request</h2>
        <p> Request your SR and get approval by next level of authority...       </p>
       <%-- <div class="more_button"><a href="#">Read more</a></div></center>--%>
        </div>
        
      
                </div>
                <div class="col-md-4">
                     <div class="service_box">
        <center><img src="../Images/MiddleApproval.png" alt="Strategic Planing Icon" width="100px" height="100px"/>
        <h2>Get Approval</h2>
        <p>Verify Required documnets and get approved with authorized signature...</p>
        
        </div>
                </div>
                <div class="col-md-4">   <div class="service_box">
        <center><img src="../Images/FinalApproved.png" alt="Strategic Planing Icon"  width="100px" height="100px" />
        <h2>Final Approval</h2>
        <p>Final Approval is done by CEO of your company....</p>
        
        </div></div>
            </div>
        </div></center>
   

   
   
     <div style="height: 30px; margin-top:-30px; width: 1350px; background-color: #0f4069;">
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


    
</body>
</html>

