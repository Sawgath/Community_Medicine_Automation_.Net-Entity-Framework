using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Community_Medicine_Automation_demo_Apps.Models
{
    public class ExtraWork
    {







        public List<Pair1> Pair1List()
        {

            string[] lines = System.IO.File.ReadAllLines(@"D:\Listofthana.txt");

            List<Pair1> listpair = new List<Pair1>();
            

            string ina = "";
            foreach (string line in lines)
            {


                if (Regex.IsMatch(line, @"\$\[\[(.*)\]\]"))
                {

                    ina = Regex.Match(line, @"([A-Za-z\s]+)").ToString();
                    

                }
                else if (Regex.IsMatch(line, @"\#\[\[(.*)\]\]"))
                {
                    Pair1 apair1= new Pair1();
                    apair1.Dis = Regex.Replace(ina, " District", "");
                    apair1.thana = Regex.Replace((Regex.Match(line, @"([A-Za-z\s]+)").ToString()), " Upazila", "");
                    

                    listpair.Add(apair1);
                }



            }

            return listpair;

        }
    
    
    }



}