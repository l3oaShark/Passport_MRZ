using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passport_MRZ.Models
{
    public class CountriesModel
    {
        public string CountriesName { get; set; }
        public string NumericCode { get; set; }
        public string TwoLetterCode { get; set; }
        public string ThreeLetterCode { get; set; }
        public string NameAndCode { get; set; }
    }
}
