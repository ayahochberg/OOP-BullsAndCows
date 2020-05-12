using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCowsLogic
{
    public class Utils
    {
        public enum ePins
        {
            Blue,
            Green,
            Purple,
            Pink,
            Peach,
            White,
            Red,
            Orange,
        }

        public static Dictionary<Utils.ePins, Color> s_ColorsDictionary = new Dictionary<Utils.ePins, Color>
        {
          {Utils.ePins.Blue, Color.FromArgb(0, 192, 192)},
          {Utils.ePins.Green,Color.FromArgb(192, 255, 192)},
          {Utils.ePins.Purple,Color.FromArgb(192, 0, 192)},
          {Utils.ePins.Pink, Color.DeepPink},
          {Utils.ePins.Peach, Color.FromArgb(255, 224, 192)},
          {Utils.ePins.White, Color.White},
          {Utils.ePins.Red, Color.Red},
          {Utils.ePins.Orange, Color.DarkOrange},
        };

        public enum eResult
        {
            V,
            X
        }

        public enum eGameStatus
        {
            On,
            Success,
            Failure
        }

        //Game settings
        public const int k_TotalNumberOfPinsValues = 8;
        public const int k_NumberOfPins = 4;
        public const int k_LowerBoundNumberOfChances = 4;
        public const int k_UpperBoundNmberOfChances = 10;

        //Form settings
        public const int k_SizeOfPinsButton = 50;
        public const int k_WidthOfArrowButton = 60;
        public const int k_HightOfArrowButton = 30;
        public const int k_MoveLineHight = 60;
        public const int k_SizeOfResultButton = 20;
        public const int k_TotalWidthOfPins = k_NumberOfPins * k_SizeOfPinsButton + 10;
        public const string k_GameName = "Bool Pgia!";
    }
}
