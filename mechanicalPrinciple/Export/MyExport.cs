using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft;
using System.Windows.Forms;
using mechanicalPrinciple.PublicElement;

namespace mechanicalPrinciple.Export
{
    class MyExport
    {
        public static ProgressBar ProgressBar1 = new ProgressBar();
        public static void ExportToExcel()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string fileName = "";
            int isOk = 0;
            saveFileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                isOk = 1;
            }
            if (isOk == 0)
            {
                MessageBox.Show("你已取消保存文件！", "小贴士", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            MessageBox.Show("导出过程需要十几秒，稍安勿躁！", "小提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.
                Application.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)(
                workbook.Worksheets[1]);
            //Microsoft.Office.Interop.Excel.Range range = null;
            ////range = worksheet.get_Range(excel.Cells[1, 1], excel.Cells[5, 5]);
            //range = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[5,5]];
            //range.ColumnWidth = 15;
            //range.RowHeight = 25;
            //range.Borders.LineStyle = 1;
            //range.BorderAround2(1, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
            //    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Color.Black, Type.Missing);
            //range.Font.Size = 12;
            //range.Font.Name = "宋体";
            //range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignJustify;
            excel.Cells[1][1] = "度数";
            excel.Cells[2][1] = "s";
            excel.Cells[3][1] = "ds/dδ";
            excel.Cells[4][1] = "d2s/dδ2";
            for (int i=0; i<=360; i++)
            {
                excel.Cells[1][i + 2] = i;
                excel.Cells[2][i + 2] = Common.s[i];
                excel.Cells[3][i + 2] = Common.ds[i];
                excel.Cells[4][i + 2] = Common.d2s[i];
                //excel.Cells[i + 1][1] = i;
                //excel.Cells[i + 1][2] = Common.s[i];
                //excel.Cells[i + 1][3] = Common.ds[i];
                //excel.Cells[i + 1][4] = Common.d2s[i];
            }
            //if (!System.IO.File.Exists("C:\\Users\\john\\Desktop\\hx2.xlsx"))
            //{
            //    worksheet.SaveAs("C:\\Users\\john\\Desktop\\hx2.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
            //        Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            //}else
            //{
            //    worksheet.Copy(Type.Missing, Type.Missing);
            //}
            worksheet.SaveAs(fileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            workbook.Save();
            workbook.Close(false, Type.Missing, Type.Missing);
            MessageBox.Show("保存成功！", "恭喜！");
        }
    }
}
