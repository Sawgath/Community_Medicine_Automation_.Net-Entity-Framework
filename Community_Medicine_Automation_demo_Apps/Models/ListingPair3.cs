using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Community_Medicine_Automation_demo_Apps.Models
{
    public class ListingPair3
    {

        public ListingPair3() 
        {
            aCenterTbs=new List<center_tb>();
            AMedicineTbs=new List<medicine_tb>();

        }

        public string temp1 { get; set; }

        public List<center_tb> aCenterTbs { get; set; }
        public List<medicine_tb> AMedicineTbs { get; set; }

    }
}