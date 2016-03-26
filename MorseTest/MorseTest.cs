using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Morse;
using System.Threading;
using System.Diagnostics;
using System.Text;
using System.Linq;
namespace MorseTest
{
    [TestClass]
    public class MorseTest
    {
        // dit dah用的字符跟默认的不一样，用来测试设置字符功能
        private static string morseAlphabet = "-- --- .-. ... .   .- .-.. .--. .... .- -... . - ";
        private static string morseCodeStr = "Morse Code";
        [TestMethod]
        public void TestAlphabet()
        {
            var morse = new MorsePlayer();

            char[] code = new char[] { ' ' };
            while (code[0] <= '~')
            {
                if (!char.IsLower(code[0]))
                {
                    var result = morse.PlayAscii(code);
                    Debug.WriteLine($"{code[0]} : {result}");
                }

                code[0]++;
            }
        }
        [TestMethod]
        public void TestMorsePlayer()
        {
            var morse = new MorsePlayer();
            morse.PlayAscii("Hello World!");
            Console.Beep(440, 500);
            morse.DitChar = '.';
            morse.DahChar = '-';
            morse.PlayMorse(morseAlphabet);
        }
        [TestMethod]
        public void TestConvertText()
        {
            var morse = new MorseConverter();
            var morseCode = morse.AsciiToMorse(morseCodeStr);
            var text = morse.MorseToAscii(morseCode);
            Debug.WriteLine(text);
            Assert.AreEqual(morseCodeStr.ToUpper(), text);


            morseCode = morseAlphabet;
            morse.DitChar = '.';
            morse.DahChar = '-';

            text = morse.MorseToAscii(morseCode);
            Debug.WriteLine(text);

        }
        [TestMethod]
        public void TestConvertBytes()
        {
            var morse = new MorseConverter();
            var convertStr = morseCodeStr;
            var morseCode = morse.BytesToMorse(Encoding.ASCII.GetBytes(convertStr));

            var textByte = morse.MorseToBytes(morseCode);
            var text = Encoding.ASCII.GetString(textByte.ToArray());
            Assert.AreEqual(text, convertStr);
        }
        [TestMethod]
        public void TestInvalidParameter()
        {
            var morse = new MorseConverter();
            morse.BytesToMorse(null);
            morse.BytesToMorse(new byte[] { 0x20 });

            string[] strParams = new string[]
            {
                null,
                string.Empty,
                "",
                "中文",
                "Ascii",
                "中文+Ascii",
                "          A                        B            ",
                ".|.|.",
                "-",
                ".",
                "- -",
                ". .",
                ". -",
                "- ."
            };
            string[] resultAsciiToMorse = new string[]
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                "·− ··· −·−· ·· ··",
                "·−·−· ·− ··· −·−· ·· ··",
                "·−                         −···",
                "·−·−·− ·−·−·− ·−·−·−",
                "−····−",
                "·−·−·−",
                "−····−  −····−",
                "·−·−·−  ·−·−·−",
                "·−·−·−  −····−",
                "−····−  ·−·−·−"
            };
            string[] resultMorseToAscii = new string[]
            {
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                "S",  // 忽略不匹配的字符
                "T",
                "E",
                "TT",
                "EE",
                "ET",
                "TE"
            };
            byte[][] resultMorseToBytes = new byte[][]
            {
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { },
                new byte[] { 0xee },
                new byte[] { },
                new byte[] { },
            };
            for (int i = 0; i < strParams.Length; i++)
            {
                Assert.AreEqual(morse.AsciiToMorse(strParams[i]), resultAsciiToMorse[i]);
            }
            morse.DitChar = '.';
            morse.DahChar = '-';
            for (int i = 0; i < strParams.Length; i++)
            {
                Assert.AreEqual(morse.MorseToAscii(strParams[i]), resultMorseToAscii[i]);
                Assert.IsTrue(morse.MorseToBytes(strParams[i]).SequenceEqual(resultMorseToBytes[i]));
            }

        }
    }
}
