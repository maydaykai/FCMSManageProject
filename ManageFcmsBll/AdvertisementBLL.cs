/*************************************************************
Author:		 卢侠勇
Create date: 2015-12-07
Description: 广告管理逻辑类
Update: 
**************************************************************/
using System.Data;
using ManageFcmsDal;
using System.Collections;
using System.Collections.Generic;

namespace ManageFcmsBll
{
    public class AdvertisementBLL
    {
        private static AdvertisementBLL _item;
        public static AdvertisementBLL Instance
        {
            get { return _item = (_item ?? new AdvertisementBLL()); }
        }

        #region 用户来源明细
        /// <summary>
        /// 用户来源明细
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-07</remarks>
        public DataTable GetUserSourceDetails(string filter, int pageIndex, int pageSize, out int total)
        {
            return AdvertisementDAL.Instance.GetUserSourceDetails(filter, pageIndex, pageSize, out total);
        }
        #endregion

        #region 渠道费用列表
        /// <summary>
        /// 渠道费用列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelFeeList(string filter, int pageIndex, int pageSize, out int total)
        {
            return AdvertisementDAL.Instance.GetChannelFeeList(filter, pageIndex, pageSize, out total);
        }
        #endregion
        
        #region 渠道费用明细列表
        /// <summary>
        /// 渠道费用明细列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelFeeDetailsList(string filter, int pageIndex, int pageSize, out int total)
        {
            return AdvertisementDAL.Instance.GetChannelFeeDetailsList(filter, pageIndex, pageSize, out total);
        }
        #endregion

        #region 获取渠道列表
        /// <summary>
        /// 获取渠道列表
        /// </summary>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelList()
        {
            return AdvertisementDAL.Instance.GetChannelList();
        }
        #endregion

        #region 获取渠道费用详情
        /// <summary>
        /// 获取渠道费用详情
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannelFee(string fields, string filter)
        {
            return AdvertisementDAL.Instance.GetChannelFee(fields, filter);
        }
        #endregion

        #region 添加渠道费用
        /// <summary>
        /// 添加渠道费用
        /// </summary>
        /// <param name="dic">渠道费用数据</param>
        /// <returns>-1 数据重复，0 失败， >0 成功</returns>
        /// <remarks>add 卢侠勇,2015-12-10</remarks>
        public int AddChannelFee(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.AddChannelFee(dic);
        }
        #endregion

        #region 修改渠道费用
        /// <summary>
        /// 修改渠道费用
        /// </summary>
        /// <param name="dic">渠道费用数据</param>
        /// <returns></returns>
        /// <returns>0 失败， >0 成功</returns>
        /// <remarks>add 卢侠勇,2015-12-10</remarks>
        public int SetChannelFee(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.SetChannelFee(dic);
        }
        #endregion

        #region 投资用户明细
        /// <summary>
        /// 投资用户明细
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-11</remarks>
        public DataTable GetBiddingUserDetailsList(string filter, int pageIndex, int pageSize, out int total)
        {
            return AdvertisementDAL.Instance.GetBiddingUserDetailsList(filter, pageIndex, pageSize, out total);
        }
        #endregion

        #region 渠道号列表
        /// <summary>
        /// 渠道号列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public DataTable GetChannelList(string filter, int pageIndex, int pageSize, out int total)
        {
            return AdvertisementDAL.Instance.GetChannelList(filter, pageIndex, pageSize, out total);
        }
        #endregion

        #region 获取渠道号详情
        /// <summary>
        /// 获取渠道号详情
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-09</remarks>
        public DataTable GetChannel(string fields, string filter)
        {
            return AdvertisementDAL.Instance.GetChannel(fields, filter);
        }
        #endregion

        #region 添加渠道
        /// <summary>
        /// 添加渠道
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public string AddChannel(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.AddChannel(dic);
        }
        #endregion

        #region 添加渠道二级分类
        /// <summary>
        /// 添加渠道二级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public string AddFoxproChannel(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.AddFoxproChannel(dic);
        }
        #endregion

        #region 添加渠道三级分类
        /// <summary>
        /// 添加渠道三级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-14</remarks>
        public string AddFlyersChannel(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.AddFlyersChannel(dic);
        }
        #endregion

        #region 修改渠道
        /// <summary>
        /// 修改渠道
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetChannel(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.SetChannel(dic);
        }
        #endregion

        #region 修改渠道二级分类
        /// <summary>
        /// 修改渠道二级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetFoxproChannel(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.SetFoxproChannel(dic);
        }
        #endregion

        #region 修改渠道三级分类
        /// <summary>
        /// 修改渠道三级分类
        /// </summary>
        /// <param name="dic">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetFlyersChannel(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.SetFlyersChannel(dic);
        }
        #endregion

        #region 渠道商帐户列表
        /// <summary>
        /// 渠道商帐户列表
        /// </summary>
        /// <param name="filter">查询条件</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public DataTable GetChannelUserList(string filter, int pageIndex, int pageSize, out int total)
        {
            return AdvertisementDAL.Instance.GetChannelUserList(filter, pageIndex, pageSize, out total);
        }
        #endregion

        #region 添加渠道商帐户
        /// <summary>
        /// 添加渠道商帐户
        /// </summary>
        /// <param name="dic">渠道商数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string AddChannelUser(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.AddChannelUser(dic);
        }
        #endregion

        #region 查询渠道商帐户信息
        /// <summary>
        /// 查询渠道商帐户信息
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public DataTable GetChannelUser(string fields, string filter)
        {
            return AdvertisementDAL.Instance.GetChannelUser(fields, filter);
        }
        #endregion
        
        #region 修改渠道商帐户
        /// <summary>
        /// 修改渠道商帐户
        /// </summary>
        /// <param name="dic">渠道商数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-17</remarks>
        public string SetChannelUser(IDictionary<string, object> dic)
        {
            return AdvertisementDAL.Instance.SetChannelUser(dic);
        }
        #endregion

        #region 获取渠道效果分析列表
        /// <summary>
        /// 获取渠道效果分析列表
        /// </summary>
        /// <param name="fields">查询字段</param>
        /// <param name="filter">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="total">总记录数</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-18</remarks>
        public DataTable GetChannelAnalyzeList(string fields, string filter, string order, int pageIndex, int pageSize, out int total)
        {
            return AdvertisementDAL.Instance.GetChannelAnalyzeList(fields, filter, order, pageIndex, pageSize, out total);
        }
        #endregion

        #region 渠道号导入
        /// <summary>
        /// 渠道号导入
        /// </summary>
        /// <param name="dt">渠道数据</param>
        /// <returns></returns>
        /// <remarks>add 卢侠勇,2015-12-22</remarks>
        public string ImportChannel(DataTable dt)
        {
            return AdvertisementDAL.Instance.ImportChannel(dt);
        }
        #endregion
    }
}
