using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ABusiness;

namespace BL
{
    public class StockBO
    {
        public HerdBO Herd;
        protected Decimal MilkStock;
        protected int SkinStock;

        public object[] data = new object[2];
        

        public StockBO(Herds herd)
        {
            this.Herd = new HerdBO(herd);
        }

        public StockBO(int day)
        {
            this.Herd = new HerdBO();
            this.ProcessStock(day);
        }

        public StockBO(Herds herd, int forDay)
        {
            this.Herd = new HerdBO(herd);
            this.ProcessStock(forDay);
        }

        public StockBO(Herds herd, int forDay, decimal milkQ, int skinsQ) : this(herd, forDay)
        {
            if (this.ReturnMilkStock() >= milkQ && this.ReturnSkinStock() >= skinsQ)
            {
                // all good
                data[0] = 201;
                data[1] = this.GetJSONStock(forDay, milkQ, skinsQ);
            }
            else if (this.ReturnMilkStock() < milkQ && this.ReturnSkinStock() >= skinsQ)
            {
                // return partial skins
                data[0] = 206;
                data[1] = this.GetJSONSkinStock(forDay, skinsQ);
            }
            else if (this.ReturnMilkStock() >= milkQ && this.ReturnSkinStock() < skinsQ)
            {
                // return partial milk
                data[0] = 206;
                data[1] = this.GetJSONMilkStock(forDay, milkQ);
            }
            else
            {
                data[0] = 204;
                data[1] = "[]";
            }
            
        }

        protected void ProcessStock(int numOfDays)
        {
            SheepBO sheep = new SheepBO();
            for (int cnt = 0; cnt < numOfDays; cnt++)
            {
                foreach (SheepBO a in this.Herd.Herd)
                {
                    a.ProcessDay(cnt);
                    if(a.IsLive)
                        this.MilkStock += a.DailyMilkProd;
                    if (a.TodaysWool)
                        this.SkinStock++;
                }
            }
        }

        public Decimal ReturnMilkStock()
        {
            return this.MilkStock;
        }

        public int ReturnSkinStock()
        {
            return this.SkinStock;
        }

        public List<string> GetHerdAges()
        {
            List<string> listOfAges = new List<string>();
            foreach (SheepBO a in this.Herd.Herd)
            {
                listOfAges.Add(a.Name + " " + ((Decimal)a.Age.Value / 100).ToString() + " years old");
            }
            return listOfAges;
        }

        public string GetJSONStock(int day)
        {
            StockResponse resp = new StockResponse(this.ReturnMilkStock(), this.ReturnSkinStock());
            string json = new JavaScriptSerializer().Serialize(resp);
            return json;
        }

        private object GetJSONStock(int forDay, decimal milkQ, int skinsQ)
        {
            StockResponse resp = new StockResponse(this.ReturnMilkStock(), this.ReturnSkinStock(), milkQ, skinsQ);
            string json = new JavaScriptSerializer().Serialize(resp);
            return json;
        }
        public string GetJSONMilkStock(int day)
        {
            StockResponseMilk resp = new StockResponseMilk(this.ReturnMilkStock());
            string json = new JavaScriptSerializer().Serialize(resp);
            return json;
        }

        private object GetJSONMilkStock(int forDay, decimal milkQ)
        {
            StockResponseMilk resp = new StockResponseMilk(milkQ);
            string json = new JavaScriptSerializer().Serialize(resp);
            return json;
        }


        public string GetJSONSkinStock(int day)
        {
            StockResponseSkins resp = new StockResponseSkins(this.ReturnSkinStock());
            string json = new JavaScriptSerializer().Serialize(resp);
            return json;
        }

        private object GetJSONSkinStock(int forDay, int skinsQ)
        {
            StockResponseSkins resp = new StockResponseSkins(skinsQ);
            string json = new JavaScriptSerializer().Serialize(resp);
            return json;
        }



        public class StockResponse
        {
            public Decimal Milk;
            public int Skins;

            public StockResponse()
            {
                Milk = 0;
                Skins = 0;
            }
            public StockResponse(Decimal milkQ, int skinsQ)
            {
                Milk = milkQ;
                Skins = skinsQ;
            }

            public StockResponse(decimal milkQ, int skinsQ, decimal milkQ1, int skinsQ1) 
            {
                Milk = milkQ1;
                Skins = skinsQ1;
            }
        }
        public class StockResponseSkins
        {
            public int Skins;

            public StockResponseSkins()
            {
                Skins = 0;
            }
            public StockResponseSkins(int skinsQ)
            {
                Skins = skinsQ;
            }
        }
        public class StockResponseMilk
        {
            public Decimal Milk;

            public StockResponseMilk()
            {
                Milk = 0;
            }
            public StockResponseMilk(Decimal milkQ)
            {
                Milk = milkQ;
            }
        }
    }
}
