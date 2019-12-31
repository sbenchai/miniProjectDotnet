using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace DS
{
    public class Dal_imp: Idal
    {
        static DataSource gr = new DataSource();
        #region Add Guest Request

        public void AddGuestRequest(GuestRequest newGuestRequest)
        {
           foreach(GuestRequest item in gr.getGuestRequestList() )
            {
                if (item.guestrequestKey==newGuestRequest.guestrequestKey)
                    throw new Exception("guest request with the same key already exists");
            }
            newGuestRequest.guestrequestKey = Configuration.GuestRequestKey++;
            gr.getGuestRequestList().Add(newGuestRequest);
            Console.WriteLine("guest added successfully");
        }

        public IEnumerable<GuestRequest> GetGuestRequests(Func<GuestRequest,bool>predicat=null)
        {
            if (predicat == null)
                return gr.getGuestRequestList().AsEnumerable();
            return gr.getGuestRequestList().Where(predicat);
        }

        public void UpdateGuestRequest(GuestRequest update)
        {
            int index = gr.getGuestRequestList().FindIndex(x => x.guestrequestKey == update.guestrequestKey);
            if (index > -1)
            {
                gr.getGuestRequestList()[index] = update;
                Console.WriteLine("request updated successfully");
            }
            else
                throw new KeyNotFoundException("this order doesnt exist");
                
        }

        private GuestRequest GetGuestRequest(int key)//this function gets a key number and returns the guest request
        {
            var v = from g in gr.getGuestRequestList()
                    where g.guestrequestKey == key
                    select g;
            return (GuestRequest)v;
        }

        #endregion

        #region Hosting Unit.
        public void AddNewHostingUnit(HostingUnit newHostingUnit)
        {
            foreach(HostingUnit item in gr.getHostingUnitList())
            {
                if (item.hostingunitKey == newHostingUnit.hostingunitKey)
                    throw new Exception("hosting unit with the same key already exists");
                
            }
            newHostingUnit.hostingunitKey = Configuration.hostingUnitKey++;
            gr.getHostingUnitList().Add(newHostingUnit);
            Console.WriteLine("hosting unit added successfully");
        }

        public void deleteHostingUnit(HostingUnit hostingUnit)
        {
            bool flag = false;
            foreach(HostingUnit item in gr.getHostingUnitList())
            {
                if(item.hostingunitKey==hostingUnit.hostingunitKey)
                {
                    flag = true;
                    hostingUnit = item;
                }
                if (flag == true)
                    gr.getHostingUnitList().Remove(hostingUnit);
                else
                    throw new Exception("hosting unit with the same key doesnt exist");
            }
        }

        public void UpdateHostingUnit(HostingUnit updateUnit)
        {
            int index = gr.getHostingUnitList().FindIndex(hostingUnit=>hostingUnit.hostingunitKey==updateUnit.hostingunitKey);
            if (index == -1)
                throw new Exception("hosting unit with the same key not found...");
            gr.getHostingUnitList()[index] = updateUnit;
        }

        public IEnumerable<HostingUnit> GetHostingUnits(Func<HostingUnit, bool> predicat = null)//returns all the hosting units in the list.
        {
            if (predicat == null)
                return gr.getHostingUnitList().AsEnumerable();
            return gr.getHostingUnitList().Where(predicat);
        }

        private HostingUnit GetHostingUnit(int key)//this function gets a key number and returns the hosting unit
        {
            var v = from g in gr.getHostingUnitList()
                    where g.hostingunitKey == key
                    select g;
            return (HostingUnit)v;
        }


        #endregion

        #region Order
        public void AddOrder(Order order)
        {
            var it = (from newOrder in gr.getOrdersList()
                      where newOrder.Orderkey == order.Orderkey
                      select newOrder).FirstOrDefault();
            var unit = gr.getHostingUnitList().Find(x => x.hostingunitKey== order.hostingUnitKey);
            if (it != null)
                throw new DuplicateWaitObjectException("order already exists");
            else
                if (unit == null)
                throw new DuplicateWaitObjectException("this hosting unit doesnt exist");
            else
                if (gr.getOrdersList().Find(x => x.guestRequestKey == order.guestRequestKey) == null)
                throw new DuplicateWaitObjectException("the order doesnt exist");
            else
            {
                Console.WriteLine("order added successfully");
                gr.getOrdersList().Add(order);
            }
        }

        public IEnumerable<Order> GetOrders(Func<Order, bool> predicat = null)//returns all the orders
        {
            if (predicat == null)
                return gr.getOrdersList().AsEnumerable();
            return gr.getOrdersList().Where(predicat);
        }

        public void updateOrder(Order update)
        {
            int index = gr.getOrdersList().FindIndex(order => order.Orderkey == update.Orderkey);
            if (index == -1)
                throw new Exception("order with the same key not found... ");
            gr.getOrdersList()[index] = update;
                
        }

        private Order GetOrder(int key)//this function gets a key number and returns the order
        {
            var v = from g in gr.getOrdersList()
                    where g.Orderkey == key
                    select g;
            return (Order)v;
        }

        #endregion

        #region Bank Branch

        public void AddBankBranch(BankBranch newBankBranch)
        {
            foreach (BankBranch item in gr.getBankBranchList())
            {
                if (item.bankNumber == newBankBranch.bankNumber)
                    throw new Exception("Bank Branch with the same number already exists");

            }
            gr.getBankBranchList().Add(newBankBranch);
            Console.WriteLine("Bank Branch added successfully");
        }


        public IEnumerable<BankBranch> GetBankBranches(Func<BankBranch, bool>predicat=null)//returns all the bank branches.
        {
            if (predicat == null)
                return gr.getBankBranchList().AsEnumerable();
            return gr.getBankBranchList().Where(predicat);
        }
        #endregion

    }
}
