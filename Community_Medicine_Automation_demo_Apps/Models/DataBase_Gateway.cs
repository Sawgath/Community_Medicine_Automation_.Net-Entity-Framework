using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Community_Medicine_Automation_demo_Apps.Models
{
    public class DataBase_Gateway
    {
        community_medicine_dbEntities db = new community_medicine_dbEntities();
        
        public void insert_data(medicine_tb aMedicineTb)
        {
            
            db.medicine_tb.Add(aMedicineTb);
            db.SaveChanges();

        }
        public void insert_data(disease_tb aDiseaseTb)
        {

            db.disease_tb.Add(aDiseaseTb);
            db.SaveChanges();

        }
        public void insert_data(center_tb aCenterTb)
        {

            db.center_tb.Add(aCenterTb);
            db.SaveChanges();

        }
        public void insert_data(doctor_tb aDoctorTb)
        {

            db.doctor_tb.Add(aDoctorTb);
            db.SaveChanges();

        }
        public void insert_data(patient_tb aTb)
        {

            db.patient_tb.Add(aTb);
            db.SaveChanges();

        }


        public List<district_tb> get_districtdata()
        {
            List<district_tb> aTbs = new List<district_tb>();
            var highScores = db.district_tb;
            foreach (var VAR1 in highScores)
            {
                district_tb aTb = new district_tb();
                aTb.districtID = VAR1.districtID;
                aTb.districtName = VAR1.districtName;
               
                aTbs.Add(aTb);
            }

            return aTbs;
        }


        public List<thana_tb> get_thanadata()
        {
            List<thana_tb> aTbs = new List<thana_tb>();
            var highScores = db.thana_tb;
            foreach (var VAR1 in highScores)
            {
                thana_tb aTb = new thana_tb();
                aTb.thanaID = VAR1.thanaID;
                aTb.thanaName = VAR1.thanaName;
                aTb.districtID = VAR1.districtID;
                aTb.districtName = VAR1.districtName;

                aTbs.Add(aTb);
            }

            return aTbs;
        }

        public List<center_tb> get_centerdata()
        {
            List<center_tb> aCenterTbs=new List<center_tb>();
            var highScores = db.center_tb;
            foreach (var VAR1 in highScores)
            { 
                center_tb aCenterTb=new center_tb();
                aCenterTb.centerId = VAR1.centerId;
                aCenterTb.centerName = VAR1.centerName;
                aCenterTb.districtID = VAR1.districtID;
                aCenterTb.districtName = VAR1.districtName;
                aCenterTb.thanaID = VAR1.thanaID;
                aCenterTb.thanaName = VAR1.thanaName;
                aCenterTbs.Add(aCenterTb);
            }

            return aCenterTbs;
        }
        public List<disease_tb> get_diseasedata()
        {
            List<disease_tb> aTbs = new List<disease_tb>();
            var highScores = db.disease_tb;
            foreach (var VAR1 in highScores)
            {
                disease_tb aTb = new disease_tb();
                aTb.diseaseID = VAR1.diseaseID;
                aTb.diseaseName = VAR1.diseaseName;
                aTb.description = VAR1.description;
                aTb.treatment = VAR1.treatment;
                aTb.prfered_drugs = VAR1.prfered_drugs;
                
                aTbs.Add(aTb);
            }

            return aTbs;
        }

        public List<doctor_tb> get_doctordata()
        {
            List<doctor_tb> aTbs = new List<doctor_tb>();
            var highScores = db.doctor_tb;
            foreach (var VAR1 in highScores)
            {
                doctor_tb aTb = new doctor_tb();
                aTb.doctorID = VAR1.doctorID;
                aTb.doctorName = VAR1.doctorName;
                aTb.centerID = VAR1.centerID;
                aTb.centerName = VAR1.centerName;
                aTb.specialization = VAR1.specialization;
                aTb.degree = VAR1.degree;
                aTbs.Add(aTb);
            }

            return aTbs;
        }
        public List<patient_tb> get_patientdata()
        {
            List<patient_tb> aTbs = new List<patient_tb>();
            var highScores = db.patient_tb;
            foreach (var VAR1 in highScores)
            {
                patient_tb aTb = new patient_tb();
                aTb.patientID = VAR1.patientID;
                aTb.voterID = VAR1.voterID;
                aTb.patientName = VAR1.patientName;
                aTb.address = VAR1.address;
                aTb.age = VAR1.age;
                aTb.age = VAR1.age;
                aTbs.Add(aTb);
            }

            return aTbs;
        }

        public List<medicine_tb> get_medicinedata()
        {
            List<medicine_tb> aMedicineTbs = new List<medicine_tb>();
            var highScores = db.medicine_tb;
            foreach (var VAR1 in highScores)
            {
                medicine_tb aMedicineTb = new medicine_tb();
                aMedicineTb.medicineID = VAR1.medicineID;
                aMedicineTb.medicineName = VAR1.medicineName;
                aMedicineTb.mg_ml = VAR1.mg_ml;
                aMedicineTbs.Add(aMedicineTb);
            }

            return aMedicineTbs;
        }


        public void insert_treatment_list(List<treatment_tb> aTreatmentTbs)
        {
            foreach (var entry in aTreatmentTbs)
            {
                db.treatment_tb.Add(entry);
            }
            db.SaveChanges();

        }



        public void insert_stock_data(List<stock_tb> aList)
        {
            foreach (var VAR in aList)
            {
                db.stock_tb.Add(VAR);
                
            }
            db.SaveChanges();
            
        }

        public void populateStockData(List<total_stock_tb> tl)
        {

            List<total_stock_tb> inputList1 = new List<total_stock_tb>();

            inputList1 = tl;
            var stockdata = db.total_stock_tb;

            if (stockdata.Count() == 0)
            {
                foreach (var one1 in inputList1)
                {
                    db.total_stock_tb.Add(one1);
                }
                db.SaveChanges();
            }
            else
            {
                foreach (var match1 in inputList1)
                {

                    foreach (var dbmatch in stockdata)
                    {
                        if (match1.centerID == dbmatch.centerID && match1.medicineID == dbmatch.medicineID)
                        {
                            var temp =from oTb in stockdata
                                      where oTb.centerID ==match1.centerID
                                      where oTb.medicineID == match1.medicineID
                                      select oTb;
                            foreach (var v1 in temp)
                            {
                            v1.stock = v1.stock + match1.stock;
                            /////////////////////////////////
                            //SqlConnection aConnection = new SqlConnection(asString);
                            //string query = "UPDATE total_stock_tb SET stock=" + v1.stock + "WHERE totalstockID=" + v1.totalstockID;
                            //aConnection.Open();
                            //aCommand = new SqlCommand(query, aConnection);
                            //int affectedRows = aCommand.ExecuteNonQuery();
                            //aConnection.Close();
                            /////////////////////////////////
                            manual_db aManualDb=new manual_db();
                            aManualDb.get_update_qury(v1);
                            //total_stock_tb aTb=new total_stock_tb();
                            //aTb = (total_stock_tb)v1;

                            //db.Entry(aTb).State = EntityState.Modified;
                            
                            }
                            //FOREACH or FOR ar maje db.SaveChanges() kaj kore nah.....reason??
                            //db.SaveChanges();
                        }
                        else
                        {
                            db.total_stock_tb.Add(match1);
                            db.SaveChanges();
                        }
                    }



                }


            }


        }


        //////////////////////////////////////////////////////////////////////////////////////


        //public void getStockData()
        //{
        //    List<stock_tb> aList1 = new List<stock_tb>();
        //    List<stock_tb> aList2 = new List<stock_tb>();
        //    List<stock_tb> tmp = new List<stock_tb>();
        //    var highScores = db.stock_tb;
        //    foreach (var VAR1 in highScores)
        //    {
        //        stock_tb aStockTb=new stock_tb();
        //        aStockTb.stockID = VAR1.stockID;
        //        aStockTb.centerID = VAR1.centerID;
        //        aStockTb.centerName = VAR1.centerName;
        //        aStockTb.medicineID = VAR1.medicineID;
        //        aStockTb.medicineName2 = VAR1.medicineName2;
        //        aStockTb.stock = VAR1.stock;
        //        aList1.Add(aStockTb);
        //    }

        //    //aList2 = aList1;
        //    //var cenresult=aList2.Select(x => x.centerID).Distinct().ToList();
        //    //var medresult = aList2.Select(x => x.medicineID).Distinct().ToList();
        //    ////var result = (from m in aList2  select m).Distinct().ToList();

        //    //    var asd = from oTb in aList2
        //    //          where oTb.centerID ==
        //    //          select oTb;
            
        //    //    stock_tb temtb = new stock_tb();
        //    //    foreach (var VAR1 in aList1)
        //    //    {
        //    //        foreach (var VAR2 in medresult)
        //    //        {
                    
        //    //        if (VAR1.medicineID==VAR2)
        //    //        {
        //    //            foreach (var VAR3 in cenresult)
        //    //            {


        //    //                if (VAR1.centerID == VAR3)
        //    //                {
        //    //                    temtb.centerID = VAR1.centerID;
        //    //                    temtb.centerName = VAR1.centerName;
        //    //                    temtb.medicineID = VAR1.medicineID;
        //    //                    temtb.medicineName2 = VAR1.medicineName2;
        //    //                    temtb.stock = temtb.stock + VAR1.stock;
        //    //                }
        //    //            }
        //    //        }
                    

        //    //    }
        //    //    tmp.Add(temtb);
        //    //}
        //    filterStockData(aList1);

            

        //}


        //public void filterStockData(List<stock_tb> aL)
        //{


        //    List<stock_tb> aList1 = new List<stock_tb>();
        //    List<stock_tb> temp = new List<stock_tb>();
            
        //    aList1 = aL;
        //    var asd = from oTb in aList1
        //              select oTb;
        //    var cenresult = asd.Select(x => x.centerID).Distinct().ToList();
        //    var medresult = asd.Select(x => x.medicineID).Distinct().ToList();

        //    foreach (var VAR3 in cenresult)
        //    {

        //        var asd2 = from oTb in aList1
        //                   where oTb.centerID == VAR3
        //                   select oTb;
        //        foreach (var VAR1 in medresult)
        //        {
        //            stock_tb temtb = new stock_tb();

        //            foreach (var VAR2 in asd2)
        //            {
        //                if (VAR2.medicineID == VAR1)
        //                {
        //                    temtb.centerID = VAR2.centerID;
        //                    temtb.centerName = VAR2.centerName;
        //                    temtb.medicineID = VAR2.medicineID;
        //                    temtb.medicineName2 = VAR2.medicineName2;
        //                    temtb.stock = temtb.stock + VAR2.stock;
        //                }

        //            }
        //            temp.Add(temtb);


        //        }

        //    }
            
        //    temp.RemoveAll(item => item.centerID == 0);
        //    List<total_stock_tb> s3 = new List<total_stock_tb>();
        //   saveInStock2Total(temp);
        //}

        //////////////////////////here.................now
        //public void saveInStock2Total(List<stock_tb> alist)
        //{
        //    List<total_stock_tb> s1 = new List<total_stock_tb>();
        //    //List<stock2_tb> s2 = new List<stock2_tb>();
        //    foreach (var VAR4 in alist)
        //    {
        //        total_stock_tb aTb = new total_stock_tb();

        //        aTb.centerID = VAR4.centerID;
        //        aTb.centerName = VAR4.centerName;
        //        aTb.medicineID = VAR4.medicineID;
        //        aTb.medicineName = VAR4.medicineName2;
        //        aTb.stock = VAR4.stock;
        //        s1.Add(aTb);
        //    }

        //    var stock2data = db.total_stock_tb;
        //    if (stock2data.Count()==0)
        //    {
        //        foreach (var one1 in s1)
        //        {
        //            db.total_stock_tb.Add(one1);    
        //        }
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        foreach (var march1 in s1)
        //        {

        //            foreach (var dbmatch in stock2data)
        //            {
        //                if (march1.centerID==dbmatch.centerID && march1.medicineID==dbmatch.medicineID)
        //                {

        //                    dbmatch.stock = dbmatch.stock + march1.stock;
        //                    db.Entry(dbmatch).State = System.Data.Entity.EntityState.Modified;
        //                    db.SaveChanges();
        //                }
        //                else
        //                {
        //                    db.total_stock_tb.Add(march1);
        //                    db.SaveChanges();
        //                }
        //            }
                    
        //        }



        //    }


           

        //}

        //////////////////////u need getStockData()////////////////////////////////////////////////////////////
        public List<total_stock_tb> GetTotalStockDATA()
        {
            var temp3 = db.total_stock_tb.ToList();

            return temp3;
        }

        public string getCenterNamebyCenterId(int str)
        {
            center_tb aTb=new center_tb();
            string name = "";
            var highScores = from ocenterTb in db.center_tb
                             where ocenterTb.centerId == str
                             select ocenterTb;
            if (highScores.Count()==1)
            {

                foreach (var VAR1 in highScores)
                {
                    name = VAR1.centerName;
                }

            }
            return name;
        }

        public string getMedNamebyMedId(int ias)
        {
            medicine_tb aTb = new medicine_tb();
            string name = "";
            var highScores = from oTb in db.medicine_tb
                             where oTb.medicineID == ias
                             select oTb;
            if (highScores.Count() == 1)
            {

                foreach (var VAR1 in highScores)
                {
                    name = VAR1.medicineName;
                }

            }
            return name;

        }


        public string getdisNamebydisId(int ias)
        {
            district_tb aTb = new district_tb();
            string name = "";
            var highScores = from oTb in db.district_tb
                             where oTb.districtID == ias
                             select oTb;
            if (highScores.Count() == 1)
            {

                foreach (var VAR1 in highScores)
                {
                    name = VAR1.districtName;
                }

            }
            return name;

        }
        public string getthanaNamebythanaId(int ias)
        {
            thana_tb aTb = new thana_tb();
            string name = "";
            var highScores = from oTb in db.thana_tb
                             where oTb.thanaID == ias
                             select oTb;
            if (highScores.Count() == 1)
            {

                foreach (var VAR1 in highScores)
                {
                    name = VAR1.thanaName;
                }

            }
            return name;

        }
    



        public string CreatePassword()
        {

            int length = 6;
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public int getcenterid(center_tb aCenterTb)
        {
            var highScores = from ocenterTb in db.center_tb
                             where ocenterTb.centerName == aCenterTb.centerName
                             where ocenterTb.password == aCenterTb.password
                             select ocenterTb;
            int i = highScores.Count();
            int c = 0;
            foreach (var VAR1 in highScores)
            {
                c = VAR1.centerId;
            }

            if (i == 1)
            {
                return c;
            }
            return 0;
        }

        public int validation_pass(center_tb aCenterTb)
        {
            var highScores = from ocenterTb in  db.center_tb
                             where ocenterTb.centerName == aCenterTb.centerName
                             where ocenterTb.password == aCenterTb.password
                             select  ocenterTb ;
            int i = highScores.Count();

            if (i != 1)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}