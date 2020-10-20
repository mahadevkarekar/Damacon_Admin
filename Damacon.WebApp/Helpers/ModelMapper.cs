using AutoMapper;
using AutoMapper.Configuration;
using Damacon.DAL.Database.EF;
using Damacon.DAL.i18n;
using Damacon.SharedWeb.Helpers;
using Damacon.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Damacon.WebApp.Helpers
{
    public sealed class ModelMapper
    {
        private int DocumentLockDayOfMonth = -1;

        private static volatile ModelMapper instance;
        private static object syncRoot = new Object();
        public IMapper Mapper { get; set; }

        private ModelMapper()
        {
            CreateMapping();
            int.TryParse(ConfigurationManager.AppSettings["DocumentLockDayOfMonth"], out DocumentLockDayOfMonth);
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
            cfg.CreateMap<User, UserModel>()
                .ForMember(dest => dest.LastModifyDateTimeFormatted, opt => opt.MapFrom(src => src.LastModifyDateTime.ToString(UIExtensions.DateTimeFormat)))
                .ForMember(dest => dest.UserTypeString, opt => opt.MapFrom(src => src.UserType.UserTypeEn));
                //.ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.StoreName));

            cfg.CreateMap<UserModel, User>();

          

            //cfg.CreateMap<DocumentType, DocumentTypeModel>();
            //cfg.CreateMap<DocumentTypeModel, DocumentType>();

            cfg.CreateMap<LanguageResource, LanguageResourceModel>().ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ResourceKey));
            cfg.CreateMap<LanguageResourceModel, LanguageResource>().ForMember(dest => dest.ResourceKey, opt => opt.MapFrom(src => src.ID));

          
            //cfg.CreateMap<DocumentRegistration, DocumentRegistrationModel>()
            //    .ForMember(dest => dest.LastModifyDateTimeFormatted, opt => opt.MapFrom(src => src.LastModifyDateTime.ToString(UIExtensions.DateTimeFormat)))
            //    .AfterMap(AfterMapDocumentRegistration);
            //cfg.CreateMap<DocumentRegistrationModel, DocumentRegistration>();

            cfg.CreateMap<EmailServiceItem, EmailServiceItemModel>()
                .ForMember(dest => dest.SendTime, opt => opt.MapFrom(src => src.SendTime == null ? null : src.SendTime.Value.ToString("HH:mm")));
            cfg.CreateMap<EmailServiceItemModel, EmailServiceItem>()
                .ForMember(dest => dest.SendTime, opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.SendTime) ? (DateTime?)null : DateTime.ParseExact(src.SendTime, "HH:mm", null)));

            cfg.CreateMap<EmailTaskResultItem, EmailTaskResultItemModel>()
               //  .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.StoreName))
                .ForMember(dest => dest.SentResult, opt => opt.MapFrom(src => Resources.GetResource(src.SentResult)));

         

            cfg.CreateMap<Documentation, DocumentationModel>()
                .ForMember(dest => dest.UploadDate, opt => opt.MapFrom(src => src.LastModifyDateTime))
              //  .ForMember(dest => dest.StoreIds, opt => opt.MapFrom(src => src.DocumentationStores.Select(x => x.StoreID)))
              //  .ForMember(dest => dest.StoreSelection, opt => opt.MapFrom(src => string.Join(",", src.DocumentationStores.Select(x => x.StoreID))))
                .ForMember(dest => dest.DocumentationTypeText, opt => opt.MapFrom(src => src.DocumentationType.TypeText));
            cfg.CreateMap<DocumentationModel, Documentation>();
            // .ForMember(dest => dest.DocumentationStores, opt => opt.MapFrom(src => src.StoreSelectionType == 1 ? new List<DocumentationStore>() : src.StoreIds.Select(x => new DocumentationStore() { StoreID = x })));

            //cfg.CreateMap<SalesBuyer, SalesBuyerModel>()
            //        .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Store.BrandDetail.Name))
            //        .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.StoreName))
            //        .ForMember(dest => dest.BuyerName, opt => opt.MapFrom(src => string.Format("{0} {1}", src.Buyer.FirstName, src.Buyer.Surname)));
            //cfg.CreateMap<SalesBuyerModel, SalesBuyer>();

            cfg.CreateMap<Client, ClientModel>()
                .ForMember(dest => dest.MobilePhoneEx, opt => opt.MapFrom(src => src.MobilePhone))
                .ForMember(dest => dest.EmailEx, opt => opt.MapFrom(src => src.Email));
                //.ForMember(dest => dest.RegisteredByStoreName, opt => opt.MapFrom(src => src.Store.StoreName));
            cfg.CreateMap<ClientModel, Client>()
                .ForMember(dest => dest.MobilePhone, opt => opt.MapFrom(src => src.MobilePhoneEx))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.EmailEx));

          

            cfg.CreateMap<Holiday, ExportHolidayModel>();
            cfg.CreateMap<ExportHolidayModel, Holiday>();

         

            cfg.CreateMap<DashBoardChartData, DashBoardChartDataModel>();
            cfg.CreateMap<DashBoardChartSeriesData, DashBoardChartSeriesDataModel>();
            cfg.CreateMap<DashBoardChartSeriesDataItem, DashBoardChartSeriesDataItemModel>();
            cfg.CreateMap<DashboardConfigModel, DashboardModel>();

            cfg.CreateMissingTypeMaps = false;

            var mapperConfig = new MapperConfiguration(cfg);
            Mapper = new Mapper(mapperConfig);
        }

      
        private WorkedHourTimeModel GetWorkedTypeByTime(WorkedHourModel workedHour, int time)
        {
            var item = workedHour.WorkedHourTimes.FirstOrDefault(x => x.WorkedHourTimeOfDay == time);
            if (item == null)
            {
                return new WorkedHourTimeModel() { WorkedHourTimeOfDay = time, WorkedHourID = workedHour.ID };
            }
            else
            {
                return item;
            }
        }

        private WorkersTimeTableEmployeeHourDataItemModel GetWorkedTypeByTime(WorkersTimeTableEmployeeDataItemModel workedHour, int time)
        {
            var item = workedHour.WorkersTimeTableEmployeeHourDataItems.FirstOrDefault(x => x.WorkedHourTimeOfDay == time);
            if (item == null)
            {
                return new WorkersTimeTableEmployeeHourDataItemModel() { WorkedHourTimeOfDay = time, WorkersTimeTableEmployeeDataItemID = workedHour.ID };
            }
            else
            {
                return item;
            }
        }

   

        
    }
}