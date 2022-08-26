using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BCAPI
{

    [XmlRoot(ElementName = "POS")]
    public class POS
    {
        [XmlElement(ElementName = "PO")]
        public List<PO> PO { get; set; }
    }


    [XmlRoot(ElementName = "PO")]
    public class PO
    {
        [XmlElement(ElementName = "documenttype")]
        public string documenttype { get; set; }

        //[XmlElement(ElementName = "noSeries")]
        //public string noSeries { get; set; }

        //[XmlElement(ElementName = "postingNoSeries")]
        //public string postingNoSeries { get; set; }

        [XmlElement(ElementName = "number")]
        public string number { get; set; }

        [XmlElement(ElementName = "vendorno")]
        public string vendorno { get; set; }

        [XmlElement(ElementName = "vendorInvoiceNumber")]
        public string vendorInvoiceNumber { get; set; }

        [XmlElement(ElementName = "vendorCrMemoNumber")]
        public string vendorCrMemoNumber { get; set; }

        //[XmlElement(ElementName = "currencyCode")]
        //public string currencyCode { get; set; }

        [XmlElement(ElementName = "DocumentDate")]
        public string DocumentDate { get; set; }

        [XmlElement(ElementName = "postingdate")]
        public string postingdate { get; set; }

        [XmlElement(ElementName = "TotalImportedFCY")]
        public decimal TotalImportedFCY { get; set; }
        



        [XmlElement(ElementName = "purchaseOrderLines")]
        public List<PurchaseOrderLines> purchaseOrderLines { get; set; }
    }

    [XmlRoot(ElementName = "purchaseOrderLines")]
    public class PurchaseOrderLines
    {
        [XmlElement(ElementName = "linetype")]
        public string linetype { get; set; }

        [XmlElement(ElementName = "number")]
        public string number { get; set; }

        [XmlElement(ElementName = "unitCost")]
        public decimal unitCost { get; set; }

        [XmlElement(ElementName = "quantity")]
        public decimal quantity { get; set; }


        [XmlElement(ElementName = "OSBReferenceDocumentNo")]
        public string OSBReferenceDocumentNo { get; set; }

        [XmlElement(ElementName = "OSBReferencePositionNo")]
        public int OSBReferencePositionNo { get; set; }

        [XmlElement(ElementName = "VAT_Prod_Posting_Group")]
        public string VAT_Prod_Posting_Group { get; set; }

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
