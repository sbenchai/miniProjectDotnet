using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class HostingUnit
    {
        private static int HostingUnitKey;
        private int hostingUnitKey;
        public int hostingunitKey
        {
            get { return hostingUnitKey; }
            set { hostingUnitKey = hostingunitKey; }
        }
        public Host Owner;
        private string HostingUnitName;
        public string hostingUnitName
        {
            get { return HostingUnitName; }
            set
            { HostingUnitName = hostingUnitName; }
        }
        public bool[,] Diary = new bool[12, 31];
        public Pool Pool { get; set; }
        public  Jacuzzi Jacuzzi { get; set; }
        public Area Area { get; set; }
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
        public override string ToString()
        {
            string HostingUnitDetails =
                "HostingUnitKey" + HostingUnitKey + "/n" +
                "HostingUnitName" + HostingUnitName + "/n" +
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
                "Meals" + Meals + "/n";
            return base.ToString() + HostingUnitDetails;
        }
    }
}
