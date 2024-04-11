using PayAndPlay.Models;
using Microsoft.AspNetCore.Mvc;
using IronBarCode;
using System.Drawing;

namespace PayAndPlay.Controllers
{
    public class QRCodeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public QRCodeController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult CreateQRCode()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateQRCode(QRCodeModel GenerateQRCode) 
        {
            try
            {
                GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(GenerateQRCode.QRCodeText, 200);
                barcode.AddBarcodeValueTextBelowBarcode();
                barcode.SetMargins(10);
                barcode.ChangeBarCodeColor(Color.Black);
                string path = Path.Combine(_hostingEnvironment.WebRootPath, "GeneratedQRCode");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "GeneratedQRCode/qrcode.png");
                barcode.SaveAsPng(filePath);
                string fileName = Path.GetFileName(filePath);
                string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/GeneratedQRCode/" + fileName;
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