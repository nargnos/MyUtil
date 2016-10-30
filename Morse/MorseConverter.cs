using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Morse
{
    public class MorseConverter
    {
        // true == dit\ false == dah
        private static Lazy<Dictionary<char, bool[]>> alphabet = new Lazy<Dictionary<char, bool[]>>(
            () =>
            {
                return new Dictionary<char, bool[]>
                {
                    {'A', new bool[] {true,false}},
                    {'B', new bool[] {false,true,true,true}},
                    {'C', new bool[] {false,true,false,true}},
                    {'D', new bool[] {false,true,true}},
                    {'E', new bool[] {true}},
                    {'F', new bool[] {true,true,false,true}},
                    {'G', new bool[] {false,false,true}},
                    {'H', new bool[] {true,true,true,true}},
                    {'I', new bool[] {true,true}},
                    {'J', new bool[] {true,false,false,false}},
                    {'K', new bool[] {false,true,false}},
                    {'L', new bool[] {true,false,true,true}},
                    {'M', new bool[] {false,false}},
                    {'N', new bool[] {false,true}},
                    {'O', new bool[] {false,false,false}},
                    {'P', new bool[] {true,false,false,true}},
                    {'Q', new bool[] {false,false,true,false}},
                    {'R', new bool[] {true,false,true}},
                    {'S', new bool[] {true,true,true}},
                    {'T', new bool[] {false}},
                    {'U', new bool[] {true,true,false}},
                    {'V', new bool[] {true,true,true,false}},
                    {'W', new bool[] {true,false,false}},
                    {'X', new bool[] {false,true,true,false}},
                    {'Y', new bool[] {false,true,false,false}},
                    {'Z', new bool[] {false,false,true,true}},
                    {'0', new bool[] {false,false,false,false,false}},
                    {'1', new bool[] {true,false,false,false,false}},
                    {'2', new bool[] {true,true,false,false,false}},
                    {'3', new bool[] {true,true,true,false,false}},
                    {'4', new bool[] {true,true,true,true,false}},
                    {'5', new bool[] {true,true,true,true,true}},
                    {'6', new bool[] {false,true,true,true,true}},
                    {'7', new bool[] {false,false,true,true,true}},
                    {'8', new bool[] {false,false,false,true,true}},
                    {'9', new bool[] {false,false,false,false,true}},
                    {'.', new bool[] {true,false,true,false,true,false}},
                    {',', new bool[] {false,false,true,true,false,false}},
                    {'?', new bool[] {true,true,false,false,true,true}},
                    {'\'',new bool[] {true,false,false,false,false,true}},
                    {'!', new bool[] {false,true,false,true,false,false}},
                    {'/', new bool[] {false,true,true,false,true}},
                    {'&', new bool[] {true,false,true,true,true}},
                    {':', new bool[] {false,false,false,true,true,true}},
                    {';', new bool[] {false,true,false,true,false,true}},
                    {'=', new bool[] {false,true,true,true,false}},
                    {'+', new bool[] {true,false,true,false,true}},
                    {'-', new bool[] {false,true,true,true,true,false}},
                    {'_', new bool[] {true,true,false,false,true,false}},
                    {'"', new bool[] {true,false,true,true,false,true}},
                    {'$', new bool[] {true,true,true,false,true,true,false}},
                    {'@', new bool[] {true,false,false,true,false,true}},
                    {'(', new bool[] {false,true,false,false,true}},
                    {')', new bool[] {false,true,false,false,true,false,}},

                    {' ', new bool[] { } }
                };
            }, true);

        private class BoolArrayComparer : IEqualityComparer<bool[]>
        {
            public bool Equals(bool[] x, bool[] y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(bool[] obj)
            {
                int result = obj.Length << 1;
                foreach (var item in obj)
                {
                    result = result << 1 | (item ? 1 : 0);
                }
                return result;
            }
        }
        private static Lazy<Dictionary<bool[], char>> morseMap = new Lazy<Dictionary<bool[], char>>(
            () => alphabet.Value.ToDictionary(
                (pair) => pair.Value,
                (pair) => pair.Key, new BoolArrayComparer()), true);

        private const int ByteHexStrLen = 2;
        public char DitChar { get; set; } = '·';
        public char DahChar { get; set; } = '−';
        public char ShortGapChar { get; set; } = ' ';
        public string MediumGapString
        {
            get
            {
                return $"{ShortGapChar}{ShortGapChar}";
            }
        }

        public event Action Dit;
        public event Action Dah;
        public event Action ShortGap;
        public event Action MediumGap;
        private void OnEvent(Action e)
        {
            e?.Invoke();
        }

        // 2间隔区分词，1间隔区分字
        public string MorseToAscii(string line)
        {
            if (string.IsNullOrEmpty(line))
            {
                return string.Empty;
            }
            var morse = morseMap.Value;
            // FIX: 使用分割字符串方式性能不高
            var morseArray = from word in line.Split(new string[] { MediumGapString }, StringSplitOptions.RemoveEmptyEntries)
                             select
                             from chr in word.Split(ShortGapChar)
                             select
                             (from morseCode in chr
                              where morseCode == DitChar || morseCode == DahChar
                              let isDit = (morseCode == DitChar ? true : false)
                              select isDit).ToArray();


            StringBuilder sb = new StringBuilder();
            foreach (var word in morseArray)
            {
                foreach (var chr in word)
                {
                    if (morse.ContainsKey(chr))
                    {
                        sb.Append(morse[chr]);
                    }
                }
                sb.Append(ShortGapChar);
            }
            return sb.ToString().Trim();
        }
        public string AsciiToMorse(IEnumerable<char> ascii)
        {
            if (ascii == null)
            {
                return string.Empty;
            }
            var morseArray = from chr in ascii
                             let upperChr = Char.ToUpper(chr)
                             where alphabet.Value.ContainsKey(upperChr)
                             select alphabet.Value[upperChr];

            StringBuilder sb = new StringBuilder();
            foreach (var morse in morseArray)
            {
                if (morse == null || !morse.Any())
                {
                    OnEvent(MediumGap);
                }
                else
                {
                    foreach (var isDit in morse)
                    {
                        if (isDit)
                        {
                            sb.Append(DitChar);
                            OnEvent(Dit);
                        }
                        else
                        {
                            sb.Append(DahChar);
                            OnEvent(Dah);
                        }
                    }
                    OnEvent(ShortGap);
                }
                sb.Append(ShortGapChar);
            }
            return sb.ToString().Trim();
        }
        public string BytesToMorse(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }
            var str = BitConverter.ToString(bytes).Replace("-", "");
            return AsciiToMorse(str);
        }
        public List<byte> MorseToBytes(string morse)
        {
            var str = MorseToAscii(morse);
            List<byte> result = new List<byte>(str.Length / ByteHexStrLen);
            for (int i = 0; i < str.Length; i += ByteHexStrLen)
            {
                if (i + ByteHexStrLen > str.Length)
                {
                    break;
                }
                var tmpStr = str.Substring(i, ByteHexStrLen);
                try
                {
                    result.Add(Convert.ToByte(tmpStr, 16));
                }
                catch (Exception)
                {
                }
            }
            return result;
        }
    }

}
