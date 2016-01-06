using System;
using System.Data;
using System.Globalization;
using System.IO;
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
    /// <summary>
    ///     该合同为两个公司的章 + 投资人签名
    /// </summary>
    internal class BuyBackContractPdfHelper : BasePdfHelper
    {
        /// <summary>
        ///     平台服务公司
        /// </summary>
        private const string PlantformKeyWord = "丙方: 深圳融金宝互联网金融服务有限公司";

        /// <summary>
        ///     贷款公司
        /// </summary>
        private readonly string _guaranteeKeyWord = "甲方: XXX小额贷款股份有限公司";

        private readonly string _guaranteeSealPath = CommonData.SealFolder + @"GuaranteeSeal.dat";

        private readonly LoanPdfModel _model;
        private readonly string _plantformSealPath = CommonData.SealFolder + @"深圳融金宝互联网金融服务有限公司.dat";

        public BuyBackContractPdfHelper(LoanPdfModel model)
        {
            _model = model;

            _guaranteeSealPath = CommonData.SealFolder + _model.MemberInfo.RealName + ".dat";
            _guaranteeKeyWord = "甲方: " + _model.MemberInfo.RealName;
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
            cgcs.Init(_guaranteeSealPath, _model.FileName, _guaranteeKeyWord).Sign();
        }

        public void BuildPdf()
        {
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
            header.Add(new Chunk("债权收益权转让及回购合同（典当）", Msyh12Bold));
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
            memberList.Add(new Chunk("甲方(转让方)：", Msyh10Bold));
            memberList.Add(new Chunk(_model.MemberInfo.RealName.PadRight(30), Msyh10));
            memberList.Add(NewLine);
            memberList.Add(NewLine);
            memberList.Add(new Chunk("融金宝理财平台用户名: ", Msyh10Bold));
            memberList.Add(
                new Chunk(
                    (_model.LoanInfo.MemberName.Substring(0, 2) + "***" +
                     _model.LoanInfo.MemberName.Substring(_model.LoanInfo.MemberName.Length - 2, 2)).PadRight(24),
                    Msyh10BolderUnderLine));
            memberList.Add(new Chunk(" ".PadRight(10), Msyh10));
            memberList.Add(new Chunk("组织机构代码: ", Msyh10Bold));
            memberList.Add(new Chunk((_model.MemberInfo.IdentityCard).PadRight(32), Msyh10BolderUnderLine));
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
            memberThird.Add(new Chunk("丙方（平台提供方）: ", Msyh10Bold));
            memberThird.Add(new Chunk("深圳融金宝互联网金融服务有限公司", Msyh10));
            memberThird.Add(new Chunk(Environment.NewLine));
            memberThird.Add(new Chunk("住所地:  ", Msyh10Bold));
            memberThird.Add(new Chunk("深圳市前海深港合作区前湾一路鲤鱼门街一号前海深港合作区管理局综合办公楼A栋201室（入驻深圳市前海商务秘书有限公司）", Msyh8));
            myDocument.Add(memberThird);

            myDocument.Add(NewLine);

            //7.合同正文
            var content = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content.Add(new Phrase("根据《中华人民共和国合同法》及相关法律法规的规定，甲乙丙三方遵循诚实信用、平等自愿、互惠互利的原则，" +
                                   "就乙方通过深圳融金宝互联网金融服务有限公司旗下“融金宝”理财平台（即www.RJB777.com网站，以下简称“融金宝”平台）" +
                                   "向甲方购买甲方合法持有的未到期典当债权收益权，甲方按约定溢价回购该债权收益权，平台为交易各方提供居间及转让管理等相关服务事宜，" +
                                   "经协商一致，达成如下合同，以资遵守。", Msyh8));
            myDocument.Add(content);

            myDocument.Add(NewLine);

            //8.合同正文第一条
            var content1 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content1.Add(new Chunk("第一条 定义与解释", Msyh10Bold));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.1 典当借款合同：指典当公司在其正常经营活动中向借款人（即“当户”）发放借款（即“当金”）而与借款人共同签署的典当借款合同及其补充协议等。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.2 当户：指根据《典当借款合同》的约定，向典当公司借款并承担清偿义务的民事主体，包括法人、自然人或其他组织。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.3 当金：当户向典当公司申请，典当公司经审核批准发放给当户的借款。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.4 担保合同：指为促使《典当借款合同》项下的债务人履行还款义务，保障典当公司的债权收益权得以实现而在典当公司与债务人之间，" +
                                    "或在典当公司与第三方担保人之间形成的，当债务人不履行或无法履行债务时，以一定方式担保典当公司债权收益权得以实现的协议、" +
                                    "承诺或者其它具有担保性质或包含同意承担还款责任的文件，" +
                                    "包括但不限于保证合同/抵押合同/质押合同以及担保人承担其他形式直接或间接担保责任的合同及其补充和修改。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.5 担保人：指在《担保合同》或担保函等担保性质文件项下承担担保义务的抵押人、出质人、保证人或其他担保义务人。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.6 担保权益：指典当公司因担保人对《典当借款合同》项下债务进行担保，而对担保人享有的所有或任一抵押、质押、保证及其他类似权益。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.7 转让债权收益权：指甲方在特定的《典当借款合同》项下所享有的可转让的未到还款结算期限的债权收益权" +
                                    "（包括但不限于尚未到期的当金本金和相应利息，因当金产生的其他权益或收入等）。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.8 债权收益权转让日：指甲方收到乙方支付的债权收益权转让价款之日。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.9 债权收益权回购付款日：指本合同约定的由甲方购回转让给乙方的债权收益权并支付全部回购款之日。" +
                                    "如发生本合同约定的提前回购情形，则回购日指未到期债权收益权回购通知书上载明的回购日期。" +
                                    "上述定义下的日期为法定节假日的，回购付款日为法定节假日前最后一个工作日。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.10 债权收益权证明文件：指用以证明债权收益权状况的合同、文件、信函、传真、凭证等书面材料，" +
                                    "包括但不限于典当借款合同、担保合同、当票、当金偿还凭证、当金催收文件、信贷档案，以及当户、担保人的最新财务报表及其他有关资产、经营、业务、债权收益权债务状况等的文件、资料、报表等。",
                                    Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.11 转让期限/转让期间：指从未到期债权收益权转让次日起至到期回购付款日期间的实际天数，以自然日计算。", Msyh8));
            content1.Add(NewLine);
            content1.Add(new Phrase("1.12 回购期/回购期限：指乙方根据本合同约定持有债权收益权的期间。即甲方收到乙方支付的债权收益权转让价款之日至乙方收到全部债权转让款及回购溢价的期间。 ",
                                    Msyh8));
            myDocument.Add(content1);

            myDocument.Add(NewLine);

            //9.合同正文第二条
            var content2 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content2.Add(new Chunk("第二条 转让标的", Msyh10Bold));
            content2.Add(NewLine);
            content2.Add(new Phrase("2.1 本合同项下转让标的特指在经本合同双方审核确认的甲方自营典当业务形成的尚未到期限的单笔典当债权收益权。", Msyh8));
            content2.Add(NewLine);
            content2.Add(new Phrase("2.2 甲方转让债权收益权本金账面金额为人民币（大写）", Msyh8));
            content2.Add(new Phrase(ConvertHelper.LowAmountConvertChCap(_model.LoanInfo.LoanAmount), Msyh8UnderLine));
            content2.Add(new Phrase("（小写）", Msyh8));
            content2.Add(new Phrase(_model.LoanInfo.LoanAmount.ToString("N2"), Msyh8UnderLine));
            content2.Add(new Phrase("，债权收益权到期日为", Msyh8));
            string endDate = (_model.LoanInfo.BorrowMode == 0)
                                 ? _model.LoanInfo.ReviewTime.AddDays(_model.LoanInfo.LoanTerm).ToString("yyyy-MM-dd")
                                 : _model.LoanInfo.ReviewTime.AddMonths(_model.LoanInfo.LoanTerm).ToString("yyyy-MM-dd");
            content2.Add(new Phrase(endDate, Msyh8UnderLine));
            content2.Add(new Phrase("。", Msyh8));
            content2.Add(NewLine);
            content2.Add(new Phrase("2.3 甲乙双方在此确认，本合同项下甲方仅将典当借款合同及担保合同项下约定的权利和利益转让给乙方，典当借款合同及担保合同项下的任何义务和责任仍由甲方承担。",
                                    Msyh8));
            myDocument.Add(content2);

            myDocument.Add(NewLine);


            //10.合同正文第三条
            var content3 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content3.Add(new Chunk("第三条 转让价格及回购期限", Msyh10Bold));
            content3.Add(new Chunk(Environment.NewLine));
            content3.Add(new Phrase("3.1 甲方收到乙方支付的全部转让价款时，该项债权收益权即转让到乙方名下。", Msyh8));
            content3.Add(NewLine);
            content3.Add(new Phrase("3.2 本合同项下债权收益权的转让价格为人民币（大写）", Msyh8));
            content3.Add(new Phrase(ConvertHelper.LowAmountConvertChCap(_model.LoanInfo.LoanAmount), Msyh8UnderLine));
            content3.Add(new Phrase("，（小写）￥", Msyh8));
            content3.Add(new Phrase(_model.LoanInfo.LoanAmount.ToString("N2"), Msyh8UnderLine));
            content3.Add(new Phrase("。", Msyh8));
            content3.Add(NewLine);
            content3.Add(new Phrase("3.3 甲方自愿将转让给乙方的转让债权收益权，在债权收益权转让期限届满前由甲方从乙方处按约定回购，乙方无条件同意甲方回购。 ", Msyh8));
            myDocument.Add(content3);

            myDocument.Add(NewLine);

            //11.合同正文第四条
            var content4 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content4.Add(new Chunk("第四条 转让价款的支付", Msyh10Bold));
            content4.Add(new Chunk(Environment.NewLine));
            content4.Add(new Phrase("4.1 乙方通过在丙方的融金宝理财平台注册，并充值，通过第三方支付平台将受让价款支付给甲方。", Msyh8));
            content4.Add(new Phrase("4.2 甲、乙双方确认由第三方支付公司办理相关的代收代付业务。 ", Msyh8));
            content4.Add(NewLine);
            myDocument.Add(content4);

            myDocument.Add(NewLine);

            //12.合同正文第五条
            var content5 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content5.Add(new Chunk("第五条 债权收益权证明文件的交付", Msyh10Bold));
            content5.Add(new Chunk(Environment.NewLine));
            content5.Add(new Phrase("5.1 甲方应在签订本合同的当天，将全部债权收益权证明文件（包括但不限于" +
                                    "《典当借款合同》《担保合同》《当票》《付款凭证》《典当标的确认书》等）扫描后传送至丙方指定电子邮箱，并由丙方公示。", Msyh8));
            content5.Add(NewLine);
            myDocument.Add(content5);

            myDocument.Add(NewLine);

            //13.合同正文第六条
            var content6 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content6.Add(new Chunk("第六条 债权收益权和风险的转移", Msyh10Bold));
            content6.Add(new Chunk(Environment.NewLine));
            content6.Add(new Phrase("6.1 自乙方付清本合同第四条规定的全部转让款之日起，转让债权收益权由乙方享有；甲方回购后，从回购之次日起转让债权收益权的收益权回转甲方。", Msyh8));
            content6.Add(new Phrase("6.2 在乙方付清本合同第四条规定的全部转让款之日前，转让债权收益权归甲方所有，风险由甲方负担。", Msyh8));
            content6.Add(NewLine);
            myDocument.Add(content6);

            myDocument.Add(NewLine);

            //14.合同正文第七条
            var content7 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content7.Add(new Chunk("第七条 债权收益权的管理", Msyh10Bold));
            content7.Add(new Chunk(Environment.NewLine));
            content7.Add(new Phrase("7.1 债权收益权转让期间，债权收益权仍由甲方负责管理，当金、利息及相关费用等由甲方代为收取。", Msyh8));
            content7.Add(NewLine);
            myDocument.Add(content7);

            myDocument.Add(NewLine);

            //15.合同正文第八条
            var content8 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content8.Add(new Chunk("第八条 债权收益权的回购", Msyh10Bold));
            content8.Add(new Chunk(Environment.NewLine));
            content8.Add(new Phrase("8.1 甲方承诺在本合同约定的转让期限届满之日无条件回购其转让给乙方的全部典当债权收益权。", Msyh8));
            content8.Add(NewLine);
            content8.Add(new Phrase("8.2 本合同项下转让债权收益权回购价格为债权收益权回购本金+回购溢价，其中回购溢价为", Msyh8));
            content8.Add(new Phrase(_model.LoanInfo.LoanRate.ToString(CultureInfo.InvariantCulture), Msyh8UnderLine));
            content8.Add(new Phrase("%/年，每年按365日计算。", Msyh8));
            content8.Add(NewLine);
            content8.Add(new Phrase("债权收益权回购款按如下方式支付：", Msyh8));

            var tableBack = new PdfPTable(5) {WidthPercentage = 100};

            //16.合同正文回购列表
            tableBack.AddCell(new PdfPCell(new Phrase("期次", Msyh8))
                {
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
            tableBack.AddCell(new PdfPCell(new Phrase("应付日期", Msyh8))
                {
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
            tableBack.AddCell(new PdfPCell(new Phrase("当期应付款（元）", Msyh8))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
            tableBack.AddCell(new PdfPCell(new Phrase("剩余应付回购本息（元）", Msyh8))
                {
                    Rowspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
            tableBack.AddCell(new PdfPCell(new Phrase("当期应付回购本金", Msyh8))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });
            tableBack.AddCell(new PdfPCell(new Phrase("当期应付回购利息", Msyh8))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                });

            //17.以下将会循环输出表格数据
            var repaymentPlanBll = new RepaymentPlanBll();
            DataTable dt = repaymentPlanBll.GetRepaymentPlanByLoanID(_model.LoanInfo.ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    tableBack.AddCell(new PdfPCell(new Phrase((dr["PeNumber"] ?? string.Empty).ToString(), Msyh8))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                    tableBack.AddCell(new PdfPCell(new Phrase((dr["RePayDate"] ?? string.Empty).ToString(), Msyh8))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                    tableBack.AddCell(new PdfPCell(new Phrase((dr["RePrincipal"] ?? string.Empty).ToString(), Msyh8))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                    tableBack.AddCell(new PdfPCell(new Phrase((dr["ReInterest"] ?? string.Empty).ToString(), Msyh8))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                    tableBack.AddCell(new PdfPCell(new Phrase((dr["RemainAmount"] ?? string.Empty).ToString(), Msyh8))
                        {
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                }
            }


            //18.合同正文第八条
            content8.Add(tableBack);
            content8.Add(new Phrase("8.3 甲乙双方一致同意甲方按照如下方式支付债权收益权转让价款及回购溢价：", Msyh8));
            content8.Add(NewLine);
            content8.Add(new Phrase("8.3.1 甲方通过在丙方的融金宝理财平台用户名，通过第三方支付平台将甲方对应本合同项下的应付回购价款支付给乙方，乙方对上述转支付行为表示认可。", Msyh8));
            content8.Add(NewLine);
            content8.Add(new Phrase("8.3.2 依本条确定的当期最后支付时间为法定节假日的，甲方应当在法定节假日前最后一个工作日内完成支付义务。", Msyh8));
            content8.Add(NewLine);
            content8.Add(new Phrase("8.4 甲方提前偿还全部借款的，应支付两天的利息作为提前还款违约金。但甲方在还款期限届满日（含当日）前15天还款，乙方同意不收取违约金。", Msyh8));
            content8.Add(NewLine);
            content8.Add(new Phrase("8.5 甲方若在回购日未能按照本合同规定履行回购义务，乙方有权自回购日次日起，" +
                                    "每日按照未付清回购款的千分之三的标准向甲方计收违约金，违约金额＝逾期未付款金额×千分之三×逾期天数。" +
                                    "甲方自回购日起逾期三日仍未履行回购义务，即视为根本违约，则乙方有权单方解除本合同，要求甲方承担违约责任并赔偿由此引起的一切损失。", Msyh8));
            content8.Add(NewLine);
            content8.Add(new Phrase("8.6 甲方及相关保证人未全额履行回购义务的，乙方为实现自身合法权益发生的一切费用（包括但不限于律师费、诉讼费、差旅费等）概由甲方承担。", Msyh8));
            content8.Add(NewLine);

            myDocument.Add(content8);

            myDocument.Add(NewLine);

            //19.合同正文第九条
            var content9 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content9.Add(new Chunk("第九条 声明和保证", Msyh10Bold));
            content9.Add(new Chunk(Environment.NewLine));
            content9.Add(new Phrase("9.1 甲方的声明和保证：", Msyh8));
            content9.Add(NewLine);
            content9.Add(
                new Phrase("9.1.1 就开展本合同项下的债权收益权转让业务，甲方已获得相关有权机关的批准或授权；代表甲方签署本合同的签署人已得到甲方有权机关的正式授权，有权代表甲方签署本合同；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.2 截止至转让日，转让债权收益权不存在任何未向乙方披露的瑕疵、争议或纠纷；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.3 债权收益权转让文件真实、合法、有效；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.4 借款、担保合同项下没有禁止债权收益权转让、回购的约定；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.5 转让债权收益权不受当户或担保人以其对甲方的其他权利所主张的抵销、索赔、留置或其他扣减等；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.6 甲方已全面、充分、适当地履行了其在借款、担保合同项下的任何责任和义务，" +
                                    "并保证在本合同项下的典当资产权益转让完成后，仍继续全面、充分、适当地履行其在典当借款合同和担保合同项下的各项责任和义务；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.7 自本合同签订之日起，当户的债务责任和担保人的担保责任均不发生任何变化；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.8 自本合同签订之日起，甲方未经乙方事先书面同意不与当户、担保人对典当借款合同或担保合同做出任何形式的变更或修改；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.9 截止至转让债权收益权转让日，当户、担保人没有拖欠典当借款合同项下已到期的任何款项，也不存在其他违反借款、担保合同的行为；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.10 甲方未将该转让债权收益权转让给其他第三方或在该典当资产权益上设定抵押、质押或其他第三方权益；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.11 甲方没有将其在借款、担保合同项下的任何和一切义务转让给乙方；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.12 甲方的转让债权收益权回购义务是无条件和不可撤销的，甲方保证在回购日前按照合同约定履行回购义务。", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.13 截止至债权收益权转让日，不存在任何对甲方商誉、业务活动、财务状况和本合同项下的履行能力可能产生较大负面影响的诉讼、" +
                                    "仲裁或任何政府部门的强制性执行措施；在本合同项下的债权收益权转让完成后，" +
                                    "如发生或可能发生任何对甲方财务状况或本合同项下的履行能力产生不利影响的重大诉讼、仲裁或行政处罚等时，" +
                                    "甲方应于发生之日起（含该日）3日内书面通知乙方；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.1.14 甲方将尽力督促当户、担保人全面、及时履行其在典当借款合同和/或担保合同项下的各项义务等。", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.2 乙方的声明和保证：", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.2.1 就开展本合同项下的债权收益权转让业务，乙方已获得相关有权机关的批准或授权；" +
                                    "代表乙方签署本合同的签署人已得到乙方有权机关的正式授权，有权代表乙方签署本合同；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.2.2 在甲方完全履行本合同为其设定的义务、满足债权收益权转让条件的前提下，按期向甲方支付债权收益权转让价款；", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.2.3 债权收益权转让期间，乙方不得在该转让债权收益权上设定抵押、质押或其他第三方权益，" +
                                    "也不得未经甲方事先书面同意与当户、担保人对典当借款合同或担保合同做出任何形式的变更或修改或造成转让债权收益权任何损失。", Msyh8));
            content9.Add(NewLine);
            content9.Add(new Phrase("9.3 本合同生效后，甲乙双方的上述声明与保证将持续有效。", Msyh8));
            myDocument.Add(content9);

            myDocument.Add(NewLine);

            //20.合同正文第十条
            var content10 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content10.Add(new Chunk("第十条 违约责任", Msyh10Bold));
            content10.Add(NewLine);
            content10.Add(new Phrase("10.1 任何一方违反本合同的约定，使得本合同的全部或部分不能履行，违约方应承担违约责任，" +
                                     "并赔偿守约方因此遭受的损失；如多方违约，根据实际情况各自承担相应的责任。", Msyh8));
            content10.Add(new Phrase("10.2 违约方应赔偿给守约方的损失，包括但不限于因违约给守约方造成的直接损失，" +
                                     "以及守约方在协议履行后可以获得的利益、守约方为防止或减少损失的扩大而支出的合理费用、守约方支付的诉讼费和律师费等间接损失。", Msyh8));
            myDocument.Add(content10);

            myDocument.Add(NewLine);

            //21.合同正文第十一条
            var content11 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content11.Add(new Chunk("第十一条 法律适用与争议的解决", Msyh10Bold));
            content11.Add(NewLine);
            content11.Add(new Phrase("11.1 本合同之效力、解释、变更、执行与争议解决均适用中华人民共和国法律。", Msyh8));
            content11.Add(NewLine);
            content11.Add(new Phrase("11.2 各方因履行本合同而发生的争议，应友好协商解决；协商不成的，双方均同意提交深圳市仲裁委员会申请仲裁，" +
                                     "并以该会届时的仲裁规则进行仲裁，该仲裁裁决是终局的，对双方均有约束力。", Msyh8));
            content11.Add(NewLine);
            content11.Add(new Phrase("11.3 在争议解决期间，本合同不涉及争议部分的条款仍须履行。", Msyh8));

            myDocument.Add(content11);

            //22.合同正文第十二条
            var content12 = new Paragraph {Alignment = Element.ALIGN_LEFT};
            content12.Add(new Chunk("第十二条 签约与转让", Msyh10Bold));
            content12.Add(NewLine);
            content12.Add(new Phrase("12.1 本合同由甲方在融金宝理财平台电子注册且加盖章备案公章，" +
                                     "乙方在融金宝理财平台点击投资确认签署后生效。生效的电子合同存放于丙方融金宝理财平台，甲乙双方均可在签约后下载电子合同。", Msyh8));
            content12.Add(NewLine);
            content12.Add(new Phrase("12.2 乙方通过融金宝理财平台再转让所购债权的，转让仅限于剩余转让期限，转让完成后，乙方退出，新的受让人将自动成为本合同新的乙方。 ", Msyh8));
            myDocument.Add(content12);

            myDocument.Add(NewLine);

            //23.签名栏
            var signContent = new Paragraph {Alignment = Element.ALIGN_CENTER};
            var signTable = new PdfPTable(2) {WidthPercentage = 100};
            signTable.AddCell(new PdfPCell(new Phrase(_guaranteeKeyWord, Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0,
                    MinimumHeight = 100
                });
            signTable.AddCell(new PdfPCell(new Phrase("丙方: 深圳融金宝互联网金融服务有限公司", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0,
                    MinimumHeight = 100
                });
            signTable.AddCell(new PdfPCell(new Phrase("乙方:（详情见受让方列表） ", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0,
                    MinimumHeight = 100
                });
            signTable.AddCell(new PdfPCell(new Phrase("", Msyh8))
                {
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    Border = 0,
                    MinimumHeight = 100
                });

            signContent.Add(signTable);
            myDocument.Add(signContent);

            var wcs = new WordsCertSign();

            //乙方签字
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
    }
}