using BullsAndCowsLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace BullsAndCowsUI
{

    // $G$ CSS-016 (-3) Bad class name - The name of classes derived from Form should start with Form.
    public class InitializeNumberOfChancesForm : Form
    {
        private Button m_ButtonNumberOfChances;
        private Button m_ButtonStart;
        private int m_CurrentNumberOfChances = Utils.k_LowerBoundNumberOfChances;

        public InitializeNumberOfChancesForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.m_ButtonNumberOfChances = new Button();
            this.m_ButtonStart = new Button();
            this.SuspendLayout();
            this.CenterToScreen();

            this.m_ButtonNumberOfChances.Location = new Point(12, 12);
            this.m_ButtonNumberOfChances.BackColor = Color.FromArgb(174, 175, 170);
            this.m_ButtonNumberOfChances.Size = new Size(246, 43);
            this.m_ButtonNumberOfChances.Text = "Number of chances: 4";
            this.m_ButtonNumberOfChances.Click += new EventHandler(this.m_ButtonNumberOfChances_Click);

            this.m_ButtonStart.Location = new System.Drawing.Point(120, 103);
            this.m_ButtonStart.Size = new Size(138, 36);
            this.m_ButtonStart.Text = "Start";
            this.m_ButtonNumberOfChances.BackColor = Color.FromArgb(174, 175, 170);
            this.m_ButtonStart.Click += new EventHandler(this.m_ButtonStart_Click);

            this.BackColor = Color.FromArgb(207, 208, 204);
            this.ClientSize = new Size(270, 152);
            this.Controls.Add(this.m_ButtonStart);
            this.Controls.Add(this.m_ButtonNumberOfChances);
            this.Font = new Font("Optima", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = Color.Black;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Text = Utils.k_GameName;
            this.ResumeLayout(false);
        }

        private void m_ButtonNumberOfChances_Click(object sender, EventArgs e)
        {
            if (this.m_CurrentNumberOfChances == Utils.k_UpperBoundNmberOfChances)
            {
                this.m_CurrentNumberOfChances = Utils.k_LowerBoundNumberOfChances;
                this.m_ButtonNumberOfChances.Text = String.Format("Number of chances: {0}", this.m_CurrentNumberOfChances);
            }
            else
            {
                this.m_CurrentNumberOfChances++;
                this.m_ButtonNumberOfChances.Text = String.Format("Number of chances: {0}", this.m_CurrentNumberOfChances);
            }
        }

        private void m_ButtonStart_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            GameForm newGame = new GameForm(m_CurrentNumberOfChances);
            this.Visible = false;
            newGame.ShowDialog();
        }
    }
}