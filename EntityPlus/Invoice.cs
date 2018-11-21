//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//
//
// <copyright file="Invoice.cs" company="Multinerd">
//     Copyright (c) Multinerd. All Rights Reserved.
// </copyright>
//------------------------------------------------------------------------------

using Prism.Mvvm;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EntityPlus
{
	[GeneratedCode("Tessa", "1.2.3.4")]
	/// <summary> Invoice model class.</summary>
	public partial class Invoice : BindableBase, IEditableObject
    {
		/// <summary> Initializes a new instance of the <see cref="Invoice"/> class.</summary>
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Invoice()
		{
			this.InvoiceLines = new HashSet<InvoiceLine>();
        }
	
        /// <summary>Gets or sets the InvoiceId property</summary>
		[Key, Required]
		[DisplayName("Invoice Id"), Display(Name = "Invoice Id")]
		public int InvoiceId
        {
            get { return this._InvoiceId; }
			set { this.SetProperty(ref this._InvoiceId, value); }
        }

		/// <summary> InvoiceId backing field</summary>
        private int _InvoiceId;


        /// <summary>Gets or sets the CustomerId property</summary>
		[Required]
		[DisplayName("Customer Id"), Display(Name = "Customer Id")]
		public int CustomerId
        {
            get { return this._CustomerId; }
			set { this.SetProperty(ref this._CustomerId, value); }
        }

		/// <summary> CustomerId backing field</summary>
        private int _CustomerId;


        /// <summary>Gets or sets the InvoiceDate property</summary>
		[Required]
		[DisplayName("Invoice Date"), Display(Name = "Invoice Date")]
		public System.DateTime InvoiceDate
        {
            get { return this._InvoiceDate; }
			set { this.SetProperty(ref this._InvoiceDate, value); }
        }

		/// <summary> InvoiceDate backing field</summary>
        private System.DateTime _InvoiceDate;


        /// <summary>Gets or sets the BillingAddress property</summary>
		[StringLength(70)]
		[DisplayName("Billing Address"), Display(Name = "Billing Address")]
		public string BillingAddress
        {
            get { return this._BillingAddress; }
			set { this.SetProperty(ref this._BillingAddress, value); }
        }

		/// <summary> BillingAddress backing field</summary>
        private string _BillingAddress;


        /// <summary>Gets or sets the BillingCity property</summary>
		[StringLength(40)]
		[DisplayName("Billing City"), Display(Name = "Billing City")]
		public string BillingCity
        {
            get { return this._BillingCity; }
			set { this.SetProperty(ref this._BillingCity, value); }
        }

		/// <summary> BillingCity backing field</summary>
        private string _BillingCity;


        /// <summary>Gets or sets the BillingState property</summary>
		[StringLength(40)]
		[DisplayName("Billing State"), Display(Name = "Billing State")]
		public string BillingState
        {
            get { return this._BillingState; }
			set { this.SetProperty(ref this._BillingState, value); }
        }

		/// <summary> BillingState backing field</summary>
        private string _BillingState;


        /// <summary>Gets or sets the BillingCountry property</summary>
		[StringLength(40)]
		[DisplayName("Billing Country"), Display(Name = "Billing Country")]
		public string BillingCountry
        {
            get { return this._BillingCountry; }
			set { this.SetProperty(ref this._BillingCountry, value); }
        }

		/// <summary> BillingCountry backing field</summary>
        private string _BillingCountry;


        /// <summary>Gets or sets the BillingPostalCode property</summary>
		[StringLength(10)]
		[DisplayName("Billing Postal Code"), Display(Name = "Billing Postal Code")]
		public string BillingPostalCode
        {
            get { return this._BillingPostalCode; }
			set { this.SetProperty(ref this._BillingPostalCode, value); }
        }

		/// <summary> BillingPostalCode backing field</summary>
        private string _BillingPostalCode;


        /// <summary>Gets or sets the Total property</summary>
		[Required]
		[DisplayName("Total"), Display(Name = "Total")]
		public decimal Total
        {
            get { return this._Total; }
			set { this.SetProperty(ref this._Total, value); }
        }

		/// <summary> Total backing field</summary>
        private decimal _Total;


		/// <summary> Gets or sets the Customer navigation property</summary>
		public virtual Customer Customer { get; set; }

		/// <summary> Gets or sets the InvoiceLines navigation property</summary>
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }

	        
		#region IEditableObject

		private Invoice_Backup _Invoice_Backup;

		private struct Invoice_Backup 
		{
			internal int _InvoiceId;
			internal int _CustomerId;
			internal System.DateTime _InvoiceDate;
			internal string _BillingAddress;
			internal string _BillingCity;
			internal string _BillingState;
			internal string _BillingCountry;
			internal string _BillingPostalCode;
			internal decimal _Total;
		}

        public void BeginEdit()
        {
			_Invoice_Backup = new Invoice_Backup() 
			{
				_InvoiceId = InvoiceId,
				_CustomerId = CustomerId,
				_InvoiceDate = InvoiceDate,
				_BillingAddress = BillingAddress,
				_BillingCity = BillingCity,
				_BillingState = BillingState,
				_BillingCountry = BillingCountry,
				_BillingPostalCode = BillingPostalCode,
				_Total = Total,
			};
		}

		public void CancelEdit()
        {
			InvoiceId = _Invoice_Backup._InvoiceId;
			CustomerId = _Invoice_Backup._CustomerId;
			InvoiceDate = _Invoice_Backup._InvoiceDate;
			BillingAddress = _Invoice_Backup._BillingAddress;
			BillingCity = _Invoice_Backup._BillingCity;
			BillingState = _Invoice_Backup._BillingState;
			BillingCountry = _Invoice_Backup._BillingCountry;
			BillingPostalCode = _Invoice_Backup._BillingPostalCode;
			Total = _Invoice_Backup._Total;
        }

        public void EndEdit()
        {
			_Invoice_Backup._InvoiceId = InvoiceId;
			_Invoice_Backup._CustomerId = CustomerId;
			_Invoice_Backup._InvoiceDate = InvoiceDate;
			_Invoice_Backup._BillingAddress = BillingAddress;
			_Invoice_Backup._BillingCity = BillingCity;
			_Invoice_Backup._BillingState = BillingState;
			_Invoice_Backup._BillingCountry = BillingCountry;
			_Invoice_Backup._BillingPostalCode = BillingPostalCode;
			_Invoice_Backup._Total = Total;
        }

		#endregion
 
		#region SampleData

		public static Invoice GetRandomInvoice()
        {
            return new Invoice()
            {
				InvoiceId = 31,
				CustomerId = 31427,
				InvoiceDate = DateTime.Parse("09/26/1930 20:25:15"),
				BillingAddress = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCity = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingState = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCountry = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingPostalCode = "XXXXXXXXXX",
            };
        }

		public static IList<Invoice> GetRandomInvoices()
        {
			var list = new List<Invoice>();
			
			list.Add(new Invoice()
			{
				InvoiceId = 32,
				CustomerId = 141140,
				InvoiceDate = DateTime.Parse("09/04/1947 18:39:50"),
				BillingAddress = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCity = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingState = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCountry = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingPostalCode = "XXXXXXXXXX",
			});

			list.Add(new Invoice()
			{
				InvoiceId = 33,
				CustomerId = 113982,
				InvoiceDate = DateTime.Parse("01/12/1919 02:43:39"),
				BillingAddress = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCity = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingState = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCountry = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingPostalCode = "XXXXXXXXXX",
			});

			list.Add(new Invoice()
			{
				InvoiceId = 34,
				CustomerId = 176921,
				InvoiceDate = DateTime.Parse("06/18/1981 12:17:39"),
				BillingAddress = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCity = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingState = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCountry = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingPostalCode = "XXXXXXXXXX",
			});

			list.Add(new Invoice()
			{
				InvoiceId = 35,
				CustomerId = 113274,
				InvoiceDate = DateTime.Parse("07/14/1990 15:24:18"),
				BillingAddress = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCity = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingState = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCountry = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingPostalCode = "XXXXXXXXXX",
			});

			list.Add(new Invoice()
			{
				InvoiceId = 36,
				CustomerId = 76721,
				InvoiceDate = DateTime.Parse("11/27/1986 00:32:13"),
				BillingAddress = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCity = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingState = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingCountry = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX",
				BillingPostalCode = "XXXXXXXXXX",
			});

			return list;
        }

		#endregion
 

	} 
} 
