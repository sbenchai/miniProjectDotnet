using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
  public class Order
    {
        private int HostingUnitKey;
        public int hostingUnitKey
        { get { return HostingUnitKey;}
            set { HostingUnitKey = hostingUnitKey; }
        }
        private int GuestRequestKey;
        public int guestRequestKey
        {
            get { return GuestRequestKey; }
            set { GuestRequestKey = guestRequestKey; }
        }
        private int OrderKey;
        public int Orderkey
        {
            get { return OrderKey; }
            set { OrderKey = Orderkey; }
        }
        public Status Status { get; set; }
        private DateTime CreateDate;
        public DateTime createDate
        {
            get { return CreateDate; }
            set { DateTime a = new DateTime(01 / 30 / 2019); }
        }
        private DateTime OrderDate;
        public DateTime orderDate
        {
            get { return OrderDate; }
            set { DateTime a = new DateTime(08 / 1 / 2019); }
        }
        public override string ToString()
        {
            string OrderDetails =
                "HostingUnitKey" + HostingUnitKey + "/n" +
                "GuestRequestKey" + GuestRequestKey + "/n" +
                "OrderKey" + OrderKey + "/n" +
                "Status" + Status + "/n" +
                "CreateDate" + CreateDate + "/n" +
                "OrderDate" + OrderDate + "/n";
            return base.ToString() + OrderDetails;
        }
    }
}
