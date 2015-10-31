using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABusiness;

namespace BL
{
    public class SheepBO : Animals
    {
        public bool TodaysWool;
        public Decimal DailyMilkProd;
        public int NextShaveDay;
        public int LastShaveDay
        {
            get;
            protected set;
        }
        protected bool IsShavedOnTime;
        public bool IsLive {
            get;
            protected set;
        }

        public SheepBO() {
            this.IsLive = true;
        }
        public SheepBO(Animals animal)
        {

            this.Age = animal.Age;
            this.Sex = animal.Sex;
            this.Name = animal.Name;
            if(this.Age.Value < 1000)
                this.IsLive = true;
            else
                this.IsLive = false;
            this.AgeOnInitInDays = animal.AgeOnInitInDays;
        }
        public SheepBO(string name, string ageString, string sexString) {
            int age;
            int sex = new int(); 

            int.TryParse(ageString, out age);
            if (sexString == Common.GendersStatic.Male)
                sex = 1;
            else if (sexString == Common.GendersStatic.Female)
                sex = 2;

            this.Sex = sex;
            this.Age = age;
            this.Name = name;
            if(this.Age.Value < 1000)
                this.IsLive = true;
            else
                this.IsLive = false;
        }


        public void ProcessDay(int day) {
            if (this.IsLive)
            {
                this.CalculateAgeInDays(day);
                if (this.CheckIfCanBeMilked())
                    this.DailyMilkProd = this.Milk();
                if (this.CheckIfCanBeShaved())
                    this.TodaysWool = this.Shave(day);
            }
        }

        public int ReturnLastShavedDay() {
            return this.LastShaveDay;
        }

        protected bool Shave(int day)
        {
            bool shave;
            if (
                (this.NextShaveDay == day && this.IsShavedOnTime) 
                ||(!this.IsShavedOnTime && this.NextShaveDay + 13 == day)
                || day == 0)
            {
                this.LastShaveDay = day;
                this.NextShaveDay = (int)(day + 8 + 1 + (this.Age * 0.01));
                this.IsShavedOnTime = shave = true;
            }
            else shave = false;

            return  shave;
        }
        protected Decimal Milk()
        {
            if (this.IsLive)
                return CalculateMilkStock4Day();
            else return 0;
        }

        protected bool CheckIfCanBeMilked() {
            return this.Sex == 2 && this.IsLive;
        }
        protected bool CheckIfCanBeShaved()
        {
            return this.Age >= 100 && this.IsLive;
        }
        protected Decimal CalculateMilkStock4Day()
        {
            Decimal stock;

            if (this.IsLive)
                stock = 50 - (Decimal)((this.Age - 1) * 0.03);
            else
                stock = 0;

            return  stock;
        }
        protected void CalculateAgeInDays(int appDayNum)
        {
            int ageInDays;

            if (this.Age.HasValue)
                ageInDays = this.Age.Value + 1;
            else
                ageInDays = appDayNum;

            this.Age = ageInDays;

            if (this.Age >= 1000)
                this.IsLive = false;
        }
    }
}
