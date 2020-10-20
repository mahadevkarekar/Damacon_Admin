using System;
using System.Collections.Generic;
using Damacon.DAL.i18n.Abstract;
using Damacon.DAL.i18n.Entities;
using Damacon.DAL.Database.EF;
using System.Linq;

namespace Damacon.DAL.i18n.Concrete
{
    internal class DbResourceProvider : BaseResourceProvider
    {
        protected override IList<ResourceEntry> ReadResources()
        {
            var resources = new List<ResourceEntry>();

            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    var result = context.LanguageResources.ToList();
                    foreach (LanguageResource lang in result)
                    {
                        resources.Add(new ResourceEntry() { Name = lang.ResourceKey, Value = lang.EnglishValue, Culture = "en-us" });
                        resources.Add(new ResourceEntry() { Name = lang.ResourceKey, Value = lang.ItalianoValue, Culture = "it-IT" });
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
            }

            return resources;
        }

        protected override ResourceEntry ReadResource(string name, string culture)
        {
            ResourceEntry resource = null;

            try
            {
                using (DamaconEntities context = new DamaconEntities())
                {
                    var result = context.LanguageResources.FirstOrDefault(x => x.ResourceKey == name);
                    if (result != null)
                    {
                        if (culture == "en")
                        {
                            resource = new ResourceEntry() { Name = result.ResourceKey, Value = result.EnglishValue, Culture = "en-us" };
                        }
                        else if (culture == "it")
                        {
                            resource = new ResourceEntry() { Name = result.ResourceKey, Value = result.ItalianoValue, Culture = "it-IT" };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //LoggerManager.Logger.Error(ex);
            }
            return resource;            
        }
    }
}
