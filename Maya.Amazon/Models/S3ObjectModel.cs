using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maya.Amazon.Models
{
    /// <summary>
    /// A S3 helper model.
    /// </summary>
    public class S3ObjectModel
    {
        /// <summary> The name of the bucket. </summary>
        public string Bucket { get; private set; }

        /// <summary> The key where your file resides. </summary>
        public string Key { get; private set; }

        /// <summary> Convenience URL property. </summary>
        public string Url => $"https://s3.amazonaws.com/{Bucket}/{Key}";

        /// <summary> Create a S3ObjectModel. </summary>
        /// <param name="bucket"> The bucket. </param>
        /// <param name="key"> The key. </param>
        public S3ObjectModel(string bucket, string key)
        {
            Bucket = bucket;
            Key = key;
        }
    }
}
