namespace JB
{
    using System;
    using System.Collections.Generic;

    public class MembershipViewData 
    {
        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        public Exception Exception { get; set; }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public IEnumerable<string> Messages { get; set; }
    }
}