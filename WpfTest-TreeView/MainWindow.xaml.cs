using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace WpfTest_TreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region OnLoaded
        /// <summary>
        /// When the application first opens
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //get every logical drive on the machine
            foreach(var drive in Directory.GetLogicalDrives())
            {
                //create a new item for it
                var item = new TreeViewItem();
                //set the header and path
                item.Header = drive;

                //add the full path
                item.Tag = drive;
                
                //add the dummy item
                item.Items.Add(null);
                
                //Listen out for item being expanded
                item.Expanded += Folder_Expanded;

                //add it to the main tree view
                FolderView.Items.Add(item);
            }
        }
        #endregion

        #region folder expanded
        /// <summary>
        /// when a folder is expanded,find the sub folders/files
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Folder_Expanded(object sender, RoutedEventArgs e)
        {
            #region initial checks
            var item = (TreeViewItem)sender;

            //if the item only contains the dummy data
            if (item.Items.Count != 1 || item.Items[0] != null)
                return;

            //clear dummy data
            item.Items.Clear();

            //get full path
            var fullPath = (string)item.Tag;
            #endregion

            #region get folders
            
            //create a blank list for directories
            var directories = new List<string>();

            //try and get directories from the folder
            //ignoring any issues doing so
            try
            {
                var dirs = Directory.GetDirectories(fullPath);
                
                if (dirs.Length > 0)
                    directories.AddRange(dirs);
            }
            catch { }

            //for each directory
            directories.ForEach(directoryPath =>
            {
                //create directory item
                var subItem = new TreeViewItem()
                {
                    //set header as folder name
                    Header=GetFileFolderName(directoryPath),
                    //and tag as full path
                    Tag=directoryPath
                };
                //add dummy item so we can expand folder
                subItem.Items.Add(null);

                //handle expanding
                subItem.Expanded += Folder_Expanded;

                //add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion

            #region get files

            //create a blank list for files
            var files = new List<string>();

            //try and get files from the folder
            //ignoring any issues doing so
            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    files.AddRange(fs);
            }
            catch { }

            //for each file
            files.ForEach(filePath =>
            {
                //create file item
                var subItem = new TreeViewItem()
                {
                    //set header as file name
                    Header = GetFileFolderName(filePath),
                    //and tag as full path
                    Tag = filePath
                };

                //add this item to the parent
                item.Items.Add(subItem);
            });

            #endregion
        }
        #endregion

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
            if(string.IsNullOrEmpty(directoryPath))
                return string.Empty;

            //make all slashes back slashes
            var normalizedPath = directoryPath.Replace('/', '\\');

            //find the last backslash in the path
            var lastIndex=normalizedPath.LastIndexOf('\\');

            //if we don't find a backslash,return the path itself
            if (lastIndex < 0) return directoryPath;

            //return the name after the last back slash
            return directoryPath.Substring(lastIndex + 1);
        }
        #endregion
    }
}
