using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public  class GuestRequest
    {
        public static int GuestRequestKey;
        private int guestRequestKey;
        public int guestrequestKey
        {
            get { return guestRequestKey; }
            set { guestRequestKey = guestrequestKey; }
        }
        private string PrivateName;
        public string privateName
        {
            get { return PrivateName; }
            set
            {
                PrivateName = privateName;
            }
        }

        private string FamilyName;
        public string familyName
        {
            get { return FamilyName; }
            set { FamilyName = familyName; }
        }
        private string MailAddress;
        public string mailAddress
        {
            get { return MailAddress; }
            set { MailAddress = mailAddress; }
        }
        public Status Status { get; set; }

        private DateTime RegistrationDate;
        public DateTime registrationDate
        {
            get { return RegistrationDate; }
            set { DateTime a = new DateTime(1 / 1 / 2019); }
        }
        private DateTime EnteryDate;
        public DateTime enteryDate
        {
            get { return EnteryDate; }
            set { DateTime a = new DateTime(1 / 1 / 2019); }
        }
        private DateTime ReleaseDate;
        public DateTime releaseDate
        {
            get { return ReleaseDate; }
            set { DateTime a = new DateTime(2 / 1 / 2019); }
        }
        public Area Area { get; set; }
        public Type Type { get; set; }
        private int Adults;
        public int adults
        {
            get { return Adults; }
            set { Adults = adults; }
        }
        private int Children;
        public int children
        {
            get { return Children; }
            set { Children = children; }
        }
        public Pool Pool { get; set; }
        public Jacuzzi Jacuzzi { get; set; }
        public Garden Garden { get; set; }
        public ChildrenAttractions ChildrenAtracttions { get; set; }
        public Television Television { get; set; }
        public Wifi Wifi { get; set; }
        public SmokingArea SmokingArea { get; set; }
        public SoccerField SoccerField { get; set; }
        public RoomService RoomService { get; set; }
        public BBQArea BBQArea { get; set; }
        public Phone Phone { get; set; }
        public Trampoline Trampoline { get; set; }
        public Meals Meals { get; set; }
        public CustomerRequestStatus CustomerRequestStatus { get; set; }
        public CollectionClearance CollectionClearance { get; set; }
        public override string ToString()
        {
            string GuestRequestDetails =
                "GuestRequestKey" + GuestRequestKey + "/n" +
                "PrivateName" + PrivateName + "/n" +
                "FamilyName" + FamilyName + "/n" +
                "MailAddress" + MailAddress + "/n" +
                "Status" + Status + "/n" +
                "RegistrationDate" + RegistrationDate + "/n" +
                "EnteryDate" + EnteryDate + "/n" +
                "ReleaseDate" + ReleaseDate + "/n" +
                "Area" + Area + "/n" +
                "Type" + Type + "/n" +
                "Adults" + Adults + "/n" +
                "Children" + Children + "/n" +
                "Pool" + Pool + "/n" +
                "Jacuzzi" + Jacuzzi + "/n" +
                "Garden" + Garden + "/n" +
                "ChildrenAttractions" + ChildrenAtracttions + "/n" +
                "Television" + Television + "/n" +
                "Wifi" + Wifi + "/n" +
                "SmokingArea" + SmokingArea + "/n" +
                "SoccerField" + SoccerField + "/n" +
                "RoomService" + RoomService + "/n" +
                "BBQArea" + BBQArea + "/n" +
                "Phone" + Phone + "/n" +
                "Trampoline" + Trampoline + "/n" +
                "Meals" + Meals + "/n" +
                "CustomerRequestStatus" + CustomerRequestStatus + "/n" +
                "CollectionClearance" + CollectionClearance + "/n";
            return base.ToString() + GuestRequestDetails;

        }
    }
}
