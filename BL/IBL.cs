using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace BL
{
    interface IBL
    {
        //adds a new guest request.
        void AddGuestRequest(BE.GuestRequest newGuestRequest);
        //updates a guest request changes the status.
        void UpdateGuestRequest(GuestRequest update);
        //adds a new hosting unit.
        void AddNewHostingUnit(HostingUnit newHostingUnit);
        //deletes an existing hosting unit.
        void deleteHostingUnit(HostingUnit hostingUnit);
        //updates an hosting unit.
        void UpdateHostingUnit(HostingUnit updateUnit);
        //adds a new order.
        void AddOrder(Order NewOrder);
        //updates an order.
        void updateOrder(Order update);
        //gets a list of all guest request, returns a list with all the guest requests.
        IEnumerable<GuestRequest> GetGuestRequests(Func<GuestRequest, bool> predicat = null);
        //gets a list of all the hosting unit, returns a list with all the hosting units.
        IEnumerable<HostingUnit> GetHostingUnits(Func<HostingUnit, bool> predicat = null);
        //gets a list of all orders, returns a list with all the orders.
        IEnumerable<Order> GetOrders(Func<Order, bool> predicat = null);
        //gets a list of all bank accounts, returns a list with all the bank accounts.
        IEnumerable<BankBranch> GetBankBranches(Func<BankBranch, bool> predicat = null);
        
        


    }
}
