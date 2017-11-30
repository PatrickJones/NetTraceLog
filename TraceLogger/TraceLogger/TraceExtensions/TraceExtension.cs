using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceLogger.TraceExtensions
{
    public static class TraceExtension
    {
        /// <summary>
        /// Returns the Stack collection as a string
        /// </summary>
        /// <param name="stack">LIFO Stack collection</param>
        /// <returns>Stack collection as a string</returns>
        public static string StackAsString(this Stack stack)
        {
            string result = "";

            if (stack != null && stack.Count > 0)
            {
                var sArray = stack.ToArray();
                var sb = new StringBuilder();

                for (int i = 0; i < stack.Count; i++)
                {
                    sb.Append(sArray[i].ToString());
                }

                result = sb.ToString();
            }

            if (result.Length > 4000)
            {
                return result.Substring(0, 4000);
            }

            return result;
        }
    }
}
