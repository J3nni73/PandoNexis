using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Litium.FieldFramework;
using Litium.Runtime.DependencyInjection;
using System.Drawing;
using System.Drawing.Imaging;
using BlipFill = DocumentFormat.OpenXml.Drawing.Spreadsheet.BlipFill;
using BottomBorder = DocumentFormat.OpenXml.Spreadsheet.BottomBorder;
using Color = DocumentFormat.OpenXml.Spreadsheet.Color;
using Fill = DocumentFormat.OpenXml.Spreadsheet.Fill;
using Font = DocumentFormat.OpenXml.Spreadsheet.Font;
using Fonts = DocumentFormat.OpenXml.Spreadsheet.Fonts;
using LeftBorder = DocumentFormat.OpenXml.Spreadsheet.LeftBorder;
using NonVisualDrawingProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualDrawingProperties;
using NonVisualPictureDrawingProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureDrawingProperties;
using NonVisualPictureProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.NonVisualPictureProperties;
using PatternFill = DocumentFormat.OpenXml.Spreadsheet.PatternFill;
using RightBorder = DocumentFormat.OpenXml.Spreadsheet.RightBorder;
using ShapeProperties = DocumentFormat.OpenXml.Drawing.Spreadsheet.ShapeProperties;
using TopBorder = DocumentFormat.OpenXml.Spreadsheet.TopBorder;
using Xdr = DocumentFormat.OpenXml.Drawing.Spreadsheet;


namespace PandoNexis.AddOns.Extensions.PNGenericGridView.Services
{

    [Service(ServiceType = typeof(ExcelExportService), Lifetime = DependencyLifetime.Singleton)]
    public class ExcelExportService
    {
        public const uint BoldStyleIndex = 1;
        public const uint HeadingStyleIndex = 2;
        public const uint WrapTextStyleIndex = 3;

        public class ExportRow
        {
            public List<ExportColumn> ExportPropertiesList { get; set; }
            public bool GroupRow { get; set; }
            public bool IsInGroup { get; set; }
            public bool ProductRow { get; set; }
        }

        public class ExportColumn
        {
            public string ExportValue { get; set; }
            public string FieldType { get; set; }
            public ImageItem MainPictureImage { get; set; }
            public uint StyleIndex { get; set; }
        }

        public class ImageItem
        {
            public string AdjustedHeight { get; set; }
            public string AdjustedWidth { get; set; }
            public byte[] FileValue { get; set; }
        }

        public MemoryStream CreateExcelPackage(List<ExportRow> exportRows)
        {
            using (var mem = new MemoryStream())
            {
                using (var package = SpreadsheetDocument.Create(mem, SpreadsheetDocumentType.Workbook))
                {
                    CreateParts(package, exportRows);
                    return mem;
                }
            }
        }

        public void StreamFileToBrowser(string fileName, MemoryStream stream)
        {
            if (stream != null)
            {
                //HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + fileName);
                //HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                //HttpContext.Current.Response.BinaryWrite(stream.ToArray());
                //HttpContext.Current.Response.Flush();
                //HttpContext.Current.Response.End();
            }
        }

        private static void CreateParts(SpreadsheetDocument document, IReadOnlyCollection<ExportRow> exportItems)
        {
            var workbookPart1 = document.AddWorkbookPart();
            GenerateWorkbookPart1Content(workbookPart1);

            GenerateStylesheet(workbookPart1);

            var worksheetPart1 = workbookPart1.AddNewPart<WorksheetPart>("rId1");
            GenerateWorksheetPart1Content(worksheetPart1, exportItems);
        }

        private static void GenerateWorkbookPart1Content(WorkbookPart workbookPart1)
        {
            var workbook1 = new Workbook();
            workbook1.AddNamespaceDeclaration("r", "http://schemas.openxmlformats.org/officeDocument/2006/relationships");

            var sheets1 = new Sheets();
            var sheet1 = new Sheet { Name = "Sheet1", SheetId = 1U, Id = "rId1" };
            sheets1.Append(sheet1);

            workbook1.Append(sheets1);
            workbookPart1.Workbook = workbook1;
        }

        private static void GenerateStylesheet(WorkbookPart workbookPart1)
        {
            var stylesPart = workbookPart1.WorkbookStylesPart ?? workbookPart1.AddNewPart<WorkbookStylesPart>();

            var stylesheet = new Stylesheet(
                new Fonts(
                    new Font(
                        new FontSize { Val = 11 },
                        new Color { Rgb = new HexBinaryValue { Value = "000000" } },
                        new FontName { Val = "Calibri" }),
                    //Bold
                    new Font(
                        new Bold(),
                        new FontSize { Val = 11 },
                        new Color { Rgb = new HexBinaryValue { Value = "000000" } },
                        new FontName { Val = "Calibri" }),
                    //Heading
                    new Font(
                        new FontSize { Val = 28 },
                        new Color { Rgb = new HexBinaryValue { Value = "000000" } },
                        new FontName { Val = "Calibri" })
                ),
                new Fills(
                    new Fill(new PatternFill { PatternType = PatternValues.None })
                ),
                new Borders(
                    new Border(
                        new LeftBorder(),
                        new RightBorder(),
                        new TopBorder(),
                        new BottomBorder(),
                        new DiagonalBorder())
                ),
                new CellFormats(
                    new CellFormat { FontId = 0, FillId = 0, BorderId = 0 },
                    new CellFormat { FontId = BoldStyleIndex, FillId = 0, BorderId = 0, ApplyFont = true },
                    new CellFormat { FontId = HeadingStyleIndex, FillId = 0, BorderId = 0, ApplyFont = true },
                    new CellFormat(new Alignment { WrapText = true }))
            );

            stylesPart.Stylesheet = stylesheet;
            stylesPart.Stylesheet.Save();
        }

        private static void GenerateWorksheetPart1Content(WorksheetPart worksheetPart1, IReadOnlyCollection<ExportRow> exportItems)
        {
            var worksheet1 = new Worksheet();
            var sheetData1 = new SheetData();
            var drawingsPart = worksheetPart1.AddNewPart<DrawingsPart>();
            uint rowIndex = 1;
            //var mergeCells = new MergeCells();

            var nbrOfColumns = exportItems.FirstOrDefault()?.ExportPropertiesList.Count ?? 0;

            var columns = AddColumns();
            worksheet1.Append(columns);

            var excelColumnNames = new string[nbrOfColumns];
            for (var n = 0; n < nbrOfColumns; n++)
            {
                excelColumnNames[n] = GetExcelColumnName(n);
            }

            foreach (var exportItem in exportItems)
            {
                if (rowIndex == 1)
                {
                    //var row = new Row { RowIndex = rowIndex, CustomHeight = true, Height = 70 };
                    var row = new Row { RowIndex = rowIndex };
                    sheetData1.Append(row);
                    for (var colIndex = 0; colIndex < nbrOfColumns; colIndex++)
                    {
                        AppendTextCell(excelColumnNames[colIndex] + rowIndex, exportItem.ExportPropertiesList[colIndex], row);
                    }
                }
                else if (exportItem.ProductRow)
                {
                    //var row = new Row { RowIndex = rowIndex, CustomHeight = true, Height = 120 };
                    var row = new Row { RowIndex = rowIndex };
                    sheetData1.Append(row);
                    for (var colIndex = 0; colIndex < nbrOfColumns; colIndex++)
                    {
                        AppendTextCell(excelColumnNames[colIndex] + rowIndex, exportItem.ExportPropertiesList[colIndex], row);
                    }
                }
                else
                {
                    //var row = new Row { RowIndex = rowIndex, CustomHeight = true, Height = 35 };
                    var row = new Row { RowIndex = rowIndex };
                    sheetData1.Append(row);
                    for (var colIndex = 0; colIndex < nbrOfColumns; colIndex++)
                    {
                        AppendTextCell(excelColumnNames[colIndex] + rowIndex, exportItem.ExportPropertiesList[colIndex], row);
                    }
                }
                rowIndex++;
            }

            worksheet1.Append(sheetData1);
            //worksheet1.Append(mergeCells);
            worksheetPart1.Worksheet = worksheet1;

            var imageColRow = 0;
            foreach (var item in exportItems)
            {
                if (item.GroupRow)
                    imageColRow++;

                for (var colIndex = 0; colIndex < nbrOfColumns; colIndex++)
                {
                    if (!item.ExportPropertiesList[colIndex].Equals(item.ExportPropertiesList.FirstOrDefault(i => i.MainPictureImage != null)))
                        continue;

                    AppendImageCell(worksheetPart1, item, worksheet1, drawingsPart, imageColRow, item.IsInGroup ? 2 : 1);
                    imageColRow++;
                }
            }
        }

        private static Columns AddColumns()
        {
            var columns = new Columns();

            var column = new Column { CustomWidth = true, Width = 26, Min = 1U, Max = 5U };
            columns.Append(column);

            column = new Column { CustomWidth = true, Width = 13, Min = 6U, Max = 11 };
            columns.Append(column);

            column = new Column { CustomWidth = true, Width = 18, Min = 12U, Max = 13 };
            columns.Append(column);

            return columns;
        }

        private static void AppendImageCell(WorksheetPart worksheetPart1, ExportRow exportItem, Worksheet worksheet1, DrawingsPart drawingsPart, int imageColRow, int colNumber)
        {
            // add image to worksheet
            if (!worksheet1.WorksheetPart.Worksheet.ChildElements.OfType<Drawing>().Any())
            {
                worksheet1.WorksheetPart.Worksheet.Append(new Drawing { Id = worksheetPart1.GetIdOfPart(drawingsPart) });
            }

            if (drawingsPart.WorksheetDrawing == null)
            {
                drawingsPart.WorksheetDrawing = new WorksheetDrawing();
            }
            var worksheetDrawing = drawingsPart.WorksheetDrawing;
            var imagePart = drawingsPart.AddImagePart(ImagePartType.Jpeg);
            var counter = 0;
            foreach (var image in exportItem.ExportPropertiesList)
            {
                counter++;
                if (image.MainPictureImage != null)
                {
                    if (image.MainPictureImage.FileValue.Length > 0)
                    {
                        byte[] imageBytes;
                        int height;
                        int width;
                        using (var ms = new MemoryStream(image.MainPictureImage.FileValue, 0, image.MainPictureImage.FileValue.Length))
                        {
                            using (var img = Image.FromStream(ms))
                            {
                                height = int.Parse(image.MainPictureImage.AdjustedHeight);
                                width = int.Parse(image.MainPictureImage.AdjustedWidth);

                                using var b = new Bitmap(img, new Size(width, height));
                                using (var ms2 = new MemoryStream())
                                {
                                    b.Save(ms2, ImageFormat.Jpeg);
                                    imageBytes = ms2.ToArray();
                                }
                            }
                        }
                        Stream stream = new MemoryStream(imageBytes);
                        imagePart.FeedData(stream);

                        // New code for image sizes
                        int extentsCx;
                        int extentsCy;
                        int colOffset;
                        int rowOffset;
                        if (counter == 1)
                        {
                            extentsCx = width * 9525;
                            extentsCy = height * 9525;
                            colOffset = 40000;
                            rowOffset = 130000;
                        }
                        else
                        {
                            extentsCx = width * 9525;
                            extentsCy = height * 9525;
                            colOffset = 40000; //740000;
                            rowOffset = 60000; //150000;
                        }
                        // Old code below, keep for future if customer wants to have old value sizes to image
                        //var extentsCx = 60 * 9250;
                        //var extentsCy = 60 * 9250;
                        //var colOffset = 40000;
                        //var rowOffset = 150000;

                        // New code for image sizes
                        //var extentsCx = 190 * 9250;
                        //var extentsCy = 190 * 9250;
                        //var colOffset = 40000;
                        //var rowOffset = 60000;

                        var nvps = worksheetDrawing.Descendants<NonVisualDrawingProperties>();
                        var nvpId = nvps.Any() ? (UInt32Value)worksheetDrawing.Descendants<NonVisualDrawingProperties>().Max(p => p.Id.Value) + 1 : 1U;
                        if (counter == 1)
                        {
                            var oneCellAnchor = new OneCellAnchor(
                                new Xdr.FromMarker
                                {
                                    ColumnId = new ColumnId((colNumber - 1).ToString()),
                                    RowId = new RowId(imageColRow.ToString()),
                                    ColumnOffset = new ColumnOffset(colOffset.ToString()),
                                    RowOffset = new RowOffset(rowOffset.ToString())
                                },
                                new Extent { Cx = extentsCx, Cy = extentsCy },
                                new Xdr.Picture(
                                    new NonVisualPictureProperties(
                                        new NonVisualDrawingProperties { Id = nvpId, Name = "Picture " + nvpId, Description = "test" },
                                        new NonVisualPictureDrawingProperties(new PictureLocks { NoChangeAspect = true })
                                    ),
                                    new BlipFill(
                                        new Blip { Embed = drawingsPart.GetIdOfPart(imagePart), CompressionState = BlipCompressionValues.Print },
                                        new Stretch(new FillRectangle())
                                    ),
                                    new ShapeProperties(
                                        new Transform2D(
                                            new Offset { X = 0, Y = 0 },
                                            new Extents { Cx = extentsCx, Cy = extentsCy }
                                        ),
                                       new PresetGeometry { Preset = ShapeTypeValues.Rectangle }
                                    )
                                ),
                                new ClientData()
                           );

                            worksheetDrawing.Append(oneCellAnchor);
                        }
                        if (counter > 1)
                        {
                            var oneCellAnchor = new OneCellAnchor(
                                new Xdr.FromMarker
                                {
                                    ColumnId = new ColumnId((colNumber - 1).ToString()),
                                    RowId = new RowId((imageColRow + 2).ToString()),
                                    ColumnOffset = new ColumnOffset(colOffset.ToString()),
                                    RowOffset = new RowOffset(rowOffset.ToString())
                                },
                                new Extent { Cx = extentsCx, Cy = extentsCy },
                                new Xdr.Picture(
                                    new NonVisualPictureProperties(
                                        new NonVisualDrawingProperties { Id = nvpId, Name = "Picture " + nvpId, Description = "test" },
                                        new NonVisualPictureDrawingProperties(new PictureLocks { NoChangeAspect = true })
                                    ),
                                    new BlipFill(
                                        new Blip { Embed = drawingsPart.GetIdOfPart(imagePart), CompressionState = BlipCompressionValues.Print },
                                        new Stretch(new FillRectangle())
                                    ),
                                    new ShapeProperties(
                                        new Transform2D(
                                            new Offset { X = 0, Y = 0 },
                                            new Extents { Cx = extentsCx, Cy = extentsCy }
                                        ),
                                        new PresetGeometry { Preset = ShapeTypeValues.Rectangle }
                                    )
                                ),
                                new ClientData()
                            );

                            worksheetDrawing.Append(oneCellAnchor);
                        }
                    }
                }
            }
        }

        private static string GetExcelColumnName(int columnIndex)
        {
            if (columnIndex < 26)
                return ((char)('A' + columnIndex)).ToString();

            var firstChar = (char)('A' + columnIndex / 26 - 1);
            var secondChar = (char)('A' + columnIndex % 26);

            return $"{firstChar}{secondChar}";
        }

        private static void AppendTextCell(string cellReference, ExportColumn exportObject, Row excelRow)
        {
            Cell cell;
            //  Add a new Excel Cell to our Row 
            if (exportObject.FieldType == SystemFieldTypeConstants.Decimal || exportObject.FieldType == SystemFieldTypeConstants.Int || exportObject.FieldType == SystemFieldTypeConstants.DecimalOption || exportObject.FieldType == SystemFieldTypeConstants.IntOption || exportObject.FieldType == SystemFieldTypeConstants.Long)
            {
                cell = new Cell { CellReference = cellReference, DataType = CellValues.Number };
            }
            else
            {
                cell = new Cell { CellReference = cellReference, DataType = CellValues.String };
            }

            if (exportObject.StyleIndex != 0)
                cell.StyleIndex = exportObject.StyleIndex;

            var cellValue = new CellValue { Text = exportObject.ExportValue };
            cell.Append(cellValue);
            excelRow.Append(cell);
        }

        //public ImageModel GetLogoForExcel(string topFolderName)
        //{
        //    var topFolder = ModuleMediaArchive.Instance.GetTopFolder(ModuleMediaArchive.Instance.AdminToken);
        //    ImageModel image = null;
        //    foreach (var iconfolder in topFolder.Folders)
        //    {
        //        if (iconfolder.Name == topFolderName)
        //        {
        //            var iconFiles = iconfolder.Files.GetFiles(ModuleMediaArchive.Instance.AdminToken);
        //            foreach (var file in iconFiles)
        //            {
        //                image = file.MapTo<ImageModel>();
        //                break;
        //            }
        //        }
        //    }

        //    return image;
        //}

        //public byte[] GetLogoForExcelInByte(string topFolderName)
        //{
        //    var topFolder = ModuleMediaArchive.Instance.GetTopFolder(ModuleMediaArchive.Instance.AdminToken);
        //    byte[] logoByte = null;
        //    foreach (var iconfolder in topFolder.Folders)
        //    {
        //        if (iconfolder.Name == topFolderName)
        //        {
        //            var iconFiles = iconfolder.Files.GetFiles(ModuleMediaArchive.Instance.AdminToken);
        //            foreach (var file in iconFiles)
        //            {
        //                logoByte = file.FileValue;
        //                break;
        //            }
        //        }
        //    }

        //    return logoByte;
        //}
    }

}
