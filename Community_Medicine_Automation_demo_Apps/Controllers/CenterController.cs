using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Community_Medicine_Automation_demo_Apps.Models;

namespace Community_Medicine_Automation_demo_Apps.Controllers
{


    public class CenterController : Controller
    {
        community_medicine_dbEntities db = new community_medicine_dbEntities();
        DataBase_Gateway aGateway = new DataBase_Gateway();
        manual_db aManualDb=new manual_db();
        // GET: Center
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "centerName,password")]center_tb aCenterTb)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    int i= aGateway.validation_pass(aCenterTb);
                    if (i==1)
                    {
                        if (aGateway.getcenterid(aCenterTb)!=0)
                        {
                            TempData["doc1"] = aGateway.getcenterid(aCenterTb);
                        }
                        else
                        {
                            return View("error1");
                        }

                        TempData["doc2"] = aCenterTb.centerName;
                        return View("Center");
                    }
                    return View("error1");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View("error1");
        }

        public ActionResult Center()
        {
            return View();
        }

        [HttpGet]
        public ActionResult DoctorEntry()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorEntry([Bind(Include = "doctorName,degree,specialization")]doctor_tb aDoctorTb)
        {
            DataBase_Gateway aGateway = new DataBase_Gateway();

            try
            {
                if (ModelState.IsValid)
                {
                    aDoctorTb.centerID = (int) TempData["doc1"];
                    aDoctorTb.centerName =Convert.ToString(TempData["doc2"]);
                    aGateway.insert_data(aDoctorTb);
                    return View("Center");
                    
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View("error1");
        }
        public ActionResult stockReport()
        {
            //TempData["doc1"] 
            List<stock_tb> aList1=new List<stock_tb>();
            List<total_stock_tb> temp= new List<total_stock_tb>();
            int ira = (int) TempData["doc1"];
            TempData["doc1"] = ira;
            //aGateway.getStockData();
            temp = aGateway.GetTotalStockDATA();
            
            var asd = from oTb in temp
                      where oTb.centerID==ira
                      select oTb;
          //  var cenresult = asd.Select(x => x.centerID).Distinct().ToList();
          //  var medresult = asd.Select(x => x.medicineID).Distinct().ToList();
           
          //  foreach (var VAR3 in cenresult)
          //  {

          //      var asd2 = from oTb in aList1
          //            where oTb.centerID == VAR3
          //            select oTb;
          //      foreach (var VAR1 in medresult)
          //      {
          //          stock_tb temtb = new stock_tb();
                    
          //              foreach (var VAR2 in asd2)
          //              {
          //                  if (VAR2.medicineID == VAR1)
          //                  {
          //                      temtb.centerID = VAR2.centerID;
          //                      temtb.centerName = VAR2.centerName;
          //                      temtb.medicineID = VAR2.medicineID;
          //                      temtb.medicineName2 = VAR2.medicineName2;
          //                      temtb.stock = temtb.stock + VAR2.stock;
          //                  }

          //              }
          //              temp.Add(temtb);
                    

          //      }

          //  }
          ////aTemp1s.RemoveAll(item => item.id == null)
          //  temp.RemoveAll(item => item.centerID == 0);
            ViewBag.as1 = asd;

            return View();
        }


        public ActionResult treatment()
        {
            ViewBag.doc1 = aGateway.get_doctordata();
            ViewBag.Med1 = aGateway.get_medicinedata();
            ViewBag.dis1 = aGateway.get_diseasedata();
            return View();
        }

        [HttpPost]
        public ActionResult treatment(string vid, string vname, string address, string age, string obs, string docid, string docname, string date, List<temp4> obj)
        {
            ViewBag.doc1 = aGateway.get_doctordata();
            ViewBag.Med1 = aGateway.get_medicinedata();
            ViewBag.dis1 = aGateway.get_diseasedata();
            int flag1 = 0;
            int vid1 = Convert.ToInt32(vid);
            List<treatment_tb> aTreatmentTbs= new List<treatment_tb>();
            List<patient_tb> alisList = aGateway.get_patientdata();

            if (alisList.Count>0)
            {
                foreach (var match1 in alisList)
                {
                    if (match1.voterID==vid1)
                    {
                        patient_tb aPatientTb = new patient_tb();
                        aPatientTb.voterID = match1.voterID;
                        aPatientTb.patientName = match1.patientName;
                        aPatientTb.address = match1.address;
                        aPatientTb.age = match1.age;
                        aPatientTb.patientService = match1.patientService + 1;
                        aManualDb.get_update_pqury(aPatientTb);
                        flag1 = 1;

                    }
                    else
                    {
                       
                    }
                }
                if (flag1 == 0)
                {
                    patient_tb aPatientTb = new patient_tb();
                    aPatientTb.voterID = Convert.ToInt32(vid);
                    aPatientTb.patientName = vname;
                    aPatientTb.address = address;
                    aPatientTb.age = Convert.ToInt32(age);
                    aPatientTb.patientService = 1;
                    aGateway.insert_data(aPatientTb);
                }


            }
            else
            {
                patient_tb aPatientTb = new patient_tb();
                aPatientTb.voterID = Convert.ToInt32(vid);
                aPatientTb.patientName = vname;
                aPatientTb.address = address;
                aPatientTb.age = Convert.ToInt32(age);
                aPatientTb.patientService = 1;
                aGateway.insert_data(aPatientTb);
            }
            int centerid =(int) TempData["doc1"];
            TempData["doc1"] = centerid;
            //int centerid = 6;
            string centerName =aGateway.getCenterNamebyCenterId(centerid);
            if (obj.Count>0)
            {


                foreach (var VAR1 in obj)
                {
                    treatment_tb aTb = new treatment_tb();
                    aTb.patientID = Convert.ToInt32(vid);
                    aTb.patientName = vname;
                    aTb.doctorID = Convert.ToInt32(docid);
                    aTb.doctorName = docname;
                    aTb.centerID = centerid;
                    aTb.centerName = centerName;
                    aTb.observation = obs;
                    aTb.date = date;
                    aTb.diseaseID = Convert.ToInt32(VAR1.id);
                    aTb.diseaseName = VAR1.text1;
                    aTb.medicineID = Convert.ToInt32(VAR1.text2);
                    aTb.medicineName = VAR1.text3;
                    aTb.dose = Convert.ToInt32(VAR1.text4);
                    aTb.timeForTake = VAR1.text5;
                    aTb.quantity = Convert.ToInt32(VAR1.text6);
                    aTb.note = VAR1.text7;

                    aTreatmentTbs.Add(aTb);

                }

                aGateway.insert_treatment_list(aTreatmentTbs);
                aManualDb.get_update_minusqury(aTreatmentTbs);
            }





            return View();
        
        }

        public ActionResult pdf()
        {

            // int centerid = (int)TempData["doc1"];

            return View();
        }

        [HttpPost]
        public ActionResult pdf(string vId)
        {

           // int centerid = (int)TempData["doc1"];
            aManualDb.pdfmake(Convert.ToInt32(vId));
            return View();


        }

        public ActionResult Logout()
        {
            TempData["doc1"] = "";
            return View("Index");
        }


        public ActionResult error1()
        {
            return View();
        }



    }

    public class temp4
    {
            public string id { get; set; }
            public string text1 { get; set; }
            public string text2 { get; set; }
            public string text3 { get; set; }
            public string text4 { get; set; }
            public string text5 { get; set; }
            public string text6 { get; set; }
            public string text7 { get; set; }
            


    }
   
}





/////////////////////////////////back up code //////////////////////////////////
 //var asd = from oTb in aList1
 //                     where oTb.centerID == ira
 //                     select oTb;
 //           var medresult = asd.Select(x => x.medicineID).Distinct().ToList();
 //           foreach (var VAR1 in medresult)
 //           {
 //               stock_tb temtb = new stock_tb();
 //               foreach (var VAR2 in asd)
 //               {
 //                   if (VAR2.medicineID == VAR1)
 //                   {
 //                       temtb.centerID = VAR2.centerID;
 //                       temtb.centerName = VAR2.centerName;
 //                       temtb.medicineID = VAR2.medicineID;
 //                       temtb.medicineName2 = VAR2.medicineName2;
 //                       temtb.stock = temtb.stock + VAR2.stock;
 //                   }
                    
 //               }
 //               temp.Add(temtb);