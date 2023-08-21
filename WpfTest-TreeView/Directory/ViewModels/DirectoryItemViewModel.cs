using System.Linq;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using WpfTest_TreeView;

namespace WpfTest_TreeView
{
    /// <summary>
    /// a view model for each directory item
    /// </summary>
    public class DirectoryItemViewModel:BaseViewModel
    {

        #region public properties

        /// <summary>
        /// the type of this item
        /// </summary>
        public DirectoryItemType Type { get; set; }

        /// <summary>
        /// the full path to the item
        /// </summary>
        public string FullPath { get; set; }

        /// <summary>
        /// the name of this directory item
        /// </summary>
        public string Name
        {
            get
            {
                return Type == DirectoryItemType.Drive
                    ? FullPath
                    : DirectoryStructure.GetFileFolderName(FullPath);
            }
        }

        /// <summary>
        /// a list of all children contained inside this item
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel>  Children{ get; set; }

        /// <summary>
        /// indicates if this item can be expanded
        /// </summary>
        public bool CanExpanded { get { return Type != DirectoryItemType.Folder; } }

        /// <summary>
        /// indicates if the current item is expanded or not
        /// </summary>
        public bool IsExpanded
        {
            get
            {
                return Children?.Count(f => f != null) > 0;
            }
            set
            {
                //if the ui tells us to expand...
                if (value == true)
                    Expand();
                //if the ui tells us to close
                else
                    ClearChildren();
            }
        }

        #endregion

        #region public commands

        /// <summary>
        /// the command to expand this item
        /// </summary>
        public ICommand ExpandCommand { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="fullPath">the full path of this item</param>
        /// <param name="type">the type of this item</param>
        public DirectoryItemViewModel(string fullPath,DirectoryItemType type)
        {
            //create a command
            ExpandCommand = new RelayCommand(Expand);

            //set path and type
            FullPath = fullPath;
            Type = type;

            //setup children as needed
            ClearChildren();
        }

        #endregion

        #region help methods

        /// <summary>
        /// removes all children from the list,adding a dummy item to show the
        /// expand icon if required
        /// </summary>
        private void ClearChildren()
        {
            //clear items
            Children = new ObservableCollection<DirectoryItemViewModel>();

            //show the expand arrow if we are not a file
            if(Type!= DirectoryItemType.File)
                Children.Add(null);
        }

        #endregion

        /// <summary>
        /// expands this directory and finds all children
        /// </summary>
        private void Expand()
        {
            //we cannot expand a file
            if (Type == DirectoryItemType.File) return;

            //find all children
            Children = new ObservableCollection<DirectoryItemViewModel>
                (DirectoryStructure.GetDirectoryContents(FullPath)
                .Select(s =>
                new DirectoryItemViewModel(s.FullPath, s.Type)));
        }
    }
}
