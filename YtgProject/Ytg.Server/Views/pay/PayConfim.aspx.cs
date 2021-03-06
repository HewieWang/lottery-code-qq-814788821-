﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ytg.BasicModel;
using Ytg.Comm;
using Ytg.Core.Service;

namespace Ytg.ServerWeb.Views.pay
{
    public partial class PayConfim : BasePage
    {
        public string OpenUrl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request.QueryString["orderid"]))
                {
                    Response.End();
                    return;
                }
                //构建支付链接
                //根据订单唯一id获取订单信息
                IRecordTempService recordService = IoC.Resolve<IRecordTempService>();
                var item = recordService.GetAll().Where(c => c.Guid == Request.QueryString["orderid"]).FirstOrDefault();
                if (null == item || item.IsCompled)
                {
                    Alert("不存或已完成订单，请不要重复提交！");
                    return;
                }
                //根据相关参数构建
                OpenUrl = BuilderMoBaoUrl(item);
            }
        }

        private string BuilderMoBaoUrl(RecordTemp item)
        {
            return Ytg.ServerWeb.BootStrapper.SiteHelper.GetParDns() + "/Views/pay/mobao/pay.aspx?tok=" + item.Guid;
        }
    }
}