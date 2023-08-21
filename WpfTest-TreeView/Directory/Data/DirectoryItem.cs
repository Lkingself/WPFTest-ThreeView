namespace WpfTest_TreeView
{
    /// <summary>
    /// information about a directory item such as a drive,a folder or a file
    /// </summary>
    public class DirectoryItem
    {
        /// <summary>
        /// the type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// the absolute path to this item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// the name of this directory item
        /// </summary>
        public string Name { 
            get { return Type==DirectoryItemType.Drive
                    ? FullPath
                    : DirectoryStructure.GetFileFolderName(FullPath); }}
    }
}
