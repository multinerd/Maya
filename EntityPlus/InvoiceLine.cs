//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//
//
// <copyright file="InvoiceLine.cs" company="Multinerd">
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
	/// <summary> InvoiceLine model class.</summary>
	public partial class InvoiceLine : BindableBase, IEditableObject
    {
	
        /// <summary>Gets or sets the InvoiceLineId property</summary>
		[Key, Required]
		[DisplayName("Invoice Line Id"), Display(Name = "Invoice Line Id")]
		public int InvoiceLineId
        {
            get { return this._InvoiceLineId; }
			set { this.SetProperty(ref this._InvoiceLineId, value); }
        }

		/// <summary> InvoiceLineId backing field</summary>
        private int _InvoiceLineId;


        /// <summary>Gets or sets the InvoiceId property</summary>
		[Required]
		[DisplayName("Invoice Id"), Display(Name = "Invoice Id")]
		public int InvoiceId
        {
            get { return this._InvoiceId; }
			set { this.SetProperty(ref this._InvoiceId, value); }
        }

		/// <summary> InvoiceId backing field</summary>
        private int _InvoiceId;


        /// <summary>Gets or sets the TrackId property</summary>
		[Required]
		[DisplayName("Track Id"), Display(Name = "Track Id")]
		public int TrackId
        {
            get { return this._TrackId; }
			set { this.SetProperty(ref this._TrackId, value); }
        }

		/// <summary> TrackId backing field</summary>
        private int _TrackId;


        /// <summary>Gets or sets the UnitPrice property</summary>
		[Required]
		[DisplayName("Unit Price"), Display(Name = "Unit Price")]
		public decimal UnitPrice
        {
            get { return this._UnitPrice; }
			set { this.SetProperty(ref this._UnitPrice, value); }
        }

		/// <summary> UnitPrice backing field</summary>
        private decimal _UnitPrice;


        /// <summary>Gets or sets the Quantity property</summary>
		[Required]
		[DisplayName("Quantity"), Display(Name = "Quantity")]
		public int Quantity
        {
            get { return this._Quantity; }
			set { this.SetProperty(ref this._Quantity, value); }
        }

		/// <summary> Quantity backing field</summary>
        private int _Quantity;


		/// <summary> Gets or sets the Invoice navigation property</summary>
		public virtual Invoice Invoice { get; set; }

		/// <summary> Gets or sets the Track navigation property</summary>
		public virtual Track Track { get; set; }

	        
		#region IEditableObject

		private InvoiceLine_Backup _InvoiceLine_Backup;

		private struct InvoiceLine_Backup 
		{
			internal int _InvoiceLineId;
			internal int _InvoiceId;
			internal int _TrackId;
			internal decimal _UnitPrice;
			internal int _Quantity;
		}

        public void BeginEdit()
        {
			_InvoiceLine_Backup = new InvoiceLine_Backup() 
			{
				_InvoiceLineId = InvoiceLineId,
				_InvoiceId = InvoiceId,
				_TrackId = TrackId,
				_UnitPrice = UnitPrice,
				_Quantity = Quantity,
			};
		}

		public void CancelEdit()
        {
			InvoiceLineId = _InvoiceLine_Backup._InvoiceLineId;
			InvoiceId = _InvoiceLine_Backup._InvoiceId;
			TrackId = _InvoiceLine_Backup._TrackId;
			UnitPrice = _InvoiceLine_Backup._UnitPrice;
			Quantity = _InvoiceLine_Backup._Quantity;
        }

        public void EndEdit()
        {
			_InvoiceLine_Backup._InvoiceLineId = InvoiceLineId;
			_InvoiceLine_Backup._InvoiceId = InvoiceId;
			_InvoiceLine_Backup._TrackId = TrackId;
			_InvoiceLine_Backup._UnitPrice = UnitPrice;
			_InvoiceLine_Backup._Quantity = Quantity;
        }

		#endregion
 
		#region SampleData

		public static InvoiceLine GetRandomInvoiceLine()
        {
            return new InvoiceLine()
            {
				InvoiceLineId = 37,
				InvoiceId = 19212,
				TrackId = 127072,
				Quantity = 107089
            };
        }

		public static IList<InvoiceLine> GetRandomInvoiceLines()
        {
			var list = new List<InvoiceLine>();
			
			list.Add(new InvoiceLine()
			{
				InvoiceLineId = 38,
				InvoiceId = 114974,
				TrackId = 95635,
				Quantity = 100598
			});

			list.Add(new InvoiceLine()
			{
				InvoiceLineId = 39,
				InvoiceId = 120703,
				TrackId = 168688,
				Quantity = 135176
			});

			list.Add(new InvoiceLine()
			{
				InvoiceLineId = 40,
				InvoiceId = 29548,
				TrackId = 54162,
				Quantity = 69661
			});

			list.Add(new InvoiceLine()
			{
				InvoiceLineId = 41,
				InvoiceId = 1373,
				TrackId = 113509,
				Quantity = 162347
			});

			list.Add(new InvoiceLine()
			{
				InvoiceLineId = 42,
				InvoiceId = 44665,
				TrackId = 42413,
				Quantity = 20916
			});

			return list;
        }

		#endregion
 

	} 
} 

