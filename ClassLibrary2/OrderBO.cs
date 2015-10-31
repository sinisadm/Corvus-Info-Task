using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class OrderBO
    {
        public class Order
        {
            public Decimal milk;
            public int skins;

            public Order(Decimal milkQ, int skinsQ)
            {
                this.milk = milkQ;
                this.skins = skinsQ;
            }
        }
        public class JsonRequest
        {
            public string customer;
            public Order order;

            public JsonRequest()
            {
                customer = "";
            }
            public JsonRequest(string cust, Decimal milkQ, int skinsQ)
            {
                customer = cust;
                order = new Order(milkQ, skinsQ);
            }
        }
        public class JsonResponse
        {
            public Decimal Milk;
            public int Skins;

            public JsonResponse()
            {
                Milk = 0;
                Skins = 0;
            }
            public JsonResponse(Decimal milkQ, int skinsQ)
            {
                Milk = milkQ;
                Skins = skinsQ;
            }
        }
    }
}
