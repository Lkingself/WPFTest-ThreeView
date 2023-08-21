using System.ComponentModel;

namespace WpfTest_TreeView
{
    /// <summary>
    /// a base view model that fires property changed events as needed
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// the event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
