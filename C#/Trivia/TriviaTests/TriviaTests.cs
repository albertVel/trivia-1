using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TriviaTests
{
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;

    using Trivia;

    [TestClass]
    public class TriviaTests
    {
        [TestMethod]
        public void CompareReferenceGameWithCurrentOne()
        {
            this.PlayGameSaveLogs("currentGame.log");

            Assert.IsTrue(this.CompareFiles("ReferenceGame.log", "currentGame.log"));
        }

        private void PlayGameSaveLogs(string fileName)
        {
            TextWriter oldOut = Console.Out;
            try
            {
                using (FileStream ostrm = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter writer = new StreamWriter(ostrm))
                    {
                        Console.SetOut(writer);
                        GameRunner.PlayGame();
                        Console.SetOut(oldOut);
                    }
                }
               
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open {0} for writing", fileName);
                Console.WriteLine(e.Message);
                return;
            }     
        }

        private bool CompareFiles(string log1, string log2)
        {
            Console.WriteLine("Comparing files...");
            var md5Log1 = CalculateMd5(log1);
            var md5Log2 = CalculateMd5(log2);

            return md5Log1.SequenceEqual(md5Log2);
        }

        private byte[] CalculateMd5(string logFile)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(logFile))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }
    }
}
