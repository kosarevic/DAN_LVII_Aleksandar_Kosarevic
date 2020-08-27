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
            //Connection to the WCF service.
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
                    //Case 1 shows all available articles.
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
                    //Case 2 implemets add new article logic.
                    case "2":
                        AllArticles = new List<Article>(proxy.GetData());
                        order = 0;
                        string input = "";
                        //All articles are shown to the user.
                        Console.WriteLine();
                        foreach (Article a in AllArticles)
                        {
                            Console.WriteLine(++order + ". Article name: " + a.Name + ", Quantity: " + a.Quantity + ", Price: " + a.Price);
                        }
                        //User selects the article from the list.
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
                                Console.WriteLine("Incorrect input, please try again.");
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
                        //User selects the quantity of articles he/she wishes to buy.
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
                            else if (Quantity < 1)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect quantity, it must be at least 1.");
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
                    //Case 3 implemets edit articles price logic.
                    case "3":
                        AllArticles = new List<Article>(proxy.GetData());
                        order = 0;
                        input = "";
                        //All articles are shown to the user.
                        Console.WriteLine();
                        foreach (Article a in AllArticles)
                        {
                            Console.WriteLine(++order + ". Article name: " + a.Name + ", Quantity: " + a.Quantity + ", Price: " + a.Price);
                        }
                        ChosenArticle = 0;
                        //User choses article from the list.
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Select Article to change its price. (Input number from the list or ~ to cancel)");
                            input = Console.ReadLine();
                            if (input == "~") break;
                            bool success = int.TryParse(input, out ChosenArticle);
                            if (!success)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect input, please try again.");
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
                        //User sets a new price for the article.
                        int Price = 0;
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Select new price of article. (or ~ to cancel)");
                            input = Console.ReadLine();
                            if (input == "~") break;

                            bool success = int.TryParse(input, out Price);
                            if (!success)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect price, please try again.");
                                continue;
                            }
                            else if (Price < 1)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect price, price can not have 0 or negative value.");
                                continue;
                            }

                            break;
                        }
                        if (input == "~") { Console.WriteLine(); continue; }

                        AllArticles[ChosenArticle - 1].Price = Price;
                        proxy.WriteToFile(AllArticles.ToArray());

                        Console.WriteLine();
                        Console.WriteLine("You successfully changed article price.\n");
                        break;
                    //Case 4 implemets add new article logic.
                    case "4":
                        AllArticles = new List<Article>(proxy.GetData());
                        Article NewArticle = new Article();
                        //User is required to input new article name.
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Input new article name. (or ~ to cancel)");
                            NewArticle.Name = Console.ReadLine();
                            if (NewArticle.Name == "~") break;

                            if (NewArticle.Name == "" || NewArticle.Name == null)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Article name must contain at least one character.");
                                continue;
                            }
                            break;
                        }
                        if (NewArticle.Name == "~") { Console.WriteLine(); continue; }
                        //User is requiered to input new article quantity.
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Input new article quantity. (or ~ to cancel)");
                            input = Console.ReadLine();
                            if (input == "~") break;
                            bool success1 = int.TryParse(input, out int ArticleQuantity);
                            if (!success1)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect article quantity, please try again.");
                                continue;
                            }
                            else if (ArticleQuantity < 1)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect article quantity, it must be at least 1.");
                                continue;
                            }
                            NewArticle.Quantity = ArticleQuantity;
                            break;
                        }
                        if (input == "~") { Console.WriteLine(); continue; }
                        //User is requiered to input new article price.
                        while (true)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Input new article price. (or ~ to cancel)");
                            input = Console.ReadLine();
                            if (input == "~") break;
                            bool success1 = int.TryParse(input, out int ArticlePrice);
                            if (!success1)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect article price, please try again.");
                                continue;
                            }
                            else if (ArticlePrice < 1)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Incorrect article price, it must be at least 1.");
                                continue;
                            }
                            NewArticle.Price = ArticlePrice;
                            break;
                        }
                        if (input == "~") { Console.WriteLine(); continue; }

                        AllArticles.Add(NewArticle);
                        proxy.WriteToFile(AllArticles.ToArray());

                        Console.WriteLine();
                        Console.WriteLine("You successfully created new article.\n");

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
