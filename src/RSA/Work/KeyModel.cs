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
    public class KeyModel
    {
        /// <summary>
        /// abs of a base prime numbers (multiplications numbers)
        /// </summary>
        public BigInteger N { get; set; }
        
        /// <summary>
        /// public exponent (public key)
        /// </summary>
        public BigInteger E { get; set; }
        
        /// <summary>
        /// secret exponent (secret key)
        /// </summary>
        public BigInteger D { get; set; }

    }

}
