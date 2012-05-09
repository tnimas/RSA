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

using System.Numerics;
using System.Text;

namespace RSA.Work
{
    public class RSACryptService
    {
        private readonly Encoding _encoding;

        public RSACryptService(int keyLength,string encode)
        {
            _keyGeneration = new KeyGenerationService(keyLength);
            _encoding = Encoding.GetEncoding(encode);
        }

        private readonly KeyGenerationService _keyGeneration;

        public KeyModel GenerateKey()
        {
            return _keyGeneration.GenerateKey();
        }

        public BigInteger[] Encrypt(string data, KeyModel key)
        {
            
            byte[] dataBytes = _encoding.GetBytes(data);
            var toReturn = new BigInteger[dataBytes.Length];
            for (int i = 0; i< dataBytes.Length;i++)
            {
                toReturn[i] = BigInteger.ModPow(dataBytes[i], key.E, key.N);
            }

            return toReturn;
        }

        public string Decrypt(BigInteger[] encryptedData, KeyModel key)
        {
            var dataBytes = new byte[encryptedData.Length];
            for (int i = 0; i < encryptedData.Length; i++)
            {
                var decodeNum = BigInteger.ModPow(encryptedData[i], key.D, key.N);
                dataBytes[i] = decodeNum.ToByteArray()[0];
            }
            string result = _encoding.GetString(dataBytes);
            return result;
        }
    }
}
