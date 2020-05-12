using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using BullsAndCowsLogic;

namespace BullsAndCowsUI
{

    // $G$ CSS-016 (-3) Bad class name - The name of classes derived from Form should start with Form.
    public class GameForm : Form
    {
        private int m_NumberOfChances;
        private readonly Button[] r_ComputerSequence;
        private readonly Button[][] r_UserGuessesMatrix;
        private readonly Button[] r_ResultArrows;
        private readonly Button[][] r_ShowResultMatrix;
        private int m_CurrentNumberOfTurn = 0;
        // $G$ DSN-999 (-3) This kind of field should be readonly.
        private PickAColorForm m_FormPickAColor;
        private Game m_Game;
        private Color m_ButtonsDefaultColor = Color.FromArgb(174, 175, 170);

        public GameForm(int i_NumberOfChances)
        {
            m_NumberOfChances = i_NumberOfChances;
            r_ComputerSequence = new Button[Utils.k_NumberOfPins];
            r_UserGuessesMatrix = new Button[m_NumberOfChances][];
            r_ResultArrows = new Button[m_NumberOfChances];
            r_ShowResultMatrix = new Button[m_NumberOfChances][];
            this.m_FormPickAColor = new PickAColorForm();
            m_Game = new Game(i_NumberOfChances);
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            initComputerSequence();
            initUserChancesPins();
            initResultArrowButtons();
            initResultButtons();

            this.SuspendLayout();
            this.ClientSize = new Size(340, (m_NumberOfChances + 1) * 60);
            this.Text = Utils.k_GameName;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Font = new Font(this.Font, FontStyle.Bold);
            this.ResumeLayout(false);
            this.BackColor = Color.FromArgb(207, 208, 204);
            this.CenterToScreen();
        }

        private void initComputerSequence()
        {
            for (int randomPin = 0; randomPin < Utils.k_NumberOfPins; randomPin++)
            {
                Button randomPinButton = new Button();
                randomPinButton.Size = new Size(Utils.k_SizeOfPinsButton, Utils.k_SizeOfPinsButton);
                randomPinButton.BackColor = Color.Black;
                randomPinButton.Location = new Point(randomPinButton.Width * randomPin, 0);
                randomPinButton.Enabled = false;
                r_ComputerSequence[randomPin] = randomPinButton;
                Controls.Add(randomPinButton);
            }
        }


        // $G$ CSS-013 (-5) Bad input variable name (should be in the form of i_PascalCased)
        private void updateButtonsProperties(Button[] io_ButtonsArray)
        {
            for (int i = 0; i < io_ButtonsArray.Length; i++)
            {
                Button button = new Button();
                button.Enabled = false;
                button.Size = new Size(Utils.k_SizeOfPinsButton, Utils.k_SizeOfPinsButton);
                io_ButtonsArray[i] = button;
            }
        }

        private void initUserChancesPins()
        {
            for (int i = 0; i < r_UserGuessesMatrix.Length; i++)
            {
                r_UserGuessesMatrix[i] = new Button[Utils.k_NumberOfPins];
                updateButtonsProperties(r_UserGuessesMatrix[i]);

                for (int j = 0; j < r_UserGuessesMatrix[0].Length; j++)
                {
                    r_UserGuessesMatrix[i][j].Location = new Point(Utils.k_SizeOfPinsButton * j, (i + 1) * Utils.k_MoveLineHight);
                    r_UserGuessesMatrix[i][j].BackColor = this.m_ButtonsDefaultColor;
                    Controls.Add(r_UserGuessesMatrix[i][j]);
                    if (i == 0)
                    {
                        r_UserGuessesMatrix[i][j].Enabled = true;
                    }

                    r_UserGuessesMatrix[i][j].Click += new EventHandler(this.buttonChosePinColor_Click);
                }
            }
        }

        private void initResultArrowButtons()
        {
            for (int i = 0; i < m_NumberOfChances; i++)
            {
                Button resultArrowButton = new Button();
                resultArrowButton.Size = new Size(10 + Utils.k_WidthOfArrowButton, Utils.k_HightOfArrowButton);
                resultArrowButton.Text = char.ConvertFromUtf32(0x00002192);
                resultArrowButton.Location = new Point(Utils.k_TotalWidthOfPins, (i + 1) * Utils.k_MoveLineHight + 10);
                resultArrowButton.Enabled = false;
                resultArrowButton.BackColor = m_ButtonsDefaultColor;
                r_ResultArrows[i] = resultArrowButton;
                Controls.Add(resultArrowButton);
                r_ResultArrows[i].Click += new EventHandler(this.buttonArrow_Click);
            }
        }

        private void initResultButtons()
        {
            for (int i = 0; i < m_NumberOfChances; i++)
            {
                r_ShowResultMatrix[i] = new Button[Utils.k_NumberOfPins];
                for (int j = 0; j < Utils.k_NumberOfPins / 2; j++)
                {
                    for (int k = 0; k < Utils.k_NumberOfPins / 2; k++)
                    {
                        Button resultButton = new Button();
                        resultButton.Size = new Size(Utils.k_SizeOfResultButton, Utils.k_SizeOfResultButton);
                        resultButton.Location = new Point(Utils.k_TotalWidthOfPins + j * Utils.k_SizeOfResultButton + 80,
                            Utils.k_MoveLineHight * (i + 1) + k * Utils.k_SizeOfResultButton);
                        resultButton.Enabled = false;
                        resultButton.BackColor = m_ButtonsDefaultColor;
                        r_ShowResultMatrix[i][2*k+j] = resultButton;
                        Controls.Add(resultButton);
                    }
                }
            }
        }

        private void buttonChosePinColor_Click(object sender, EventArgs e)
        {
            DialogResult result = m_FormPickAColor.ShowDialog();

            if(result == DialogResult.OK)
            {
                if ((sender as Button).BackColor != this.m_ButtonsDefaultColor)
                {
                    m_FormPickAColor.EnableColorButton((sender as Button).BackColor);
                }
      
                (sender as Button).BackColor = m_FormPickAColor.Color;
                if (ensureAllButtonsAreColored())
                {
                    r_ResultArrows[m_CurrentNumberOfTurn].Enabled = true;
                }
            }
        }

        private bool ensureAllButtonsAreColored()
        {
            bool allButtonsColored = true;

            for (int i = 0; i < Utils.k_NumberOfPins; i++)
            {
                if(r_UserGuessesMatrix[m_CurrentNumberOfTurn][i].BackColor == m_ButtonsDefaultColor)
                {
                    allButtonsColored = false;
                    break;
                }
            }

            return allButtonsColored;
        }

        private void buttonArrow_Click(object sender, EventArgs e)
        {
            List<Utils.ePins> userGuessPins = new List<Utils.ePins>();
            List<Utils.eResult> usersTurnResult = new List<Utils.eResult>();

            for (int i = 0; i < Utils.k_NumberOfPins; i++)
            {
                userGuessPins.Add(colorToEnum(r_UserGuessesMatrix[m_CurrentNumberOfTurn][i].BackColor));
                r_UserGuessesMatrix[m_CurrentNumberOfTurn][i].Enabled = false;
            }

            usersTurnResult = m_Game.CreateGameMove(userGuessPins);
            showUsersTurnResult(usersTurnResult);
            if (m_Game.GameStatus == Utils.eGameStatus.Success || this.m_CurrentNumberOfTurn == m_NumberOfChances - 1)
            {
                showComputerSequence();
            }
            else
            {
                this.m_CurrentNumberOfTurn++;
                for (int i = 0; i < Utils.k_NumberOfPins; i++)
                {
                    r_UserGuessesMatrix[m_CurrentNumberOfTurn][i].Enabled = true;
                }

                m_FormPickAColor.EnableAllButtons();
            }
            
            (sender as Button).Enabled = false;
        }

        private Utils.ePins colorToEnum(Color color)
        {
            Utils.ePins pinToReturn = default(Utils.ePins);
            foreach (KeyValuePair<Utils.ePins, Color> pair in Utils.s_ColorsDictionary)
            {
                if (pair.Value == color)
                {
                    pinToReturn = pair.Key;
                }
            }

            return pinToReturn;
        }

        private void showComputerSequence()
        {
            for(int i = 0; i < Utils.k_NumberOfPins; i++)
            {
                r_ComputerSequence[i].BackColor = Utils.s_ColorsDictionary[m_Game.ComputerSequence[i]];
            }
        }

        private void showUsersTurnResult(List<Utils.eResult> i_UsersTurnResult)
        {
            for (int i = 0; i < i_UsersTurnResult.Count; i++)
            {
                bool isAccurateHit = i_UsersTurnResult[i] == Utils.eResult.V;
                r_ShowResultMatrix[m_CurrentNumberOfTurn][i].BackColor = isAccurateHit ? Color.Black : Color.Yellow;
            }
        }
    }
}