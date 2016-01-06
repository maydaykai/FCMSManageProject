using System;
using System.Globalization;
using System.IO;
using ManageFcmsCommon;
using ManageFcmsModel;
using SignCert.Business.CertSignHelper;
using SignCert.Common;
using SignCert.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SignCert.Business.PdfHelper
{ 
    /// <summary>
    ///     采用约定优先：即对于动态章，按其真实的中文名存放
    ///     该合同为两个公司的章 + 投资人签名 + 借款人签名
    /// </summary>
    internal class LoanGuaranteeContractPdfHelper : BasePdfHelper
    {
        /// <summary>
        ///     平台服务公司
        /// </summary>
        private const string PlantformKeyWord = "丙方: 深圳融金宝互联网金融服务有限公司";

        /// <summary>
        ///     担保公司
        /// </summary>
        private readonly string _guaranteeKeyWord = "丁方: XXX担保有限公司";

        private readonly string _guaranteeSealPath = CommonData.SealFolder + @"GuaranteeSeal.dat";

        private readonly LoanPdfModel _model;
        private readonly string _plantformSealPath = CommonData.SealFolder + @"深圳融金宝互联网金融服务有限公司.dat";

        public LoanGuaranteeContractPdfHelper(LoanPdfModel model)
        {
            _model = model;

            _guaranteeSealPath = CommonData.SealFolder + _model.GuaranteeMemberInfo.RealName + ".dat";
            _guaranteeKeyWord = "丁方： " + _model.GuaranteeMemberInfo.RealName;
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

        #region 生成和签名相关的方法

        public override string FileName
        {
            get { return _model.FileName; }
        }

        private void SignPlantformAndGuaranteeSeal()
        {
            var cgcs = new ChapterGraphicsCertSign();
            cgcs.Init(_plantformSealPath, _model.FileName, PlantformKeyWord).Sign();
            Log4NetHelper.WriteLog(string.Format("平台服务商信息：{0},章的路径：{1}", PlantformKeyWord, _plantformSealPath));

            cgcs.Init(_guaranteeSealPath, _model.FileName, _guaranteeKeyWord).Sign();
            Log4NetHelper.WriteLog(string.Format("担保方商信息：{0},章的路径：{1}", _guaranteeKeyWord, _guaranteeSealPath));
        }

        private void BuildPdf()
        {
            var myDocument = new Document(PageSize.A4);

            //打开文档
            string path = Path.GetDirectoryName(_model.FileName);
            if (path != null && !Directory.Exists(path)) Directory.CreateDirectory(path);

            PdfWriter.GetInstance(myDocument, new FileStream(_model.FileName, FileMode.Create));

            //加密设置
            //writer.SetEncryption(PdfWriter.STRENGTH128BITS, "user", "user", PdfWriter.AllowCopy | PdfWriter.AllowPrinting);

            myDocument.Open();

            //1.增加标题
            var header = new Paragraph {Alignment = Element.ALIGN_CENTER};
            header.Add(new Chunk("借款及担保合同", Msyh12Bold));
            myDocument.Add(header);

            myDocument.Add(NewLine);

            //2.编号
            var sn = new Paragraph {Alignment = Element.ALIGN_RIGHT};
            sn.Add(new Chunk("编号：", Msyh10));
            sn.Add(new Chunk(_model.LoanInfo.LoanNumber.PadRight(20), Msyh10));
            myDocument.Add(sn);

            myDocument.Add(NewLine);

            //3.参与方列表甲方
            var memberList = new Paragraph {Alignment = Element.ALIGN_LEFT};
            memberList.Add(new Chunk("甲方(借款人)：", Msyh10Bold));
            memberList.Add(new Chunk(
                               _model.MemberInfo.RealName.Replace(_model.MemberInfo.RealName, "***").PadRight(18),
                               Msyh10BolderUnderLine));
            memberList.Add(new Chunk("".PadRight(6), Msyh10Bold));
            memberList.Add(new Chunk("融金宝账号：", Msyh10Bold));
            memberList.Add(
                new Chunk(
                    (_model.LoanInfo.MemberName.Substring(0, 1) + "*****" +
                     _model.LoanInfo.MemberName.Substring(_model.LoanInfo.MemberName.Length - 1, 1)).PadRight(18),
                    Msyh10BolderUnderLine));
            memberList.Add(new Chunk("".PadRight(6), Msyh10Bold));
            memberList.Add(new Chunk("身份证/组织机构代码：", Msyh10Bold));
            memberList.Add(
                new Chunk(
                    (_model.MemberInfo.IdentityCard.Replace(
                        _model.MemberInfo.IdentityCard.Substring(0, _model.MemberInfo.IdentityCard.Length - 3),
                        "************")).PadRight(32), Msyh10BolderUnderLine));
            myDocument.Add(memberList);

            myDocument.Add(NewLine);

            //4.参与方列表乙方
            var memberList1 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            memberList1.Add(new Chunk("乙方（出借人）:  ", Msyh10Bold));
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
            table.AddCell(new PdfPCell(new Phrase("出借本金（¥）", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_RIGHT
                });

            for (int i = 0; i < _model.BidList.Count; i++)
            {
                BidModel item = _model.BidList[i];

                string memberName = item.MemberName.Substring(0, 1) + "*****" +
                                    item.MemberName.Substring(item.MemberName.Length - 1, 1);
                table.AddCell(new PdfPCell(new Phrase(memberName, Msyh8))
                    {
                        Colspan = 1,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });

                string identityCard =
                    item.IdentityCard.Replace(item.IdentityCard.Substring(0, item.IdentityCard.Length - 3),
                                              "************");
                table.AddCell(new PdfPCell(new Phrase(identityCard, Msyh8))
                    {
                        Colspan = 1,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });

                table.AddCell(new PdfPCell(new Phrase(item.BidAmount.ToString("N2"), Msyh8))
                    {
                        Colspan = 1,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    });
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
            memberThird.Add(new Chunk("组织机构代码:  ", Msyh10Bold));
            memberThird.Add(new Chunk("30613792-3", Msyh10));
            myDocument.Add(memberThird);


            //7.参与方列表丁方
            var memberFourth = new Paragraph {Alignment = Element.ALIGN_LEFT};
            memberFourth.Add(new Chunk("丁方（担保方）: ", Msyh10Bold));
            memberFourth.Add(new Chunk(_model.GuaranteeMemberInfo.RealName, Msyh10));
            memberFourth.Add(new Chunk(Environment.NewLine));
            memberFourth.Add(new Chunk("组织机构代码: ", Msyh10Bold));
            memberFourth.Add(new Chunk(_model.GuaranteeMemberInfo.IdentityCard, Msyh10));
            myDocument.Add(memberFourth);

            myDocument.Add(NewLine);

            //8.合同正文
            var content = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content.Add(new Chunk("鉴于：", Msyh10));
            var contentComment = new List(false, 10);
            contentComment.Add(new ListItem(new Phrase(
                                                "甲方因日常运营资金周转需要，拟寻求资金支持；乙方因合法拥有资金，决定通过一定的投资渠道投资，实现资金保值增值；" +
                                                "丙方通过自行创立的投 融资中介服务平台（网址：www.rjb777.com，下称融金宝），" +
                                                "能获取大量的投融资信息，并能有效提供投融资居间撮合、管理等服务。", Msyh8)));
            contentComment.Add(new ListItem(new Phrase(
                                                "乙方同意接受 深圳金信联融资担保有限公司为借款用户向乙方提供保证担保，" +
                                                "以确保借款用户因通过融金宝网站向出借人借款而形成的有关债权的实现，为此就保证人向出借人提供保证担保事宜。", Msyh8)));
            content.Add(contentComment);
            content.Add(new Phrase("基于以上，四方本着实事求是、平等互利、真诚合作、共同发展的原则，就甲方通过丙方向乙方申请借款，丁方提供担保事宜，经过友好协商，达成协议如下：", Msyh8));
            myDocument.Add(content);

            myDocument.Add(NewLine);

            //9.合同正文第一条
            var content1 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content1.Add(new Chunk("第一条 借款", Msyh10Bold));
            var contentComment1 = new List(false, 10);
            contentComment1.Add(new ListItem(new Phrase(
                                                 "借款本金：人民币（大写） " +
                                                 ConvertHelper.LowAmountConvertChCap(_model.LoanInfo.LoanAmount) +
                                                 " （小写¥" + _model.LoanInfo.LoanAmount.ToString("N2") + "）。", Msyh8)));
            contentComment1.Add(new ListItem(new Phrase(
                                                 "借款期限：" + _model.LoanInfo.LoanTerm + _model.LoanInfo.BorrowModeStr +
                                                 "，自 " + _model.LoanInfo.ReviewTime.ToString("yyyy年MM月dd日") + "至 "
                                                 +
                                                 ((_model.LoanInfo.BorrowMode == 0)
                                                      ? _model.LoanInfo.ReviewTime.AddDays(_model.LoanInfo.LoanTerm)
                                                              .ToString("yyyy年MM月dd日")
                                                      : _model.LoanInfo.ReviewTime.AddMonths(_model.LoanInfo.LoanTerm)
                                                              .ToString("yyyy年MM月dd日")) + "止。" +
                                                 "如借款实际发放的日期与上述起始日期不一致的，以借款实际发放的日期为准，到期日相应顺延。", Msyh8)));
            contentComment1.Add(new ListItem(new Phrase(
                                                 "借款利率：" +
                                                 _model.LoanInfo.LoanRate.ToString(CultureInfo.InvariantCulture) +
                                                 "%/年。借款利率不因国家利率政策变化而调整。 借款利率的折算：日利率=年利率/365，月利率=年利率/12。",
                                                 Msyh8)));
            contentComment1.Add(new ListItem(new Phrase(
                                                 "借款用途：" + _model.LoanInfo.LoanUseName, Msyh8)));
            content1.Add(contentComment1);
            myDocument.Add(content1);

            myDocument.Add(NewLine);

            //10.合同正文第二条
            var content2 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content2.Add(new Chunk("第二条 还款", Msyh10Bold));
            var contentComment2 = new List(false, 10);
            contentComment2.Add(
                new ListItem(
                    new Phrase(
                        "甲方采用以下第 " + _model.LoanInfo.RepaymentMethod.ToString(CultureInfo.InvariantCulture) +
                        " 种方式向乙方还款：", Msyh8)));
            var contentComment21 = new List(false, 10);
            contentComment21.Add(new ListItem(new Phrase("1、按天计息按月还款。", Msyh8)));
            contentComment21.Add(new ListItem(new Phrase("2、按天计息一次性还款。", Msyh8)));
            contentComment21.Add(new ListItem(new Phrase("3、按月付息到期还本。", Msyh8)));
            contentComment21.Add(new ListItem(new Phrase("4、按月等额本息。", Msyh8)));
            contentComment2.Add(contentComment21);

            contentComment2.Add(
                new ListItem(
                    new Phrase(
                        "还款日为每月" +
                        ((_model.LoanInfo.BorrowMode == 0)
                             ? "—"
                             : _model.LoanInfo.ReviewTime.Day.ToString(CultureInfo.InvariantCulture)) +
                        "号。当月无该日的，还款日为该月最后一日。（上述第2条还款方式不适用本条）", Msyh8)));

            contentComment2.Add(new ListItem(new Phrase("乙方收到的还款，应依次偿还先前期后当期的如下费用：", Msyh8)));
            var contentComment23 = new List(false, 10);
            contentComment23.Add(new ListItem(new Phrase("依法及约定的应当支付的费用；", Msyh8)));
            contentComment23.Add(new ListItem(new Phrase("违约金（包括但不限于逾期还款违约金、提前还款违约金等）、罚息；", Msyh8)));
            contentComment23.Add(new ListItem(new Phrase("利息；", Msyh8)));
            contentComment23.Add(new ListItem(new Phrase("本金。", Msyh8)));
            contentComment2.Add(contentComment23);

            content2.Add(contentComment2);
            myDocument.Add(content2);

            myDocument.Add(NewLine);

            //11.合同正文第三条
            var content3 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content3.Add(new Chunk("第三条 提前还款", Msyh10Bold));
            var contentComment3 = new List(false, 10);
            contentComment3.Add(new ListItem(new Phrase("甲方提前偿还全部借款的，应支付两天的利息作为提前还款违约金。" +
                                                        "但甲方在还款期限届满日（含当日）前15天还款，乙方同意不收取违约金。", Msyh8)));
            contentComment3.Add(new ListItem(new Phrase("若丙方另行公布的借款规则中对提前还款违约金另行约定的，各方同意以另行公布的借款规则为准。", Msyh8)));
            content3.Add(contentComment3);
            myDocument.Add(content3);

            myDocument.Add(NewLine);

            //11.合同正文第四条
            var content4 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content4.Add(new Chunk("第四条 逾期还款", Msyh10Bold));
            var contentComment4 = new List(false, 10);
            contentComment4.Add(new ListItem(new Phrase("还款日24时前，甲方未足额支付应付款项的，则视为逾期。", Msyh8)));
            contentComment4.Add(new ListItem(new Phrase("甲方逾期的，乙方有权在约定利息之外另行加收逾期还款违约金及催收费用，" +
                                                        "逾期还款违约金标准为按日加收未偿还本金的千分之三，催收费用按丙方公布的规则计收。", Msyh8)));
            content4.Add(contentComment4);
            myDocument.Add(content4);

            myDocument.Add(NewLine);

            //11.合同正文第五条
            var content5 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content5.Add(new Chunk("第五条 保证方式", Msyh10Bold));
            content5.Add(new Chunk(Environment.NewLine));
            content5.Add(new Phrase("丁方提供的保证为连带责任保证。只要主合同项下单笔债务履行期限届满，借款用户没有履行或者没有全部履行其还款义务，出借人即有权直接要求丁方承担保证责任。", Msyh8));
            myDocument.Add(content5);

            myDocument.Add(new Chunk("\n"));

            //11.合同正文第六条
            var content6 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content6.Add(new Chunk("第六条 保证期间", Msyh10Bold));
            content6.Add(new Chunk(Environment.NewLine));
            content6.Add(new Phrase("保证期间自债务的履行期满之日起两年。", Msyh8));
            myDocument.Add(content6);

            myDocument.Add(NewLine);

            //12.合同正文第七条
            var content7 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content7.Add(new Chunk("第七条 各方权利义务", Msyh10Bold));
            var contentComment7 = new List(false, 10);
            contentComment7.Add(new ListItem(new Phrase("甲方权利义务", Msyh8)));
            var contentComment71 = new List(false, 10);
            contentComment71.Add(new ListItem(new Phrase("有权向乙方申请借款；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("应按丙方要求提供相关资料，确保所提供资料是真实、完整、有效的；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("应接受乙方、丙方对其使用借款资金情况和其财务情况的监督；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("应按约定按时足额偿还借款本金和利息、支付约定的相关费用；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("必须按本合同约定的用途使用借款，并保证不利用本合同项下借款从事违反国家法律法规的非法活动；", Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("有如下任何信息或情形变动、发生时，有义务于5日内通知乙方、丙方：" +
                                                         "住所、联系方式、涉及重大诉讼及其它有损还款能力事宜、（法人）发生隶属关系变更、经营范围改变、" +
                                                         "高管变动、组织架构调整、重大违纪违法或被索赔事件、经营困难、财务状况恶化、债权债务纠纷、其他可能影响偿债能力的事宜。",
                                                         Msyh8)));
            contentComment71.Add(new ListItem(new Phrase("出现如下情形的，应通知乙方、丙方并取得其同意：" +
                                                         "为他人提供担保、再次向第三方借款、处置名下重大资产、（法人）发生对外投资及资产转让的重大事项、" +
                                                         "股权调整或股权转让等、减少注册资本、进行分立、合并、重组、股份制改造等重大体制变更或撤销、解散、停业等。", Msyh8)));
            contentComment7.Add(contentComment71);

            contentComment7.Add(new ListItem(new Phrase("乙方权利义务", Msyh8)));
            var contentComment72 = new List(false, 10);
            contentComment72.Add(new ListItem(new Phrase("有权委托丙方了解甲方的个人资信状况和经济状况，要求甲方提供与本合同项下借款使用有关的资料。", Msyh8)));
            contentComment72.Add(new ListItem(new Phrase("有权委托丙方指定一名出借人与甲方签署线下《借款合同》，并办理相应的抵押、质押等担保手续。" +
                                                         "该出借人作为乙方指定的出借人，享有债 权人应有的权利，有权要求借款人按时还本付息、支付逾期违约金、罚息、依法实现债权等权利。",
                                                         Msyh8)));
            contentComment72.Add(new ListItem(new Phrase("有权委托丙方通过资料分析、查验或现场调查等方式监督或检查甲方对借款的使用。", Msyh8)));
            contentComment72.Add(
                new ListItem(new Phrase("有权委托丙方通过向甲方在融金宝平台的电子信箱发送催收\"站内信\"、在公众媒体上发布催收公告或通过快递催收信函的方式对甲方进行催收通知。", Msyh8)));
            contentComment72.Add(
                new ListItem(new Phrase("有权按照本合同和融金宝平台的规定，在甲方发生还款逾期并达到相关借款规则规定期限时，在履行相关手续之后，获得丁方的垫付（担保代偿）。", Msyh8)));
            contentComment72.Add(new ListItem(new Phrase("应当对甲方的个人信息保密，但本合同另有规定的或为实现债权等必要情况下除外。", Msyh8)));
            contentComment7.Add(contentComment72);

            contentComment7.Add(new ListItem(new Phrase("丙方权利义务", Msyh8)));
            var contentComment73 = new List(false, 10);
            contentComment73.Add(new ListItem(new Phrase("丙方根据甲、乙方的授权，通过融金宝平台，为甲、乙双方提供对接、撮合、资金监管等服务。", Msyh8)));
            contentComment73.Add(new ListItem(new Phrase("接受乙方委托，有权代乙方指定一名出借人与借款人在线下签署相关合同，并办理相关的抵押、质押等担保手续。", Msyh8)));
            contentComment73.Add(new ListItem(new Phrase("有权向甲、乙方收取相关费用，费用标准根据融金宝平台规则计收。", Msyh8)));
            contentComment7.Add(contentComment73);

            contentComment7.Add(new ListItem(new Phrase("丁方权利义务", Msyh8)));
            var contentComment74 = new List(false, 10);
            contentComment74.Add(
                new ListItem(new Phrase("丁方在甲方借款发生逾期之日起10个工作日内，在履行相关手续之后，应代甲方向乙方偿付符合融金宝平台规则及本合同约定的本息。", Msyh8)));
            contentComment74.Add(
                new ListItem(new Phrase("丁方自代偿完成之时，乙方享有的债权转让给丁方享有。该债权转让可通过融金宝平台的站内信或绑定的电子邮件进行通知，各方认可此种通 知为有效合法。", Msyh8)));
            contentComment74.Add(
                new ListItem(new Phrase("丁方有权在发生逾期时，采取合理合法的手段进行催收，包括但不限于通过站内信、电子邮件、公共媒体公告等方式，各方对此予以认可。", Msyh8)));
            contentComment7.Add(contentComment74);

            content7.Add(contentComment7);
            myDocument.Add(content7);

            myDocument.Add(NewLine);

            //11.合同正文第八条
            var content8 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content8.Add(new Chunk("第八条 违约事项", Msyh10Bold));
            content8.Add(new Chunk(Environment.NewLine));
            content8.Add(new Phrase("发生下列事项之一，即视为甲方根本违约：", Msyh8));
            var contentComment8 = new List(false, 10);
            contentComment8.Add(new ListItem(new Phrase("甲方提供虚假、伪造的申请材料，或进行虚假陈述；", Msyh8)));
            contentComment8.Add(new ListItem(new Phrase("甲方未按合同约定用途使用借款；", Msyh8)));
            contentComment8.Add(new ListItem(new Phrase("甲方未按合同约定偿还或支付到期本金、利息、费用及其他任何应付款项；", Msyh8)));
            contentComment8.Add(new ListItem(new Phrase("违反本合同规定的甲方的义务；", Msyh8)));
            contentComment8.Add(new ListItem(new Phrase("甲方拒不配合乙、丙方对其借款使用情况和经营情况进行的监督和调查；", Msyh8)));
            contentComment8.Add(new ListItem(new Phrase("甲方为逃避债务，恶意转移资产或放弃、减免对第三方的债权；无法清偿第三方到期债务；", Msyh8)));
            contentComment8.Add(new ListItem(new Phrase("其它足以让甲方认为危及本合同项下债权安全的其他情况；", Msyh8)));
            content8.Add(contentComment8);
            myDocument.Add(content8);

            myDocument.Add(NewLine);

            //11.合同正文第九条
            var content9 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content9.Add(new Chunk("第九条 违约责任", Msyh10Bold));
            content9.Add(new Chunk(Environment.NewLine));
            content9.Add(new Phrase("甲方违反本合同约定时，乙方、丙方、丁方有权分别或同时采取下列措施：", Msyh8));
            var contentComment9 = new List(false, 10);
            contentComment9.Add(new ListItem(new Phrase("要求限期改正；", Msyh8)));
            contentComment9.Add(new ListItem(new Phrase("宣布本合同项下的借款本息全部立即到期，要求立即偿还全部借款本息、违约金及其他各项应付费用等；", Msyh8)));
            contentComment9.Add(new ListItem(new Phrase("解除本合同；", Msyh8)));
            content9.Add(contentComment9);
            myDocument.Add(content9);

            myDocument.Add(NewLine);


            //12.合同正文第十条
            var content10 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content10.Add(new Chunk("第十条 保密条款", Msyh10Bold));
            content10.Add(new Chunk(Environment.NewLine));
            content10.Add(
                new Phrase("各方保证，对讨论、签订、履行本协议过程中所获悉的属于其他方的且无法自公开渠道获得的文件和资料（包括但不限 于个人隐私、商业计划、运营资金、财务信息等）予以保密。" +
                           "否则，应承担相应的违约责任并赔偿由此造成的全部损失。", Msyh8));
            myDocument.Add(content10);

            myDocument.Add(NewLine);

            //12.合同正文第十一条
            var content11 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content11.Add(new Chunk("第十一条 争议的解决", Msyh10Bold));
            content11.Add(NewLine);
            content11.Add(
                new Phrase("本协议适用中华人民共和国法律和法规，各方权益受中华人民共和国法律保障。因本协议发生纠纷，各方协商解决；协商不成时，任何一方可向本协议签订地有管辖权人民法院提出诉讼。", Msyh8));
            myDocument.Add(content11);

            myDocument.Add(NewLine);


            //11.合同正文第十二条
            var content12 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content12.Add(new Chunk("第十二条 协议的生效及其他", Msyh10Bold));
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

            //12.签名栏
            var signContent = new Paragraph {Alignment = Element.ALIGN_CENTER};
            var signTable = new PdfPTable(2) {WidthPercentage = 100};
            signTable.AddCell(
                new PdfPCell(new Phrase("甲方: " + _model.MemberInfo.RealName.Replace(_model.MemberInfo.RealName, "***"),
                                        Msyh8))
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
            signTable.AddCell(new PdfPCell(new Phrase(_guaranteeKeyWord, Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0,
                    MinimumHeight = 100
                });
            signContent.Add(signTable);
            myDocument.Add(signContent);

            var wcs = new WordsCertSign();
            //甲方（借款人）签文字
            string keywordMember = "甲方同意:" + _model.LoanInfo.LoanNumber;
            wcs.Init(myDocument, _model.MemberInfo.MemberID, _model.MemberInfo.RealName.Replace(_model.MemberInfo.RealName, "***"), keywordMember, "甲方").Sign();

            ////乙方（投资人）签文字
            foreach (BidModel investor in _model.BidList)
            {
                string keywordInvestor = "乙方同意:" + _model.LoanInfo.LoanNumber;
                string memberName = investor.MemberName.Substring(0, 1) + "*****" +
                    investor.MemberName.Substring(investor.MemberName.Length - 1, 1);
                wcs.Init(myDocument, investor.MemberID, memberName, keywordInvestor, "乙方").Sign();
            }

            //关闭文档
            myDocument.Close();
        }

        #endregion
    }
}