using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAgenda.Data.Repository.Interfaces;

namespace WebApplicationAgenda.Controllers
{
   [Route("api/Reports")]
   [ApiController]
   public class ReportController : ControllerBase
   {
       private readonly IUserRepository _userRepository;
       private readonly ISaleRepository _saleRepository;
       //private readonly IcardRepository _cardRepository;
       public ReportController(IUserRepository userRepository, ISaleRepository saleRepository, ICardRepository cardRepository)
       {
           _userRepository = userRepository;
           //_cardRepository = cardRepository;
           _saleRepository = saleRepository;
       }
       [HttpGet("")]
       public IActionResult SellersWithCardsReport()
       {
           var users = _userRepository.GetReportUserCardsAsync().Result;
           using (var pdfStream = new MemoryStream())
           {
               using (var writer = new PdfWriter(pdfStream))
               {
                   using (var pdf = new PdfDocument(writer))
                   {
                       var document = new iText.Layout.Document(pdf);
                       document.Add(new iText.Layout.Element.Paragraph("TUP - UNIVERSIDAD AUSTRAL"));
                       document.Add(new iText.Layout.Element.Paragraph("Trabajo Final - Proyecto de Laboratorio"));
                       document.Add(new iText.Layout.Element.Paragraph("Alumnos: Razovich Sebastián - Pereira Yost Roman"));
                       document.Add(new iText.Layout.Element.Paragraph("Informe de Sellers de Cards"));

                       var table = new iText.Layout.Element.Table(5);
                       table.AddHeaderCell("ID");
                       table.AddHeaderCell("Nombre");
                       table.AddHeaderCell("Apellido");
                       table.AddHeaderCell("Email");
                       table.AddHeaderCell("Nro. de ventas");

                       if (users != null)
                       {
                           foreach (var usr in users)
                           {
                               table.AddCell(usr.Id.ToString());
                               table.AddCell(usr.Name);
                               table.AddCell(usr.LastName);
                               table.AddCell(usr.Price.ToString());
                               table.AddCell(usr.CardStock.ToString());

                           }
                           document.Add(table);
                       }
                   }
               }
               return File(pdfStream.ToArray(), "application/pdf", "Informe.pdf");
           }
       }

       [HttpGet("sales-in-month/{month}")]
       public IActionResult SalesInMonthReport(int month, int year)
       {

           var sales = _saleRepository.SalesInMonth(month, year).Result;
           using (var pdfStream = new MemoryStream())
           {
               using (var writer = new PdfWriter(pdfStream))
               {
                   using (var pdf = new PdfDocument(writer))
                   {
                       var document = new iText.Layout.Document(pdf);
                       document.Add(new iText.Layout.Element.Paragraph("TUP - UNIVERSIDAD AUSTRAL"));
                       document.Add(new iText.Layout.Element.Paragraph("Trabajo Final - Proyecto de Laboratorio"));
                       document.Add(new iText.Layout.Element.Paragraph("Alumnos: Razovich Sebastián - Pereira Yost Roman"));
                       document.Add(new iText.Layout.Element.Paragraph($"Informe de ventas hechas en el mes {month} del año {year}"));

                       var table = new iText.Layout.Element.Table(7);
                       table.AddHeaderCell("ID");
                       table.AddHeaderCell("Fecha");
                       table.AddHeaderCell("ID Usr");
                       table.AddHeaderCell("Mail Usr");
                       table.AddHeaderCell("ID Card");
                       table.AddHeaderCell("Titulo Card");
                       table.AddHeaderCell("Precio Card");

                       if (sales != null)
                       {
                           foreach (var sal in sales)
                           {
                               table.AddCell(sal.Id.ToString());
                               table.AddCell(sal.Date.ToString());
                               //table.AddCell(sal.UserId.ToString());
                              // table.AddCell(sal.UserEmail.ToString());
                              // table.AddCell(sal.FieldId.ToString());
                               //table.AddCell(sal.FieldName.ToString());
                               //table.AddCell(sal.CardPrice.ToString());

                           }
                           document.Add(table);
                       }
                   }
               }
               return File(pdfStream.ToArray(), "application/pdf", $"InformeReservas{month}-{year}.pdf");

           }
       }

   }

   
}
