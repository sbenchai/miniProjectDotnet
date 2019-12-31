using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BankBranch
    {
        private int BankNumber;
        public int bankNumber
        {
            get { return BankNumber; }
            set
            { BankNumber = bankNumber; }
        }
        private string BankName;
        public string bankName
        {
            get { return BankName; }
            set
            { BankName = bankName; }
        }
        private int BranchNumber;
        public int branchNumber
        {
            get { return BranchNumber; }
            set
            {
                BranchNumber = branchNumber;
            }
        }
        private string BranchAddress;
        public string branchAddress
        {
            get { return BranchAddress; }
            set { BranchAddress = branchAddress; }
        }
        public BranchCity BranchCity { get; set; }
        public override string ToString()
        {
            string BankAccountDetails =
                "BankNumber" + BankNumber + "/n" +
                "BankName" + BankName + "/n" +
                "BranchNumber" + BranchNumber + "/n" +
                "BranchAddress" + BranchAddress + "/n" +
                "BranchCity" + BranchCity + "/n";
               
            return base.ToString()+BankAccountDetails;
            
        }
    }
}
