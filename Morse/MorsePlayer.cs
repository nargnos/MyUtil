using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Morse
{
    public class MorsePlayer
    {

        private MorseConverter morseConverter;
        public char DitChar { get { return morseConverter.DitChar; } set { morseConverter.DitChar = value; } }
        public char DahChar { get { return morseConverter.DahChar; } set { morseConverter.DahChar = value; } }
        public char ShortGapChar { get { return morseConverter.ShortGapChar; } set { morseConverter.ShortGapChar = value; } }
        public int Hz { get; set; } = 1000;
        public int Duration { get; set; } = 120;

        private Action onDit;
        private Action onDah;
        private Action onShortGap;
        private Action onMediumGap;

        public MorsePlayer()
        {
            onDit = () => Console.Beep(Hz, Duration);
            onDah = () => Console.Beep(Hz, 3 * Duration);
            onShortGap = () => Thread.Sleep(3 * Duration);
            onMediumGap = () => Thread.Sleep(7 * Duration);

            morseConverter = new MorseConverter();
            morseConverter.Dit += onDit;
            morseConverter.Dah += onDah;
            morseConverter.ShortGap += onShortGap;
            morseConverter.MediumGap += onMediumGap;
        }
        public string PlayAscii(IEnumerable<char> ascii)
        {
            return morseConverter.AsciiToMorse(ascii);
        }
        public void PlayMorse(string morse)
        {
            var words = morse.Split(new string[] { morseConverter.MediumGapString }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var word in words)
            {
                foreach (var chr in word)
                {
                    if (chr == ShortGapChar)
                    {
                        onShortGap();
                    }
                    else if (chr == DitChar)
                    {
                        onDit();
                    }
                    else if (chr == DahChar)
                    {
                        onDah();
                    }
                }
                onMediumGap();
            }
        }
    }
}
