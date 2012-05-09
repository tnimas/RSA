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
using System.Numerics;
using System.Text;
using RSA.Exceptions;

namespace RSA.Work
{
    internal static class RSAHelper
    {

        public static KeyModel GetKeyByHeader(string header)
        {
            var keyModel = new KeyModel();
            string[] forKey = header.Split('_');
            if (forKey.Length < 2)
            {
                throw new InputFormatException("header");
            }
            string[] keys = forKey[1].Split(',');
            if (keys.Length < 2)
            {
                throw new InputFormatException("header");
            }
            try
            {
                if (DefinedEncrypt(header))
                    keyModel.E = BigInteger.Parse(keys[0]);
                else
                    keyModel.D = BigInteger.Parse(keys[0]);
                
                keyModel.N = BigInteger.Parse(keys[1]);
            }
            catch (Exception e)
            {
                throw new InputFormatException(e.Message);
            }
            return keyModel;
        }

        public static bool DefinedEncrypt(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                throw new InputFormatException("header");
            }
            if (header.IndexOf("encrypt", StringComparison.Ordinal) == 0)
            {
                return true;
            }
            if (header.IndexOf("decrypt", StringComparison.Ordinal) == 0)
                return false;
            throw
                new InputFormatException("header");
        }

        public static string PublicKeyAsString(KeyModel model)
        {
            bool isDefined = model.E != default(BigInteger) && model.N != default(BigInteger);
            return isDefined ? String.Format("Public key (e,n): {0},{1}{2}", model.E, model.N, Environment.NewLine) : null;
        }

        public static string SecretKeyAsString(KeyModel model)
        {
            bool isDefined = model.D != default(BigInteger) && model.N != default(BigInteger);
            return isDefined ? String.Format("Secret key (d,n): {0},{1}{2}", model.D, model.N, Environment.NewLine) : null;
        }

        public static string NumbersDataToString(BigInteger[] data)
        {
            var tmpData = new StringBuilder();
            foreach (BigInteger number in data)
            {
                var nextnum = number.ToString() + ' ';
                tmpData = tmpData.Append(nextnum);
            }
            tmpData = tmpData.Remove(tmpData.Length-1,1);
            return tmpData.ToString();

        }

        public static BigInteger[] StringDataToNumbers(string data)
        {
            var strokeNumbers = data.Split(' ');
            var numbers = new BigInteger[strokeNumbers.Length];
            for (int i = 0; i < strokeNumbers.Length; i++)
            {
                numbers[i] = BigInteger.Parse(strokeNumbers[i]);
            }
            return numbers;
        }

    }
}