﻿/*
 *  Copyright 2012-2016 The Asn1Net Project
 *
 *  Licensed under the Apache License, Version 2.0 (the "License");
 *  you may not use this file except in compliance with the License.
 *  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 *  Unless required by applicable law or agreed to in writing, software
 *  distributed under the License is distributed on an "AS IS" BASIS,
 *  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *  See the License for the specific language governing permissions and
 *  limitations under the License.
 */
/*
 *  Written for the Asn1Net project by:
 *  Peter Polacko <peter.polacko+asn1net@gmail.com>
 */

namespace Net.Asn1.Writer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// Implementation of ASN.1 ENUMERATED
    /// </summary>
    public class Asn1Enumerated : Asn1Object<Enum>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Asn1Enumerated"/> class.
        /// </summary>
        /// <param name="content">Content to be encoded.</param>
        public Asn1Enumerated(Enum content)
            : base(Asn1Class.Universal, false, (int)Asn1Type.Enumerated, content)
        {
        }

        /// <inheritdoc/>
        public override byte[] Write()
        {
            var res = new List<byte>();
            var val = (int)Enum.Parse(this.Content.GetType(), this.Content.ToString(), true);
            var valBytes = new BigInteger(val).ToByteArray().Reverse().ToArray();

            res.Add(DerWriter.WriteTag(this.Asn1Class, this.Asn1Tag, this.Constructed));
            res.AddRange(DerWriter.WriteLength(valBytes, (Asn1Type)this.Asn1Tag));
            res.AddRange(valBytes);

            return res.ToArray();
        }
    }
}
