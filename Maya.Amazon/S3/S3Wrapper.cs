﻿using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Maya.Amazon.Helpers;
using Maya.Amazon.Models;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Maya.Amazon.S3
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// var asd = Task.Run(async () =>
    /// {
    ///    var upload = await s3.Upload("c.txt", "D:\\Reset Windows Login.txt");
    ///    Console.WriteLine(upload);
    ///    await Task.Delay(100);
    ///
    ///    var download = await s3.Download("c.txt", "D:\\Reset Windows Login22.txt");
    ///    Console.WriteLine(download);
    ///    await Task.Delay(100);
    ///
    ///    var delete = await s3.Delete("c.txt");
    ///    Console.WriteLine(delete);
    /// });
    /// </example>
    public class S3Wrapper
    {
        private const string AwsPrefix = "https://s3.amazonaws.com";
        private readonly string _bucketName;

        /// <summary>
        /// The S3 client provided by Amazon.
        /// </summary>
        public readonly IAmazonS3 Client;

        /// <summary>
        /// Create a S3 wrapper to upload, download and delete files.
        /// </summary>
        /// <param name="bucket"> The bucket to use. </param>
        /// <param name="accessKey"> Your access key. </param>
        /// <param name="secretKey"> Your secret key. </param>
        public S3Wrapper(string bucket, string accessKey, string secretKey)
        {
            _bucketName = bucket;
            Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);
        }

        /// <summary>
        /// Upload a file to AWS S3.
        /// </summary>
        /// <param name="key"> The key to use when uploading, relative to the bucket. </param>
        /// <param name="sourceFilePath"> The file path. </param>
        /// <returns> Returns AWS Response. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<S3ObjectModel> Upload(string key, string sourceFilePath)
        {
            #region Null Checks

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (sourceFilePath == null)
                throw new ArgumentNullException(nameof(sourceFilePath));

            #endregion

            var obj = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                FilePath = sourceFilePath,
                CannedACL = S3CannedACL.PublicRead,
                ContentType = MimeTypeMap.GetMimeType(Path.GetExtension(sourceFilePath))
            };

            await Client.PutObjectAsync(obj).ConfigureAwait(false);

            var model = new S3ObjectModel(_bucketName, key);
            return model;
        }

        /// <summary>
        /// Upload a file to AWS S3.
        /// </summary>
        /// <param name="key"> The key to use when uploading, relative to the bucket. </param>
        /// <param name="sourceStream"> The file stream. </param>
        /// <returns> Returns AWS Response. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<S3ObjectModel> Upload(string key, Stream sourceStream)
        {
            #region Null Checks

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (sourceStream == null)
                throw new ArgumentNullException(nameof(sourceStream));

            #endregion

            var obj = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
                InputStream = sourceStream,
                CannedACL = S3CannedACL.PublicRead,
                ContentType = MimeTypeMap.GetMimeType(Path.GetExtension(key.Split('/').Last()))
            };

            await Client.PutObjectAsync(obj).ConfigureAwait(false);

            var model = new S3ObjectModel(_bucketName, key);
            return model;
        }

        /// <summary>
        /// Download a file from AWS S3.
        /// </summary>
        /// <param name="key"> The key to use when downloading, relative to the bucket. </param>
        /// <param name="destinationFilePath"> The destination to save the file to. </param>
        /// <returns> Returns AWS Response. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<GetObjectResponse> Download(string key, string destinationFilePath = null)
        {
            #region Null Checks

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            #endregion

            var obj = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = key,
            };

            var myObj = await Client.GetObjectAsync(obj);

            if (!string.IsNullOrEmpty(destinationFilePath))
                await myObj.WriteResponseStreamToFileAsync(destinationFilePath, false, CancellationToken.None);

            return myObj;
        }

        /// <summary>
        /// Delete a file from AWS S3.
        /// </summary>
        /// <param name="key"> The key to use when deleting, relative to the bucket. </param>
        /// <returns> Returns AWS Response. </returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<DeleteObjectResponse> Delete(string key)
        {
            #region Null Checks

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            #endregion

            var obj = new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = key
            };

            return await Client.DeleteObjectAsync(obj).ConfigureAwait(false);
        }







        //private IEnumerable<string> PopulateFoldersForKeyPath(string path, string diskLoc)
        //{
        //    var fileNames = new List<string>();

        //    var listRequest = new ListObjectsRequest
        //    {
        //        BucketName = _bucketName,
        //        Prefix = path,
        //        Delimiter = "/"
        //    };

        //    var listResponse = S3Client.ListObjects(listRequest);
        //    do
        //    {
        //        foreach (var subFolder in listResponse.CommonPrefixes)
        //        {

        //        }

        //        foreach (var file in listResponse.S3Objects)
        //        {
        //            fileNames.Add(file.Key);
        //        }

        //        listRequest.Marker = listResponse.NextMarker;
        //    } while (listResponse.IsTruncated);

        //    return fileNames;
        //}


        //private void CreateDirectoryForPath(string path)
        //{
        //    //if (Directory.Exists(path)) return;

        //    //try
        //    //{
        //    //    Directory.CreateDirectory(path);
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    ShowErrorAlert(ex);
        //    //}
        //}


        //private void CreateFileForPath(S3Object file, string diskLoc)
        //{
        //    //var filePath = Path.Combine(diskLoc,
        //    //    file.Key.Replace($"{SelectedProject.Originuser}/{SelectedProject.Project}/", string.Empty).Replace(@"/", @"\"));
        //    //if (File.Exists(filePath))
        //    //{
        //    //    Debug.WriteLine("fileExist: " + filePath);
        //    //    return;
        //    //}

        //    //S3Download(file.Key, filePath, false);
        //}


        //private void syncMyFiles(object state)
        //{
        //    createDirectoryForPath(_diskLocation);
        //    populateFoldersForKeyPath(_s3KeyRootFolder);
        //}

    }
}
