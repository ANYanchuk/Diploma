using Microsoft.EntityFrameworkCore;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using TaskManager.Data.DbContexts;
using TaskManager.Core.Services;
using TaskManager.Core.Models.Entities;
using TaskManager.Core.Models.Data;

namespace TaskManager.Data.Services;

public class FileService : IFileService
{
    private readonly ApplicationDbContext context;

    public FileService(ApplicationDbContext context)
    {
        this.context = context;
    }

    public (string, string)? GetFile(int id)
    {
        UploadedFile? file = context.Files.FirstOrDefault(f => f.Id == id);
        if (file is null)
            return null;
        return (file.Path, file.Name);
    }

    public Stream GetErrandsDoc(DateTime since, DateTime till)
    {
        IEnumerable<Errand> errands = context.Errands
            .Include(e => e.Users)
            .Include(e => e.Report)
            .Include(e => e.ReportFormat)
            .Where(e => e.Started >= since && e.Deadline <= till);

        WordDocument document = new();
        IWSection section = document.AddSection();
        //Adds a new table into Word document
        IWTable table = section.AddTable();
        table.ResetCells(3, 4);
        table.TableFormat.Borders.LineWidth = 1.5f;

        // Set up Title row
        table.ApplyHorizontalMerge(0, 0, 3);
        WTableRow titleRow = table.Rows[0];
        WTableCell titleCell = titleRow.Cells[0];
        IWParagraph titleParagraph = titleCell.AddParagraph();
        titleParagraph.AppendText("Відомість виконаних доручень співробітниками кафедри інформаційних систем");
        titleCell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

        // Set up time range row
        table.ApplyHorizontalMerge(1, 0, 3);
        WTableRow timeRangeRow = table.Rows[1];
        WTableCell timeRangeCell = timeRangeRow.Cells[0];
        IWParagraph timeRangeParagraph = timeRangeCell.AddParagraph();
        timeRangeParagraph.AppendText($"З {since.ToShortDateString()} по {till.ToShortDateString()}");
        timeRangeCell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;

        // Set up header row
        {
            WTableRow headerRow = table.Rows[2];
            WTableCell userCell = headerRow.Cells[0];
            WTableCell stateCell = headerRow.Cells[1];
            WTableCell reportCell = headerRow.Cells[2];
            WTableCell dateCell = headerRow.Cells[3];
            userCell.CellFormat.Borders.LineWidth = 1.5f;
            stateCell.CellFormat.Borders.LineWidth = 1.5f;
            reportCell.CellFormat.Borders.LineWidth = 1.5f;
            dateCell.CellFormat.Borders.LineWidth = 1.5f;
            IWParagraph userParagraph = userCell.AddParagraph();
            IWParagraph stateParagraph = stateCell.AddParagraph();
            IWParagraph reportParagraph = reportCell.AddParagraph();
            IWParagraph dateParagraph = dateCell.AddParagraph();

            userParagraph.AppendText($"Користувач");
            stateParagraph.AppendText($"Статус");
            reportParagraph.AppendText($"Звіт");
            dateParagraph.AppendText($"Дата");
        }

        AlignCellContentForTable(table, HorizontalAlignment.Center);

        foreach (Errand e in errands)
        {
            WTableRow row = table.AddRow();
            table.ApplyHorizontalMerge(row.GetRowIndex(), 0, 3);
            WTableCell cell = row.Cells[0];
            cell.CellFormat.Borders.Top.LineWidth = 1.5f;
            IWParagraph paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"{e.Title}");
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"Тип: {e.Type}, Форма звіту: {e.ReportFormat.Name}");
            cell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"Дата видачі: {e.Started.ToShortDateString()}, Дата завершення: {e.Deadline?.ToShortDateString()}");
            cell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Single;
            cell.CellFormat.Borders.Bottom.LineWidth = 1.5f;

            foreach (ApplicationUser u in e.Users)
            {
                WTableRow userRow = table.AddRow(false);
                WTableCell userCell = userRow.Cells[0];
                WTableCell stateCell = userRow.Cells[1];
                WTableCell reportCell = userRow.Cells[2];
                WTableCell dateCell = userRow.Cells[3];
                IWParagraph userParagraph = userCell.AddParagraph();
                IWParagraph stateParagraph = stateCell.AddParagraph();
                IWParagraph reportParagraph = reportCell.AddParagraph();
                IWParagraph dateParagraph = dateCell.AddParagraph();

                stateParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                reportParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                dateParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;

                userParagraph.AppendText($"{u.FirstName} {u.LastName}");
                stateParagraph.AppendText($"{e.State}");
                reportParagraph.AppendText($"{e.ReportFormat.Name}");
                dateParagraph.AppendText($"{e.Report?.LastChanged.ToShortDateString()}");
            }
        }

        // Save the Word document to MemoryStream
        MemoryStream stream = new MemoryStream();
        document.Save(stream, FormatType.Docx);
        stream.Position = 0;

        return stream;
    }

    public Stream? GetUsersDoc(DateTime since, DateTime till, uint userId)
    {

        ApplicationUser? user = context.Users
            .Include(u => u.Errands).ThenInclude(e => e.Report)
            .Include(u => u.Errands).ThenInclude(e => e.ReportFormat)
            .FirstOrDefault(u => u.Id == userId);

        if (user is null)
            return null;

        IEnumerable<Errand> errands = user.Errands.ToList();

        WordDocument document = new();
        IWSection section = document.AddSection();
        //Adds a new table into Word document
        IWTable table = section.AddTable();
        table.ResetCells(4, 3);
        table.TableFormat.Borders.LineWidth = 1.5f;

        // Set up Title row
        table.ApplyHorizontalMerge(0, 0, 2);
        WTableRow titleRow = table.Rows[0];
        WTableCell titleCell = titleRow.Cells[0];
        IWParagraph titleParagraph = titleCell.AddParagraph();
        titleParagraph.AppendText("Відомість виконаних доручень для співробітника");
        titleCell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

        // Set up time range row
        table.ApplyHorizontalMerge(1, 0, 2);
        WTableRow nameRow = table.Rows[1];
        WTableCell nameCell = nameRow.Cells[0];
        IWParagraph nameParagraph = nameCell.AddParagraph();
        nameParagraph.AppendText($"{user.FirstName} {user.LastName}");
        nameCell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
        nameCell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

        // Set up time range row
        table.ApplyHorizontalMerge(2, 0, 2);
        WTableRow timeRangeRow = table.Rows[2];
        WTableCell timeRangeCell = timeRangeRow.Cells[0];
        IWParagraph timeRangeParagraph = timeRangeCell.AddParagraph();
        timeRangeParagraph.AppendText($"З {since.ToShortDateString()} по {till.ToShortDateString()}");
        timeRangeCell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;

        // Set up header row
        {
            WTableRow headerRow = table.Rows[3];
            WTableCell stateCell = headerRow.Cells[0];
            WTableCell reportCell = headerRow.Cells[1];
            WTableCell dateCell = headerRow.Cells[2];
            stateCell.CellFormat.Borders.LineWidth = 1.5f;
            reportCell.CellFormat.Borders.LineWidth = 1.5f;
            dateCell.CellFormat.Borders.LineWidth = 1.5f;
            IWParagraph stateParagraph = stateCell.AddParagraph();
            IWParagraph reportParagraph = reportCell.AddParagraph();
            IWParagraph dateParagraph = dateCell.AddParagraph();
            stateParagraph.AppendText($"Статус");
            reportParagraph.AppendText($"Звіт");
            dateParagraph.AppendText($"Дата");
        }

        AlignCellContentForTable(table, HorizontalAlignment.Center);

        foreach (Errand e in errands)
        {
            WTableRow row = table.AddRow();
            table.ApplyHorizontalMerge(row.GetRowIndex(), 0, 2);
            WTableCell cell = row.Cells[0];
            cell.CellFormat.Borders.Top.LineWidth = 1.5f;
            IWParagraph paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"{e.Title}");
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"Тип: {e.Type}, Форма звіту: {e.ReportFormat.Name}");
            cell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"Дата видачі: {e.Started.ToShortDateString()}, Дата завершення: {e.Deadline?.ToShortDateString()}");
            cell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Single;
            cell.CellFormat.Borders.Bottom.LineWidth = 1.5f;

            foreach (ApplicationUser u in e.Users)
            {
                WTableRow userRow = table.AddRow(false);
                WTableCell stateCell = userRow.Cells[0];
                WTableCell reportCell = userRow.Cells[1];
                WTableCell dateCell = userRow.Cells[2];
                IWParagraph stateParagraph = stateCell.AddParagraph();
                IWParagraph reportParagraph = reportCell.AddParagraph();
                IWParagraph dateParagraph = dateCell.AddParagraph();

                stateParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                reportParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                dateParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;

                stateParagraph.AppendText($"{e.State}");
                reportParagraph.AppendText($"{e.ReportFormat.Name}");
                dateParagraph.AppendText($"{e.Report?.LastChanged.ToShortDateString()}");
            }
        }

        // Save the Word document to MemoryStream
        MemoryStream stream = new MemoryStream();
        document.Save(stream, FormatType.Docx);
        stream.Position = 0;

        return stream;
    }

    public Stream GetDistributionDoc(DateTime since, DateTime till)
    {
        IEnumerable<Errand> errands = context.Errands
            .Include(e => e.Users)
            .Include(e => e.Report)
            .Include(e => e.ReportFormat)
            .Where(e => e.Started >= since && e.Deadline <= till);

        WordDocument document = new();
        IWSection section = document.AddSection();
        //Adds a new table into Word document
        IWTable table = section.AddTable();
        table.ResetCells(3, 4);
        table.TableFormat.Borders.LineWidth = 1.5f;

        // Set up Title row
        table.ApplyHorizontalMerge(0, 0, 3);
        WTableRow titleRow = table.Rows[0];
        WTableCell titleCell = titleRow.Cells[0];
        IWParagraph titleParagraph = titleCell.AddParagraph();
        titleParagraph.AppendText("Відомість розподілу доручень співробітниками кафедри інформаційних систем");
        titleCell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

        // Set up time range row
        table.ApplyHorizontalMerge(1, 0, 3);
        WTableRow timeRangeRow = table.Rows[1];
        WTableCell timeRangeCell = timeRangeRow.Cells[0];
        IWParagraph timeRangeParagraph = timeRangeCell.AddParagraph();
        timeRangeParagraph.AppendText($"{since.ToShortDateString()}-{till.ToShortDateString()}");
        timeRangeCell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;

        // Set up header row
        {
            WTableRow headerRow = table.Rows[2];
            WTableCell userCell = headerRow.Cells[0];
            WTableCell stateCell = headerRow.Cells[1];
            WTableCell reportCell = headerRow.Cells[2];
            WTableCell dateCell = headerRow.Cells[3];
            userCell.CellFormat.Borders.LineWidth = 1.5f;
            stateCell.CellFormat.Borders.LineWidth = 1.5f;
            reportCell.CellFormat.Borders.LineWidth = 1.5f;
            dateCell.CellFormat.Borders.LineWidth = 1.5f;
            IWParagraph userParagraph = userCell.AddParagraph();
            IWParagraph stateParagraph = stateCell.AddParagraph();
            IWParagraph reportParagraph = reportCell.AddParagraph();
            IWParagraph dateParagraph = dateCell.AddParagraph();

            userParagraph.AppendText($"Ім'я виконавця");
            stateParagraph.AppendText($"Прізвище виконавця");
            reportParagraph.AppendText($"Імейл виконавця");
            dateParagraph.AppendText($"Телефон виконавця");
        }

        AlignCellContentForTable(table, HorizontalAlignment.Center);

        foreach (Errand e in errands)
        {
            WTableRow row = table.AddRow();
            table.ApplyHorizontalMerge(row.GetRowIndex(), 0, 3);
            WTableCell cell = row.Cells[0];
            cell.CellFormat.Borders.Top.LineWidth = 1.5f;
            IWParagraph paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"{e.Title}");
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

            row = table.AddRow();
            table.ApplyHorizontalMerge(row.GetRowIndex(), 0, 3);
            cell = row.Cells[0];
            cell.CellFormat.Borders.Top.LineWidth = 1.5f;
            paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"{e.Body}");
            cell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"Тип: {e.Type}, Форма звіту: {e.ReportFormat.Name}");
            cell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Cleared;

            row = table.AddRow();
            cell = row.Cells[0];
            paragraph = cell.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.AppendText($"Дата видачі: {e.Started.ToShortDateString()}, Дата завершення: {e.Deadline?.ToShortDateString()}");
            cell.CellFormat.Borders.Top.BorderType = BorderStyle.Cleared;
            cell.CellFormat.Borders.Bottom.BorderType = BorderStyle.Single;
            cell.CellFormat.Borders.Bottom.LineWidth = 1.5f;

            foreach (ApplicationUser u in e.Users)
            {
                WTableRow userRow = table.AddRow(false);
                WTableCell userCell = userRow.Cells[0];
                WTableCell stateCell = userRow.Cells[1];
                WTableCell reportCell = userRow.Cells[2];
                WTableCell dateCell = userRow.Cells[3];
                IWParagraph userParagraph = userCell.AddParagraph();
                IWParagraph stateParagraph = stateCell.AddParagraph();
                IWParagraph reportParagraph = reportCell.AddParagraph();
                IWParagraph dateParagraph = dateCell.AddParagraph();

                stateParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                reportParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;
                dateParagraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Center;

                userParagraph.AppendText($"{u.FirstName}");
                stateParagraph.AppendText($"{u.LastName}");
                reportParagraph.AppendText($"{u.Email}");
                dateParagraph.AppendText($"{u.PhoneNumber}");
            }
        }

        // Save the Word document to MemoryStream
        MemoryStream stream = new MemoryStream();
        document.Save(stream, FormatType.Docx);
        stream.Position = 0;

        return stream;
    }

    private void AlignCellContent(WTableCell tableCell, VerticalAlignment verticalAlignment, HorizontalAlignment horizontalAlignment)
    {
        //Sets vertical alignment to the cell.
        tableCell.CellFormat.VerticalAlignment = verticalAlignment;
        //Iterates body items in table cell and set horizontal alignment.
        AlignCellContentForTextBody(tableCell, horizontalAlignment);
    }

    private void AlignCellContentForTextBody(WTextBody textBody, HorizontalAlignment horizontalAlignment)
    {
        for (int i = 0; i < textBody.ChildEntities.Count; i++)
        {
            //IEntity is the basic unit in DocIO DOM. 
            //Accesses the body items as IEntity
            IEntity bodyItemEntity = textBody.ChildEntities[i];
            //A Text body has 3 types of elements - Paragraph, Table and Block Content Control
            //Decides the element type by using EntityType
            switch (bodyItemEntity.EntityType)
            {
                case EntityType.Paragraph:
                    WParagraph paragraph = bodyItemEntity as WParagraph;
                    //Sets horizontal alignment for paragraph.
                    paragraph.ParagraphFormat.HorizontalAlignment = horizontalAlignment;
                    break;
                case EntityType.Table:
                    //Table is a collection of rows and cells
                    //Iterates through table's DOM and set horizontal alignment.
                    AlignCellContentForTable(bodyItemEntity as WTable, horizontalAlignment);
                    break;
                case EntityType.BlockContentControl:
                    BlockContentControl blockContentControl = bodyItemEntity as BlockContentControl;
                    //Iterates to the body items of Block Content Control and set horizontal alignment.
                    AlignCellContentForTextBody(blockContentControl.TextBody, horizontalAlignment);
                    break;
            }
        }
    }
    private void AlignCellContentForTable(IWTable table, HorizontalAlignment horizontalAlignment)
    {
        //Iterates the row collection in a table
        foreach (WTableRow row in table.Rows)
        {
            //Iterates the cell collection in a table row
            foreach (WTableCell cell in row.Cells)
            {
                //Iterate items in cell and set horizontal alignment
                AlignCellContentForTextBody(cell, horizontalAlignment);
            }
        }
    }
}
