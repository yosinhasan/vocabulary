using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyLearn.Interfaces;
using Xamarin.Forms;
using System.IO;
using EasyLearn.Models;
using EasyLearn.Helpers;

[assembly: Dependency(typeof(EasyLearn.Droid.FileWorker))]
namespace EasyLearn.Droid
{
    public class FileWorker : IFileWorker
    {
        private static string DirectoryPath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, Constants.FOLDER);

        public FileWorker()
        {
            checkFolder();
        }
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
            IEnumerable<string> filenames = from filepath in Directory.EnumerateFiles(DirectoryPath)
                                            select Path.GetFileName(filepath);
            return Task<IEnumerable<string>>.FromResult(filenames);
        }

        public async Task<string> LoadTextAsync(string filename)
        {
            string filepath = GetFilePath(filename);
            using (StreamReader reader = File.OpenText(filepath))
            {
                return await reader.ReadToEndAsync();
            }
        }

        public async Task SaveTextAsync(string filename, string text)
        {
            string filepath = GetFilePath(filename);
            using (StreamWriter writer = File.CreateText(filepath))
            {
                await writer.WriteAsync(text);
            }
        }
        private string GetFilePath(string filename)
        {
            return Path.Combine(DirectoryPath, filename);
        }
        private void checkFolder()
        {
            try
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            catch (Exception ex)
            {

            }

        }

        public Task<FileData> GetFileInfoAsync(string path = null)
        {
            IList<FileSystemInfo> visibleThings = new List<FileSystemInfo>();
            try
            {
                var dir = new DirectoryInfo(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath);
                foreach (var item in dir.GetFileSystemInfos())
                {
                    visibleThings.Add(item);
                }
            }
            catch (Exception ex)
            {
                return null;
            }
            return null;
        }
    }
}