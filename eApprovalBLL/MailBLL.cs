using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace eApprovalBLL
{
   public  class MailBLL 
   {

       #region ClassDeclaration
       eApprovalDB.MailDB MailDB = new eApprovalDB.MailDB();

       #endregion

       #region Variable Declarations

       public static string SenderMailId = string.Empty;

       public static string SenderSmtp = string.Empty;

       public static string SenderPassword = string.Empty;

       public static int Port = 0;

       public static string SenderUName = string.Empty;

       public static string SRNo = string.Empty;

       #endregion

       #region Method Expose to ExternalClass
       public  string  SendSRMail(int UserID, int RoleID, string SRNO)
       {

           string MailSucess = string.Empty;

           try
           {

               DataTable dtSrData = MailDB.GetSRDataForMail(SRNO);

               string Html = dtTohtml(dtSrData);

               DataSet dsMailDetails = MailDB.GetMailDetails(UserID, RoleID, SRNO);

               if (dsMailDetails.Tables[0].Rows.Count > 0)
               {
                   SenderMailId = dsMailDetails.Tables[0].Rows[0]["SENDER_MAILID"].ToString();

                   SenderPassword = dsMailDetails.Tables[0].Rows[0]["PASSWORD"].ToString();

                   SenderSmtp = dsMailDetails.Tables[0].Rows[0]["SMTPCLIENT"].ToString();

                   Port = Convert.ToInt32(dsMailDetails.Tables[0].Rows[0]["PORT"].ToString());

                   SenderUName = dsMailDetails.Tables[0].Rows[0]["SENDER_USERNAME"].ToString();

                   SRNo = SRNO;

               }

               //Main Receiver Mail ID

               if (dsMailDetails.Tables[1].Rows.Count > 0)
               {

                   Parallel.ForEach(dsMailDetails.Tables[1].AsEnumerable(), row =>
                   {
                       SendMail(row["RECEIVER_MAILID"].ToString(), Html,false);
                   });

                   //for(int i=0;i<dsMailDetails.Tables[1].Rows.Count;i++)

                   //{
                   //    SendMail(dsMailDetails.Tables[1].Rows[i]["RECEIVER_USERID"].ToString(),Html);
                   //}

               }


               //Initiator ReceiverMail ID

               if (dsMailDetails.Tables.Count == 3)
               {
                   if (dsMailDetails.Tables[2].Rows.Count > 0)
                   {

                       Parallel.ForEach(dsMailDetails.Tables[2].AsEnumerable(), row =>
                       {
                           SendMail(row["INITIATOR_MAILID"].ToString(), Html,true);
                       });
                   }
               }

               MailSucess = "S";
           }
           catch (Exception ex)
           {
               MailSucess = "F";
           }

           return MailSucess;

       }

       #endregion

       #region MainMethodTosendMail
       public void SendMail(string ReceiverMailid,string html,bool IsReceiverIsInitiator)
       {
           string ClickHereTag = string.Empty;


           if (IsReceiverIsInitiator == true)
           {
               ClickHereTag = string.Empty;
           }

           else
           {
               ClickHereTag = "<a href=\"http://10.16.0.22:3030/Portal/Index.aspx\">Click here</a>" + " to approve the SR. <br><br>";
             
           }



           MailMessage mailMeassage = new MailMessage(SenderMailId, ReceiverMailid);

           string Message = "Dear Sir,<br><br>"

               + "The Below SR is initiated,Kindly check and give the Approval from your End. <br><br>"

               + html + "<br><br>"

               + ClickHereTag

               + "Note: This is Auto Generated Email.If any Queries Pl contact Undersigned. <br><br>"

               + "Regards,<br>"

               + SenderUName + " <br>"

               + "www.khazanajewellery.com";



           mailMeassage.Subject = "Sanction Request - "+ SRNo ;
           mailMeassage.Body = Message;

           SmtpClient smtpclient = new SmtpClient(SenderSmtp, Port);

           smtpclient.Credentials = new System.Net.NetworkCredential()
           {
               UserName = SenderMailId,
               Password = SenderPassword
           };

           smtpclient.EnableSsl = false;
           mailMeassage.IsBodyHtml = true;
           smtpclient.Send(mailMeassage);
       }

       #endregion

       #region ConvertingDatatable to Html
       private string dtTohtml(DataTable dt)
       {

           string htmlHeaderRowstart = "<tr style=\"background-color:#6FA1D2; color:#ffffff; \">";
           string ColumnStart = "<td style=\"border:solid 1px black;text-align:center; padding:10px; \">";
           string HeaderColumnStart = "<td style=\"border:solid 1px black;text-align:center; padding:10px; \">";
           string ColumnEnd = "</td>";
           string Rowstart = "<tr>";
           string RowEnd = "</tr>";

           string html = "<table style=\"border-collapse : collapse; text-align:left;\">";

           string htmlEnd = "</table>";

           //Create Hardcoded Row Header

           html += htmlHeaderRowstart +

               HeaderColumnStart + "SR No" + ColumnEnd +

               HeaderColumnStart + "Location" + ColumnEnd +

               HeaderColumnStart + "SR Date" + ColumnEnd +

               HeaderColumnStart + "Department" + ColumnEnd +

               HeaderColumnStart + "Initiator" + ColumnEnd +

               HeaderColumnStart + "SR Description" + ColumnEnd +

               HeaderColumnStart + "Amount" + ColumnEnd +

               HeaderColumnStart + "Process Level" + ColumnEnd +

               HeaderColumnStart + "Status" + ColumnEnd +

               RowEnd;



           //For Each Row of datatable

           for (int i = 0; i < dt.Rows.Count; i++)
           {
               //Create a Row of Data

               html += Rowstart;

               for (int j = 0; j < dt.Columns.Count; j++)
               {
                   html += ColumnStart + dt.Rows[i][j].ToString() + ColumnEnd;
               }

               html += RowEnd;
           }

           html += htmlEnd;

           return html;

       }

       #endregion
   }
}
