using System.Collections.ObjectModel;
using System.Linq;

namespace WpfTest_TreeView
{
    /// <summary>
    /// the view model for the applications main directory view
    /// </summary>
    public class DirectoryStructureViewModel
    {
        #region public properties

        /// <summary>
        /// a list of the directories on the machine
        /// </summary>
        public ObservableCollection<DirectoryItemViewModel> Items { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// default constructor
        /// </summary>
        public DirectoryStructureViewModel()
        {
            //get all logical drives
            var children = DirectoryStructure.GetLogicalDrives();

            //create the view models from the data
            Items = new ObservableCollection<DirectoryItemViewModel>(
                children.Select(s => new DirectoryItemViewModel(s.FullPath, s.Type)));
        }

        #endregion
    }
}
