//    This program implements the encryption and decription algorithm RSA.
//    Copyright (C) 2012  Maslov Nikolay
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see http://www.gnu.org/licenses/.

using NUnit.Framework;
using RSA.Work;

namespace Tests
{
    [TestFixture]
   public class CryptTests
    {
        private const string _encode = "windows-1251";

        [Test]
        public void KeyGenerationTest()
        {
            var keyM = new RSACryptService(512, _encode);


            for (int i = 0; i < 5; i++)
            {
                KeyModel generateKey = keyM.GenerateKey();
                bool eCorrect = generateKey.E >= 3;
                bool nCorrect = generateKey.N >= 9;
                bool dCorrect = generateKey.D > 0;

                Assert.True(dCorrect);
                Assert.True(nCorrect);
                Assert.True(eCorrect);

            }
        }
        [Test]
        public void EncryptDecryptTest()
        {
            var service = new RSACryptService(1024,_encode);
            const string testString = "rsa is complete";
            var key = new KeyModel {D = 44273, E = 65537, N = 49163};
            var encryptData = service.Encrypt(testString, key);
            string decryptString = service.Decrypt(encryptData, key);
            
            Assert.AreEqual(testString,decryptString);
        }

        [Test]
        public void FullTest()
        {
            var cryptService = new RSACryptService(100, _encode);
            for (int i = 0; i < 10; i++)
            {
                KeyModel generateKey = cryptService.GenerateKey();
                const string testString = "rsa is complete";
                var encryptData = cryptService.Encrypt(testString, generateKey);
                string decryptString = cryptService.Decrypt(encryptData, generateKey);

                Assert.AreEqual(testString, decryptString);
            }
        }
    }
}
