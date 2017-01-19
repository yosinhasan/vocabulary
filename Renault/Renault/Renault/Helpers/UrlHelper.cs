using System;

namespace Renault.Helpers
{
    /// <summary>Application command helper.</summary>
    /// <author>Yosin Hasan<yosinhasan@gmail.com></author>
    /// <version>1.0</version>
    public sealed class UrlHelper
    {
        /// <summary>
        /// Generates command.
        /// </summary>
        /// <param name="precommand">Server command</param>
        /// <param name="param">Parameter for server command</param>
        /// <returns>Generated command</returns>
        /// <see cref="CommandConfig"/>
        public static String generateCommand(String precommand, Object param = null)
        {
            string s;
            if (param == null)
            {
                s = string.Format(precommand);
            } else
            {
               s = string.Format(precommand, param);
            }
            return s;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="precommand"></param>
        /// <param name="param">Parameter's array for server command</param>
        /// <returns>Generated command</returns>
        /// <see cref="CommandConfig"/>
        public static String generateCommand(String precommand, Object[] param = null)
        {
            string s;
            if (param == null)
            {
                s = string.Format(precommand);
            }
            else
            {
                s = string.Format(precommand, param);
            }
            return s;
        }
    }
}
