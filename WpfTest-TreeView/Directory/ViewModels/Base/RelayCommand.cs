using System;
using System.Windows.Input;

namespace WpfTest_TreeView
{
    /// <summary>
    /// a basic command to run an action
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region private members

        /// <summary>
        /// the action to run
        /// </summary>
        private Action mAction;

        #endregion

        #region public events

        /// <summary>
        /// the event that's fired when the <see cref="CanExecute(object?)"/>
        /// value has changed
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        #endregion

        #region constructor

        /// <summary>
        /// default constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion

        #region command methods

        /// <summary>
        /// a relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <summary>
        /// executes the commands action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object? parameter)
        {
            mAction();
        }

        #endregion
    }
}
