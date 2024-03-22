using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _AbstractFactory._02Example.Storage
{
    // 存储服务接口
    public interface IStorageService
    {
        // 上传文件
        void UploadFile(string filePath);

        // 下载文件
        string DownloadFile(string fileId);
    }

    // AWS存储服务
    public class AWSStorageService : IStorageService
    {
        public void UploadFile(string filePath)
        {
            // 调用AWS存储服务的上传方法
            Console.WriteLine($"将{filePath}文件上传到AWS");
        }

        public string DownloadFile(string fileId)
        {
            // 调用AWS存储服务的下载方法
            return "从AWS下载文件";
        }
    }

    // Azure存储服务
    public class AzureStorageService : IStorageService
    {
        public void UploadFile(string filePath)
        {
            // 调用Azure存储服务的上传方法
            Console.WriteLine($"将{filePath}文件上传到 Azure Blob 存储");
        }

        public string DownloadFile(string fileId)
        {
            // 调用Azure存储服务的下载方法
            return "从 Azure Blob 下载文件";
        }
    }
}
