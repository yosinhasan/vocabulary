using Renault.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renault.Interfaces
{
    public interface ICardManager
    {
        /// <summary>
        ///  Gets from server Card by license code.
        /// </summary>
        /// <param name="url">server url</param>
        /// <returns>Card</returns>
        Task<Card> get(string url);
    }
}
