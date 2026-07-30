using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Case.Subs.KeyMatcher.ViewModels
{
  public class clsViewModelBase : INotifyPropertyChanged
  {

    /// <summary>
    /// Event handler for updated values
    /// </summary>
    public event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    /// Setter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storage"></param>
    /// <param name="value"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
    {
      if (Equals(storage, value))
      {
        return false;
      }

      storage = value;
      OnPropertyChanged(propertyName);
      return true;
    }

    /// <summary>
    /// Listener
    /// </summary>
    /// <param name="propertyName"></param>
    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler m_eventHandler = this.PropertyChanged;
      if (m_eventHandler != null)
      {
        m_eventHandler(this, new PropertyChangedEventArgs(propertyName));
      }
    }
    
  }
}