//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//
//
// <copyright file="Test_Result.cs." company="Multinerd">
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
	/// <summary> Test_Result model class.</summary>
	public partial class Test_Result : BindableBase, IEditableObject
    {
	
        /// <summary>Gets or sets the ArtistId property</summary>
		[Required]
		[DisplayName("Artist Id"), Display(Name = "Artist Id")]
		public int ArtistId
        {
            get { return this._ArtistId; }
			set { this.SetProperty(ref this._ArtistId, value); }
        }

		/// <summary> ArtistId backing field</summary>
        private int _ArtistId;


        /// <summary>Gets or sets the Name property</summary>
		[StringLength(120)]
		[DisplayName("Name"), Display(Name = "Name")]
		public string Name
        {
            get { return this._Name; }
			set { this.SetProperty(ref this._Name, value); }
        }

		/// <summary> Name backing field</summary>
        private string _Name;


	        
		#region IEditableObject

		private Test_Result_Backup _Test_Result_Backup;

		private struct Test_Result_Backup 
		{
			internal int _ArtistId;
			internal string _Name;
		}

        public void BeginEdit()
        {
			_Test_Result_Backup = new Test_Result_Backup() 
			{
				_ArtistId = ArtistId,
				_Name = Name,
			};
		}

		public void CancelEdit()
        {
			ArtistId = _Test_Result_Backup._ArtistId;
			Name = _Test_Result_Backup._Name;
        }

        public void EndEdit()
        {
			_Test_Result_Backup._ArtistId = ArtistId;
			_Test_Result_Backup._Name = Name;
        }

		#endregion
 
		#region SampleData

		public static Test_Result GetRandomTest_Result()
        {
            return new Test_Result()
            {
				ArtistId = 34752,
				Name = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
            };
        }

		public static IList<Test_Result> GetRandomTest_Results()
        {
			var list = new List<Test_Result>();
			
			list.Add(new Test_Result()
			{
				ArtistId = 328,
				Name = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
			});

			list.Add(new Test_Result()
			{
				ArtistId = 96804,
				Name = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
			});

			list.Add(new Test_Result()
			{
				ArtistId = 195338,
				Name = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
			});

			list.Add(new Test_Result()
			{
				ArtistId = 190320,
				Name = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
			});

			list.Add(new Test_Result()
			{
				ArtistId = 17028,
				Name = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX"
			});

			return list;
        }

		#endregion
 

	} 
} 

