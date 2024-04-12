using PayAndPlay.Models;
using Microsoft.AspNetCore.Mvc;
using IronBarCode;
using System.Drawing;
using PayAndPlay.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PayAndPlay.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _context;
        public QRCodeController(IWebHostEnvironment hostingEnvironment, ApplicationDbContext context)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }
        public IActionResult CreateQRCode()
        {
            //ViewBag.DJs = new SelectList(_context.Tdjs.ToList(), "ID", "UserName");
            try
            {
                string redirectUrl = "http://localhost:5281/PedidoUser/PlayListDjPedidos/" + HttpContext.Session.GetString("ID"); // Replace with your desired URL

                GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(redirectUrl, 500);
                barcode.AddBarcodeValueTextBelowBarcode();
                barcode.AddAnnotationTextAboveBarcode("SCANEIA ESTE QR CODE PARA OUVIRES AS TUAS MÚSICAS");
                barcode.AddAnnotationTextBelowBarcode("DJ: " + HttpContext.Session.GetString("UTILIZADOR"));
                barcode.SetMargins(30);
                barcode.ChangeBarCodeColor(Color.Black);

                string path = Path.Combine(_hostingEnvironment.WebRootPath, "GeneratedQRCode");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string imageName = HttpContext.Session.GetString("UTILIZADOR") + ".png"; // Provide a fixed filename for the QR code image
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "GeneratedQRCode/" + imageName);
                barcode.SaveAsPng(filePath);

                string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}/GeneratedQRCode/{imageName}";
                ViewBag.QrCodeUrl = imageUrl;
            }
            catch (Exception)
            {
                throw;
            }

            return View();
        }

    }
}