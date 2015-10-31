using System;
using System.Web.Mvc;
using ABusiness;
using BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SheepShop.Controllers
{
    public class sheep_shopController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Stock()
        {
            int id;
            if (int.TryParse(Request.QueryString.Get("id"), out id))
            {
                StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), id);
                var t = stock.GetJSONStock(id);
                string jsonFormatted = JValue.Parse(t).ToString(Newtonsoft.Json.Formatting.Indented);
                ViewBag.str = jsonFormatted;
            }
            else
                ViewBag.str = "No day index";

            return View();
        }
        [HttpPost]
        public ActionResult Stock(int? id)
        {
            if (id.HasValue)
            {
                StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), id.Value);
                string t = stock.GetJSONStock(id.Value);
                string jsonFormatted = JValue.Parse(t).ToString(Newtonsoft.Json.Formatting.Indented);  
                ViewBag.str = jsonFormatted;
            }
            else
                ViewBag.str = "No day index";

            return View();
        }
        [HttpGet]
        public ActionResult Herd()
        {
            int id;
            if(int.TryParse(Request.QueryString.Get("id"), out id))
            {
                StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), id);
                var t = stock.Herd.GetJSONStock(id);
                string jsonFormatted = JValue.Parse(t).ToString(Newtonsoft.Json.Formatting.Indented);
                ViewBag.str = jsonFormatted;
            }
            else
                ViewBag.str = "No day index";

            return View();
        }
        [HttpPost]
        public string Herd(int? id)
        {
            if (id.HasValue)
            {
                StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), id.Value);
                var t = stock.Herd.GetJSONStock(id.Value);
                return t;
            }
            else
                return "No day index";
        }
        [HttpGet]
        public ActionResult  Order()
        {
            return View();
        }
        [HttpPost]
        public string Order(int? id, Decimal? milk, int? skins)
        {

            int day;
            Decimal milkQ;
            int skinsQ;

            if (!id.HasValue || !milk.HasValue || !skins.HasValue)
            {
                return "Missing data";
            }
            else
            {
                day = id.Value;
                milkQ = milk.Value;
                skinsQ = skins.Value;

                StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), day, milkQ, skinsQ);

                Response.ContentType = "application/json";
                Response.StatusCode = (int)stock.data[0];
                return (string)stock.data[1];
            }
        }
        [HttpPost]
        public string OrderConsole(int id, OrderInputModel input)
        {

            input.GetRequestFromReST();

            StockBO stock = new StockBO(new Herds(HerdBO.GetXmlEntries()), id, input.Request.order.milk, input.Request.order.skins);

            Response.ContentType = "application/json";
            Response.StatusCode = (int)stock.data[0];
            return (string)stock.data[1];

        }

        public class OrderInputModel
        {
            public string RequestJson { get; set; }
            public BL.OrderBO.JsonRequest Request;
            
            public void GetRequestFromReST()
            { 
                this.Request = JsonConvert.DeserializeObject<OrderBO.JsonRequest>(RequestJson); 
            }
        }
    }
}