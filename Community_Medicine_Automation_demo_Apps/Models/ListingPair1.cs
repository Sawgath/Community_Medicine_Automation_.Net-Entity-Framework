using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Community_Medicine_Automation_demo_Apps.Models
{
    public class ListingPair1
    {

    //    public ListingPair1()
    //{
    //    List1 = new List<Pair1>();
    //    AStockTbs=new List<stock_tb>();
    //}

    //    public List<stock_tb> AStockTbs { get; set; }
    //    public List<Pair1> List1 { get; set; }


        community_medicine_dbEntities db = new community_medicine_dbEntities();


    }


    public class hello
    {
        public string Dis { get; set; }
        public string thana { get; set; }
    }
}

//<select name="dropdown1" id="123" onselect="s">
//                    @foreach (var recordrow in Model.List1)
//                    {
//                        if (recordrow.Dis != string1)
//                        {
//                            <option>@recordrow.Dis</option>
//                            string1 = recordrow.Dis;
//                        }
//                    }
//                </select>


/////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////


//private void entry_data()
//{
//    string[] lines = System.IO.File.ReadAllLines(@"D:\Listofthana.txt");

//    List<hello> aHellos = new List<hello>();

//    string ina = "";
//    foreach (string line in lines)
//    {


//        if (Regex.IsMatch(line, @"\$\[\[(.*)\]\]"))
//        {
//            //aHello.Dis = Regex.Match(line, @"\$\[\[(.*)\]\]").ToString();
//            //Regex rgx = new Regex(@"\$\[\[\]\]"); // get rid of the quotes and braces
//            //aHello.Dis = rgx.Replace(line, ""); 
//            //Console.WriteLine("\t" + line + " hola");
//            //ina = Regex.Match(line, @"([A-Za-z\s]+)").ToString();
//            ina = Regex.Match(line, @"([A-Za-z\s]+)").ToString();       

//        }
//        else if (Regex.IsMatch(line, @"\#\[\[(.*)\]\]"))
//        {
//            hello aHello = new hello();
//            aHello.Dis = Regex.Replace(ina, " District", "");
//            aHello.thana = Regex.Replace((Regex.Match(line, @"([A-Za-z\s]+)").ToString()), " Upazila", "");

//            aHellos.Add(aHello);
//        }

//    }
//    string disFlag = "";
//    //new work
//    foreach (var v1 in aHellos)
//    {
//        if (v1.Dis!=disFlag)
//        {
//            district_tb aDistrictTb = new district_tb();
//            aDistrictTb.districtName = v1.Dis;
//            disFlag = v1.Dis;
//            db.district_tb.Add(aDistrictTb);
//        }
//    }
//    db.SaveChanges();
//    string thanaFlag = "";
//    //new work

//    foreach (var v1 in aHellos)
//    {
//            var dbDis = from odisTb in db.district_tb
//                         where odisTb.districtName == v1.Dis
//                         select odisTb;
//            thana_tb aThanaTb=new thana_tb();
//            if (dbDis.Count()==1)
//            {
//            foreach (var var2 in dbDis)
//            {
//                aThanaTb.thanaName = v1.thana;
//                aThanaTb.districtID = var2.districtID;
//                aThanaTb.districtName = var2.districtName;
//                db.thana_tb.Add(aThanaTb);        
//            }
//            }

//    }
//    db.SaveChanges();

//}