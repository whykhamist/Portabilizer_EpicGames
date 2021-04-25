using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Settings.Core
{
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// litle helper to fire OnPropertyChanged event
        /// </summary>
        /// <typeparam name="T">Making sure the values are comparable</typeparam>
        /// <param name="field">the field that is compared</param>
        /// <param name="value">the value that is assigned</param>
        /// <param name="fieldName">the name of the Property</param>
        /// <remarks>Updates re-paints the form when needed</remarks>
        protected void Set<T>(ref T field, T value, string fieldName = null)
        {
            if (field.Equals(value))
                return;
            field = value;
            OnPropertyChanged(fieldName);
        }
    }
}
