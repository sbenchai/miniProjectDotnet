using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Host
    {
        private int HostKey;
        public int hostKey
        {
            get { return HostKey; }
            set { HostKey = hostKey; }
        }
        private string PrivateName;
        public string privateName
        {
            get { return PrivateName; }
            set { PrivateName = privateName; }
        }
        private string FamilyName;
        public string familyName
        {
            get { return FamilyName; }
            set { FamilyName = familyName; }
        }
        private int PhoneNumber;
        public int phoneNumber
        {
            get { return PhoneNumber; }
            set { PhoneNumber = phoneNumber; }
        }
        private string MailAddress;
        public string mailAddress
        {
            get { return MailAddress; }
            set { MailAddress = mailAddress; }
        }
        public BankBranch BankBranchDetails;
        private string BankAccountNumber;
        public string bankAccountNumber
        {
            get { return BankAccountNumber; }
            set { BankAccountNumber = bankAccountNumber; }
        }
        private int payment;
        public int Payment
        {
            get { return payment; }
            set { payment = Payment; }
        }
        public CollectionClearance CollectionClearance { get; set; }
        public override string ToString()
        {
            string HostDetails =
                "HostKey" + HostKey + "/n" +
                "PrivateName" + PrivateName + "/n" +
                "FamilyName" + FamilyName + "/n" +
                "PhoneNumber" + PhoneNumber + "/n" +
                "MailAddress" + MailAddress + "/n" +
                "Payment"+payment+"/n"+
                "BankBranchDetails" + BankBranchDetails + "/n" +
                "BankAccountNumber"+BankAccountNumber+"/n"+
                "CollectionClearance" + CollectionClearance + "/n";
            return base.ToString() + HostDetails;
        }
    }
}