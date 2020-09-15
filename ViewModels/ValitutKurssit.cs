using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NäyttöProjekti.ViewModels
{
    public class ValitutKurssit
    {
        public int KurssiID { get; set; }
        public string Nimi { get; set; }
        public string Kuvaus { get; set; }
        public Nullable<System.DateTime> alkuPvm { get; set; }
        public Nullable<System.DateTime> loppuPvm { get; set; }
        public string Laajuus { get; set; }
        public string Toteutuskausi { get; set; }

    }
}