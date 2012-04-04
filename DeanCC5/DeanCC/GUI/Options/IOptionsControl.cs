using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeanCCCore.Core;

namespace DeanCC.GUI.Options
{
    public interface IOptionsControl
    {
        //string Name { get; }
        /// <summary>
        /// 現在の状態を指定した設定に反映させます
        /// </summary>
        /// <param name="target">反映先の設定</param>
        /// <exception cref="System.ArgumentException">ユーザーの入力した値に不正なものが含まれています</exception>
        void Get(DeanCCCore.Core.Options.OptionItems destination);
        /// <summary>
        /// 指定した設定の状態を読み込みます
        /// </summary>
        /// <param name="source">指定する設定</param>
        void Set(DeanCCCore.Core.Options.OptionItems source);
        void Reset();
        /// <summary>
        /// 一度読み込まれたかどうかを示す値
        /// </summary>
        bool Loaded { get; }
    }
}
