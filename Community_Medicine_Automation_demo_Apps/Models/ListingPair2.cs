using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Community_Medicine_Automation_demo_Apps.Models
{
    public class ListingPair2
    {
            community_medicine_dbEntities db = new community_medicine_dbEntities();
           public ListingPair2()
           {
               Availabledis = new List<SelectListItem>();
               Availablethana = new List<SelectListItem>();
           }


           [Display(Name = "Country")]
           
           public IList<SelectListItem> Availabledis { get; set; }
           [Display(Name = "State")]
           
           public IList<SelectListItem> Availablethana { get; set; }
           public IList<center_tb> GetAlldis()
           {
               var query = from states in db.center_tb
                           select states;
               var content = query.ToList();
               return content;
           }
           public IList<center_tb> GetAllthanaBydis(string dis)
           {
               var query = from states in db.center_tb
                           where states.districtName == dis
                           select states;
               var content = query.ToList();
               return content;
           }
    }
}