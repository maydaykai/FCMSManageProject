using System;
using System.Data;
using System.IO;
using FCMSModel;
using ManageFcmsBll;
using ManageFcmsCommon;
using ManageFcmsModel;
using SignCert.Business.CertSignHelper;
using SignCert.Common;
using SignCert.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SignCert.Business.PdfHelper
{
    internal class CreditAssignmentContractPdfHelper : BasePdfHelper
    {
        /// <summary>
        ///     平台服务公司
        /// </summary>
        private const string PlantformKeyWord = "丙方: 深圳融金宝互联网金融服务有限公司";

        private readonly CreditAssignmentPdfModel _model;
        private readonly string _plantformSealPath = CommonData.SealFolder + @"深圳融金宝互联网金融服务有限公司.dat";

        public CreditAssignmentContractPdfHelper(CreditAssignmentPdfModel model)
        {
            _model = model;
        }

        public override string FileName
        {
            get { return _model.FileName; }
        }

        public override bool CanExecute()
        {
            bool exist = File.Exists(_model.FileName);

            if (!exist)
            {
                return true;
            }

            var fileInfo = new FileInfo(_model.FileName);
            if (fileInfo.Length < MiniFileSize)
            {
                return true;
            }

            return false;
        }

        public override void Execute()
        {
            BuildPdf();

            SignPlantformAndGuaranteeSeal();
        }

        private void SignPlantformAndGuaranteeSeal()
        {
            var cgcs = new ChapterGraphicsCertSign();
            cgcs.Init(_plantformSealPath, _model.FileName, PlantformKeyWord).Sign();
        }

        public void BuildPdf()
        {
            CreditAssignmentDetailModel caDetailModel = _model.CreditAssignmentInfo;
            MemberInfoModel memberInfo = _model.MemberInfo;
            LoanModel loanInfo = new LoanBll().GetLoanModel(caDetailModel.OldLoanId);


            var myDocument = new Document(PageSize.A4);

            //打开文档
            string path = Path.GetDirectoryName(_model.FileName);
            if (path != null && !Directory.Exists(path)) Directory.CreateDirectory(path);

            PdfWriter.GetInstance(myDocument, new FileStream(_model.FileName, FileMode.Create));

            //加密
            //writer.SetEncryption(PdfWriter.STRENGTH128BITS, "user", "user", PdfWriter.AllowCopy | PdfWriter.AllowPrinting);

            myDocument.Open();

            //1.增加标题
            var header = new Paragraph {Alignment = Element.ALIGN_CENTER};
            header.Add(new Chunk("债权转让合同", Msyh12Bold));
            myDocument.Add(header);

            myDocument.Add(NewLine);

            //2.编号
            var sn = new Paragraph {Alignment = Element.ALIGN_RIGHT};
            sn.Add(new Chunk("编号：", Msyh10));
            sn.Add(new Chunk(caDetailModel.LoanNumber.PadRight(20), Msyh10));
            myDocument.Add(sn);

            myDocument.Add(NewLine);

            //3.参与方列表甲方
            var memberList = new Paragraph {Alignment = Element.ALIGN_LEFT};
            memberList.Add(new Chunk("甲方(转让方)：", Msyh10Bold));
            memberList.Add(new Chunk(memberInfo.RealName.Replace(memberInfo.RealName, "***").PadRight(30),
                                     Msyh10BolderUnderLine));
            memberList.Add(new Chunk("身份证号: ", Msyh10Bold));
            memberList.Add(
                new Chunk(
                    memberInfo.IdentityCard.Replace(
                        memberInfo.IdentityCard.Substring(0, memberInfo.IdentityCard.Length - 3), "************")
                              .PadRight(30), Msyh10BolderUnderLine));
            memberList.Add(NewLine);
            myDocument.Add(memberList);

            myDocument.Add(NewLine);

            //4.参与方列表乙方
            var memberList1 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            memberList1.Add(new Chunk("乙方（受让方）:  ", Msyh10Bold));
            memberList1.Add(new Chunk("（如下表所述）", Msyh8));
            myDocument.Add(memberList1);

            //5.乙方名单：
            var memberListTable = new Paragraph {Alignment = Element.ALIGN_CENTER};
            var table = new PdfPTable(3) {WidthPercentage = 100};
            table.AddCell(new PdfPCell(new Phrase("融金宝用户名", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            table.AddCell(new PdfPCell(new Phrase("身份证号码", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_CENTER
                });
            table.AddCell(new PdfPCell(new Phrase("申购本金（¥）", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });


            if (_model.BidList != null)
            {
                for (int i = 0; i < _model.BidList.Count; i++)
                {
                    DataRow dr = _model.BidList[i];
                    string memberName = dr["MemberName"].ToString().Substring(0, 1) + "*****" +
                                        dr["MemberName"].ToString().Substring(dr["MemberName"].ToString().Length - 1, 1);
                    table.AddCell(new PdfPCell(new Phrase(memberName, Msyh8))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                    string identityCard =
                        dr["IdentityCard"].ToString()
                                          .Replace(
                                              dr["IdentityCard"].ToString()
                                                                .Substring(0, dr["IdentityCard"].ToString().Length - 3),
                                              "************");

                    table.AddCell(new PdfPCell(new Phrase(identityCard, Msyh8))
                        {
                            Colspan = 1,
                            HorizontalAlignment = Element.ALIGN_CENTER
                        });

                    table.AddCell(
                        new PdfPCell(new Phrase(ConvertHelper.ToDecimal(dr["BidAmount"].ToString()).ToString("N2"),
                                                Msyh8))
                            {
                                Colspan = 1,
                                HorizontalAlignment = Element.ALIGN_RIGHT
                            });
                }
            }


            myDocument.Add(new Chunk("").setLineHeight(5));
            memberListTable.Add(table);
            myDocument.Add(memberListTable);

            myDocument.Add(NewLine);

            //6.参与方列表丙方
            var memberThird = new Paragraph {Alignment = Element.ALIGN_LEFT};
            memberThird.Add(new Chunk("丙方（平台方）: ", Msyh10Bold));
            memberThird.Add(new Chunk("深圳融金宝互联网金融服务有限公司", Msyh10));
            memberThird.Add(new Chunk(Environment.NewLine));
            memberThird.Add(new Chunk("营业执照号:", Msyh10Bold));
            memberThird.Add(new Chunk("30613792-3", Msyh8));
            myDocument.Add(memberThird);

            myDocument.Add(NewLine);

            //7.合同正文
            var content = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content.Add(new Phrase("鉴于：", Msyh8));
            memberThird.Add(new Chunk(Environment.NewLine));
            content.Add(new Phrase("甲方此前通过丙方撮合，持有债权，现因资金周转需要，拟将持有的债权的全部或部分进行转让；" +
                                   "乙方同意受让其全部或部分债权，甲、乙双方通过丙方自行创立的投融资信息中介服务平台（网址：www.rjb777.com，下称融金宝）达成债权转让与受让合意。" +
                                   "基于以上，三方本着实事求是、平等互利、真诚合作、共同发展的原则，经过友好协商，达成协议如下：", Msyh8));
            myDocument.Add(content);

            myDocument.Add(NewLine);

            //8.合同正文第一条
            DateTime transferSuccessDate = caDetailModel.FullScaleTime;
            var content1 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content1.Add(new Chunk("第一条 转让的标的债权", Msyh10Bold));
            content1.Add(NewLine);
            content1.Add(new Phrase(transferSuccessDate.ToString("yyyy年MM月dd日") +
                                    "甲方通过出借或债权受让持有编号为：" +
                                    loanInfo.LoanNumber +
                                    "《借款及担保合同》项下本金 ¥" +
                                    caDetailModel.LoanAmount.ToString("N2") +
                                    "元及相应利息的债权。现甲方拟以债权转让日（" +
                                    transferSuccessDate.ToString("yyyy年MM月dd日") +
                                    "）为基准日，" +
                                    "将该债权中的全部债权即具体对应为本金¥" +
                                    caDetailModel.LoanAmount.ToString("N2") +
                                    "元的债权，以下简称为“标的债权”）" +
                                    "转让给乙方。基准日前已产生的利息归甲方所有，基准日后产生的利息归乙方收取。", Msyh8));
            myDocument.Add(content1);

            myDocument.Add(NewLine);

            //9.合同正文第二条
            var content2 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content2.Add(new Chunk("第二条 转让价款", Msyh10Bold));
            content2.Add(NewLine);
            content2.Add(new Phrase("甲方拟将上述标的债权以人民币¥" +
                                    caDetailModel.LoanAmount.ToString("N2") +
                                    "元转让给乙方，乙方同意受让。", Msyh8));
            myDocument.Add(content2);

            myDocument.Add(NewLine);

            //10.合同正文第三条
            var content3 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content3.Add(new Chunk("第三条 转让价款的支付", Msyh10Bold));
            content3.Add(NewLine);
            content3.Add(new Phrase("自本协议签署之日起两个工作日内，乙方需支付转让价款给甲方，" +
                                    "甲方上述标的债权未结算的收益将按照基准日至原债权当期收款日之间实际持有债权天数由融金宝系统自动清分收益给甲方，剩余收益结算给乙方。", Msyh8));
            myDocument.Add(content3);

            myDocument.Add(NewLine);

            //11.合同正文第四条
            var content4 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content4.Add(new Chunk("第四条 转让前后权利义务", Msyh10Bold));
            content4.Add(NewLine);
            content4.Add(new Phrase("转让完成后，甲方不再享有前述标的债权。" +
                                    "《借款及担保合同》中关于标的债权的全部权利义务皆由乙方享有及承担，包括丙方对标的债权需承担的权利义务等。", Msyh8));
            myDocument.Add(content4);

            myDocument.Add(NewLine);

            //12.合同正文第五条
            var content5 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content5.Add(new Chunk("第五条 标的债权的转移及通知", Msyh10Bold));
            content5.Add(new Chunk(Environment.NewLine));
            content5.Add(new Phrase("甲方通过丙方撮合，与乙方达成本债权转让合意后，自基准日起，标的债权转移至乙方。" +
                                    "甲方有义务通知借款人标的债权转移事项。各方同意，丙方以站内信、邮件等形式进行标的债权的转让的，皆为有效通知形式。", Msyh8));
            myDocument.Add(content5);

            myDocument.Add(new Chunk("\n"));

            //13.合同正文第六条
            var content7 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content7.Add(new Chunk("第六条 各方权利义务", Msyh10Bold));
            var contentComment7 = new List(false, 10);
            contentComment7.Add(new ListItem(new Phrase("甲方权利义务", Msyh8)));
            var contentComment71 = new List(false, 10);
            contentComment71.Add(new ListItem(new Phrase("有权实施本协议项下的债权转让并能够独立承担民事责任；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("其转让的债权系合法、有效的债权；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("本协议生效后，及时就本债权转让事宜向其借款人出具债权转让通知书；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("本协议生效前，转让标的从未转让给任何第三方；", Msyh8)));
            contentComment7.Add(contentComment71);

            contentComment7.Add(new ListItem(new Phrase("乙方权利义务", Msyh8)));
            var contentComment72 = new List(false, 10);
            contentComment72.Add(new ListItem(new Phrase("有权受让本协议项下的债权并能独立承担民事责任；", Msyh8)));
            contentComment72.Add(new ListItem(new Phrase("其受让本协议项下的债权是乙方独立审慎的判断结果。", Msyh8)));
            contentComment7.Add(contentComment72);

            contentComment7.Add(new ListItem(new Phrase("丙方权利义务", Msyh8)));
            var contentComment73 = new List(false, 10);
            contentComment73.Add(new ListItem(new Phrase("丙方根据甲、乙方的授权，通过融金宝平台，" +
                                                         "为甲、乙双方提供对接、撮合、资金监管等服务，并有权向甲、乙方收取相关费用，费用标准根据融金宝平台规则计收。", Msyh8)));
            contentComment73.Add(
                new ListItem(new Phrase("丙方协助甲方发送债权转让的通知，该等债权转让可通过融金宝平台的站内信或绑定的电子邮件进行通知，各方认可此种通知为有效合法。", Msyh8)));
            contentComment7.Add(contentComment73);

            content7.Add(contentComment7);
            myDocument.Add(content7);

            myDocument.Add(NewLine);

            //14.合同正文第七条
            var content8 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content8.Add(new Chunk("第七条 违约责任", Msyh10Bold));
            content8.Add(new Chunk(Environment.NewLine));
            content8.Add(new ListItem(new Phrase("任一方违反本协议约定的，违约方应承担违约责任，并向守约方赔偿因此遭受的全部损失。", Msyh8)));
            myDocument.Add(content8);

            myDocument.Add(NewLine);

            //15.合同正文第八条
            var content9 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content9.Add(new Chunk("第八条 保密条款", Msyh10Bold));
            content9.Add(new Chunk(Environment.NewLine));
            content9.Add(new Phrase("各方保证，对讨论、签订、履行本协议过程中所获悉的属于其他方的且无法自公开渠道获得的文件和资料" +
                                    "（包括但不限于个人隐私、商业计划、运营资金、财务信息等）予以保密。否则，应承担相应的违约责任并赔偿由此造成的全部损失。", Msyh8));
            myDocument.Add(content9);

            myDocument.Add(NewLine);


            //16.合同正文第九条
            var content11 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content11.Add(new Chunk("第九条 争议的解决", Msyh10Bold));
            content11.Add(NewLine);
            content11.Add(
                new Phrase("本协议适用中华人民共和国法律和法规，各方权益受中华人民共和国法律保障。" +
                           "因本协议发生纠纷，各方协商解决；协商不成时，任何一方可向本协议签订地有管辖权人民法院提出诉讼。", Msyh8));
            myDocument.Add(content11);

            myDocument.Add(NewLine);


            //17.合同正文第十条
            var content12 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content12.Add(new Chunk("第十条 协议的生效及其他", Msyh10Bold));
            content12.Add(new Chunk(Environment.NewLine));
            var contentCommentP12C1 = new List(false, 10);
            contentCommentP12C1.SetListSymbol("\u2022");
            contentCommentP12C1.Add(new ListItem(new Phrase("各方在此再次重申，各方已经全面、完整和准确地理解了本协议全部条款的内容，并愿意接受本协议的约束。", Msyh8)));
            contentCommentP12C1.Add(new ListItem(new Phrase("各方确认并同意，基于保障资金安全及实现债权的需要，各方另行签署的相关协议亦具有独立的法律效力。", Msyh8)));
            contentCommentP12C1.Add(new ListItem(new Phrase("各方确认，各方自行保管在丙方提供的融金宝平台注册的账户信息及密码，同时该账户为各自资金管理账户。" +
                                                            "若有遗失，应及时书面通知丙方。各方承诺自行操作自己的账户，由此所产生的一切后果自行承担。", Msyh8)));
            contentCommentP12C1.Add(new ListItem(new Phrase("本协议采用数据电文形式，自各方网上签署确认之日起生效。各方确认该数据电文协议具有法律认可的效力。", Msyh8)));
            content12.Add(contentCommentP12C1);
            myDocument.Add(content12);

            myDocument.Add(NewLine);

            //18.签名栏
            var signContent = new Paragraph {Alignment = Element.ALIGN_CENTER};
            var signTable = new PdfPTable(1) {WidthPercentage = 100};
            signTable.AddCell(
                new PdfPCell(new Phrase("甲方: " + memberInfo.RealName.Replace(memberInfo.RealName, "***"), Msyh8))
                    {
                        Colspan = 1,
                        HorizontalAlignment = Element.ALIGN_LEFT,
                        Border = 0,
                        MinimumHeight = 100
                    });
            signTable.AddCell(new PdfPCell(new Phrase("乙方: （见出借人列表）", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0,
                    MinimumHeight = 100
                });
            signTable.AddCell(new PdfPCell(new Phrase(PlantformKeyWord, Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0,
                    MinimumHeight = 100
                });
            signContent.Add(signTable);
            myDocument.Add(signContent);

            var wcs = new WordsCertSign();
            //甲方（转让方）签文字
            string keywordMember = "甲方同意:" + _model.CreditAssignmentInfo.LoanNumber;
            wcs.Init(myDocument, _model.MemberInfo.MemberID, memberInfo.RealName.Replace(memberInfo.RealName, "***"), keywordMember, "甲方").Sign();

            ////乙方（受让方）签文字
            if (_model.BidList != null)
                foreach (DataRow investor in _model.BidList)
                {
                    string memberName = investor["MemberName"].ToString().Substring(0, 1) + "*****" +
                    investor["MemberName"].ToString().Substring(investor["MemberName"].ToString().Length - 1, 1);
                    string keywordInvestor = "乙方同意:" + _model.CreditAssignmentInfo.LoanNumber;
                    wcs.Init(myDocument, Convert.ToInt32(investor["MemberID"]), memberName, keywordInvestor, "乙方").Sign();
                }

            //关闭文档
            myDocument.Close();
        }
    }
}