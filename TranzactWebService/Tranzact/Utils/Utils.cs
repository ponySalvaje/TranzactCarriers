using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using Tranzact.Models.Adapter;

namespace Tranzact.Utils
{
    public static class Utils
    {
        public static IEnumerable<CarrierFileAdapter> GetCarriersFromFile(string FileDirectory)
        {
            IList<CarrierFileAdapter> carrierRows = new List<CarrierFileAdapter>();

            using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(FileDirectory)))
            {
                ExcelWorksheet xlWorksheet = package.Workbook.Worksheets[0];

                //get the total row count
                int rowCount = xlWorksheet.Dimension.End.Row;

                for (int i = 2; i < rowCount; i++)
                {
                    carrierRows.Add(new CarrierFileAdapter
                    {
                        Carrier = xlWorksheet.Cells[i, 1].Value.ToString(),
                        Plan = xlWorksheet.Cells[i, 2].Value.ToString(),
                        State = xlWorksheet.Cells[i, 3].Value.ToString(),
                        MonthOfBirth = xlWorksheet.Cells[i, 4].Value.ToString(),
                        MinimumAge = xlWorksheet.Cells[i, 5].Value.ToString(),
                        MaximumAge = xlWorksheet.Cells[i, 6].Value.ToString(),
                        Premium = Double.Parse(xlWorksheet.Cells[i, 7].Value.ToString())
                    });
                }

                return carrierRows;
            }
        }
    }
}
