// -----------------------------------------------------------------------
// <copyright file="KerbalDataException.cs" company="OpenSauceSolutions">
// © 2013 OpenSauce Solutions
// </copyright>
// -----------------------------------------------------------------------

namespace KerbalData
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Runtime.Serialization;

    /// <summary>
    /// Base Exception used any time an error with the underlying cache API is surfaced. 
    /// </summary>
    public class KerbalDataException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataException" /> class.
        /// </summary>
        public KerbalDataException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataException" /> class.
        /// </summary>
        /// <param name="message">error message to surface, BE DESCRIPTIVE!</param>
        public KerbalDataException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataException" /> class.
        /// </summary>
        /// <param name="message">error message to surface, BE DESCRIPTIVE!</param>
        /// <param name="inner">Underlying exception.</param>
        public KerbalDataException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KerbalDataException" /> class.
        /// </summary>
        /// <param name="info">serialization meta-data <see cref="SerializationInfo" /></param>
        /// <param name="context">serialization stream <see cref="StreamingContext" /></param>
        protected KerbalDataException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
