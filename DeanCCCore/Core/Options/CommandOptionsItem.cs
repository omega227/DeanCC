using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core.Options
{
    [Serializable]
    public sealed class CommandOptionsItem
    {
        public CommandOptionsItem()
        {
            CommandList = new CommandCollection();
        }
        public CommandCollection CommandList { get; set; }
    }
}
