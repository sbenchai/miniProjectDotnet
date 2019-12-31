using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DS;

namespace BL
{
    public class IBLDefinitions
    {
        protected Idal dAccess;

        #region AddBankBranch
        /// <summary>
        /// adds a bank branch
        /// </summary>
        /// <param name="bank"></param>
        public void AddBankBranch(BankBranch bankBranch)
        {
            if (dAccess.GetBankBranches().Any(y => y.bankNumber == bankBranch.bankNumber))
                throw new Exception("Bank Branch already exists");
            try
            {
                dAccess.AddBankBranch(bankBranch);
            }
            catch (InvalidOperationException c)
            {
                throw c;
            }
        }
        #endregion

        #region Add Guest Request
        /// <summary>
        /// adds a new guest Guestrequest if it doesnt exist already and checks if the date is in the right range
        /// </summary>
        /// <param name="newGuestRequest"></param>

        public void AddGuestRequest(GuestRequest newGuestRequest)
        {
            if (dAccess.GetGuestRequests().Any(x => x.guestrequestKey == newGuestRequest.guestrequestKey))
                throw new InvalidOperationException("Guest Request already exists");
            if (newGuestRequest.enteryDate >= newGuestRequest.releaseDate || newGuestRequest.registrationDate > newGuestRequest.enteryDate)
                throw new InvalidOperationException("Invalid date range");
            try
            {
                dAccess.AddGuestRequest(newGuestRequest);
            }
            catch (DuplicateWaitObjectException c)
            {
                throw c;
            }
        }
        #endregion


        #region Update Guest Request
        /// <summary>
        /// updates a guest Guestrequest.
        /// </summary>
        /// <param name="update"></param>
        public void UpdateGuestRequest(GuestRequest update)
        {
            var gr1 = (from newGuest in dAccess.GetGuestRequests()
                       where update.guestrequestKey == newGuest.guestrequestKey
                       select newGuest).FirstOrDefault();
            var gr2 = (from newGuest in dAccess.GetOrders()
                       where update.guestrequestKey == newGuest.guestRequestKey
                       select newGuest).FirstOrDefault();
            var gr3 = (from newGuest in dAccess.GetHostingUnits()
                       where gr2.hostingUnitKey == newGuest.hostingunitKey
                       select newGuest).FirstOrDefault();
            if (gr1 == null)
                throw new KeyNotFoundException("the Guest doesnt exist");
            var guest = (from newGuest in dAccess.GetGuestRequests()
                         where newGuest.guestrequestKey == update.guestrequestKey
                         select newGuest).FirstOrDefault();
            TimeSpan difference = guest.registrationDate - DateTime.Now;
            if (difference.Days == 14)//if the order date is from more than 2 weeks then the order is expired.
                guest.Status = Status.expired;
            int stayingDuration = (gr1.releaseDate - gr1.enteryDate).Days;
            if (stayingDuration <= 0)//the vacation duration has to at least one day.
                throw new InvalidOperationException("the duration has to be at least one day");
            if (update.Status == Status.closed_by_website || update.Status == Status.expired)//if the order was expired or closed then all the days who were taken needs to be released. 
            {
                int temp = 0;
                for (int i = gr1.enteryDate.Month - 1; i < 12; i++)
                    for (int j = 0; j < 31; j++)
                    {
                        if (temp < stayingDuration)
                        {
                            if ((j >= (gr1.enteryDate.Day - 1)) || (temp > 0 && temp < stayingDuration))
                            {
                                if (temp < stayingDuration)
                                {
                                    if (gr3.Diary[i, j] == true)
                                    {
                                        gr3.Diary[i, j] = false;
                                    }
                                }
                            }
                        }
                    }

            }
            try
            {
                dAccess.UpdateGuestRequest(update);
            }
            catch (KeyNotFoundException c)
            {
                throw c;
            }

        }
        #endregion


        #region Add Hosting Unit.
        /// <summary>
        /// adds a hosting hostingunit.
        /// </summary>
        /// <param name="newHostingUnit"></param>
        public void AddNewHostingUnit(HostingUnit newHostingUnit)
        {
            try
            {
                foreach (HostingUnit item in dAccess.GetHostingUnits())
                {
                    if (item.hostingunitKey == newHostingUnit.hostingunitKey)
                        throw new Exception("hosting hostingunit with the same key already exists");
                    dAccess.AddNewHostingUnit(newHostingUnit);
                }
            }
            catch (InvalidOperationException c)
            {
                throw c;
            }

        }
        #endregion

        #region Delete hosting unit
        /// <summary>
        /// deletes a hosting hostingunit and checks if there are active orders if there is the hostingunit cant be deleted if there isnt it deletes the hosting hostingunit.
        /// </summary>
        /// <param name="hostingUnit"></param>
        public void deleteHostingUnit(HostingUnit hostingUnit)
        {
            var hu = from newHostingUnit in dAccess.GetHostingUnits()
                     let x = newHostingUnit.hostingunitKey
                     where x == hostingUnit.hostingunitKey
                     select new { HostingUnitKey = x };
            if (hu != null)
                throw new Exception("Hosting Unit" + hostingUnit.hostingunitKey + "Doesnt exist");
            if (dAccess.GetOrders().Any(a => a.hostingUnitKey == hostingUnit.hostingunitKey && (a.Status == Status.email_sent || a.Status == Status.not_dealt_yet || a.Status == Status.accepted)))
                throw new Exception("Cant delete an hosting hostingunit with active orders");
            try
            {
                dAccess.deleteHostingUnit(hostingUnit);
            }
            catch (KeyNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Update Hosting Unit
        /// <summary>
        /// updates a hostingunit. checks if it exits.
        /// </summary>
        /// <param name="updateUnit"></param>
        public void UpdateHostingUnit(HostingUnit updateUnit)
        {
            var order = (from unit in dAccess.GetHostingUnits()
                         where unit.hostingunitKey == updateUnit.hostingunitKey
                         select unit).FirstOrDefault();
            if (order == null)
                throw new InvalidOperationException("hostingunit" + updateUnit.hostingunitKey + "doesnt exit");
            try
            {
                dAccess.UpdateHostingUnit(updateUnit);
            }
            catch (KeyNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region get Hosting Unit list
        /// <summary>
        /// returns a list off hosting units.
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<HostingUnit> GetHostingUnitList()
        {
            try
            {
                List<HostingUnit> HUList = new List<HostingUnit>();
                foreach (var hunit in dAccess.GetHostingUnits())
                {
                    var list = from unitlist in dAccess.GetHostingUnits()
                               orderby unitlist.hostingunitKey
                               select unitlist;
                    HUList = list.ToList();
                }
                return HUList;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region get Guest Request list
        /// <summary>
        /// returns a list of all guest requests
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<GuestRequest> GetGuestRequestList()
        {
            try
            {
                List<GuestRequest> GRList = new List<GuestRequest>();
                foreach (var grequest in dAccess.GetGuestRequests())
                {
                    var list = from guestrequestlist in dAccess.GetGuestRequests()
                               orderby guestrequestlist.guestrequestKey
                               select guestrequestlist;
                    GRList = list.ToList();
                }
                return GRList;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region get Order list
        /// <summary>
        /// returns a list of all orders
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<Order> GetOrdersList()
        {
            try
            {
                List<Order> OrderList = new List<Order>();
                foreach (var order in dAccess.GetGuestRequests())
                {
                    var list = from orderlist in dAccess.GetOrders()
                               orderby orderlist.Orderkey
                               select orderlist;
                    OrderList = list.ToList();
                }
                return OrderList;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region get Branches
        /// <summary>
        /// returns a list of all branch branches
        /// </summary>
        /// <returns></returns>
        public List<BankBranch> GetBranches()
        {
            try
            {
                List<BankBranch> banklist = new List<BankBranch>();
                foreach (var bankbranch in dAccess.GetBankBranches())
                {
                    var list = from bank in dAccess.GetBankBranches()
                               orderby bank.bankNumber
                               select bank;
                    banklist = list.ToList();
                }
                return banklist;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Number of guest orders
        /// <summary>
        /// the function gets a client and returns all the orders that has been sent to him
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public int NumberOfOrdersSentToGuest(GuestRequest guest)
        {
            try
            {
                return dAccess.GetOrders().Count(x => x.guestRequestKey == guest.guestrequestKey);
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Number of Units Orders
        /// <summary>
        /// the function gets a hostingunit and returns the number of sent orders for this hostingunit
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public int NumberOfOrdersForUnit(HostingUnit unit)
        {
            try
            {
                return dAccess.GetOrders().Count(x => x.hostingUnitKey == unit.hostingunitKey);
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Get Grouped By Area
        /// <summary>
        /// groups a list of guest Guestrequest by an area
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<Area, GuestRequest>> GetGroupedByArea()
        {
            try
            {
                var group = from guestrequest in dAccess.GetGuestRequests()
                            orderby guestrequest.guestrequestKey
                            group guestrequest by guestrequest.Area
                          into g
                            orderby g.Key
                            select g;
                return group;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Get Grouped By Num Of Vacation
        /// <summary>
        /// groups a list of guest Guestrequest by the number of ordered vacation
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, GuestRequest>> GetGroupedByNumOfVacation()
        {
            try
            {
                var group = from guestrequest in dAccess.GetGuestRequests()
                            orderby guestrequest.guestrequestKey
                            group guestrequest by NumberOfOrdersSentToGuest(guestrequest)
                            into g
                            orderby g.Key
                            select g;
                return group;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Get Grouped By Number Of Units
        /// <summary>
        /// groups a list of hosting units by the number of their owners
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, HostingUnit>> GetGroupedByNumberOfUnits()
        {
            try
            {
                var group = from host in dAccess.GetHostingUnits()
                            orderby host.Owner.hostKey
                            group host by host.Owner.hostKey
                            into g
                            orderby g.Key
                            select g;
                return group;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Get Grouped By Unit Area
        /// <summary>
        /// groups a list of hosting units by their area
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<Area, HostingUnit>> GetGroupedByUnitsArea()
        {
            try
            {
                var group = from unit in dAccess.GetHostingUnits()
                            orderby unit.hostingunitKey
                            group unit by unit.Area
                            into g
                            orderby g.Key
                            select g;
                return group;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Get Units in Eilat with Jaccuzzi and Pool
        /// <summary>
        /// returns a list of units in eilat with a pool and a jaccuzzi
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<HostingUnit> GetUnitsInEilatWithPoolAndJaccuzzi()
        {
            try
            {
                List<HostingUnit> list = new List<HostingUnit>();
                foreach (var hostingunit in dAccess.GetHostingUnits())
                {
                    if (hostingunit.Pool == Pool.yes && hostingunit.Jacuzzi == Jacuzzi.yes && hostingunit.Area == Area.eilat)
                        list.Add(hostingunit);

                }


                if (list == null)
                    throw new InvalidOperationException("in eilat there are no units with a pool and a jaccuzzi");
                return list;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion

        # region Get Grouped By Bank Name
        /// <summary>
        /// groups a list of banks by their names
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<string, BankBranch>> GetGroupedByBankName()
        {
            try
            {
                return from branch in dAccess.GetBankBranches()
                       orderby branch.bankNumber
                       group branch by branch.bankName
                            into g
                       orderby g.Key
                       select g;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
        }
        #endregion

        #region Get Guests that want a BBQ and to smoke
        /// <summary>
        /// gets a list of guests who wants a BBQ place and to smoke.
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<GuestRequest> GetGuestsWithBBQAndWantToSmoke()
        {
            try
            {
                List<GuestRequest> list = new List<GuestRequest>();
                foreach (var guestrequest in dAccess.GetGuestRequests())
                {
                    if (guestrequest.BBQArea == BBQArea.yes && guestrequest.SmokingArea == SmokingArea.yes)
                        list.Add(guestrequest);
                }
                if (list == null)
                    throw new InvalidOperationException("there are no guests who want a smoking and a BBQ area");
                return list;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion

        #region Get Grouped By  Tv , Wifi and Phone
        /// <summary>
        /// groups hosting units by if they have tv and wifi or a phone
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<bool, HostingUnit>> GetGroupedByTelevisionPhoneAndWifi()
        {
            try
            {
                return from hostingunit in dAccess.GetHostingUnits()
                       orderby hostingunit.hostingunitKey
                       group hostingunit by hostingunit.Wifi == Wifi.yes && hostingunit.Television == Television.yes && hostingunit.Phone == Phone.yes
                            into g
                       orderby g.Key
                       select g;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
        }
        #endregion


        #region Get Grouped By Room Service and Meals
        /// <summary>
        /// groups hosting units by if they have room service and meals
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<bool, HostingUnit>> GetGroupedByRoomServiceAndMeal()
        {
            try
            {
                return from hostingunit in dAccess.GetHostingUnits()
                       orderby hostingunit.hostingunitKey
                       group hostingunit by hostingunit.RoomService == RoomService.yes && hostingunit.Meals == Meals.yes 
                            into g
                       orderby g.Key
                       select g;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
        }
        #endregion


        #region Get Grouped By Soccer Field, Trampoline And Children Attractions
        /// <summary>
        /// groups hosting units by if they have a soccer field a trampoline and children attractions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IGrouping<bool, HostingUnit>> GetGroupedBySoccerFieldTrampolineAndChildrenAttractions()
        {
            try
            {
                return from hostingunit in dAccess.GetHostingUnits()
                       orderby hostingunit.hostingunitKey
                       group hostingunit by hostingunit.SoccerField == SoccerField.yes && hostingunit.Trampoline == Trampoline.yes && hostingunit.ChildrenAtracttions == ChildrenAttractions.yes
                            into g
                       orderby g.Key
                       select g;
            }
            catch (DirectoryNotFoundException e)
            {
                throw e;
            }
        }
        #endregion


        #region Get Banks In Rehovot
        /// <summary>
        /// returns list of banks in Rehovot
        /// </summary>
        /// <returns></returns>
        public List<BankBranch> GetBankInRehovot()
        {
            List<BankBranch> list = new List<BankBranch>();
            foreach (var bankbranch in dAccess.GetBankBranches())
            {
                if (bankbranch.BranchCity == BranchCity.rehovot)
                    list.Add(bankbranch);
            }
            if (list == null)
                throw new InvalidOperationException("there are no banks in Rehovot");
            return list;
        }
        #endregion

        #region Get Hosts Partial List By Predicate
        /// <summary>
        /// gets a partial list of hosts by using a predicate
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<HostingUnit> GetHostsPartialListByPredicate(Func<Host, bool> predicat)
        {
            try
            {
                var partialUnitList = (from item in dAccess.GetHostingUnits()
                                       where predicat(item.Owner)
                                       orderby item.Owner.familyName, item.Owner.privateName
                                       select item).ToList();
                return partialUnitList;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Add Order
        /// <summary>
        /// adds an order and checks if the dates are available and not taken.,
        /// </summary>
        /// <param name="order"></param>
        public void AddOrder(int HostingUnitKey, int guestRequestKey)
        {
            Order o = new Order();
            var it = (from Order in dAccess.GetHostingUnits()
                      let a = Order
                      where a.hostingunitKey == HostingUnitKey
                      select Order).FirstOrDefault();

            if (it == null)
                throw new DuplicateWaitObjectException("hosting hostingunit doesnt exist");
            else
            {
                var guest = (from newGuest in dAccess.GetGuestRequests()
                             where newGuest.guestrequestKey == guestRequestKey
                             select newGuest).FirstOrDefault();
                int stayingDuration = (guest.releaseDate - guest.enteryDate).Days;
                int temp = 0;
                for (int i = guest.enteryDate.Month - 1; i < 12; i++)
                    for (int j = 0; j < 31; j++)
                    {
                        if (temp < stayingDuration)
                        {
                            if ((j >= (guest.enteryDate.Day - 1)) || (temp > 0 && temp < stayingDuration))
                            {
                                if (temp < stayingDuration)
                                {
                                    if (it.Diary[i, j] == true)
                                        throw new DuplicateWaitObjectException("Dates are already taken");
                                }
                            }
                        }
                        if (temp == stayingDuration)
                            break;
                    }
                try
                {
                    o.hostingUnitKey = HostingUnitKey;
                    o.guestRequestKey = guestRequestKey;
                    o.Status = Status.not_dealt_yet;
                    dAccess.AddOrder(o);
                }
                catch (DuplicateWaitObjectException c)
                {
                    throw c;
                }
            }
        }

        #endregion

        #region Update order
        /// <summary>
        /// updates an order checks if the date are not taken and  also checks if the order can be updated which means not cancelled by anyone.
        /// </summary>
        /// <param name="update"></param>
        /// <param name="status"></param>
        public void updateOrder(Order update, Status status)
        {
            var guest = (from newGuest in dAccess.GetGuestRequests()
                         where newGuest.guestrequestKey == update.guestRequestKey
                         select newGuest).FirstOrDefault();
            var order = (from newOrder in dAccess.GetOrders()
                         where newOrder.Status != update.Status && newOrder.Orderkey == update.Orderkey
                         select newOrder).FirstOrDefault();
            var unit = (from newOrder in dAccess.GetHostingUnits()
                        where newOrder.hostingunitKey == update.hostingUnitKey
                        select newOrder).FirstOrDefault();
            if (order.Status == Status.closes_out_of_customer_unresponsiveness)
                throw new Exception("the order was canceled ans cannot be updated");
            if (order != null)
                throw new Exception("no changes to make");
            int stayingDuration = (guest.releaseDate - guest.enteryDate).Days;
            if (status == Status.accepted)
            {
                int temp = 0;
                for (int i = guest.enteryDate.Month - 1; i < 12; i++)
                    for (int j = 0; j < 31; j++)
                    {
                        if (temp < stayingDuration)
                        {
                            if ((j >= (guest.enteryDate.Day - 1)) || (temp > 0 && temp < stayingDuration))
                            {
                                if (temp < stayingDuration)
                                {
                                    if (unit.Diary[i, j] == true)

                                    {
                                        throw new DuplicateWaitObjectException("Dates are  taken already");
                                    }
                                }
                            }


                        }

                    }

                temp = 0;
                for (int i = guest.enteryDate.Month - 1; i < 12; i++)
                    for (int j = 0; j < 31; j++)
                    {
                        if (temp < stayingDuration)
                        {
                            if ((j >= (guest.enteryDate.Day - 1)) || (temp > 0 && temp < stayingDuration))
                            {
                                if (temp < stayingDuration)
                                {
                                    unit.Diary[i, j] = true;

                                }
                            }


                        }

                    }
            }
            if (status == Status.email_sent)
            {
                if (unit.Owner.CollectionClearance != CollectionClearance.yes)
                    throw new Exception("no charge because of cancellation");
                Console.WriteLine("Email sent");
            }
            if (status == Status.closes_out_of_customer_unresponsiveness)
            {
                Console.WriteLine("the guest canceled");
                if (unit.Owner.CollectionClearance == CollectionClearance.yes)
                    unit.Owner.Payment = stayingDuration + Configuration.comision;
                int temp = 0;
                for (int i = guest.enteryDate.Month - 1; i < 12; i++)
                    for (int j = 0; j < 31; j++)
                    {
                        if (temp < stayingDuration)
                        {
                            if ((j >= (guest.enteryDate.Day - 1)) || (temp > 0 && temp < stayingDuration))
                            {
                                if (temp < stayingDuration)
                                {
                                    unit.Diary[i, j] = false;

                                }
                            }


                        }

                    }
            }
            dAccess.updateOrder(update);

        }

        #endregion

        #region  Number Of Successful Orders
        /// <summary>
        /// returns the number of successful orders each hosting unit has
        /// </summary>
        /// <param name="hostingunit"></param>
        /// <returns></returns>
        public int NumberOfSuccessfullOrders(HostingUnit hostingunit)
        {
            try
            {
                return dAccess.GetOrders().Count(x => x.hostingUnitKey == hostingunit.hostingunitKey && x.Status == Status.accepted);
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Get Days
        ///<summary>
        ///the function gets two dates and returns the number of days between those two dates
        /// </summary>
        /// <param name="dates"></param>
        /// <returns></returns>
        public int GetDays(params DateTime[] dates)
        {
            try
            {
                if (dates.Length == 1)//if we got only one date
                {
                    if (dates[0] >= DateTime.Now)//if the date we got is bigger than today the function returns the subtraction between today and the date
                        return (dates[0] - DateTime.Now).Days;
                    else
                        return (DateTime.Now - dates[0]).Days;//else if today is bigger than we subtract the date from today.
                }
                else
                if (dates.Length == 2)//if we got two dates
                {
                    if (dates[1] >= dates[0])//if the first date is before the second one
                        return (dates[1] - dates[0]).Days;//we subtract the first date from the second
                    else
                        return (dates[0] - dates[1]).Days;//if the second date is before the first one we subtract the second date from the first
                }
                else
                {
                    return 0;
                }
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion

        #region OrdersBiggerOrEqualToDays
        /// <summary>
        /// the function gets a number of days and returns all the orders that since they have been created or since an email was sent to the client, is equal or bigger than the number of days we got
        /// </summary>
        /// <param name="days"></param>
        /// <returns></returns>
        public List<Order> OrdersBiggerOrEqualToDays(int days)
        {
            try
            {
                List<Order> orderlist = new List<Order>();
                foreach (var order in dAccess.GetOrders())
                {
                    if (order.Status == Status.email_sent || order.Status == Status.not_dealt_yet)//if the order is not complete yet
                    {
                        if (DateTime.Now.Day - order.createDate.Day >= days)//we check if the creation date-today is bigger or equal to the number of days we got.
                        {
                            orderlist.Add(order);//if it is bigger or equal we add it to the list
                        }
                    }

                }
                return orderlist;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion

        #region Get Guests Partial List By Predicate
        /// <summary>
        /// returns partial guest list by using a predicate all guests who fits for some condition
        /// </summary>
        /// <param name="predicat"></param>
        /// <returns></returns>
        public List<GuestRequest> GetGuestsPartialListByPredicate(Func<GuestRequest, bool> predicat)
        {
            try
            {
                var GuestList = (from item in dAccess.GetGuestRequests() where predicat(item) orderby item.familyName, item.privateName select item).ToList();
                return GuestList;
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


        #region Get Available Units
        /// <summary>
        /// returns list of available units for a specific amount of days
        /// </summary>
        /// <param name="date"></param>
        /// <param name="VacationDays"></param>
        /// <returns></returns>
        public List<HostingUnit> GetAvailableUnits(DateTime date, int Days)
        {
            try
            {
                List<HostingUnit> hostingunitlist = new List<HostingUnit>();
                int temp = 0;
                bool flag = true;
                foreach (var hostingunit in dAccess.GetHostingUnits())
                {
                    temp = 0;
                    flag = true;
                    for (int i = date.Month - 1; i < 12; i++)// we check if the wanted days are taken
                        for (int j = 0; j < 31; j++)
                        {
                            if (temp < Days)
                            {
                                if ((j >= (date.Day - 1)) || (temp > 0 && temp < Days))
                                {
                                    if (temp < Days)
                                    {
                                        if (hostingunit.Diary[i, j] == true)//the days are taken

                                        {
                                            flag = false;//we cant order taken days
                                        }
                                    }
                                }


                            }
                            if (flag == true)//the days are not taken which means they are available
                                hostingunitlist.Add(hostingunit);
                        }
                }
                return hostingunitlist;//a list with available/taken days
            }
            catch (DirectoryNotFoundException c)
            {
                throw c;
            }
        }
        #endregion


    }
}

