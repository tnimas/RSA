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

namespace RSA.Work
{
    internal class KeyGenerationService
    {
        private readonly NumberService _numberGetter;

        public KeyGenerationService(int keyLength)
        {
            _numberGetter = new NumberService(keyLength);
        }

        public KeyModel GenerateKey()
        {

            var p = _numberGetter.GetRandomPrime();
            var q = _numberGetter.GetRandomPrime();
            while (p == q)
            {
                q = _numberGetter.GetRandomPrime();
            }
            BigInteger n = p*q;
            BigInteger m = (p - 1)*(q - 1);

            BigInteger e;
            BigInteger d;
            do
            {
                e = GetE(m);
                d = GetD(e, m);
                
            } while (d < 0 || e == d);

            return new KeyModel {D = d, E = e, N = n};
        }

        private BigInteger GetE(BigInteger m)
        {
            while (true)
            {
                BigInteger i = _numberGetter.GetRandomPrime();
                if (BigInteger.GreatestCommonDivisor(m, i) == 1)
                {
                    return i;
                }
            }
        }

        private BigInteger GetD(BigInteger e, BigInteger m)
        {
            return ExtendedEuclide(e%m, m).U1;
        }

        /// <summary>
        /// u1 * a + u2 * b = u3
        /// </summary>
        private ExtendedEuclideanResult  ExtendedEuclide(BigInteger a, BigInteger b)
        {
            BigInteger u1 = 1;
            BigInteger u3 = a;
            BigInteger v1 = 0;
            BigInteger v3 = b;

            while (v3 > 0)
            {
                BigInteger q0 = u3/v3;
                BigInteger q1 = u3%v3;

                BigInteger tmp = v1*q0;
                BigInteger tn = u1 - tmp;
                u1 = v1;
                v1 = tn;

                u3 = v3;
                v3 = q1;
            }

            BigInteger tmp2 = u1 * (a);
            tmp2 = u3 - (tmp2);
            BigInteger res = tmp2 / (b);

            var result = new ExtendedEuclideanResult()
            {
                U1 = u1,
                U2 = res,
                Gcd = u3
            };

            return result;
        }

        private struct ExtendedEuclideanResult
        {
            public BigInteger U1;
            public BigInteger U2;
            public BigInteger Gcd;
        }

    }
}