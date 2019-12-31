using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public enum Type {all, hotel, apartement, tent, house };
    public enum Area { all, north, south, east,west, jerusalem, center,eilat};
    public enum Status { not_dealt_yet, email_sent, closes_out_of_customer_unresponsiveness, dealt_with,expired,closed_by_website,accepted};
    public enum CustomerRequestStatus { open, closed_by_website, approved};
    public enum CollectionClearance {yes, no};
    public enum Pool { yes, no, doesnt_matter};
    public enum Jacuzzi { yes, no, doesnt_matter };
    public enum Wifi { yes, no, doesnt_matter };
    public enum Television { yes, no, doesnt_matter };
    public enum Garden { yes, no, doesnt_matter };
    public enum ChildrenAttractions { yes, no, doesnt_matter };
    public enum SmokingArea { yes, no, doesnt_matter };
    public enum SoccerField { yes, no, doesnt_matter };
    public enum BBQArea { yes, no, doesnt_matter };
    public enum Phone { yes, no, doesnt_matter };
    public enum Trampoline { yes, no, doesnt_matter };
    public enum RoomService { yes, no, doesnt_matter };
    public enum Meals { yes, no, doesnt_matter };
    public enum BranchCity { jerusalem, tel_aviv, tveria, heiffa, bnei_brak, ashdod, modiin, eilat,dimona, rehovot, hadera, affula, beit_shean, beit_shemesh, netivot, beer_sheva};
}
