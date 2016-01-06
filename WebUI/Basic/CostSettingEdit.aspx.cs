using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;

namespace WebUI.Basic
{
    public partial class CostSettingEdit : BasePage
    {
        private int _id;
        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ConvertHelper.QueryString(Request, "id", 0);
            if (!IsPostBack)
            {
                BindFeeType();
                if (_id > 0)
                {
                    selFeeType.Disabled = true;
                    var costSettingBll = new CostSettingBll();
                    var costSettingModel = costSettingBll.GetModel(_id);
                    selFeeType.Value = costSettingModel.FeeType.ToString();
                    selCalculationMode.Value = costSettingModel.CalculationMode.ToString();
                    txtCalInitialValue.Value = costSettingModel.CalInitialValue.ToString();
                    txtCalInitialProportion.Value = costSettingModel.CalInitialProportion.ToString();
                    selIncreasingMode.Value = costSettingModel.IncreasingMode.ToString();
                    txtIncreasUnit.Value = costSettingModel.IncreasUnit.ToString();
                    txtIncreasProportion.Value = costSettingModel.IncreasProportion.ToString();
                    chkEnableStatus.Checked = costSettingModel.EnableStatus;
                }
            }
        }

        protected void Operate_Btn_Click(object sender, EventArgs e)
        {
            var costSettingBll = new CostSettingBll();

            if (_id > 0)
            {
                var costSettingModel = costSettingBll.GetModel(_id);
                costSettingModel.CalculationMode = ConvertHelper.ToInt(selCalculationMode.Value.Trim());
                costSettingModel.CalInitialValue = ConvertHelper.ToDecimal(txtCalInitialValue.Value.Trim());
                costSettingModel.CalInitialProportion = ConvertHelper.ToDecimal(txtCalInitialProportion.Value.Trim());
                costSettingModel.IncreasingMode = ConvertHelper.ToInt(selIncreasingMode.Value.Trim());
                costSettingModel.IncreasUnit = ConvertHelper.ToDecimal(txtIncreasUnit.Value.Trim());
                costSettingModel.IncreasProportion = ConvertHelper.ToDecimal(txtIncreasProportion.Value.Trim());
                costSettingModel.EnableStatus = chkEnableStatus.Checked;
                costSettingModel.UpdateTime = DateTime.Now;
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                       costSettingBll.Update(costSettingModel)
                           ? "MessageAlert('更新成功。','success', '');"
                           : "MessageAlert('更新失败。','error', '');", true);
            }
            else
            {
                var costSettingModel = new CostSettingModel
                    {
                        FeeType = ConvertHelper.ToInt(selFeeType.Value.Trim()),
                        CalculationMode = ConvertHelper.ToInt(selCalculationMode.Value.Trim()),
                        CalInitialValue = ConvertHelper.ToDecimal(txtCalInitialValue.Value.Trim()),
                        CalInitialProportion = ConvertHelper.ToDecimal(txtCalInitialProportion.Value.Trim()),
                        IncreasingMode = ConvertHelper.ToInt(selIncreasingMode.Value.Trim()),
                        IncreasUnit = ConvertHelper.ToDecimal(txtIncreasUnit.Value.Trim()),
                        IncreasProportion = ConvertHelper.ToDecimal(txtIncreasProportion.Value.Trim()),
                        EnableStatus = chkEnableStatus.Checked,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    };
                ClientScript.RegisterClientScriptBlock(GetType(), "",
                                       costSettingBll.Add(costSettingModel) > 0
                                           ? "MessageAlert('添加成功。','success', '/Basic/CostSetting.aspx');"
                                           : "MessageAlert('添加失败。','error', '');", true);

            }
        }


        //绑定用于设置的费用类型
        private void BindFeeType()
        {
            selFeeType.DataSource = FeeType.GetSetFeeList();
            selFeeType.DataValueField = "Key";
            selFeeType.DataTextField = "Value";
            selFeeType.DataBind();
            selFeeType.Items.Insert(0, new ListItem("--请选择--", ""));
        }


    }
}