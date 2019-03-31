using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace eApprovalBO
{
    public class eApprovalBO
    {
        public string LoginUserID { get; set; }
        public string UserID { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string NewPassword { get; set; }

        public string FullName { get; set; }

        public string EmailID { get; set; }

        public string MobileNo { get; set; }

        public int DeptID { get; set; }

        public int RoleID { get; set; }

        public byte[] SignatureContent { get; set; }

        public string SR_No { get; set; }

        public int LocationID { get; set; }

        public DateTime  Date { get; set; }

        public string  SanctionRequestFor { get; set; }

        public int  AccountHeadID { get; set; }

        public decimal Amount_AED { get; set; }

        public string AmountInWords { get; set; }

        public string VendorRecomended { get; set; }

        public string JustificationDetails { get; set; }



        public DataTable FileData { get; set; }
       
         
            public string FileName { get; set; }

            public Int64 FileSize { get; set; }

            public string Extn { get; set; }

            public byte[] FileContent { get; set; }

            public int FileCount { get; set; }

            public string FinaceApprovalComments { get; set; }

            public string CEOApprovalChkFlag { get; set; }

            public string CEOApprovalComments { get; set; }

            public string SRTypeID { get; set; }
            public string SRType { get; set; }

            public string OldSRno { get; set; }

            public int ParamID { get; set; }

    }
}
