using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;

namespace PL
{
    class Program
    {
        static void Main(string[] args)
        {
            IBLDefinitions ibl = new IBLDefinitions();
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
            try
            {
                ibl.AddGuestRequest(guestRequest);
                ibl.UpdateGuestRequest(guestRequest);
                ibl.GetGuestRequestList();
            }
            catch(DuplicateWaitObjectException c)
            {
                throw c;
            }
        }
    }
}
