using System;

namespace DeanCC.GUI
{
    public sealed class PatrolPatternNameChangedEventArgs : EventArgs
    {
        public PatrolPatternNameChangedEventArgs(string key , string newName)
        {
            Key = key;
            NewName = newName;
        }
        public string Key { get; private set; }
        public string NewName { get; set; }
    }
}
