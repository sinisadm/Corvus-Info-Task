using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Xml.Linq;
using ABusiness;

namespace BL
{
    public class HerdBO
    {
        public List<SheepBO> Herd = new List<SheepBO>();

        public HerdBO()
        {
            CreateHerdFromXml();
        }
        public HerdBO(Herds herd)
        {
            foreach (Animals anim in herd.Animals)
                this.Herd.Add(new SheepBO(anim));
        }

        public void CreateHerdFromXml() {
            foreach (Animals anim in HerdBO.GetXmlEntries())
                this.Herd.Add(new SheepBO(anim));
        }
        public static List<Animals> GetXmlEntries() {
            var context = new ABEntities();
            List<Animals> list = new List<Animals>();
            var xml = XDocument.Load(@"C:\XML\Herd.xml");
            
            var query = from c in xml.Root.Descendants("mountainsheep")
                        select new Animals(c.Attribute("name").Value,
                                           c.Attribute("sex").Value,
                                           c.Attribute("age").Value);

            foreach (Animals a in query.ToList())
                list.Add(a);

            return list;
        }
        public string GetJSONStock(int day)
        {
            foreach(SheepBO anim in this.Herd)
            {
                if (anim.Age.Value >= 1000)
                    this.Herd.Remove(anim);
            }
            HerdResponse resp = new HerdResponse(this.Herd);
            string json = new JavaScriptSerializer().Serialize(resp);
            return json.Replace("_", "-");
        }

        public class HerdResponse
        {
            public List<JsonSheep> Herd = new List<JsonSheep>();
            public HerdResponse() { }
            public HerdResponse(List<SheepBO> list) {
                foreach (SheepBO sheep in list)
                    Herd.Add(new JsonSheep(sheep.Name, sheep.Age.Value, sheep.LastShaveDay, sheep.AgeOnInitInDays));
            }

        }
        public class JsonSheep {
            public string name;
            public Decimal age;
            public Decimal age_Last_Shaved;

            public JsonSheep() { }
            public JsonSheep(string sName, int sAge, int ageOnLastShave, Decimal ageOnInit)
            {
                this.name = sName;
                this.age = (Decimal)sAge / 100;
                this.age_Last_Shaved = (ageOnLastShave+ ageOnInit) / 100 ;
            }
        }
    }
}
