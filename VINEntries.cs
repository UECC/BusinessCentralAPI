using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BCAPI
{
	[XmlRoot(ElementName = "root")]
	public class Root
	{

		[XmlElement(ElementName = "documentType")]
		public string documentType { get; set; }

		[XmlElement(ElementName = "documentNo")]
		public string documentNo { get; set; }

		[XmlElement(ElementName = "itemNo")]
		public string itemNo { get; set; }

		[XmlElement(ElementName = "vinNo")]
		public string vinNo { get; set; }

		[XmlElement(ElementName = "pod")]
		public string pod { get; set; }

		[XmlElement(ElementName = "pol")]
		public string pol { get; set; }

		[XmlElement(ElementName = "vessel")]
		public string vessel { get; set; }

		[XmlElement(ElementName = "amount")]
		public double amount { get; set; }
	}

	[XmlRoot(ElementName = "roots")]
	public class Roots
	{

		[XmlElement(ElementName = "root")]
		public List<Root> Root { get; set; }
	}

}
