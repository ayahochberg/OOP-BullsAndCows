using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsLogic
{
    public class Result
    {
        private readonly List<Utils.eResult> r_ResultFeedback = new List<Utils.eResult>();
        private Utils.eGameStatus m_GameStatus = Utils.eGameStatus.On;
        private Pins m_PlayerGuess;

        public Result(Pins i_PlayerGuess, Pins i_ComputerSequence)
        {
            this.m_PlayerGuess = i_PlayerGuess;
            int numOfAccurateGuess = 0;

            for (int i = 0; i < this.m_PlayerGuess.Sequence.Count; i++)
            {
                if (i_PlayerGuess.Sequence[i] == i_ComputerSequence.Sequence[i])
                {
                    this.r_ResultFeedback.Add(Utils.eResult.V);
                    numOfAccurateGuess++;
                }
                else if (i_ComputerSequence.Sequence.Contains(i_PlayerGuess.Sequence[i]))
                {
                    this.r_ResultFeedback.Add(Utils.eResult.X);
                }
            }

            this.r_ResultFeedback.Sort();
            //checks if player succeeded to guess
            if (numOfAccurateGuess == Utils.k_NumberOfPins)
            {
                m_GameStatus = Utils.eGameStatus.Success;
            }
        }

        internal List<Utils.eResult> PlayerMoveFeedback
        {
            get
            {
                return r_ResultFeedback;
            }
        }

        internal Utils.eGameStatus Status
        {
            get
            {
                return m_GameStatus;
            }
        }
    }
}
