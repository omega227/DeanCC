using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeanCCCore.Core._2ch
{
    [Serializable]
    public sealed class Category : ICategory
    {
        private BoardInfoCollection children = new BoardInfoCollection();
        private bool isExpanded = false;
        private string name = string.Empty;

        public Category()
        {
        }

        public Category(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            this.name = name;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public BoardInfoCollection Children
        {
            get
            {
                return this.children;
            }
        }

        public int Count
        {
            get
            {
                return this.children.Count;
            }
        }

        public bool IsExpanded
        {
            get
            {
                return this.isExpanded;
            }
            set
            {
                if (value != this.isExpanded)
                {
                    this.isExpanded = value;
                }
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("Name");
                }
                this.name = value;
            }
        }

        public IEnumerator<IBoardInfo> GetEnumerator()
        {
            return children.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return children.GetEnumerator();
        }
    }
}
