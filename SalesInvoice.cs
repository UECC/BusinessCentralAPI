using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BCAPI
{

    [XmlRoot(ElementName = "Orders")]
    public class Orders
    {
        [XmlElement(ElementName = "Order")]
        public List<Order> Order { get; set; }
    }


    [XmlRoot(ElementName = "Order")]
    public class Order
    {
        [XmlElement(ElementName = "documenttype")]
        public string documenttype { get; set; }

        [XmlElement(ElementName = "number")]
        public string number { get; set; }

        [XmlElement(ElementName = "sellToCustomerNumber")]
        public string sellToCustomerNumber { get; set; }

        [XmlElement(ElementName = "billToCustomerNumber")]
        public string billToCustomerNumber { get; set; }

        [XmlElement(ElementName = "currencyCode")]
        public string currencyCode { get; set; }

        [XmlElement(ElementName = "externalDocumentNo")]
        public string externalDocumentNo { get; set; }
        
        [XmlElement(ElementName = "DocumentDate")]
        public string DocumentDate { get; set; }

        [XmlElement(ElementName = "postingdate")]
        public string postingdate { get; set; }

        [XmlElement(ElementName = "yourReference")]
        public string yourReference { get; set; }

        [XmlElement(ElementName = "payementMethodCode")]
        public string payementMethodCode { get; set; }


        [XmlElement(ElementName = "salesOrderLines")]
        public List<SalesOrderLines> salesOrderLines { get; set; }
    }

    [XmlRoot(ElementName = "salesOrderLines")]
    public class SalesOrderLines
    {
        [XmlElement(ElementName = "linetype")]
        public string linetype { get; set; }

        [XmlElement(ElementName = "number")]
        public string number { get; set; }

        [XmlElement(ElementName = "unitPrice")]
        public decimal unitPrice { get; set; }

        [XmlElement(ElementName = "quantity")]
        public decimal quantity { get; set; }

        [XmlElement(ElementName = "OSBReferenceDocumentNo")]
        public string OSBReferenceDocumentNo { get; set; }

        [XmlElement(ElementName = "OSBReferencePositionNo")]
        public int OSBReferencePositionNo { get; set; }

        [XmlElement(ElementName = "VAT_Prod_Posting_Group")]
        public string VAT_Prod_Posting_Group { get; set; }

       
    }

    //[XmlRoot(ElementName = "dimensions")]
    //public class Dimensions
    //{
    //    [XmlElement(ElementName = "code")]
    //    public string code { get; set; }
    //    [XmlElement(ElementName = "displayName")]
    //    public string displayName { get; set; }
    //    [XmlElement(ElementName = "valueCode")]
    //    public string valueCode { get; set; }
    //    [XmlElement(ElementName = "valueDisplayName")]
    //    public string valueDisplayName { get; set; }
    //}
}
