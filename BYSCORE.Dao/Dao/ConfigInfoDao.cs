using System;
using System.Collections.Generic;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Dao;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao
{
    public class ConfigInfoDao : DaoBase
    {
        private readonly CoreDbContext _coreDbContext;

        public ConfigInfoDao(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="configInfo">configInfo.</param>
        public bool Add(ConfigInfo configInfo)
        {
            _coreDbContext.ConfigInfo.Add(configInfo);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code删除
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="code">code.</param>
        public bool Delete(string code, int type)
        {
            var model = _coreDbContext.ConfigInfo.Where(t => t.IsDelete == false && t.Code == code && t.Type == type).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsDelete = true;
            _coreDbContext.ConfigInfo.Update(model);

            // 递归删除所有子集
            DelChild(model.Id);

            return _coreDbContext.SaveChanges() > 0;
        }

        private void DelChild(int parid)
        {
            var configs = _coreDbContext.ConfigInfo.Where(t => t.ParentId == parid).ToList();
            foreach (var item in configs)
            {
                item.IsDelete = true;
                _coreDbContext.ConfigInfo.Update(item);
                DelChild(item.Id);
            }
        }

        /// <summary>
        /// 根据code获取数据
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="code">Code.</param>
        public ConfigInfo Get(string code)
        {
            return _coreDbContext.ConfigInfo.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetListByPage(PublicQuery query)
        {
            var list = _coreDbContext.ConfigInfo.Where(w => w.IsDelete == false && w.Type == query.ConfigType);

            if (query.KeyWorld.IsNotNullOrEmpty())
            {
                list = list.Where(w => w.Name.Contains(query.KeyWorld));
            }
            list = list.OrderBy(t => t.Id).Paging<ConfigInfo>(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, ConfigInfoList = list.AsNoTracking().ToList().OrderBy(x => x.SortNumber) };
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<ConfigInfo> GetList()
        {
            return _coreDbContext.ConfigInfo.Where(w => w.IsDelete == false).AsNoTracking().ToList();
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<ConfigInfo> GetList(int type)
        {
            return _coreDbContext.ConfigInfo.Where(w => w.IsDelete == false && w.Type == type).OrderBy(t => t.Id).AsNoTracking().ToList().OrderBy(x => x.SortNumber);
        }

        /// <summary>
        /// 根据名称获取配置信息
        /// </summary>
        /// <returns>The config info by name.</returns>
        /// <param name="name">Name.</param>
        public ConfigInfo GetConfigInfoByName(string name, int type)
        {
            return _coreDbContext.ConfigInfo.Where(w => w.IsDelete == false && w.Name == name && w.Type == type).AsNoTracking().FirstOrDefault();
        }

        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="model">Model.</param>
        public bool Update(ConfigInfo model)
        {
            _coreDbContext.ConfigInfo.Attach(model);
            _coreDbContext.Entry(model).State = EntityState.Modified;
            return _coreDbContext.SaveChanges() > 0;
        }

    }
}
