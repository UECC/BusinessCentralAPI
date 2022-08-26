using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCAPI
{
    public class PaymentTerms
    {
        public IList<Value> value { get; set; }
    }

    public class Value
    {
        public string id { get; set; }
        public string code { get; set; }
        public string displayName { get; set; }
        public string dueDateCalculation { get; set; }
        public string discountDateCalculation { get; set; }
        public int discountPercent { get; set; }
        public bool calculateDiscountOnCreditMemos { get; set; }
        public DateTime lastModifiedDateTime { get; set; }
    }


}
