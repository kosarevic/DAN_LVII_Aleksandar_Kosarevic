using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Zadatak_1_Service.Model;

namespace Zadatak_1_Service
{
    public class Service1 : IService1
    {
        //Static variable added to generate file number, during application life time.
        public static int BillCount = 0;
        /// <summary>
        /// Method responsible for getting all data from text file and converting them to List of Article objects.
        /// </summary>
        /// <returns></returns>
        public List<Article> GetData()
        {
            List<string> text = new List<string>();
            List<Article> AllArticles = new List<Article>();
            string location = AppDomain.CurrentDomain.BaseDirectory + @"\Articles.txt";

            StreamReader sr = new StreamReader(location);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                text.Add(line);
            }

            sr.Close();

            if (text.Any())
            {
                foreach (string t in text)
                {
                    string[] temp = t.Split(',');
                    Article article = new Article();

                    article.Name = temp[0];
                    article.Quantity = int.Parse(temp[1]);
                    article.Price = int.Parse(temp[2]);

                    AllArticles.Add(article);
                }
            }
            return AllArticles;
        }
        /// <summary>
        /// Method responsible for directing objects and paramaters to WriteToFile and CreateBill methods.
        /// </summary>
        /// <param name="AllArticles"></param>
        /// <param name="article"></param>
        /// <param name="Quantity"></param>
        public void BuyArticles(List<Article> AllArticles, Article article, int Quantity)
        {
            WriteToFile(AllArticles);
            CreateBill(article, Quantity);
        }
        /// <summary>
        /// Method responsible for writing data into file Articles.txt
        /// </summary>
        /// <param name="AllArticles"></param>
        public void WriteToFile(List<Article> AllArticles)
        {
            for (int j = 0; j < AllArticles.Count; j++)
            {
                if (AllArticles[j].Quantity == 0)
                    AllArticles.Remove(AllArticles[j]);
            }

            string[] output = new string[AllArticles.Count];
            string line = "";

            for (int i = 0; i < AllArticles.Count; i++)
            {
                line = AllArticles[i].Name + "," + AllArticles[i].Quantity + "," + AllArticles[i].Price;
                output[i] = line;
            }
            File.WriteAllLines(AppDomain.CurrentDomain.BaseDirectory + @"\Articles.txt", output);
        }
        /// <summary>
        /// Method responsible for creating new text file, representing user previous purchise.
        /// </summary>
        /// <param name="article"></param>
        /// <param name="Quantity"></param>
        public void CreateBill(Article article, int Quantity)
        {
            string CurrentTime = DateTime.Now.ToString("HH.mm");
            string path = AppDomain.CurrentDomain.BaseDirectory + string.Format(@"\Racun_{0}_{1}.txt", ++BillCount, CurrentTime);
            string appendText = string.Format("{0}, {1} - {2} * {3}, {4}", DateTime.Now.ToShortTimeString(), article.Name, Quantity, article.Price, Quantity * article.Price);

            File.WriteAllText(path, appendText);
        }
    }
}
