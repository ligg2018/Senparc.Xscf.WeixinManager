﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Senparc.Scf.Core.Areas;
using Senparc.Scf.XscfBase;
using Senparc.Weixin.MP.AdvancedAPIs.UserTag;
using Senparc.Xscf.WeixinManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senparc.Xscf.WeixinManager
{
    public partial class Register : IXscfDatabase  //注册 XSCF 模块数据库（按需选用）
    {
        #region IXscfDatabase 接口

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserTag_WeixinUserConfigurationMapping());
            modelBuilder.ApplyConfiguration(new WeixinUserConfigurationMapping());
            modelBuilder.ApplyConfiguration(new UserTagConfigurationMapping());
        }

        public void AddXscfDatabaseModule(IServiceCollection services)
        {
            services.AddScoped<MpAccount>();
            services.AddScoped<MpAccountDto>();
            services.AddScoped<MpAccount_CreateOrUpdateDto>();

            services.AddScoped<WeixinUser>();
            services.AddScoped<WeixinUserDto>();

            services.AddScoped<UserTag>();
            services.AddScoped<UserTag_WeixinUser>();

            //AutoMap映射
            base.AddAutoMapMapping(profile =>
            {
                //MpAccount
                profile.CreateMap<MpAccountDto, MpAccount>();
                profile.CreateMap<MpAccount, MpAccountDto>();
                //WeixinUser
                profile.CreateMap<Weixin.MP.AdvancedAPIs.User.UserInfoJson, WeixinUser_UpdateFromApiDto>();
                profile.CreateMap<WeixinUser_UpdateFromApiDto, WeixinUser>();
                profile.CreateMap<WeixinUser, WeixinUser_UpdateFromApiDto>();
                profile.CreateMap<WeixinUserDto, WeixinUser>();
                profile.CreateMap<WeixinUser, WeixinUserDto>();
                //UserTag
                profile.CreateMap<UserTag, UserTag_CreateOrUpdateDto>();
                profile.CreateMap<TagJson_Tag, UserTag_CreateOrUpdateDto>();
                profile.CreateMap<UserTag_CreateOrUpdateDto, UserTag>();
                profile.CreateMap<TagJson_Tag, UserTag>();
                //UserTag_WeixinUser
                profile.CreateMap<UserTag_WeixinUserDto, UserTag_WeixinUser>();
                profile.CreateMap<UserTag_WeixinUser, UserTag_WeixinUserDto>();
            });
        }

        public const string DATABASE_PREFIX = "WeixinManager_";

        public string DatabaseUniquePrefix => DATABASE_PREFIX;

        public Type XscfDatabaseDbContextType => typeof(WeixinSenparcEntities);

        public override void DbContextOptionsAction(IRelationalDbContextOptionsBuilderInfrastructure dbContextOptionsAction, string assemblyName = null)
        {
            base.DbContextOptionsAction(dbContextOptionsAction, assemblyName);
        }

        #endregion
    }
}
