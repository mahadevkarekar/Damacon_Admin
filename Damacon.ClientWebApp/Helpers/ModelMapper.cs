using AutoMapper;
using AutoMapper.Configuration;
using Damacon.ClientWebApp.Models;
using Damacon.DAL.Database.EF;
using System;

namespace Damacon.ClientWebApp.Helpers
{
    public sealed class ModelMapper
    {
        private static volatile ModelMapper instance;
        private static object syncRoot = new Object();
        public IMapper Mapper { get; set; }

        private ModelMapper()
        {
            CreateMapping();
        }

        public static ModelMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ModelMapper();
                    }
                }

                return instance;
            }
        }

        private void CreateMapping()
        {
            var cfg = new MapperConfigurationExpression();

            

            cfg.CreateMissingTypeMaps = false;

            var mapperConfig = new MapperConfiguration(cfg);
            Mapper = new Mapper(mapperConfig);
        }
    }
}