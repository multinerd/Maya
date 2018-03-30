using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace Maya.Amazon.S3
{
    public class AWSS3Wrapper
    {
        private const string AwsPrefix = "https://s3.amazonaws.com";
        private readonly string _bucketName;


        /// <summary>
        /// 
        /// </summary>
        public readonly IAmazonS3 S3Client;

        public AWSS3Wrapper(string bucket, string accessKey, string secretKey)
        {
            _bucketName = bucket;
            S3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.USEast1);
        }

        private IEnumerable<string> PopulateFoldersForKeyPath(string path, string diskLoc)
        {
            var fileNames = new List<string>();

            var listRequest = new ListObjectsRequest
            {
                BucketName = _bucketName,
                Prefix = path,
                Delimiter = "/"
            };

            var listResponse = S3Client.ListObjects(listRequest);
            do
            {
                foreach (var subFolder in listResponse.CommonPrefixes)
                {

                }

                foreach (var file in listResponse.S3Objects)
                {
                    fileNames.Add(file.Key);
                }

                listRequest.Marker = listResponse.NextMarker;
            } while (listResponse.IsTruncated);

            return fileNames;
        }


        private void CreateDirectoryForPath(string path)
        {
            //if (Directory.Exists(path)) return;

            //try
            //{
            //    Directory.CreateDirectory(path);
            //}
            //catch (Exception ex)
            //{
            //    ShowErrorAlert(ex);
            //}
        }


        private void CreateFileForPath(S3Object file, string diskLoc)
        {
            //var filePath = Path.Combine(diskLoc,
            //    file.Key.Replace($"{SelectedProject.Originuser}/{SelectedProject.Project}/", string.Empty).Replace(@"/", @"\"));
            //if (File.Exists(filePath))
            //{
            //    Debug.WriteLine("fileExist: " + filePath);
            //    return;
            //}

            //S3Download(file.Key, filePath, false);
        }


        //private void syncMyFiles(object state)
        //{
        //    createDirectoryForPath(_diskLocation);
        //    populateFoldersForKeyPath(_s3KeyRootFolder);
        //}


        public Task S3DownloadAsync(string key, string filePath, string fileName, bool showFolder)
        {
            return Task.Factory.StartNew(() => S3Download(key, filePath, fileName, showFolder));
        }

        public void S3Download(string key, string tempFolder, string fileName, bool showFolder)
        {
            var awsKey = key.Replace($"{AwsPrefix}/{_bucketName}/", string.Empty);
            var file = Path.Combine(Path.GetTempPath(), tempFolder, awsKey, fileName);

            if (File.Exists(file))
                File.Delete(file);

            S3Client.GetObject(new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = awsKey
            }).WriteResponseStreamToFile(file);

            if (showFolder)
            {
                var directory = Path.GetDirectoryName(file);
                if (directory != null)
                    Process.Start(directory);
            }
        }


        public Task<string> S3UploadAsync(string key, string sourceFilePath)
        {
            return Task.Factory.StartNew(() => S3Upload(key, sourceFilePath));
        }

        public string S3Upload(string key, string sourceFilePath)
        {
            if (sourceFilePath == null) return null;

            var awsKey = Path.Combine(key, Path.GetFileName(sourceFilePath)).Replace("\\", "/");
            var contentType = Path.GetExtension(sourceFilePath).Equals(".plist")
                ? "text/plain"
                : "application/octet-stream";


            S3Client.PutObject(new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = awsKey,
                FilePath = sourceFilePath,
                CannedACL = S3CannedACL.PublicRead,
                ContentType = "text/plain",
            });
            return $"{AwsPrefix}/{_bucketName}/{awsKey}";
        }




        public Task S3DeleteAsync(string key)
        {
            return Task.Factory.StartNew(() => S3Delete(key));
        }

        public void S3Delete(string key)
        {
            var awsKey = key.Replace($"{AwsPrefix}/{_bucketName}/", string.Empty);
            S3Client.DeleteObject(new DeleteObjectRequest
            {
                BucketName = _bucketName,
                Key = awsKey
            });
        }







        public static string GetFileSize(double len)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            var order = 0;
            while (len >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                len = len / 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
