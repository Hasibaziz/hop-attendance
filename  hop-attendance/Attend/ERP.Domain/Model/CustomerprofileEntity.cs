using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;


namespace ERP.Domain.Model
{
	public class CustomerprofileEntity
	{
		public string Id
		{
			get;
			set;
		}
         [Required(ErrorMessage = "*")]
		public string Profiletype
		{
			get;
			set;
		}
         [Required(ErrorMessage = "*")]
		public string Customertype
		{
			get;
			set;
		}
         [Required(ErrorMessage = "*")]
		public string Name
		{
			get;
			set;
		}
		public string Ownername
		{
			get;
			set;
		}
		public string Ownership
		{
			get;
			set;
		}
		public string Noofvechile
		{
			get;
			set;
		}
		public string Typeofvechile
		{
			get;
			set;
		}
		public string Oilchanage
		{
			get;
			set;
		}
		public string Noofcrews
		{
			get;
			set;
		}
		public string Typeofbuisness
		{
			get;
			set;
		}
		public string Mainmechanicname
		{
			get;
			set;
		}
		public string Qualifications
		{
			get;
			set;
		}
         [Required(ErrorMessage = "*")]
		public string Address
		{
			get;
			set;
		}
		public string Telephone
		{
			get;
			set;
		}
		public string Mobile
		{
			get;
			set;
		}
		public string Sizeofoutlet
		{
			get;
			set;
		}
		public string Selfpositions
		{
			get;
			set;
		}
		public string Contactperson1
		{
			get;
			set;
		}
		public string Contactperson2
		{
			get;
			set;
		}
		public string Contactperson3
		{
			get;
			set;
		}
		public string Washramp
		{
			get;
			set;
		}
		public string Lift
		{
			get;
			set;
		}
		public string Waterjet
		{
			get;
			set;
		}
		public string Electricalmotor
		{
			get;
			set;
		}
		public string Breakoiltester
		{
			get;
			set;
		}
		public string Modeofpayent
		{
			get;
			set;
		}
		public string Location
		{
			get;
			set;
		}
		public string Socialhealth
		{
			get;
			set;
		}
		public string Financialhealth
		{
			get;
			set;
		}
		public string Zone
		{
			get;
			set;
		}
		public string Route
		{
			get;
			set;
		}
		public string Remarks
		{
			get;
			set;
		}
		public string CreatedBy
		{
			get;
			set;
		}
		public string CreatedDate
		{
			get;
			set;
		}
		public string UpdatedBy
		{
			get;
			set;
		}
		public string UpdatedDate
		{
			get;
			set;
		}
	}
}

