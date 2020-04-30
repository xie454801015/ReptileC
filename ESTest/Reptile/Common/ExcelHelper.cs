using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Eval;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace Reptile.Common
{
    public static class ExcelHelper
    {

        /// <summary>
        /// 导入excel 文件
        /// </summary>
        /// <param name="filePath">目标Excel文件的物理路径(xxx.xls)</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns></returns>
        public static DataTable ImportExcelFile(string filePath, bool isFirstRowColumn)
        {
            HSSFWorkbook hssfworkbook;
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    hssfworkbook = new HSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            NPOI.SS.UserModel.ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();

            // 添加列
            if (isFirstRowColumn)
            {
                for (int j = sheet.GetRow(0).FirstCellNum; j < (sheet.GetRow(0).LastCellNum); ++j)
                {
                    ICell cell = sheet.GetRow(0).GetCell(j);
                    if (cell != null)
                    {
                        string cellValue = cell.StringCellValue;//将列的值设为列名称
                        if (cellValue != null)
                        {
                            DataColumn column = new DataColumn(cellValue);
                            dt.Columns.Add(column);
                        }
                    }
                }
                rows.MoveNext();//如果Excel表的第一行是表名称，则往下推进一行（数据的遍历从第二行开始）           
            }
            while (rows.MoveNext())
            {
                HSSFRow row = (HSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    NPOI.SS.UserModel.ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        if (cell.CellType == CellType.Numeric && DateUtil.IsCellDateFormatted(cell)) //如果单元格是日期格式  
                        {
                            dr[i] = cell.DateCellValue.ToString("yyyy-MM-dd");
                        }
                        else
                        {
                            dr[i] = cell.ToString();
                        }
                    }
                }
                dt.Rows.Add(dr);
            }
            RemoveEmpty(dt);//删除DataTable中的空白行
            return dt;
        }

        /// <summary>
        /// 讲datatable 数据导出excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filePath"></param>
        public static void DataTableToExcel(DataTable dt, string filePath)
        {
            if (dt.TableName == null || dt.TableName == "")
            {
                dt.TableName = DateTime.Now.ToString();
            }
            if (!string.IsNullOrEmpty(filePath) && null != dt && dt.Rows.Count > 0)
            {
                NPOI.HSSF.UserModel.HSSFWorkbook workbook = new NPOI.HSSF.UserModel.HSSFWorkbook();
                NPOI.SS.UserModel.ISheet sheet = workbook.CreateSheet();
                NPOI.SS.UserModel.IRow row = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    row.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    NPOI.SS.UserModel.IRow row2 = sheet.CreateRow(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row2.CreateCell(j).SetCellValue(Convert.ToString(dt.Rows[i][j]));
                    }
                }
                sheet.CreateFreezePane(0, 1, 0, 1);//冻结第一行（第一行不能拖动）
                // 写入到客户端    
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    workbook.Write(ms);
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                    {
                        byte[] data = ms.ToArray();
                        fs.Write(data, 0, data.Length);
                        fs.Flush();
                    }
                    workbook = null;
                }
            }

        }

        /// <summary>
        /// 除去DataTable中的空白行
        /// </summary>
        /// <param name="dt"></param>
         static void RemoveEmpty(DataTable dt)
        {
            List<DataRow> removelist = new List<DataRow>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                bool IsNull = true;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (!string.IsNullOrEmpty(dt.Rows[i][j].ToString().Trim()))
                    {
                        IsNull = false;
                    }
                }
                if (IsNull)
                {
                    removelist.Add(dt.Rows[i]);
                }
            }
            foreach(var item in removelist)
            {
                dt.Rows.Remove(item);
            }
        }

    }
}


