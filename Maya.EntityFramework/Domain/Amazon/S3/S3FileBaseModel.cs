using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Maya.EntityFramework.Domain.Amazon.S3
{
    /// <summary> 
    /// S3FileBase model class.
    /// </summary>
    public class S3FileBaseModel
    {
        /// <summary>
        /// Gets or sets the Id property
        /// </summary>
        [Key, Required]
        [DisplayName("Id"), Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the Filename property
        /// </summary>
        [StringLength(100)]
        [DisplayName("Filename"), Display(Name = "Filename")]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the S3Key property
        /// </summary>
        [StringLength(100)]
        [DisplayName("S3 Key"), Display(Name = "S3 Key")]
        public string S3Key { get; set; }

        /// <summary>
        /// Gets or sets the S3URL property
        /// </summary>
        [DisplayName("S3 URL"), Display(Name = "S3 URL")]
        public string S3Url { get; set; }
    }
}
