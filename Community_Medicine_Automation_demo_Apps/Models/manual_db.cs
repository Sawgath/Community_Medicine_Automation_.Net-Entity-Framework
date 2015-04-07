using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Community_Medicine_Automation_demo_Apps.Models
{
    public class manual_db
    {

        public string asString = WebConfigurationManager.ConnectionStrings["community_medicine_dbEntities25"].ConnectionString;
        public SqlConnection aConnection;
        private DataBase_Gateway aGateway = new DataBase_Gateway();
        public SqlCommand aCommand;


        public void get_update_qury(total_stock_tb tb)
        {                    aConnection = new SqlConnection(asString);
                            string query = "UPDATE total_stock_tb SET stock=" + tb.stock + "WHERE totalstockID=" + tb.totalstockID;
                            aConnection.Open();
                            aCommand = new SqlCommand(query, aConnection);
                            int affectedRows = aCommand.ExecuteNonQuery();
                            aConnection.Close();
                           
        }


        public void get_update_pqury(patient_tb tb)
        {
            aConnection = new SqlConnection(asString);
            string query = "UPDATE patient_tb SET patientService=" + tb.patientService + "WHERE voterID=" + tb.voterID;
            aConnection.Open();
            aCommand = new SqlCommand(query, aConnection);
            int affectedRows = aCommand.ExecuteNonQuery();
            aConnection.Close();

        }


        public void get_update_minusqury(List<treatment_tb> aTreatmentTbs)
        {
            List<total_stock_tb> aList = aGateway.GetTotalStockDATA();
            foreach (var mtbs in aTreatmentTbs)
            {



                foreach (var m2tbs in aList)
                {

                    if (mtbs.centerID==m2tbs.centerID && mtbs.medicineID==m2tbs.medicineID)
                    {

                        double asd = (double) (m2tbs.stock - mtbs.quantity);
                        aConnection = new SqlConnection(asString);
                        string query = "UPDATE total_stock_tb SET stock=" + asd + "WHERE centerID=" + mtbs.centerID + "AND medicineID=" + mtbs.medicineID;
                        aConnection.Open();
                        aCommand = new SqlCommand(query, aConnection);
                        int affectedRows = aCommand.ExecuteNonQuery();
                        aConnection.Close();


                        
                    }
   



                }



            }
           

        }




        public void pdfmake(int it)
        {
            aConnection = new SqlConnection(asString);
           
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();
            int i = 0;

            int yPoint = 0;
            string name = null;
            string adress = null;
            string age = null;
            string date = null;
            string doctor_name = null;
            string medname = null;
            string diseasenam = null;
            string dose = null;
            string timefortake = null;
            string quantity = null;

            //string sql = "select patientName,address,age from patient_tb where voterID='343287582'";
            string sql = "select patientName,address,age from patient_tb where voterID="+it;
            
            aConnection.Open();
            aCommand = new SqlCommand(sql, aConnection);
            adapter.SelectCommand = aCommand;
            adapter.Fill(ds);
            aConnection.Close();

            //string sql2 = "select date, doctorName , diseaseName ,medicineName,dose ,timeForTake ,quantity from treatment_tb where patientID='343287582'";
            string sql2 = "select date, doctorName , diseaseName ,medicineName,dose ,timeForTake ,quantity from treatment_tb where patientID="+it;
            aConnection.Open();
            aCommand = new SqlCommand(sql2, aConnection);
            adapter.SelectCommand = aCommand;
            adapter.Fill(ds2);
            aConnection.Close();

            PdfDocument pdf = new PdfDocument();
            pdf.Info.Title = "Database to PDF";
            PdfPage pdfPage = pdf.AddPage();
           
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 10, XFontStyle.Regular);

            yPoint = yPoint + 100;
            name = ds.Tables[0].Rows[0].ItemArray[0].ToString();

            graph.DrawString("patient name: " + name, font, XBrushes.Black, new XRect(40, 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);
            

            for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
            {

                adress = ds.Tables[0].Rows[i].ItemArray[1].ToString();
                age = ds.Tables[0].Rows[i].ItemArray[2].ToString();

                graph.DrawString("--------------------------------", font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString(" Address: " + adress, font, XBrushes.Black, new XRect(40, yPoint + 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("AGE: " + age, font, XBrushes.Black, new XRect(40, yPoint + 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                graph.DrawString("--------------------------------", font, XBrushes.Black, new XRect(40, yPoint + 60, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                yPoint = yPoint + 100;
            }

            yPoint = yPoint +100;
            for (i = 0; i <= ds2.Tables[0].Rows.Count - 1; i++)
            {
                
                 date= ds2.Tables[0].Rows[i].ItemArray[0].ToString();
                 doctor_name= ds2.Tables[0].Rows[i].ItemArray[1].ToString();
                 diseasenam = ds2.Tables[0].Rows[i].ItemArray[2].ToString();
                 medname = ds2.Tables[0].Rows[i].ItemArray[3].ToString();
                 dose = ds2.Tables[0].Rows[i].ItemArray[4].ToString();
                 timefortake = ds2.Tables[0].Rows[i].ItemArray[5].ToString();
                graph.DrawString("History by date-------------------------", font, XBrushes.Black, new XRect(40, yPoint, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString(" Date: " + date, font, XBrushes.Black, new XRect(40, yPoint + 20, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("Doc Name: " + doctor_name, font, XBrushes.Black, new XRect(40, yPoint + 40, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("Disease: " + diseasenam, font, XBrushes.Black, new XRect(40, yPoint + 60, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("Medicine Name: " + medname, font, XBrushes.Black, new XRect(40, yPoint + 80, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("Dose: " + dose, font, XBrushes.Black, new XRect(40, yPoint + 100, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                graph.DrawString("When to take: " + timefortake, font, XBrushes.Black, new XRect(40, yPoint + 120, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                


                graph.DrawString("--------------------------------", font, XBrushes.Black, new XRect(40, yPoint + 160, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                yPoint = yPoint + 180;
            }


            string pdfFilename = "E:/fullinformation.pdf";
            pdf.Save(pdfFilename);
            Process.Start(pdfFilename);



        }




    }
}