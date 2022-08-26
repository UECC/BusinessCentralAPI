using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BCAPI
{
	[XmlRoot(ElementName = "Item")]
	public class Item
	{
		[XmlElement(ElementName = "CUSTOMER")]
		public string customerNo { get; set; }
		[XmlElement(ElementName = "INVOICEDATE")]
		public string paymentReferenceDate { get; set; }
		[XmlElement(ElementName = "CURRENCY")]
		public string currencyCode { get; set; }
		[XmlElement(ElementName = "POD")]
		public string pod { get; set; }
		[XmlElement(ElementName = "POL")]
		public string pol { get; set; }
		[XmlElement(ElementName = "PAYMENTREFDATE")]
		public string sailingDate { get; set; }
		[XmlElement(ElementName = "VIN")]
		public string vinNo { get; set; }
		[XmlElement(ElementName = "TRANSPORTNUMBER")]
		public string vessel { get; set; }
		[XmlElement(ElementName = "UNITPRICE")]
		public double rate { get; set; }
		[XmlElement(ElementName = "SURCHARGECODE")]
		public string surchargeCode { get; set; }
		[XmlElement(ElementName = "PaymentID")]
		public string paymentId { get; set; }
	}

	[XmlRoot(ElementName = "Items")]
	public class Items
	{
		[XmlElement(ElementName = "Item")]
		public List<Item> Item { get; set; }
	}

}
