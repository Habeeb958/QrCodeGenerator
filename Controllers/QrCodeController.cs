using Microsoft.AspNetCore.Mvc;
using QRCoder;

[Route("api/qrcode")]
[ApiController]
public class QrCodeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetQrCode()
    {
        try
        {
            string frontendUrl = $"{Request.Scheme}://{Request.Host}/movies.html";

            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");

            string qrCodeUrl = $"{frontendUrl}?timestamp={timestamp}";

            using QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrCodeUrl, QRCodeGenerator.ECCLevel.Q);
            using PngByteQRCode qrCode = new(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(10);

            return File(qrCodeImage, "image/png");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating QR code: {ex.Message}");
            return StatusCode(500, "An error occurred while generating the QR code.");
        }
    }
}
