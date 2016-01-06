using System;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.Basic
{
    public partial class RewardScaleEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    var bll = new RewardLevelBll();
                    var model = bll.getModel(_id);
                    txtInterest.Value = model.Interest.ToString("0.00");
                    txtRewardScale.Value = model.RewardScale.ToString("0.00");
                    txtRankLevel.Value = model.RankLevel.ToString();
                    txtLevelDesc.Value = model.LevelDesc ?? "";
                    ckbEnable.Checked = !model.IsDisable;
                }
            }
        }

        protected void Operate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInterest.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入利息收益指标','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtRewardScale.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入奖励比例','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtRankLevel.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入奖励等级','warning', '');", true);
                return;
            }
            if (string.IsNullOrEmpty(txtLevelDesc.Value.Trim()))
            {
                ClientScript.RegisterClientScriptBlock(GetType(), "", "MessageAlertChild('请输入等级描述','warning', '');", true);
                return;
            }

            var bll = new RewardLevelBll();
            if (_id > 0)
            {
                var model = bll.getModel(_id);
                model.Interest = ConvertHelper.ToDecimal(txtInterest.Value.Trim());
                model.RewardScale = ConvertHelper.ToDecimal(txtRewardScale.Value.Trim());
                model.RankLevel = ConvertHelper.ToInt(txtRankLevel.Value.Trim());
                model.LevelDesc = txtLevelDesc.Value.Trim();
                model.UpdateTime = DateTime.Now;
                model.IsDisable = !ckbEnable.Checked;

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                                       bll.updateDimRewardLevel(model)
                                                           ? "MessageAlertChild('修改成功','success', '/Basic/RewardScaleSetting.aspx?columnId=" + ColumnId + "');"
                                                           : "MessageAlertChild('修改失败','error', '');", true);

            }
            else
            {
                var model = new DimRewardLevelModel()
                {
                    Interest = ConvertHelper.ToDecimal(txtInterest.Value.Trim()),
                    RewardScale = ConvertHelper.ToDecimal(txtRewardScale.Value.Trim()),
                    RankLevel = ConvertHelper.ToInt(txtRankLevel.Value.Trim()),
                    LevelDesc = txtLevelDesc.Value.Trim(),
                    IsDisable = !ckbEnable.Checked
                };

                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       bll.addDimRewardLevel(model) > 0
                                           ? "MessageAlertChild('添加成功','success', '/Basic/RewardScaleSetting.aspx?columnId=" + ColumnId + "');"
                                           : "MessageAlertChild('添加失败','error', '');", true);
            }
        }
    }
}