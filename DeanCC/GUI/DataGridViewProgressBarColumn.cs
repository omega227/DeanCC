using System;
using System.Drawing;
using System.Windows.Forms;

namespace DeanCC.GUI
{
    /// <summary>
    /// プログレスバーを表示するDataGridViewColumn を表します
    /// </summary>
    public sealed class DataGridViewProgressBarColumn : DataGridViewTextBoxColumn
    {
        //コンストラクタ
        public DataGridViewProgressBarColumn()
        {
            this.CellTemplate = new DataGridViewProgressBarCell();
        }

        //CellTemplateの取得と設定
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                //DataGridViewProgressBarCell以外はホストしない
                if (!(value is DataGridViewProgressBarCell))
                {
                    throw new ArgumentException("DataGridViewProgressBarCell のみ設定できます");
                }
                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// ProgressBarの最大値
        /// </summary>
        public int Maximum
        {
            get
            {
                return ((DataGridViewProgressBarCell)this.CellTemplate).Maximum;
            }
            set
            {
                if (this.Maximum == value)
                {
                    return;
                }
                //セルテンプレートの値を変更する
                ((DataGridViewProgressBarCell)this.CellTemplate).Maximum = value;
                //DataGridViewにすでに追加されているセルの値を変更する
                if (this.DataGridView == null)
                {
                    return;
                }
                int rowCount = this.DataGridView.RowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    DataGridViewRow row = this.DataGridView.Rows.SharedRow(i);
                    ((DataGridViewProgressBarCell)row.Cells[this.Index]).Maximum = value;
                }
            }
        }

        /// <summary>
        /// ProgressBarの最小値
        /// </summary>
        public int Mimimum
        {
            get
            {
                return ((DataGridViewProgressBarCell)this.CellTemplate).Mimimum;
            }
            set
            {
                if (this.Mimimum == value)
                {
                    return;
                }
                //セルテンプレートの値を変更する
                ((DataGridViewProgressBarCell)this.CellTemplate).Mimimum = value;
                //DataGridViewにすでに追加されているセルの値を変更する
                if (this.DataGridView == null)
                {
                    return;
                }
                int rowCount = this.DataGridView.RowCount;
                for (int i = 0; i < rowCount; i++)
                {
                    DataGridViewRow row = this.DataGridView.Rows.SharedRow(i);
                    ((DataGridViewProgressBarCell)row.Cells[this.Index]).Mimimum = value;
                }
            }
        }
    }

    /// <summary>
    /// ProgressBarをDataGridViewに表示する
    /// </summary>
    public sealed class DataGridViewProgressBarCell : DataGridViewTextBoxCell
    {
        //コンストラクタ
        public DataGridViewProgressBarCell()
        {
            this.maximumValue = 100;
            this.mimimumValue = 0;
        }

        private int maximumValue;
        public int Maximum
        {
            get
            {
                return this.maximumValue;
            }
            set
            {
                this.maximumValue = value;
            }
        }

        private int mimimumValue;
        public int Mimimum
        {
            get
            {
                return this.mimimumValue;
            }
            set
            {
                this.mimimumValue = value;
            }
        }

        //セルの値のデータ型を指定する
        public override Type ValueType
        {
            get
            {
                return typeof(float);
            }
        }

        //新しいレコード行のセルの既定値を指定する
        public override object DefaultNewRowValue
        {
            get
            {
                return 0;
            }
        }

        //新しいプロパティを追加しているため、
        // Cloneメソッドをオーバーライドする必要がある
        public override object Clone()
        {
            DataGridViewProgressBarCell cell = (DataGridViewProgressBarCell)base.Clone();
            cell.Maximum = this.Maximum;
            cell.Mimimum = this.Mimimum;
            return cell;
        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds,
            int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            //値を決定する
            float floatValue = 0;
            if (value is float)
            {
                floatValue = (float)value;
            }
            if (floatValue < this.mimimumValue)
            {
                floatValue = this.mimimumValue;
            }
            if (floatValue > this.maximumValue)
            {
                floatValue = this.maximumValue;
            }
            //割合を計算する
            double rate = (double)(floatValue - this.mimimumValue) / (this.maximumValue - this.mimimumValue);

            //セルの境界線（枠）を描画する
            if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
            {
                this.PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
            }

            //境界線の内側に範囲を取得する
            Rectangle borderRect = this.BorderWidths(advancedBorderStyle);
            Rectangle paintRect = new Rectangle(cellBounds.Left + borderRect.Left,
                cellBounds.Top + borderRect.Top, cellBounds.Width - borderRect.Right,
                cellBounds.Height - borderRect.Bottom);

            //背景色を決定する
            //選択されている時とされていない時で色を変える
            bool selected = (cellState & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected;
            Color backColor;
            if (selected &&
                (paintParts & DataGridViewPaintParts.SelectionBackground) == DataGridViewPaintParts.SelectionBackground)
            {
                backColor = cellStyle.SelectionBackColor;
            }
            else
            {
                backColor = cellStyle.BackColor;
            }
            //背景を描画する
            if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
            {
                using (SolidBrush backBrush = new SolidBrush(backColor))
                {
                    graphics.FillRectangle(backBrush, paintRect);
                }
            }

            //Paddingを差し引く
            paintRect.Offset(cellStyle.Padding.Right, cellStyle.Padding.Top);
            paintRect.Width -= cellStyle.Padding.Horizontal;
            paintRect.Height -= cellStyle.Padding.Vertical;

            //ProgressBarを描画する
            if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
            {
                if (ProgressBarRenderer.IsSupported)
                {
                    //visualスタイルで描画する

                    //ProgressBarの枠を描画する
                    ProgressBarRenderer.DrawHorizontalBar(graphics, paintRect);
                    //ProgressBarのバーを描画する
                    Rectangle barBounds = new Rectangle(paintRect.Left + 3, paintRect.Top + 3,
                        paintRect.Width - 4, paintRect.Height - 6);
                    barBounds.Width = (int)Math.Round(barBounds.Width * rate);
                    ProgressBarRenderer.DrawHorizontalChunks(graphics, barBounds);
                }
                else
                {
                    //visualスタイルで描画できない時

                    graphics.FillRectangle(Brushes.White, paintRect);
                    graphics.DrawRectangle(Pens.Black, paintRect);
                    Rectangle barBounds = new Rectangle(paintRect.Left + 1, paintRect.Top + 1,
                        paintRect.Width - 1, paintRect.Height - 1);
                    barBounds.Width = (int)Math.Round(barBounds.Width * rate);
                    graphics.FillRectangle(Brushes.Blue, barBounds);
                }
            }

            //フォーカスの枠を表示する
            if (this.DataGridView.CurrentCellAddress.X == this.ColumnIndex &&
                this.DataGridView.CurrentCellAddress.Y == this.RowIndex &&
                (paintParts & DataGridViewPaintParts.Focus) == 
                DataGridViewPaintParts.Focus && this.DataGridView.Focused)
            {
                //フォーカス枠の大きさを適当に決める
                Rectangle focusRect = paintRect;
                focusRect.Inflate(-3, -3);
                ControlPaint.DrawFocusRectangle(graphics, focusRect);
                //背景色を指定してフォーカス枠を描画する時
                //ControlPaint.DrawFocusRectangle(
                //    graphics, focusRect, Color.Empty, bkColor);
            }

            //テキストを表示する
            if ((paintParts & DataGridViewPaintParts.ContentForeground) == DataGridViewPaintParts.ContentForeground)
            {
                //表示するテキストを決定
                string text = rate.ToString("P1");
                //string txt = formattedValue.ToString();

                //本来は、cellStyleによりTextFormatFlagsを決定すべき
                TextFormatFlags flags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                //色を決定
                Color foreColor = cellStyle.ForeColor;
                //if (isSelected)
                //    fColor = cellStyle.SelectionForeColor;
                //else
                //    fColor = cellStyle.ForeColor;
                //テキストを描画する
                paintRect.Inflate(-2, -2);
                TextRenderer.DrawText(graphics, text, cellStyle.Font, paintRect, foreColor, flags);
            }

            //エラーアイコンの表示
            if ((paintParts & DataGridViewPaintParts.ErrorIcon) == DataGridViewPaintParts.ErrorIcon &&
                this.DataGridView.ShowCellErrors && !string.IsNullOrEmpty(errorText))
            {
                //エラーアイコンを表示させる領域を取得
                Rectangle iconBounds = this.GetErrorIconBounds(graphics, cellStyle, rowIndex);
                iconBounds.Offset(cellBounds.X, cellBounds.Y);
                //エラーアイコンを描画
                this.PaintErrorIcon(graphics, iconBounds, cellBounds, errorText);
            }
        }
    }
}
