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

namespace RSA.Work
{
    public class NumberService
    {
        private readonly int _keyLength;
        private readonly Random _randomizer = new Random(DateTime.Now.Millisecond + 42);

        public NumberService(int keyLength)
        {
            if (keyLength < 4)
            {
                throw new ArgumentException("the key length does not allow for random primes");
            }
            _keyLength = keyLength;
        }

        public BigInteger GetRandomPrime()
        {
            string bigIntString = Utils.BigInteger.genPseudoPrime(_keyLength, 5, _randomizer).ToString();
            BigInteger result = BigInteger.Parse(bigIntString);

            return result;
        }
    }
}