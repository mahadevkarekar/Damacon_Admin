using System;
using System.Collections.Generic;
using System.Linq;
using Damacon.DAL.i18n.Entities;

namespace Damacon.DAL.i18n.Abstract
{
    internal abstract class BaseResourceProvider: IResourceProvider
    {
        // Cache list of resources
        private static Dictionary<string, ResourceEntry> resources = null;
        private static object lockResources = new object();

        public BaseResourceProvider() {
            CanCache = true; // By default, enable caching for performance
        }

        protected bool CanCache { get; set; } // Cache resources ?

        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        public object GetResource(string name, string culture)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Resource name cannot be null or empty.");

            if (string.IsNullOrWhiteSpace(culture))
                throw new ArgumentException("Culture name cannot be null or empty.");

            // normalize
            culture = culture.ToLowerInvariant();

            if (CanCache && resources == null) {
                // Fetch all resources
                
                lock (lockResources) {

                    if (resources == null) {
                        resources = ReadResources().ToDictionary(r => string.Format("{0}.{1}", r.Culture.ToLowerInvariant(), r.Name));
                    }
                }
            }

            if (CanCache) {
                return resources[string.Format("{0}.{1}", culture, name)].Value;
            }

            return ReadResource(name, culture).Value;

        }


        /// <summary>
        /// Returns all resources for all cultures. (Needed for caching)
        /// </summary>
        /// <returns>A list of resources</returns>
        protected abstract IList<ResourceEntry> ReadResources();


        /// <summary>
        /// Returns a single resource for a specific culture
        /// </summary>
        /// <param name="name">Resorce name (ie key)</param>
        /// <param name="culture">Culture code</param>
        /// <returns>Resource</returns>
        protected abstract ResourceEntry ReadResource(string name, string culture);

        public void RefreshCache()
        {
            if (CanCache)
            {
                resources = null;
                ReadResources();
            }
        }
    }
}
