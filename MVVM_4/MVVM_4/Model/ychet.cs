
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_4.Model
{
    internal class ychet
    {
        public string Name { get; set; }
        public string Tip { get; set; }
        private int Money { get; set; }
        public int money
        {
            get { return Money; }
            set
            {
                if (value < 0)
                {
                    Money = -1 * value;
                }
                else { Money = value; };
            }
        }
        public bool Record { get; set; }
        public object Date;


        public ychet(string Name, string Tip, int Money, bool Record, object Date)
        {
            this.Name = Name;
            this.Tip = Tip;
            this.money = Money;
            this.Record = Record;
            this.Date = Date;
        }
    }
}
