using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zadatak_1_Console.ServiceReference1;

namespace Zadatak_1_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Service1Client proxy = new Service1Client();
            string option = "";

            do
            {
                Console.WriteLine("Menu:");
                Console.WriteLine();
                Console.WriteLine("1. View all articles.");
                Console.WriteLine("2. Buy article.");
                Console.WriteLine("3. Change article price.");
                Console.WriteLine("4. Add new article.");
                Console.WriteLine("5. Exit.");
                Console.WriteLine();
                Console.WriteLine("Chose the option:");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        List<Article> AllArticles = new List<Article>(proxy.GetData());
                        int order = 0;

                        Console.WriteLine();
                        foreach (Article a in AllArticles)
                        {
                            Console.WriteLine(++order + ". Article name: " + a.Name + ", Quantity: " + a.Quantity + ", Price: " + a.Price);
                        }
                        Console.WriteLine();
                        break;

                    case "2":
                        AllArticles = new List<Article>(proxy.GetData());
                        order = 0;
                        string input = "";

                        Console.WriteLine();
                        foreach (Article a in AllArticles)
                        {
                            Console.WriteLine(++order + ". Article name: " + a.Name + ", Quantity: " + a.Quantity + ", Price: " + a.Price);
                        }
                        int ChosenArticle = 0;
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Select Article you wish to buy. (Input number from the list or ~ to cancel)");
                            input = Console.ReadLine();
                            if (input == "~") break;
                            bool success = int.TryParse(input, out ChosenArticle);
                            if (!success)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect input, please select article number from the list.");
                                continue;
                            }
                            else if (ChosenArticle < 1 || ChosenArticle > AllArticles.Count)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect input, please select article number from the list.");
                                continue;
                            }
                            break;
                        }
                        if (input == "~") { Console.WriteLine(); continue; }
                        int Quantity = 0;
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Select quantity of article. (or ~ to cancel)");
                            input = Console.ReadLine();
                            if (input == "~") break;

                            bool success = int.TryParse(input, out Quantity);
                            if (!success)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect quantity, please try again.");
                                continue;
                            }
                            else if (AllArticles[ChosenArticle - 1].Quantity - Quantity < 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect quantity, please chose within avalaible article quantity range.");
                                continue;
                            }

                            break;
                        }
                        if (input == "~") { Console.WriteLine(); continue; }

                        AllArticles[ChosenArticle - 1].Quantity -= Quantity;
                        proxy.BuyArticles(AllArticles.ToArray(), AllArticles[ChosenArticle - 1], Quantity);

                        Console.WriteLine();
                        Console.WriteLine("You successfully bought article.\n");
                        break;

                    case "3":

                        break;
                    case "4":

                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Invalid input, please try again.\n");
                        break;
                }

            } while (option != "5");
        }
    }
}
