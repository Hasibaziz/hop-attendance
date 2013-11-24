using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace ERP.Domain.Model
{
	public class PurchasereqdetailsEntity
	{
		public string Id
		{
			get;
			set;
		}
		public string Purchaseno
		{
			get;
			set;
		}
		public string Productid
		{
			get;
			set;
		}
		public string Packsizeid
		{
			get;
			set;
		}
		public string Unitprice
		{
			get;
			set;
		}
		public string Purchaseqnty
		{
			get;
			set;
		}
		public string Totalprice
		{
			get;
			set;
		}

        public string SaveStat
        {
            get;
            set;
        }
        public string PacksizeName
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
	}
}

