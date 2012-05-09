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
using NUnit.Framework;
using RSA.Work;

namespace Tests
{
    [TestFixture]
    public class NumbersTests
    {


        [Test]
        public void RandomPrimeTest()
        {
            var numberManager = new NumberService(4);
            double average = 0;
            const int iterations = 1000;
            for (int i = 0; i < iterations; i++)
            {
                average += (double)numberManager.GetRandomPrime() / iterations;
            }
            Console.WriteLine("average = "+average);
        }

    }
}
