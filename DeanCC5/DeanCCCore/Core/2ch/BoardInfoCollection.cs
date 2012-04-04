using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    [Serializable]
    public sealed class BoardInfoCollection : System.Collections.ObjectModel.Collection<IBoardInfo>
    {
        public BoardInfoCollection()
        {
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(0x10 * base.Count);
            for (int i = 0; i < base.Count; i++)
            {
                builder.Append(base[i].Name);
                if ((i + 1) < base.Count)
                {
                    builder.Append("<>");
                }
            }
            return builder.ToString();
        }
    }
}