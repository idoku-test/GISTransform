using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace GISTransform.Utils
{
    public class BuildExcel
    {
        public BuildExcel()
        {
            this.workbook = new HSSFWorkbook();
            currentSheet = (HSSFSheet)this.workbook.CreateSheet("sheet1");
            this.workbook.CreateSheet("sheet2");
            this.workbook.CreateSheet("sheet3");

        }

        public BuildExcel(Stream fileStream)
        {
            workbook = new HSSFWorkbook(fileStream);
            this.currentSheet = (HSSFSheet)this.workbook.GetSheetAt(0);

        }

        /// <summary>
        /// NPOI文档流
        /// </summary>
        private HSSFWorkbook workbook = null;

        /// <summary>
        /// 当前操作页
        /// </summary>
        private HSSFSheet currentSheet = null;

        /// <summary>
        /// 当前单元
        /// </summary>
        private HSSFCell currentCell = null;

        /// <summary>
        /// 单元样式
        /// </summary>
        private ICellStyle cellStyle = null;


        #region 页操作
        /// <summary>
        /// 选择操作页
        /// </summary>
        /// <param name="sheetName"></param>
        public void SelectSheet(string sheetName)
        {
            currentSheet = (HSSFSheet)workbook.GetSheet(sheetName);
        }

        public void SetSheetName(int index, string sheetName)
        {
            workbook.SetSheetName(index, sheetName);
        }

        /// <summary>
        /// 自适应宽高
        /// </summary>
        /// <param name="sheetName"></param>
        public void AutoSizeColumn()
        {
            int noOfColumns = currentSheet.GetRow(0).LastCellNum;
            for (int columnNum = 0; columnNum <= noOfColumns; columnNum++)
            {
                int columnWidth = currentSheet.GetColumnWidth(columnNum) / 256;//获取当前列宽度  
                for (int rowNum = 1; rowNum <= currentSheet.LastRowNum; rowNum++)//在这一列上循环行  
                {
                    IRow currentRow = currentSheet.GetRow(rowNum);
                    ICell currentCell = currentRow.GetCell(columnNum);
                    if (currentCell != null)
                    {
                        int length = Encoding.UTF8.GetBytes(currentCell.ToString()).Length;//获取当前单元格的内容宽度  
                        if (columnWidth < length + 1)
                        {
                            columnWidth = length + 1;
                        }//若当前单元格内容宽度大于列宽，则调整列宽为当前单元格宽度，后面的+1是我人为的将宽度增加一个字符  
                    }
                }
                currentSheet.SetColumnWidth(columnNum, columnWidth * 256);
            }
        }
        #endregion

        #region--get stream
        /// <summary>
        /// 获取Excel文件流
        /// </summary>
        /// <returns></returns>
        public Stream GetStream()
        {
            MemoryStream stream = new MemoryStream();
            workbook.Write(stream);
            stream.Position = 0;
            return stream;
        }
        #endregion

        #region--sava

        public void SaveAs(string filename)
        {
            try
            {
                using (FileStream file = new FileStream(filename, FileMode.Create))
                {
                    workbook.Write(file);
                    file.Close();
                }
            }
            catch (IOException ex)
            {

            }
        }

        #endregion

        #region --insert text
        public void InsertText(string text, int row, int col)
        {
            IRow r = CellUtil.GetRow(row, currentSheet);
            if (r == null)
                r = currentSheet.CreateRow(row);
            ICell cell = CellUtil.CreateCell(r, col, text, CreateStyle());
        }

        public void Replace(string what, string replacement)
        {
            ICell cell = FindFirstCell(currentSheet, what);
            if (cell != null)
            {
                cell.SetCellValue(replacement);
            }
        }
        #endregion

        #region--GetBookmarks，GetAllMarks，DelBookmarks
        /// <summary>
        /// 获取所有书签
        /// </summary>
        /// <returns></returns>
        public List<string> GetBookmarks()
        {
            return FindAllText(currentSheet, @"《([^》]+)》"); ;
        }

        #endregion

        #region --insert table

        public void InsertTable(DataTable table, int row, int col)
        {
            ICell cell = GetCell(row, col);
            if (cell != null)
            {
                int rowCount = table.Rows.Count;
                int rowIndex = cell.RowIndex;
                InsertTable(table, rowIndex, true);
            }
        }


        public void ReplaceInsertTable(string what, DataTable table)
        {
            ICell cell = FindFirstCell(currentSheet, what);
            if (cell != null)
            {
                int rowCount = table.Rows.Count;
                int rowIndex = cell.RowIndex;
                ShiftRows(currentSheet, rowIndex + 1, rowIndex + rowCount, rowCount - 1);//keep what row
                InsertTable(table, rowIndex, false);
            }
        }

        public void InsertTable(DataTable table, int rowIndex, bool hasHeader)
        {
            InsertTable(table, rowIndex, hasHeader, GetThinBDRStyle());
        }

        private void InsertTable(DataTable table, int rowIndex, bool hasHeader, ICellStyle style)
        {
            var sheet = currentSheet;
            if (hasHeader)
            {
                var headerRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in table.Columns)
                {
                    var headerCell = headerRow.CreateCell(column.Ordinal);
                    headerCell.SetCellValue(column.ColumnName);
                    headerCell.CellStyle = style;
                }
                rowIndex++;
            }
            foreach (DataRow row in table.Rows)
            {
                var dataRow = sheet.CreateRow(rowIndex++);

                foreach (DataColumn column in table.Columns)
                {
                    var cell = dataRow.CreateCell(column.Ordinal);
                    cell.SetCellValue(row[column].ToString());
                    cell.CellStyle = style;
                }
            }
        }

        #endregion

        #region--find cell / find all
        private ICell FindFirstCell(ISheet sheet, string text)
        {
            for (int rowIndex = 0; rowIndex < sheet.LastRowNum; rowIndex++)
            {
                IRow row = sheet.GetRow(rowIndex);
                for (int cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
                {
                    ICell cell = row.GetCell(cellIndex);
                    if (cell.StringCellValue.Equals(text))
                    {
                        return cell;
                    }
                }
            }
            return null;
        }

        private List<string> FindAllText(ISheet sheet, string pattern)
        {
            List<string> labels = new List<string>();
            Regex labelRegex = new Regex(pattern);
            for (int rowIndex = 0; rowIndex < sheet.LastRowNum; rowIndex++)
            {
                IRow row = sheet.GetRow(rowIndex);
                for (int cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
                {
                    ICell cell = row.GetCell(cellIndex);
                    string strValue = cell.StringCellValue;
                    if (labelRegex.IsMatch(strValue))
                    {
                        MatchCollection matchCollection = labelRegex.Matches(strValue);
                        foreach (Match match in matchCollection)
                        {
                            labels.Add(match.Value);
                        }
                    }
                }
            }
            return labels;
        }

        #endregion

        #region--merge region
        public void MergedRegion(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            currentSheet.AddMergedRegion(new CellRangeAddress(firstRow, lastRow, firstCol, lastCol));
        }
        #endregion

        #region -- set border
        public void SetBorder(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            for (int rowIndex = firstRow; rowIndex < lastRow; rowIndex++)
            {
                var row = HSSFCellUtil.GetRow(rowIndex, currentSheet);
                for (int cellIndex = firstCol; cellIndex < lastCol; cellIndex++)
                {
                    var cell = HSSFCellUtil.GetCell(row, cellIndex);
                    cell.CellStyle = GetThinBDRStyle();
                }
            }
        }

        public void SetFont(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            for (int rowIndex = firstRow; rowIndex < lastRow; rowIndex++)
            {
                var row = HSSFCellUtil.GetRow(rowIndex, currentSheet);
                for (int cellIndex = firstCol; cellIndex < lastCol; cellIndex++)
                {
                    var cell = HSSFCellUtil.GetCell(row, cellIndex);
                    ICellStyle style = workbook.CreateCellStyle(); //务必创建新的CellStyle,否则所有单元格同样式
                    IFont font = workbook.CreateFont();
                    font.Boldweight = short.MaxValue;
                    style.SetFont(font);
                    cell.CellStyle = style;
                }
            }
        }

        private void SetBorderLeft(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            HSSFRegionUtil.SetBorderLeft(BorderStyle.Thin, new CellRangeAddress(firstRow, lastRow, firstCol, lastCol), currentSheet, workbook);
        }

        private void SetBorderRight(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            HSSFRegionUtil.SetBorderRight(BorderStyle.Thin, new CellRangeAddress(firstRow, lastRow, firstCol, lastCol), currentSheet, workbook);
        }

        private void SetBorderBottom(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            HSSFRegionUtil.SetBorderTop(BorderStyle.Thin, new CellRangeAddress(firstRow, lastRow, firstCol, lastCol), currentSheet, workbook);
        }

        private void SetBorderTop(int firstRow, int lastRow, int firstCol, int lastCol)
        {
            HSSFRegionUtil.SetBorderTop(BorderStyle.Thin, new CellRangeAddress(firstRow, lastRow, firstCol, lastCol), currentSheet, workbook);
        }

        private ICellStyle GetThinBDRStyle()
        {
            ICellStyle style = workbook.CreateCellStyle();
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            return style;
        }

        private void SetCellBorder(ISheet sheet, int firstRow, int lastRow, int firstCol, int lastCol)
        {

            for (int rowIndex = 0; rowIndex < sheet.LastRowNum; rowIndex++)
            {
                IRow row = sheet.GetRow(rowIndex);
                for (int cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
                {
                    ICell cell = row.GetCell(cellIndex);
                    cell.CellStyle = GetThinBDRStyle();
                }
            }
        }
        #endregion

        #region--set style
        private ICellStyle CreateStyle()
        {
            ICellStyle style = workbook.CreateCellStyle();
            return style;
        }

        /// <summary>
        /// 设置单元居中
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void SetCellCenter(int row, int col)
        {
            ICell cell = GetCell(row, col);
            ICellStyle style = cell.CellStyle;
            style.Alignment = HorizontalAlignment.Center;
            cell.CellStyle = style;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fontHeight"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public void SetCellFont(int fontHeight, int row, int col)
        {
            ICell cell = GetCell(row, col);
            ICellStyle style = cell.CellStyle;
            IFont font = workbook.CreateFont();
            font.FontHeight = (short)fontHeight;
            style.SetFont(font);
            cell.CellStyle = style;

        }

        public void SetCellBlod(int row, int col)
        {
            ICell cell = GetCell(row, col);
            ICellStyle style = cell.CellStyle;
            IFont font = workbook.CreateFont();
            font.Boldweight = short.MaxValue;
            style.SetFont(font);
            cell.CellStyle = style;
        }
        #endregion

        #region-- get row/cell
        /// <summary>
        /// 获取单元对象
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private ICell GetCell(int row, int col)
        {
            IRow curRow = GetRow(row);
            return CellUtil.GetCell(curRow, col);
        }

        /// <summary>
        /// 获取行对象
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        private IRow GetRow(int row)
        {
            return CellUtil.GetRow(row, currentSheet);
        }
        #endregion

        #region--debug
        [Conditional("DEBUG")]
        public void PrintCurrentSheet()
        {
            for (int rowIndex = 0; rowIndex < currentSheet.LastRowNum; rowIndex++)
            {
                IRow row = currentSheet.GetRow(rowIndex);
                for (int cellIndex = 0; cellIndex < row.LastCellNum; cellIndex++)
                {
                    Console.WriteLine(row.GetCell(cellIndex).StringCellValue);
                }
            }
        }
        #endregion

        #region DataTable helper
        /// <summary>
        /// 移动行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="fromRowIndex"></param>
        /// <param name="endRowIndex"></param>
        /// <param name="n"></param>
        private void ShiftRows(ISheet sheet, int fromRowIndex, int endRowIndex, int n)
        {
            sheet.ShiftRows(fromRowIndex, endRowIndex, n, false, true);
        }

        /// <summary>
        /// 拷贝行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="sourceRowIndex"></param>
        /// <param name="formRowIndex"></param>
        /// <param name="n"></param>
        private void CopyRows(ISheet sheet, int sourceRowIndex, int formRowIndex, int n)
        {
            for (int i = formRowIndex; i < formRowIndex + n; i++)
            {
                SheetUtil.CopyRow(sheet, sourceRowIndex, i);
            }
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="fromRowIndex"></param>
        /// <param name="n"></param>
        //private void InsertRows(ISheet sheet, int fromRowIndex, int n,ICell style)
        //{
        //    for (int rowIndex = fromRowIndex; rowIndex < fromRowIndex + n; rowIndex++)
        //    {
        //        IRow rowSource = sheet.GetRow(rowIndex + n);
        //        IRow rowInsert = sheet.CreateRow(rowIndex);
        //        rowInsert.Height = rowSource.Height;
        //        for (int colIndex = 0; colIndex < rowSource.LastCellNum; colIndex++)
        //        {
        //            ICell cellSource = rowSource.GetCell(colIndex);
        //            ICell cellInsert = rowInsert.CreateCell(colIndex);
        //            if (cellSource != null)
        //            {
        //                cellInsert.CellStyle = cellSource.CellStyle;
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 将文件流读取到DataTable数据表中
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable ReadStreamToDataTable(Stream fileStream, string sheetName = null, bool isFirstRowColumn = true)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "");
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                if (cell.CellType == CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = row.GetCell(j).DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString().Trim();
                                }
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 将泛类型集合List类转换成DataTable
        /// </summary>
        /// <param name="list">泛类型集合</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {
            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }
            //取出第一个实体的所有Propertie
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            //生成DataTable的structure
            //生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                dt.Columns.Add(entityProperties[i].Name);
            }
            //将所有entity添加到DataTable中
            foreach (object entity in entitys)
            {
                //检查所有的的实体都为同一类型
                if (entity.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(entity, null);
                }
                dt.Rows.Add(entityValues);
            }
            return dt;
        }

        #endregion



    }
}
