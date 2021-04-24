using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScreenScraper.Domain
{
    public class Currency
    {
        public int ID { get; set; }
        public int ParentID { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public string NameBel { get; set; }
        public string NameEng { get; set; }
        public string QuotName { get; set; }
        public string QuotNameBel { get; set; }
        public string QuotNameEng { get; set; }
        public string NameMulti { get; set; }
        public string NameBelMulti { get; set; }
        public string NameEngMulti { get; set; }
        public int Scale { get; set; }
        public Periodicity Periodicity { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public Currency()
        { }
        public Currency(int iD)
        {
            ID = iD;
        }

        public Currency(int iD, int parentID, string code, string abbreviation, string name, string nameBel, string nameEng, string quotName, string quotNameBel, string quotNameEng, string nameMulti, string nameBelMulti, string nameEngMulti, int scale, Periodicity periodicity, DateTime dateStart, DateTime dateEnd) : this(iD)
        {
            ParentID = parentID;
            Code = code;
            Abbreviation = abbreviation;
            Name = name;
            NameBel = nameBel;
            NameEng = nameEng;
            QuotName = quotName;
            QuotNameBel = quotNameBel;
            QuotNameEng = quotNameEng;
            NameMulti = nameMulti;
            NameBelMulti = nameBelMulti;
            NameEngMulti = nameEngMulti;
            Scale = scale;
            Periodicity = periodicity;
            DateStart = dateStart;
            DateEnd = dateEnd;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Currency p = (Currency)obj;
                return (ID == p.ID) && (ParentID == p.ParentID);
            }
        }

        public override int GetHashCode()
        {
            return (ID << 2) ^ ParentID;
        }

        public override string ToString()
        {
            return String.Format($"Currency: {Abbreviation}, ID = {ID}");
        }
    }
}
