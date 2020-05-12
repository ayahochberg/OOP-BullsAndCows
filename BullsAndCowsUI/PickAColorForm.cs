using BullsAndCowsLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BullsAndCowsUI
{

    // $G$ CSS-016 (-3) Bad class name - The name of classes derived from Form should start with Form.
    public class PickAColorForm : Form
    {
        private Color m_ChosenColor;

        public PickAColorForm()
        {
            InitializeComponent();
        }

        public Color Color
        {
            get
            {
                return this.m_ChosenColor;
            }
        }

        private void InitializeComponent()
        {
            Color[] colorsArray = (new List<Color>(Utils.s_ColorsDictionary.Values)).ToArray();
            int index = 0;

            for (int i = 0; i < Utils.k_NumberOfPins / 2; i++)
            {
                for (int j = 0; j < Utils.k_NumberOfPins; j++)
                {
                    Button button = new Button();
                    button.Size = new Size(Utils.k_SizeOfPinsButton, Utils.k_SizeOfPinsButton);
                    button.BackColor = colorsArray[index];
                    button.Top = 10 + (i) * Utils.k_SizeOfPinsButton;
                    button.Left = 10 + (j) * Utils.k_SizeOfPinsButton;
                    this.Controls.Add(button);
                    button.Click += new EventHandler(this.buttonColor_Click);
                    index++;
                }
            }

            this.Size = new Size(Utils.k_NumberOfPins * 60, Utils.k_NumberOfPins * 40);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "Pick A Color";
            this.ResumeLayout(false);
            this.SuspendLayout();
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            this.m_ChosenColor = (sender as Button).BackColor;
            (sender as Button).Enabled = false;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        public void EnableAllButtons()
        {
            foreach (Control control in this.Controls)
            {
                (control as Button).Enabled = true;
            }
        }

        public void EnableColorButton(Color i_Color)
        {
            foreach (Control control in this.Controls)
            {
                if ((control as Button).BackColor == i_Color)
                {
                    (control as Button).Enabled = true;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
        }
    }
}