using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyLearn.Interfaces;
using Xamarin.Forms;
using System.Threading.Tasks;
using EasyLearn.Models;

[assembly: Dependency(typeof(EasyLearn.iOS.FileWorker))]
namespace EasyLearn.iOS
{
    public class FileWorker : IFileWorker
    {
        public Task DeleteAsync(string filename)
        {
            File.Delete(GetFilePath(filename));
            return Task.FromResult(true);
        }

        public Task<bool> ExistsAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            bool exists = System.IO.File.Exists(filepath);
            return Task<bool>.FromResult(exists);
        }

        public Task<IEnumerable<string>> GetFilesAsync()
        {
            IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(GetDocsPath())
                                            select Path.GetFileName(filepath);
            return Task<IEnumerable<string>>.FromResult(filenames);
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            using (StreamReader reader = System.IO.File.OpenText(filepath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task SaveTextAsync(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            using (StreamWriter writer = System.IO.File.CreateText(filepath))
            {
                await writer.WriteAsync(text);
            }
        }

        string GetFilePath(string filename)
        {
            return Path.Combine(GetDocsPath(), filename);
        }
        string GetDocsPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        public Task<FileData> GetFileInfoAsync(string path = null)
        {
            throw new NotImplementedException();
        }
    }
}