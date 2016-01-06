// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OpenXmlHelper.cs" company="PCITC.MES.EM.OpenXmlUtils">
//   PCITC.MES.EM.OpenXmlUtils
// </copyright>
// <summary>
//   The open xml helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace ManageFcmsCommon
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using DocumentFormat.OpenXml.Validation;

    /// <summary>
    /// 其于OpenXml SDK写的帮助类
    /// </summary>
    public static class OpenXmlHelper
    {
        /// <summary>
        /// 单元格样式
        /// </summary>
        public static uint CellStyleIndex { get; set; }

        /// <summary>
        /// 获取Worksheet
        /// </summary>
        /// <param name="document">document对象</param>
        /// <param name="sheetName">sheetName可空</param>
        /// <returns>Worksheet对象</returns>
        public static Worksheet GetWorksheet(this SpreadsheetDocument document, string sheetName = null)
        {
            var sheets = document.WorkbookPart.Workbook.Descendants<Sheet>();
            var sheet = (sheetName == null
                             ? sheets.FirstOrDefault()
                             : sheets.FirstOrDefault(s => s.Name == sheetName)) ?? sheets.FirstOrDefault();

            var worksheetPart = (WorksheetPart)document.WorkbookPart.GetPartById(sheet.Id);
            return worksheetPart.Worksheet;
        }

        /// <summary>
        /// 获取第一个SheetData
        /// </summary>
        /// <param name="document">SpreadsheetDocument对象</param>
        /// <param name="sheetName">sheetName可为空</param>
        /// <returns>SheetData对象</returns>
        public static SheetData GetFirstSheetData(this SpreadsheetDocument document, string sheetName = null)
        {
            return document.GetWorksheet(sheetName).GetFirstChild<SheetData>();
        }

        /// <summary>
        /// 获取第一个SheetData
        /// </summary>
        /// <param name="worksheet">Worksheet对象</param>
        /// <returns>SheetData对象</returns>
        public static SheetData GetFirstSheetData(this Worksheet worksheet)
        {
            return worksheet.GetFirstChild<SheetData>();
        }

        /// <summary>
        /// 获了共享字符的表格对象
        /// </summary>
        /// <param name="document">SpreadsheetDocument</param>
        /// <returns>SharedStringTablePart对角</returns>
        public static SharedStringTablePart GetSharedStringTable(this SpreadsheetDocument document)
        {
            var sharedStringTable = document.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
            return sharedStringTable;
        }

        /// <summary>
        /// 修改单元格的内容.
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        public static void UpdateCellText(this SheetData sheetData, string cellName, string cellText)
        {
            var cell = sheetData.GetCell(cellName);
            if (cell == null)
            {
                return;
            }

            cell.UpdateCellText(cellText);
        }

        /// <summary>
        /// 设置单元格的值.
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        public static void SetCellValue(this SheetData sheetData, string cellName, object cellText = null)
        {
            SetCellValue(sheetData, cellName, cellText ?? string.Empty, CellStyleIndex);
        }

        /// <summary>
        /// 添加一个单元格
        /// </summary>
        /// <param name="row">Row对象</param>
        /// <param name="cellName">单元格名称</param>
        /// <param name="cellText">单元格文本</param>
        /// <param name="cellStyleIndex">样式</param>
        private static void CreateCell(this Row row, string cellName, object cellText, uint cellStyleIndex)
        {
            var refCell =
                row.Elements<Cell>()
                   .FirstOrDefault(
                       cell =>
                       string.Compare(cell.CellReference.Value, cellName, StringComparison.OrdinalIgnoreCase) > 0);
            var resultCell = new Cell { CellReference = cellName };
            resultCell.UpdateCell(cellText, cellStyleIndex);
            row.InsertBefore(resultCell, refCell);
        }

        /// <summary>
        /// 设置单元格的值.
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        /// <param name="cellStyleIndex">
        /// The cell style index.
        /// </param>
        private static void SetCellValue(this SheetData sheetData, string cellName, object cellText, uint cellStyleIndex)
        {
            uint rowIndex = GetRowIndex(cellName);
            var row = sheetData.GetRow(rowIndex);
            if (row == null)
            {
                row = new Row { RowIndex = rowIndex };
                row.CreateCell(cellName, cellText, cellStyleIndex);
                sheetData.Append(row);
            }
            else
            {
                var cell = row.GetCell(cellName);
                if (cell == null)
                {
                    row.CreateCell(cellName, cellText, cellStyleIndex);
                }
                else
                {
                    cell.UpdateCell(cellText, cellStyleIndex);
                }
            }
        }

        /// <summary>
        /// The get rows count.
        /// </summary>
        /// <param name="rows">
        /// The rows.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int GetRowsCount(this IEnumerable<Row> rows)
        {
            return rows.GroupBy(x => x.RowIndex.Value).Count();
        }

        /// <summary>
        /// 根据行索引获取单元
        /// </summary>
        /// <param name="rows">
        /// The rows.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <returns>
        /// Cell集合
        /// </returns>
        public static IList<Cell> GetCells(this IEnumerable<Row> rows, int rowIndex)
        {
            return rows.Where(row => row.RowIndex.Value == rowIndex).SelectMany(row => row.Elements<Cell>()).ToList();
        }

        /// <summary>
        /// 获取单元格值
        /// </summary>
        /// <param name="cells">
        /// The cells.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <param name="stringTablePart">
        /// The string table part.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetCellValue(this IEnumerable<Cell> cells, string cellName, SharedStringTablePart stringTablePart)
        {
            if (cells == null)
            {
                throw new ArgumentNullException("cells");
            }

            if (cellName == null)
            {
                throw new ArgumentNullException("cellName");
            }

            var cell = (from item in cells where item.CellReference == cellName select item).FirstOrDefault();
            if (cell == null)
            {
                return string.Empty;
            }

            if (cell.ChildElements.Count == 0)
            {
                return string.Empty;
            }

            var value = cell.CellValue.InnerText.Trim();
            if (cell.DataType == null)
            {
                return value;
            }

            switch (cell.DataType.Value)
            {
                case CellValues.SharedString:
                    if (stringTablePart != null)
                    {
                        value = stringTablePart.SharedStringTable.ElementAt(int.Parse(value)).InnerText.Trim();
                    }

                    break;
                case CellValues.Boolean:
                    value = value == "0" ? "FALSE" : "TRUE";
                    break;
            }

            return value;
        }

        /// <summary>
        /// 验证文档
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ValidateDocument(this SpreadsheetDocument document)
        {
            var msg = new StringBuilder();
            try
            {
                var validator = new OpenXmlValidator();
                int count = 0;
                foreach (ValidationErrorInfo error in validator.Validate(document))
                {
                    count++;
                    msg.Append("\nError " + count)
                       .Append("\nDescription: " + error.Description)
                       .Append("\nErrorType: " + error.ErrorType)
                       .Append("\nNode: " + error.Node)
                       .Append("\nPath: " + error.Path.XPath)
                       .Append("\nPart: " + error.Part.Uri)
                       .Append("\n-------------------------------------------");
                }
            }
            catch (Exception ex)
            {
                msg.Append(ex.Message);
            }

            return msg.ToString();
        }

        /// <summary>
        /// 根据单元格名称获取行索引.
        /// </summary>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <returns>
        /// The <see cref="uint"/>.
        /// </returns>
        private static uint GetRowIndex(string cellName)
        {
            var regex = new Regex(@"\d+");
            var match = regex.Match(cellName);
            return uint.Parse(match.Value);
        }

        /// <summary>
        /// The get cell data type.
        /// </summary>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        /// <returns>
        /// The <see cref="CellValues"/>.
        /// </returns>
        private static CellValues GetCellDataType(object cellText)
        {
            var type = cellText.GetType();
            switch (type.Name)
            {
                case "Int32":
                case "Decimal":
                case "Double":
                case "Int64":
                    return CellValues.Number;
                case "String":
                    return CellValues.String;
                ////            case "DateTime":
                ////                return CellValues.Date;
                default:
                    return CellValues.String;
            }
        }

        /// <summary>
        /// 修改单元格内容（文本、样式）                                                                                                                                                                                                                                            
        /// </summary>
        /// <param name="cell">
        /// The cell.
        /// </param>
        /// <param name="cellText">
        /// The cell text.
        /// </param>
        /// <param name="cellStyleIndex">
        /// The cell style index.
        /// </param>
        private static void UpdateCell(this Cell cell, object cellText, uint cellStyleIndex)
        {
            cell.UpdateCellText(cellText);
            cell.StyleIndex = cellStyleIndex;
        }

        /// <summary>
        /// 修改单元格的文本
        /// </summary>
        /// <param name="cell">Cell对象</param>
        /// <param name="cellText">文本字符串</param>
        private static void UpdateCellText(this Cell cell, object cellText)
        {
            cell.DataType = GetCellDataType(cellText);
            cell.CellValue = cell.CellValue ?? new CellValue();
            cell.CellValue.Text = cellText.ToString();
        }

        /// <summary>
        /// 获取行
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <returns>
        /// The <see cref="Row"/>.
        /// </returns>
        private static Row GetRow(this SheetData sheetData, long rowIndex)
        {
            return sheetData.Elements<Row>().FirstOrDefault(r => r.RowIndex == rowIndex);
        }

        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <param name="row">
        /// The row.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <returns>
        /// The <see cref="Cell"/>.
        /// </returns>
        private static Cell GetCell(this Row row, string cellName)
        {
            return row.Elements<Cell>().FirstOrDefault(c => c.CellReference.Value == cellName);
        }

        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <param name="sheetData">
        /// The sheet data.
        /// </param>
        /// <param name="cellName">
        /// The cell name.
        /// </param>
        /// <returns>
        /// The <see cref="Cell"/>.
        /// </returns>
        private static Cell GetCell(this SheetData sheetData, string cellName)
        {
            return sheetData.Descendants<Cell>().FirstOrDefault(c => c.CellReference.Value == cellName);
        }

        #region 样式

        /*
        public static Borders CreateStylesheet()
        {
            //Stylesheet stylesheet1 = new Stylesheet()
            //                             {
            //                                 MCAttributes =
            //                                     new MarkupCompatibilityAttributes()
            //                                         {
            //                                             Ignorable = "x14ac"
            //                                         }
            //                             };
            //stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            //stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");


            var borders1 = new Borders(
                new Border(
                    // Index 1 - Applies a Left, Right, Top, Bottom border to a cell
                    new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                    new DiagonalBorder()));

        //    stylesheet1.Append(borders1);
          //  return stylesheet1;
            return borders1;
        }

        public static Border CreateBorder()
        {
            //Stylesheet stylesheet1 = new Stylesheet()
            //                             {
            //                                 MCAttributes =
            //                                     new MarkupCompatibilityAttributes()
            //                                         {
            //                                             Ignorable = "x14ac"
            //                                         }
            //                             };
            //stylesheet1.AddNamespaceDeclaration("mc", "http://schemas.openxmlformats.org/markup-compatibility/2006");
            //stylesheet1.AddNamespaceDeclaration("x14ac", "http://schemas.microsoft.com/office/spreadsheetml/2009/9/ac");


       //     var borders1 = new Borders(
            return new Border(
                // Index 1 - Applies a Left, Right, Top, Bottom border to a cell
                new LeftBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                new RightBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                new TopBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                new BottomBorder(new Color() { Auto = true }) { Style = BorderStyleValues.Thin },
                new DiagonalBorder());

            //    stylesheet1.Append(borders1);
            //  return stylesheet1;
            // return borders1;
        }

         */
        #endregion
    }
}
