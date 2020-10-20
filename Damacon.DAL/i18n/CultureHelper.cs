using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Damacon.DAL.i18n
{
    public static class CultureHelper
    {
        // Include ONLY cultures you are implementing
        private static readonly List<string> _cultures = new List<string> {
        "en-US",  // first culture is the DEFAULT
        "it-IT", // Italian NEUTRAL culture
        };

        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture "en-US"
        /// </summary>
        /// <param name="name" />Culture's name (e.g. en-US)</param>
        public static string GetImplementedCulture(string name)
        {
            // make sure it's not null
            if (string.IsNullOrEmpty(name))
                return GetDefaultCulture(); // return Default culture
            
            if (_cultures.Where(c => c.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Count() > 0)
                return name; // accept it
                             // Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
                             // the function will return closes match that is "en-US" because at least the language is the same (ie English)  
           
            return GetDefaultCulture(); // return Default culture as no match found
        }

        /// <summary>
        /// Returns default culture name which is the first name decalared (e.g. en-US)
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultCulture()
        {
            return _cultures[1]; // return Default culture
        }
    }
}
