using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;

namespace Maya.Amazon.S3
{
    //public class AWSS3Wrapper
    //{
    //    public static readonly IAmazonS3 S3Client = new AmazonS3Client(AccessKey, SecretKey, RegionEndpoint.USEast1);
    //    public static string AWSPrefix => "https://s3.amazonaws.com";
    //    public static string BucketName { get; set; }
    //    public static string AccessKey { get; set; }
    //    public static string SecretKey { get; set; }

    //    public AWSS3Wrapper()
    //    {
            
    //    }

    //    private IEnumerable<string> PopulateFoldersForKeyPath(string path, string diskLoc)
    //    {
    //        var fileNames = new List<string>();

    //        var listRequest = new ListObjectsRequest
    //        {
    //            BucketName = AWSS3Wrapper.BucketName,
    //            Prefix = path,
    //            Delimiter = "/"
    //        };

    //        var listResponse = S3Client.ListObjects(listRequest);
    //        do
    //        {
    //            foreach (var subFolder in listResponse.CommonPrefixes)
    //            {

    //            }

    //            foreach (var file in listResponse.S3Objects)
    //            {
    //                fileNames.Add(file.Key);
    //            }

    //            listRequest.Marker = listResponse.NextMarker;
    //        } while (listResponse.IsTruncated);

    //        return fileNames;
    //    }


    //    private void CreateDirectoryForPath(string path)
    //    {
    //        //if (Directory.Exists(path)) return;

    //        //try
    //        //{
    //        //    Directory.CreateDirectory(path);
    //        //}
    //        //catch (Exception ex)
    //        //{
    //        //    ShowErrorAlert(ex);
    //        //}
    //    }


    //    private void CreateFileForPath(S3Object file, string diskLoc)
    //    {
    //        //var filePath = Path.Combine(diskLoc,
    //        //    file.Key.Replace($"{SelectedProject.Originuser}/{SelectedProject.Project}/", string.Empty).Replace(@"/", @"\"));
    //        //if (File.Exists(filePath))
    //        //{
    //        //    Debug.WriteLine("fileExist: " + filePath);
    //        //    return;
    //        //}

    //        //S3Download(file.Key, filePath, false);
    //    }


    //    //private void syncMyFiles(object state)
    //    //{
    //    //    createDirectoryForPath(_diskLocation);
    //    //    populateFoldersForKeyPath(_s3KeyRootFolder);
    //    //}


    //    public static Task S3DownloadAsync(string key, string filePath, string fileName, bool showFolder)
    //    {
    //        return Task.Factory.StartNew(() => S3Download(key, filePath, fileName, showFolder));
    //    }

    //    public static void S3Download(string key, string tempFolder, string fileName, bool showFolder)
    //    {
    //        var awsKey = key.Replace($"{AWSPrefix}/{BucketName}/", string.Empty);
    //        var file = Path.Combine(Path.GetTempPath(), tempFolder, awsKey, fileName);

    //        if (File.Exists(file))
    //            File.Delete(file);

    //        S3Client.GetObject(new GetObjectRequest
    //        {
    //            BucketName = BucketName,
    //            Key = awsKey
    //        }).WriteResponseStreamToFile(file);

    //        if (showFolder)
    //        {
    //            var directory = Path.GetDirectoryName(file);
    //            if (directory != null)
    //                Process.Start(directory);
    //        }
    //    }


    //    public static Task<string> S3UploadAsync(string key, string sourceFilePath)
    //    {
    //        return Task.Factory.StartNew(() => S3Upload(key, sourceFilePath));
    //    }

    //    public static string S3Upload(string key, string sourceFilePath)
    //    {
    //        var awsKey = $"{key}{Path.GetFileName(sourceFilePath)}";
    //        S3Client.PutObject(new PutObjectRequest
    //        {
    //            BucketName = BucketName,
    //            Key = awsKey,
    //            FilePath = sourceFilePath,
    //            CannedACL = S3CannedACL.PublicRead
    //        });
    //        return $"{AWSPrefix}/{BucketName}/{awsKey}";
    //    }




    //    public static Task S3DeleteAsync(string key)
    //    {
    //        return Task.Factory.StartNew(() => S3Delete(key));
    //    }

    //    public static void S3Delete(string key)
    //    {
    //        var awsKey = key.Replace($"{AWSPrefix}/{BucketName}/", string.Empty);
    //        S3Client.DeleteObject(new DeleteObjectRequest
    //        {
    //            BucketName = BucketName,
    //            Key = awsKey
    //        });
    //    }







    //    public static string GetFileSize(double len)
    //    {
    //        string[] sizes = { "B", "KB", "MB", "GB" };
    //        var order = 0;
    //        while (len >= 1024 && order + 1 < sizes.Length)
    //        {
    //            order++;
    //            len = len / 1024;
    //        }

    //        return $"{len:0.##} {sizes[order]}";
    //    }
    //}
}
