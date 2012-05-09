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

using System;
using System.IO;
using System.Numerics;
using System.Text;
using RSA.Exceptions;
using RSA.Properties;

namespace RSA.Work
{
    public class RSAEncrypter
    {
        private readonly RSACryptService _cryptService;
        readonly Settings _settings = new Settings();

        public RSAEncrypter()
        {
            var keyLength = _settings.KeyLength;
            var encode = _settings.Encode;
            //http://msdn.microsoft.com/en-us/library/system.text.encoding.aspx list of encodings
            _cryptService = new RSACryptService(keyLength,encode);

        }



        public void Run()
        {
            try
            {
                bool forEncrypt;
                string data;
                KeyModel key;
                Input(out forEncrypt, out data, out key);
                string resultData;
                if (forEncrypt)
                {
                    BigInteger[] numbersData = _cryptService.Encrypt(data, key);
                    resultData = RSAHelper.NumbersDataToString(numbersData);
                }
                else
                {
                    BigInteger[] numbersData = RSAHelper.StringDataToNumbers(data);
                    resultData = _cryptService.Decrypt(numbersData, key);
                }
                ResultToOutput(resultData, key);
            }
            catch (Exception e)
            {
                ToOutput(e.Message, false);
            }
        }

        private void Input(out bool forEncode, out string data, out KeyModel key)
        {
            using (var reader = new StreamReader(_settings.InputFilename, Encoding.UTF8))
            {
                string firstLine = reader.ReadLine();
                bool isEncrypt = RSAHelper.DefinedEncrypt(firstLine);
                KeyModel keyModel;
                try
                {
                     keyModel = RSAHelper.GetKeyByHeader(firstLine);
                }catch(InputFormatException)
                {
                    //need to decrypt secret key to  encrypt the public
                    //if the encrypt key is not specified - it will generate
                    if (isEncrypt)
                        keyModel = _cryptService.GenerateKey();
                    else 
                        throw new InputFormatException("header");
                }
                string content = reader.ReadLine();

                if (string.IsNullOrEmpty(content))
                {
                    throw new InputFormatException("data");
                }

                key = keyModel;
                forEncode = isEncrypt;
                data = content;
            }
        }



        private void ResultToOutput(string data, KeyModel key)
        {
            WriteHeader(key);
            ToOutput(data, true);
        }

        private void WriteHeader(KeyModel key)
        {
            string writeData = "";
            writeData += RSAHelper.PublicKeyAsString(key);
            writeData += RSAHelper.SecretKeyAsString(key);
            ToOutput(writeData, false);
        }


        private void ToOutput(string message, bool append)
        {
            using (var writer = new StreamWriter(_settings.OutputFilename, append, Encoding.UTF8))
            {
                writer.WriteLine(message);
            }
        }
    }
}