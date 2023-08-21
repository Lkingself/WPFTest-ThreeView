using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WpfTest_TreeView
{
    /// <summary>
    /// a helper class to query information about directories
    /// </summary>
    public static class DirectoryStructure
    {
        /// <summary>
        /// get all logical drives on the computer
        /// </summary>
        /// <returns></returns>
        public static List<DirectoryItem> GetLogicalDrives()
        {
            //get every logical drive on the machine
            return Directory.GetLogicalDrives()
                .Select(s =>
            new DirectoryItem { FullPath = s, Type = DirectoryItemType.Drive })
                .ToList();
        }

        /// <summary>
        /// get directories top-level content
        /// </summary>
        /// <param name="fullPath">the full path to the directory</param>
        /// <returns></returns>
        public static List<DirectoryItem> GetDirectoryContents(string fullPath)
        {
            //create a empty list
            var items = new List<DirectoryItem>();

            #region get folders

            //try and get directories from the folder
            //ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    items.AddRange(dirs.Select(s=>
                    new DirectoryItem { FullPath=s,Type=DirectoryItemType.Folder}));
            }
            catch { }

            #endregion

            #region get files

            //try and get files from the folder
            //ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    items.AddRange(fs.Select(s=>
                    new DirectoryItem { FullPath=s,Type=DirectoryItemType.File}));
            }
            catch { }

            #endregion

            return items;
        }

        #region helpers
        /// <summary>
        /// Find the file or folder name from a full path
        /// </summary>
        /// <param name="directoryPath">the full path</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string GetFileFolderName(string directoryPath)
        {
            //if we have no path, return empty
            if (string.IsNullOrEmpty(directoryPath))
                return string.Empty;

            //make all slashes back slashes
            var normalizedPath = directoryPath.Replace('/', '\\');

            //find the last backslash in the path
            var lastIndex = normalizedPath.LastIndexOf('\\');

            //if we don't find a backslash,return the path itself
            if (lastIndex < 0) return directoryPath;

            //return the name after the last back slash
            return directoryPath.Substring(lastIndex + 1);
        }
        #endregion
    }
}
