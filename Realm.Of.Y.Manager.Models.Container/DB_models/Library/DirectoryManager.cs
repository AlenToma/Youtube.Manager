using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Realm.Of.Y.Manager.Models.Container
{
    public class DirectoryManager
    {
        public string DirectoryPath { get; private set; }

        public DirectoryInfo DirectoryInfo { get; private set; }

        public bool Exist { get => Directory.Exists(DirectoryPath); }

        public DirectoryManager(params string[] directory)
        {
            DirectoryPath = Path.Combine(directory);
            DirectoryInfo = new DirectoryInfo(DirectoryPath);
        }


        public void Delete()
        {
            if (Exist)
            {
                foreach (var file in GetFiles(SearchOption.AllDirectories))
                    file.File.Delete();
                DirectoryInfo.Delete(true);

            }
        }

        public List<DirectoryInfo> GetDirectories(string search = null)
        {
            return Exist ? DirectoryInfo.GetDirectories(search ?? "*").ToList() : new List<DirectoryInfo>();
        }

        /// <summary>
        /// Folder withing this directory 
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public DirectoryManager Folder(string folderName)
        {
            if (this.DirectoryPath.EndsWith(folderName))
                return this;
            return new DirectoryManager(Path.Combine(DirectoryPath, folderName));
        }

        public DirectoryManager ReName(string name)
        {

            if (!Exist)
                return this;
            var doc = new DirectoryManager(Path.Combine(this.DirectoryInfo.Parent.FullName, name)).Create();
            if (doc.Exist)
                foreach (var file in GetFiles(SearchOption.AllDirectories))
                {
                    file.MovtoDirectory(doc.DirectoryPath);
                }

            this.Delete(); // delete the current directory

            return this;

        }

        /// <summary>
        /// Create folder if it dose not exist
        /// </summary>
        public DirectoryManager Create()
        {
            if (!Exist)
                DirectoryInfo.Create();

            return this;
        }

        public List<FileInfoProp> GetFiles(SearchOption searchOption = SearchOption.TopDirectoryOnly, params string[] values)
        {
            string searchString = values.Any() ? string.Join(",", values.Select(r => $"*{r}*")) : "*.*";
            if (!Exist)
                return new List<FileInfoProp>();
            return DirectoryInfo.EnumerateFiles(searchString, searchOption).Select(x => new FileInfoProp(x)).ToList();
        }

        public FileInfoProp GetFile(string file, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return GetFiles(searchOption, file).FirstOrDefault();
        }



    }
}
