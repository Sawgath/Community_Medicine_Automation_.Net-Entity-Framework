using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Community_Medicine_Automation_demo_Apps.Models;

namespace Community_Medicine_Automation_demo_Apps.Controllers
{

    public class HeadOfficeController : Controller
    {

        private DataBase_Gateway aGateway = new DataBase_Gateway();
        private List<thana_tb> aThanaTbs = new List<thana_tb>();
        private List<center_tb> acenterTbs = new List<center_tb>();
        // GET: HeadOffice
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Medicine_Setup([Bind(Include = "medicineName,mg_ml")] medicine_tb aMedicineTb)
        {
            DataBase_Gateway aGateway = new DataBase_Gateway();

            try
            {
                if (ModelState.IsValid)
                {
                    aGateway.insert_data(aMedicineTb);
                    return View("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View("Medicine_Setup");
        }

        [HttpGet]
        public ActionResult Medicine_Setup()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Disease_Setup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disease_Setup(
            [Bind(Include = "diseaseName,description,treatment,prfered_drugs")] disease_tb aDiseaseTb)
        {
            DataBase_Gateway aGateway = new DataBase_Gateway();

            try
            {
                if (ModelState.IsValid)
                {
                    aGateway.insert_data(aDiseaseTb);
                    return View("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("",
                    "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View("Disease_Setup");

        }

        [HttpGet]
        public ActionResult Create_Center()
        {
            ViewBag.Dis1 = aGateway.get_districtdata();
            ViewBag.than1 = aGateway.get_thanadata();
            return View();
        }


        [HttpPost]

        public ActionResult Create_Center(string districtId, string thanaId, string centerName)
        {
            
            center_tb aCenterTb=new center_tb();
            if (districtId != null && thanaId != null && centerName != null)
            {


                // System.Web.Security.Membership.GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
                aCenterTb.centerName = centerName;
                aCenterTb.districtID = Convert.ToInt32(districtId);
                aCenterTb.districtName = aGateway.getdisNamebydisId(Convert.ToInt32(districtId));
                aCenterTb.thanaID = Convert.ToInt32(thanaId);
                aCenterTb.thanaName = aGateway.getthanaNamebythanaId(Convert.ToInt32(thanaId));
                aCenterTb.password = aGateway.CreatePassword();
                aGateway.insert_data(aCenterTb);
                return View("Index");
            }

            return View();

        }

        public ActionResult Center_Info()
        {


            return View();
        }

        //[HttpGet]
        public ActionResult Medicine_Supply()
        {
            DataBase_Gateway aGateway = new DataBase_Gateway();
            stock_tb asStockTb = new stock_tb();
            ViewBag.Dis = aGateway.get_districtdata();
            ViewBag.Med = aGateway.get_medicinedata();

            return View();

        }

        //[HttpPost]
        public JsonResult GetThanaByDistrictId(int DistrictID)
        {

            aThanaTbs = aGateway.get_thanadata();
            var thanaList = aThanaTbs.Where(a => a.districtID == DistrictID).ToList();

            return Json(thanaList, JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetCenterByThanaId(int thanaId)
        {

            acenterTbs = aGateway.get_centerdata();
            var centerList = acenterTbs.Where(a => a.thanaID == thanaId).ToList();

            return Json(centerList, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult Medicine_Supply(string districtId, string thanaId, string centerId, List<temp1> obj)
        {
            DataBase_Gateway aGateway = new DataBase_Gateway();
            List<district_tb> aDistrictTbs = aGateway.get_districtdata();
            List<medicine_tb> ameTbs = aGateway.get_medicinedata();
            ViewBag.Dis = aDistrictTbs;
            ViewBag.Med = ameTbs;
            try
            {
                obj.RemoveAll(item => item == null);
                List<temp1> aTemp1s = new List<temp1>();
                aTemp1s = obj;
                aTemp1s.RemoveAll(item => item.id == null);

                string check1 = "";
                string check2 = "";
                //List<stock_tb> aList = new List<stock_tb>();
                List<total_stock_tb> aList = new List<total_stock_tb>();
                foreach (temp1 item in aTemp1s)
                {
                    total_stock_tb asStockTb = new total_stock_tb();
                    
                    if (item.id != null && item.text2 != null)
                    {
                        asStockTb.centerID = Convert.ToInt32(centerId);
                        check1 = aGateway.getCenterNamebyCenterId(Convert.ToInt32(centerId));
                        if (check1 != null)
                        {
                            asStockTb.centerName = check1;
                        }

                        asStockTb.medicineID = Convert.ToInt32(item.id);
                        check2 = aGateway.getMedNamebyMedId(Convert.ToInt32(item.id));
                        if (check2 != null)
                        {
                            asStockTb.medicineName = check2;
                        }

                        asStockTb.stock = Convert.ToInt32(item.text2);
                    }
                    aList.Add(asStockTb);
                }

                //aGateway.insert_stock_data(aList);
                aGateway.populateStockData(aList);
                


            }
            catch (NullReferenceException ex)
            {

            }

            return View();



        }

        public class temp1
        {
            public string id { get; set; }
            public string text { get; set; }
            public string text2 { get; set; }
        }
    }
}