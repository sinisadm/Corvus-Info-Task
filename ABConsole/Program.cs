using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ABusiness;
using System.Web.Script.Serialization;
using BL;
using ABConsole;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Sheep_shop
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("XML file should be at location: C:\\XML");
            Console.WriteLine();
            Console.WriteLine("Press:");
            Console.WriteLine("\"t\" for test SHEEP–1:");
            Console.WriteLine("\"h\" for test HerdBO or");
            Console.WriteLine("\"j\" for test SHEEP–2 Stock or");
            Console.WriteLine("\"d\" for test SHEEP–2 Herd or");
            Console.WriteLine("\"p\" for test SHEEP–3 or");
            Console.WriteLine("\"e\" for exit!!!");
            string command = Console.ReadLine();

            switch(command)
            {
                default:
                    Main();
                    break;

                case "t":
                    TestSheep();
                    break;

                case "h":
                    TestHerdBO();
                    break;

                case "j":
                    TestJsonStock();
                    break;

                case "d":
                    TestJsonHerd();
                    break;

                case "p":
                    PostJson();
                    break;

                case "e":
                    Environment.Exit(0);
                    break;
            }
        }

        static void TestHerdBO()
        {
            HerdBO Herd = new HerdBO();
            foreach (SheepBO s in Herd.Herd)
                Console.WriteLine(s.Name);
            Console.WriteLine();
            Main();
        }

        static void TestSheep()
        {
            int dayNum;

            Console.WriteLine("Enter number of the day!!!");

            var dayNumString = Console.ReadLine();
            List<Animals> aList = new List<Animals>();
            Herds herd;
            StockBO stock;
            var context = new ABusiness.ABEntities();

            if (int.TryParse(dayNumString, out dayNum))
            {
                herd = new Herds(HerdBO.GetXmlEntries());

                stock = new StockBO(herd, dayNum);

                Console.WriteLine();
                Console.WriteLine("In Stock:");
                Console.WriteLine("   " + string.Format("{0:0.000}", stock.ReturnMilkStock()) + " liters of milk");
                Console.WriteLine("   " + stock.ReturnSkinStock().ToString() + " skins of wool");
                Console.WriteLine("Herd: ");

                foreach( string str in stock.GetHerdAges())
                    Console.WriteLine(str);

                Console.WriteLine();
                Main();
            }
            else
            {
                Console.WriteLine("You entered a string!!! Try again");
                Console.WriteLine();
                TestSheep();
            }
        }

        static void TestJsonStock()
        {
            int day;
            Console.WriteLine("Enter number of the day for JSON stock!!!");
            string input = Console.ReadLine();
            if (int.TryParse(input, out day))
            {
                StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), day);

                Console.WriteLine(stock.GetJSONStock(day));
                Main();
            }
            else
            {
                Console.WriteLine("You entered a string!!! Try again");
                Console.WriteLine();
                TestJsonStock();
            }
        }

        static void TestJsonHerd()
        {
            int day;
            Console.WriteLine("Enter number of the day for JSON stock!!!");
            string input = Console.ReadLine();
            if (int.TryParse(input, out day))
            {
                StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), day);
                Console.WriteLine(stock.Herd.GetJSONStock(day));
                Main();
            }
            else
            {
                Console.WriteLine("You entered a string!!! Try again");
                Console.WriteLine();
                TestJsonStock();
            }
        }

        static void PostJson()
        {
            int day;
            Decimal milk;
            int skins;
            Console.WriteLine("Order for day?");
            string dayInput = Console.ReadLine();
            Console.WriteLine("Customer?");
            string customer = Console.ReadLine();
            Console.WriteLine("Quantity of Milk?");
            string milkInput = Console.ReadLine();
            Console.WriteLine("Quantity of Skins?");
            string skinsInput = Console.ReadLine();
            if (int.TryParse(dayInput, out day) && Decimal.TryParse(milkInput, out milk) && int.TryParse(skinsInput, out skins))
            {
                
                OrderBO.JsonRequest req = new OrderBO.JsonRequest(customer, milk, skins);

                var json = RestHelper.ConvertObjectToJason(req);
                var response = RestHelper.Post("http://localhost:51190/", "sheep_shop/orderConsole/" + day.ToString() , json);
//                System.Console.WriteLine(string.Format("Status Code {0}", response.StatusCode));
                if (response.ErrorMessage != null)
                {
                    System.Console.WriteLine(string.Format("Error Message {0}", response.ErrorMessage));
                }
                else if (response.StatusCode.ToString() != "OK")
                {
                    System.Console.WriteLine(JValue.Parse(response.Content.ToString()).ToString(Newtonsoft.Json.Formatting.Indented));
                }
                else
                {
                    var resObj = RestHelper.GetResponseObject(response);
                    System.Console.WriteLine();
                }
                System.Console.Read();
                Main();
            }
            else
            {
                Console.WriteLine("You entered a string!!! Try again");
                Console.WriteLine();
                PostJson();
            }
        }
    }
}
