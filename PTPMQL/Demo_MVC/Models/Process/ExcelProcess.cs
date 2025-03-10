using OfficeOpenXml;
using System.Data;

namespace Demo_MVC.Models.Process
{
    public class ExcelProcess
    {
        public ExcelProcess()
        {
            // Set the license context for EPPlus (required for non-commercial use)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// Reads data from an Excel file and converts it into a DataTable.
        /// </summary>
        /// <param name="filePath">The full path to the Excel file.</param>
        /// <returns>A DataTable containing the Excel data.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file does not exist.</exception>
        /// <exception cref="Exception">Thrown if there is an error reading the Excel file.</exception>
        public DataTable ReadExcelToDataTable(string filePath)
        {
            // Check if the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("The specified Excel file does not exist.", filePath);
            }

            DataTable dataTable = new DataTable();

            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    // Get the first worksheet in the Excel file
                    var worksheet = package.Workbook.Worksheets[0];
                    if (worksheet == null || worksheet.Dimension == null)
                    {
                        throw new Exception("The Excel file is empty or has no valid worksheets.");
                    }

                    // Get the dimensions of the worksheet
                    int rowCount = worksheet.Dimension.Rows;
                    int colCount = worksheet.Dimension.Columns;

                    // Add columns to the DataTable based on the first row (header)
                    for (int col = 1; col <= colCount; col++)
                    {
                        string columnName = worksheet.Cells[1, col].Text?.Trim() ?? $"Column{col}";
                        dataTable.Columns.Add(columnName);
                    }

                    // Read the data rows (starting from the second row, assuming the first row is the header)
                    for (int row = 2; row <= rowCount; row++)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int col = 1; col <= colCount; col++)
                        {
                            dataRow[col - 1] = worksheet.Cells[row, col].Text?.Trim();
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }

                return dataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Error reading the Excel file: " + ex.Message, ex);
            }
        }
    }
}