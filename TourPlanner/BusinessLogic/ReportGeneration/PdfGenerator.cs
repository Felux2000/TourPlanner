using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Converters;
using TourPlanner.Models;

namespace TourPlanner.BusinessLogic.ReportGeneration
{
    public static class PdfGenerator
    {
        public static void TourReportGenerator(Tour tour, string savePath, byte[] image)
        {
            string TARGET_PDF = savePath;
            PdfWriter writer = new PdfWriter(TARGET_PDF);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Paragraph nameParagraph = new Paragraph("Tour Information")
                .SetFont(boldFont)
                .SetFontSize(14);
            document.Add(nameParagraph);

            Paragraph tourNameParagraph = new Paragraph();
            tourNameParagraph.Add(new Text("Name: ").SetFont(boldFont));
            tourNameParagraph.Add(tour.Name).SetFont(normalFont);
            document.Add(tourNameParagraph);

            Paragraph tourDescriptionParagraph = new Paragraph();
            tourDescriptionParagraph.Add(new Text("Description: ").SetFont(boldFont));
            tourDescriptionParagraph.Add(tour.Description ?? "No Desciption").SetFont(normalFont);
            document.Add(tourDescriptionParagraph);

            Paragraph tourFromParagraph = new Paragraph();
            tourFromParagraph.Add(new Text("From: ").SetFont(boldFont));
            tourFromParagraph.Add(tour.From).SetFont(normalFont);
            document.Add(tourFromParagraph);

            Paragraph tourToParagraph = new Paragraph();
            tourToParagraph.Add(new Text("To: ").SetFont(boldFont));
            tourToParagraph.Add(tour.To).SetFont(normalFont);
            document.Add(tourToParagraph);

            Paragraph tourTransportPagraph = new Paragraph();
            tourTransportPagraph.Add(new Text("Transportation Type: ").SetFont(boldFont));
            tourTransportPagraph.Add(tour.TransportType).SetFont(normalFont);
            document.Add(tourTransportPagraph);

            Paragraph tourDistanceParagraph = new Paragraph();
            tourDistanceParagraph.Add(new Text("Distance: ").SetFont(boldFont));
            tourDistanceParagraph.Add(Math.Round(tour.Distance / 1000, 2).ToString()).SetFont(normalFont);
            tourDistanceParagraph.Add("km").SetFont(normalFont);
            document.Add(tourDistanceParagraph);

            Paragraph tourPopularityPagraph = new Paragraph();
            tourPopularityPagraph.Add(new Text("Popularity: ").SetFont(boldFont));
            tourPopularityPagraph.Add(tour.Popularity.Value.ToString()).SetFont(normalFont);
            document.Add(tourPopularityPagraph);

            Paragraph tourChildFriendlinessParagraph = new Paragraph();
            tourChildFriendlinessParagraph.Add(new Text("Child Friendliness (0-100% higher value means more child friendly): ").SetFont(boldFont));
            tourChildFriendlinessParagraph.Add(tour.ChildFriendliness.Value.ToString()).SetFont(normalFont);
            tourChildFriendlinessParagraph.Add("%").SetFont(normalFont);
            document.Add(tourChildFriendlinessParagraph);

            Paragraph tourEstimationParagraph = new Paragraph();
            tourEstimationParagraph.Add(new Text("Estimated time: ").SetFont(boldFont));
            tourEstimationParagraph.Add(TimeSpan.FromSeconds(tour.Estimation).ToString(@"hh\:mm")).SetFont(normalFont);
            tourEstimationParagraph.Add("h").SetFont(normalFont);
            document.Add(tourEstimationParagraph);
            Paragraph LogTableHeader = new Paragraph();
            LogTableHeader.Add(new Text("Logs: ").SetFont(boldFont));
            if (tour.LogList.Count > 0)
            {
                document.Add(LogTableHeader);
                Table table = new Table(UnitValue.CreatePercentArray(6)).UseAllAvailableWidth();
                table.AddHeaderCell(getHeaderCell("Date"));
                table.AddHeaderCell(getHeaderCell("Duration"));
                table.AddHeaderCell(getHeaderCell("Distance"));
                table.AddHeaderCell(getHeaderCell("Comment"));
                table.AddHeaderCell(getHeaderCell("Difficulty"));
                table.AddHeaderCell(getHeaderCell("Rating"));
                table.SetBackgroundColor(ColorConstants.WHITE);
                foreach (TourLog log in tour.LogList)
                {
                    table.AddCell(log.Date.ToString());
                    table.AddCell(log.Duration.ToString());
                    table.AddCell(log.Distance.ToString());
                    table.AddCell(log.Comment ?? string.Empty);
                    table.AddCell(log.Difficulty.ToString());
                    table.AddCell(log.Rating.ToString());
                }
                document.Add(table);
                document.Add(new AreaBreak());
            }
            else
            {
                LogTableHeader.Add("No Logs").SetFont(normalFont);
                document.Add(LogTableHeader);
            }

            Paragraph tourImageParagraph = new Paragraph();
            tourImageParagraph.Add("Map Image:");
            if (image.Length > 0)
            {
                document.Add(tourImageParagraph);
                ImageData imageData = ImageDataFactory.Create(image);
                document.Add(new Image(imageData));
            }
            else
            {
                tourImageParagraph.Add("No Image").SetFont(normalFont);
                document.Add(tourImageParagraph);
            }

            document.Close();

            using Process fileOpener = new Process();
            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = $"\"{TARGET_PDF}\"";
            fileOpener.Start();
        }

        public static void TourSummaryGenerator(List<Tour> tourList, string savePath)
        {
            string TARGET_PDF = savePath;
            PdfWriter writer = new PdfWriter(TARGET_PDF);
            PdfDocument pdf = new PdfDocument(writer);
            Document document = new Document(pdf);

            PdfFont normalFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            Paragraph nameParagraph = new Paragraph("Tour Summaries")
                .SetFont(boldFont)
                .SetFontSize(14);
            document.Add(nameParagraph);

            Table table = new Table(UnitValue.CreatePercentArray(4)).UseAllAvailableWidth();
            table.AddHeaderCell(getHeaderCell("Tour Name"));
            table.AddHeaderCell(getHeaderCell("Average Time"));
            table.AddHeaderCell(getHeaderCell("Average Distance (in km)"));
            table.AddHeaderCell(getHeaderCell("Average Rating"));
            table.SetBackgroundColor(ColorConstants.WHITE);
            foreach (Tour tour in tourList)
            {
                int counter = 0;
                double averageTime = 0;
                double averageDistance = 0;
                double averageRating = 0;
                foreach (TourLog log in tour.LogList)
                {
                    averageTime += log.Duration.TotalSeconds;
                    averageDistance += log.Distance;
                    averageRating += log.Rating;
                    counter++;
                }
                averageDistance = counter == 0 ? 0 : Math.Round(averageDistance / counter, 2);
                averageTime = counter == 0 ? 0 : averageTime / counter;
                averageRating = counter == 0 ? 0 : Math.Round(averageRating / counter, 2);
                TimeSpan averageDuration = TimeSpan.FromSeconds(averageTime);
                table.AddCell(tour.Name);
                table.AddCell(averageDuration.ToString());
                table.AddCell(averageDistance.ToString());
                table.AddCell(averageRating.ToString());
            }
            document.Add(table);

            document.Close();

            using Process fileOpener = new Process();
            fileOpener.StartInfo.FileName = "explorer";
            fileOpener.StartInfo.Arguments = $"\"{TARGET_PDF}\"";
            fileOpener.Start();
        }

        private static Cell getHeaderCell(String s)
        {
            return new Cell().Add(new Paragraph(s)).SetBold().SetBackgroundColor(ColorConstants.GRAY);
        }
    }
}
