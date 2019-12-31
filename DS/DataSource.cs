using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
   public class DataSource
    {
       
        private static List<GuestRequest> guestRequestsList;//a list of guest requests .
        private static List<Host> hostsList;//a list of host.
        private static List<Order> ordersList;//a list of orders .
        private static List<HostingUnit> hostingUnitsList;//a list of hosting units.
        private static List<BankBranch> bankBranchesList;//a list of bank branches.

        public List<GuestRequest> getGuestRequestList()//returns the guest requests list.
        {
            return guestRequestsList;
        }

        public List<Host> getHostList()//returns the host list.
        {
            return hostsList;
        }

        public List<Order> getOrdersList()//returns the orders list.
        {
            return ordersList;
        }

        public List<HostingUnit> getHostingUnitList()//returns the hosting unit list.
        {
            return hostingUnitsList;
        }

        public List<BankBranch> getBankBranchList()//returns the bank branches list.
        {
            return bankBranchesList;
        }

        static DataSource()
        {
            guestRequestsList = new List<GuestRequest>();
            hostsList = new List<Host>();
            ordersList = new List<Order>();
            hostingUnitsList = new List<HostingUnit>();
            bankBranchesList = new List<BankBranch>();

            BE.GuestRequest guestRequest = new BE.GuestRequest();
            guestRequest.privateName = "Sarah";
            guestRequest.familyName = "Toledano";
            guestRequest.mailAddress = "sarahtoledano947@gmail.com";
            guestRequest.Status = Status.email_sent;
            guestRequest.registrationDate = new DateTime(01 / 01 / 2019);
            guestRequest.enteryDate = new DateTime(01 / 10 / 2019);
            guestRequest.releaseDate = new DateTime(02 / 20 / 2019);
            guestRequest.Area = Area.jerusalem;
            guestRequest.Type = BE.Type.hotel;
            guestRequest.adults = 2;
            guestRequest.children = 1;
            guestRequest.Pool = Pool.yes;
            guestRequest.Jacuzzi = Jacuzzi.yes;
            guestRequest.Wifi = Wifi.yes;
            guestRequest.BBQArea = BBQArea.yes;
            guestRequest.ChildrenAtracttions = ChildrenAttractions.yes;
            guestRequest.CollectionClearance = CollectionClearance.no;
            guestRequest.CustomerRequestStatus = CustomerRequestStatus.approved;
            guestRequest.Garden = Garden.yes;
            guestRequest.Meals = Meals.yes;
            guestRequest.Phone = Phone.doesnt_matter;
            guestRequest.RoomService = RoomService.yes;
            guestRequest.SmokingArea = SmokingArea.no;
            guestRequest.SoccerField = SoccerField.doesnt_matter;
            guestRequest.Television = Television.yes;
            guestRequest.Trampoline = Trampoline.yes;

            BE.GuestRequest guestRequest1 = new BE.GuestRequest();
            guestRequest1.privateName = "Shirel";
            guestRequest1.familyName = "Ben-Chaim";
            guestRequest1.mailAddress = "benchaimshirel@gmail.com";
            guestRequest1.Status = Status.dealt_with;
            guestRequest1.registrationDate = new DateTime(01 / 30 / 2019);
            guestRequest1.enteryDate = new DateTime(08 / 23 / 2019);
            guestRequest1.releaseDate = new DateTime(09 / 10 / 2019);
            guestRequest1.Area = Area.north;
            guestRequest1.Type = BE.Type.apartement;
            guestRequest1.adults = 2;
            guestRequest1.children = 15;
            guestRequest1.Pool = Pool.yes;
            guestRequest1.Jacuzzi = Jacuzzi.yes;
            guestRequest1.Wifi = Wifi.no;
            guestRequest1.BBQArea = BBQArea.yes;
            guestRequest1.ChildrenAtracttions = ChildrenAttractions.yes;
            guestRequest1.CollectionClearance = CollectionClearance.yes;
            guestRequest1.CustomerRequestStatus = CustomerRequestStatus.closed_by_website;
            guestRequest1.Garden = Garden.yes;
            guestRequest1.Meals = Meals.yes;
            guestRequest1.Phone = Phone.no;
            guestRequest1.RoomService = RoomService.yes;
            guestRequest1.SmokingArea = SmokingArea.no;
            guestRequest1.SoccerField = SoccerField.yes;
            guestRequest1.Television = Television.no;
            guestRequest1.Trampoline = Trampoline.yes;

            BE.Host host = new BE.Host();
            host.hostKey = 12345678;
            host.privateName = "Mike";
            host.familyName = "Ross";
            host.phoneNumber = 0547201224;
            host.mailAddress = "mike123@gmail.com";
            //host.BankBranchDetails = ;
            host.bankAccountNumber = "2453566";
            host.CollectionClearance = CollectionClearance.yes;

            BE.Host host1 = new BE.Host();
            host.hostKey = 87654321;
            host.privateName = "Harvey";
            host.familyName = "Specter";
            host.phoneNumber = 0547235322;
            host.mailAddress = "harvey@gmail.com";
            //host.BankBranchDetails = ;
            host.bankAccountNumber = "3436457";
            host.CollectionClearance = CollectionClearance.yes;

            BE.Order order = new BE.Order();
            order.createDate = new DateTime(05 / 25 / 2019);
            order.orderDate = new DateTime(04 / 20 / 2019);
            order.Status = Status.dealt_with;

            BE.Order order1 = new BE.Order();
            order.createDate = new DateTime(06 / 26 / 2019);
            order.orderDate = new DateTime(07 / 12 / 2019);
            order.Status = Status.email_sent;

            BE.HostingUnit hostingUnit = new BE.HostingUnit();
            hostingUnit.hostingUnitName = "apartement1";
            hostingUnit.Pool = Pool.yes;
            hostingUnit.RoomService = RoomService.yes;
            hostingUnit.SmokingArea = SmokingArea.no;
            hostingUnit.SoccerField = SoccerField.doesnt_matter;
            hostingUnit.Television = Television.yes;
            hostingUnit.Trampoline = Trampoline.yes;
            hostingUnit.Wifi = Wifi.doesnt_matter;
            hostingUnit.BBQArea = BBQArea.doesnt_matter;
            hostingUnit.ChildrenAtracttions = ChildrenAttractions.no;
            hostingUnit.Garden = Garden.yes;
            hostingUnit.Jacuzzi = Jacuzzi.yes;
            hostingUnit.Meals = Meals.yes;
            hostingUnit.Phone = Phone.doesnt_matter;
            //hostingUnit.Owner;

            BE.HostingUnit hostingUnit1 = new BE.HostingUnit();
            hostingUnit.hostingUnitName = "apartement2";
            hostingUnit.Pool = Pool.yes;
            hostingUnit.RoomService = RoomService.yes;
            hostingUnit.SmokingArea = SmokingArea.yes;
            hostingUnit.SoccerField = SoccerField.yes;
            hostingUnit.Television = Television.yes;
            hostingUnit.Trampoline = Trampoline.no;
            hostingUnit.Wifi = Wifi.yes;
            hostingUnit.BBQArea = BBQArea.yes;
            hostingUnit.ChildrenAtracttions = ChildrenAttractions.doesnt_matter;
            hostingUnit.Garden = Garden.no;
            hostingUnit.Jacuzzi = Jacuzzi.yes;
            hostingUnit.Meals = Meals.yes;
            hostingUnit.Phone = Phone.no;
            //hostingUnit.Owner;

            BE.BankBranch bankBranch = new BE.BankBranch();
            bankBranch.bankNumber = 11;
            bankBranch.bankName = "diskont";
            bankBranch.branchNumber = 41;
            bankBranch.branchAddress = "yafo 220";
            bankBranch.BranchCity = BranchCity.tel_aviv;

            BE.BankBranch bankBranch1 = new BE.BankBranch();
            bankBranch.bankNumber = 10;
            bankBranch.bankName = "leumi";
            bankBranch.branchNumber = 70;
            bankBranch.branchAddress = "hapisga 22";
            bankBranch.BranchCity = BranchCity.jerusalem;

            BE.BankBranch bankBranch2 = new BE.BankBranch();
            bankBranch.bankNumber = 9;
            bankBranch.bankName = "mizrachi tfahot";
            bankBranch.branchNumber = 32;
            bankBranch.branchAddress = "modiin";
            bankBranch.BranchCity = BranchCity.tveria;

            BE.BankBranch bankBranch3 = new BE.BankBranch();
            bankBranch.bankNumber = 8;
            bankBranch.bankName = "igud";
            bankBranch.branchNumber = 20;
            bankBranch.branchAddress = "herzl 21";
            bankBranch.BranchCity = BranchCity.rehovot;

            BE.BankBranch bankBranch4 = new BE.BankBranch();
            bankBranch.bankNumber = 7;
            bankBranch.bankName = "poalim";
            bankBranch.branchNumber = 56;
            bankBranch.branchAddress = "arlozorov 12";
            bankBranch.BranchCity = BranchCity.tel_aviv;
        }



    }
}
