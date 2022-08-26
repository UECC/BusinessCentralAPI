using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BCAPI
{

    [XmlRoot(ElementName = "JOURNALS")]
    public class JOURNALS
    {
        [XmlElement(ElementName = "JOURNAL")]
        public List<JOURNAL> JOURNAL { get; set; }
    }


    [XmlRoot(ElementName = "JOURNAL")]
    public class JOURNAL
    {
        [XmlElement(ElementName = "code")]
        public string code { get; set; }

        [XmlElement(ElementName = "displayName")]
        public string displayName { get; set; }

        [XmlElement(ElementName = "templateDisplayName")]
        public string templateDisplayName { get; set; }        

        [XmlElement(ElementName = "journalLines")]
        public List<JournalLines> journalLines { get; set; }
    }

    [XmlRoot(ElementName = "journalLines")]
    public class JournalLines
    {
        [XmlElement(ElementName = "documentType")]
        public string documentType { get; set; }

        [XmlElement(ElementName = "accountType")]
        public string accountType { get; set; }

        [XmlElement(ElementName = "accountNumber")]
        public string accountNumber { get; set; }

        [XmlElement(ElementName = "postingDate")]
        public string postingDate { get; set; }

        [XmlElement(ElementName = "documentNumber")]
        public string documentNumber { get; set; }

        [XmlElement(ElementName = "currencyCode")]
        public string currencyCode { get; set; }

        [XmlElement(ElementName = "amount")]
        public decimal amount { get; set; }

        [XmlElement(ElementName = "taxCode")]
        public string taxCode { get; set; }

        [XmlElement(ElementName = "paymentMethodCode")]
        public string paymentMethodCode { get; set; }

        //[XmlElement(ElementName = "dimension1Code")]
        //public string dimension1Code { get; set; }

        //[XmlElement(ElementName = "dimension2Code")]
        //public string dimension2Code { get; set; }

        [XmlElement(ElementName = "dimension3Code")]
        public string dimension3Code { get; set; }

        [XmlElement(ElementName = "dimension4Code")]
        public string dimension4Code { get; set; }

        [XmlElement(ElementName = "dimension5Code")]
        public string dimension5Code { get; set; }

        [XmlElement(ElementName = "dimension6Code")]
        public string dimension6Code { get; set; }

        [XmlElement(ElementName = "dimension7Code")]
        public string dimension7Code { get; set; }

        [XmlElement(ElementName = "dimension8Code")]
        public string dimension8Code { get; set; }
    }

}
