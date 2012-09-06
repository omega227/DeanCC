using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace DeanCCCore.Core
{
    /// <summary>
    /// データバインディングをサポートするフィルター・ソート可能なコレクションを提供します
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class BindingCollection<T> : BindingList<T>
    {
        public BindingCollection()
        {
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {
            originalItems = new List<T>();
        }

        [NonSerialized]
        private List<T> originalItems = new List<T>();
        [NonSerialized]
        private bool filtered;
        [NonSerialized]
        private Func<T, bool> filterPredicate;

        /// <summary>
        /// フィルターを実行する述語を取得・設定します
        /// </summary>
        public Func<T, bool> FilterPredicate
        {
            get { return filterPredicate; }
            set
            {
                if (filterPredicate == null ||
                    (value != null && !filterPredicate.Equals(value)))
                {
                    filterPredicate = value;
                    OnFilterPredicateChanged();
                }
            }
        }

        /// <summary>
        /// 述語に基づいて内容をフィルター処理します
        /// 既にフィルターされている場合でもすべての内容をフィルター処理します
        /// </summary>
        /// <param name="predicate">条件を表す関数</param>
        public void Filter(Func<T, bool> predicate)
        {
            FilterPredicate = predicate;
        }

        private void OnFilterPredicateChanged()
        {
            if (!filtered)
            {
                originalItems.Clear();
                originalItems.AddRange(Items);
            }
            Items.Clear();
            ((List<T>)Items).AddRange(originalItems.Where(filterPredicate));
            filtered = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
        }

        /// <summary>
        /// フィルターを解除します
        /// </summary>
        public void ClearFilter()
        {
            if (filtered)
            {
                Items.Clear();
                ((List<T>)Items).AddRange(originalItems);
                filtered = false;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, 0));
            }
        }

        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            Comparer<T> comparer = new Comparer<T>(prop, direction);
            List<T> result = Items.OrderBy(x => x, comparer).ToList();
            Items.Clear();
            ((List<T>)Items).AddRange(result);
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemMoved, prop));
        }
    }

    public sealed class Comparer<T> : IComparer<T>
    {
        //ソートの向き（昇順／降順）
        private ListSortDirection direction;
        //ソート項目
        private PropertyDescriptor property;

        public Comparer(PropertyDescriptor prop, ListSortDirection direction)
        {
            this.property = prop;
            this.direction = direction;
        }

        //同値の場合ゼロを返します。
        public int Compare(T objX, T objY)
        {
            // 比較対象のオブジェクトからクリックしたプロパティを取得します。
            object valX = this.GetPropValue(objX, this.property.Name);
            object valY = this.GetPropValue(objY, this.property.Name);

            //directionの値（昇順／降順）に応じて取得した値を比較します。
            if ((this.direction == ListSortDirection.Ascending))
            {
                return this.CompareAsc(valX, valY);
            }
            else
            {
                return this.CompareDesc(valX, valY);
            }

        }

        //昇順で比較を行います。
        private int CompareAsc(object valX, object valY)
        {
            if (valX == null && valY == null)
            {
                return 0;
            }
            else if (valX == null)
            {
                return 1;
            }
            else if (valY == null)
            {
                return -1;
            }
            else if (valX is int && valY is int)
            {
                return ((int)valX).CompareTo((int)valY);
            }
            else if (valX is float && valY is float)
            {
                return ((float)valX).CompareTo((float)valY);
            }
            else if (valX is DateTime && valY is DateTime)
            {
                return ((DateTime)valX).CompareTo((DateTime)valY);
            }
            else
            {
                return valX.ToString().CompareTo(valY.ToString());
            }
        }

        //降順で比較を行います。
        private int CompareDesc(object valX, object valY)
        {
            return (this.CompareAsc(valX, valY) * -1);
        }

        //プロパティ値を取得します。
        private object GetPropValue(T val, string prop)
        {
            PropertyInfo propInfo = val.GetType().GetProperty(prop);
            return propInfo.GetValue(val, null);
        }
    }
}
