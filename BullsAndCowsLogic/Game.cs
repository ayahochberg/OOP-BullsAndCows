using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsLogic
{
    public class Game
    {

        private Pins m_ComputerSequence;
        private int m_NumberOfTurns;
        private Utils.eGameStatus m_GameStatus = Utils.eGameStatus.On;

        public Game(int i_NumberOfTurns)
        {
            this.m_ComputerSequence = new Pins();
            this.m_NumberOfTurns = i_NumberOfTurns;
        }

        public List<Utils.ePins> ComputerSequence
        {
            get
            {
                return m_ComputerSequence.Sequence;
            }
        }

        public Utils.eGameStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }
        }

        public List<Utils.eResult> CreateGameMove(List<Utils.ePins> i_UserGuess)
        {
            Pins currentGuess = new Pins(i_UserGuess);
            Result currentFeedback = new Result(currentGuess, this.m_ComputerSequence);
            this.m_GameStatus = currentFeedback.Status;

            return currentFeedback.PlayerMoveFeedback;
        }
    }
}
