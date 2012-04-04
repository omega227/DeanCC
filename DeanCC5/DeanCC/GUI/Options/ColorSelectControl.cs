using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DeanCC.GUI.Options
{
    public sealed partial class ColorSelectControl : UserControl
    {
        public ColorSelectControl()
        {
            InitializeComponent();
        }

        ColorDialog dialog;
        private Color selectedColor;
        public Color SelectedColor
        {
            get { return selectedColor; }
            set
            {
                if (selectedColor != value)
                {
                    selectedColor = value;
                    OnSelectedColorChanged(new SelectedColorChangedEventArgs(value));
                }
            }
        }
        public string Description
        {
            get
            {
                return descriptionLabel.Text;
            }
            set
            {
                descriptionLabel.Text = value;
            }
        }
        public ColorSelectPosition TargetPosion { get; set; }

        private void customColorButton_Click(object sender, EventArgs e)
        {
            if (dialog == null)
            {
                dialog = new ColorDialog();
            }
           
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = dialog.Color;
            }
        }

        private void OnSelectedColorChanged(SelectedColorChangedEventArgs e)
        {
            switch (TargetPosion)
            {
                case ColorSelectPosition.ForeColor:
                    descriptionLabel.ForeColor = e.Color;
                    break;

                case ColorSelectPosition.BackColor:
                    descriptionLabel.BackColor = e.Color;
                    break;
            }
        }
    }

    public enum ColorSelectPosition
    {
        ForeColor,
        BackColor
    }

    public sealed class SelectedColorChangedEventArgs : EventArgs
    {
        public SelectedColorChangedEventArgs(Color color)
        {
            this.Color = color;
        }
        public Color Color { get; private set; }
    }
}
