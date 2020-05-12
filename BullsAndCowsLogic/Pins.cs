using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsLogic
{
    public class Pins
    {
        // $G$ DSN-999 (-3) This array should be readonly.
        private List<Utils.ePins> m_Pins = new List<Utils.ePins>();

        // $G$ NTT-999 (-5) There is no need to re-instantiate the random instance every time it is used.
        //construct pins object for computer sequnce
        //called only once - when initialize a new game
        public Pins()
        {
            Random random = new Random();
            Array valusOfPins = Enum.GetValues(typeof(Utils.ePins));
            Utils.ePins randomPin = (Utils.ePins)valusOfPins.GetValue(random.Next(valusOfPins.Length));

            for (int i = 0; i < Utils.k_NumberOfPins; i++)
            {
                while (this.m_Pins.Contains(randomPin))
                {
                    randomPin = (Utils.ePins)valusOfPins.GetValue(random.Next(valusOfPins.Length));
                }
                this.m_Pins.Add(randomPin);
            }
        }

        //construct pins object for user guess
        public Pins(List<Utils.ePins> i_UserGuess)
        {
            foreach (Utils.ePins guess in i_UserGuess)
            {
                m_Pins.Add(guess);
            }
        }

        public List<Utils.ePins> Sequence
        {
            get
            {
                return this.m_Pins;
            }
        }
    }
}
