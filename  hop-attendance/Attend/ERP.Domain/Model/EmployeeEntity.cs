using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Web.Mvc;

namespace ERP.Domain.Model
{
	public class EmployeeEntity
	{
		public string Id
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
        [Required(ErrorMessage = "*")]
		public string Fname
		{
			get;
			set;
		}
        [Required(ErrorMessage = "*")]
		public string Mname
		{
			get;
			set;
		}
        [Required(ErrorMessage = "*")]
		public string Genderid
		{
			get;
			set;
		}
        
		public string Bloodgroupid
		{
			get;
			set;
		}
        [Required(ErrorMessage = "*")]
		public string Caddress
		{
			get;
			set;
		}
        [Required(ErrorMessage = "*")]
		public string Paddress
		{
			get;
			set;
		}
		public string Phone
		{
			get;
			set;
		}
        [Required(ErrorMessage = "*")]
		public string Compnayid
		{
			get;
			set;
		}
        [Required(ErrorMessage = "*")]
		public string Departmentid
		{
			get;
			set;
		}
        [Required(ErrorMessage = "*")]
		public string Designationid
		{
			get;
			set;
		}
        //[Required(ErrorMessage = "*")]
		public string Basic
		{
			get;
			set;
		}
		public bool Status
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

         [Required(ErrorMessage = "*")]
        public string JoiningDate
        {
            get;
            set;
        }
         public string DateOfBirth
         {
             get;
             set;
         }
         public string Education
         {
             get;
             set;
         }
         public string BloodGroup
         {
             get;
             set;
         }
         public string Grade
         {
             get;
             set;
         }
	}
}

