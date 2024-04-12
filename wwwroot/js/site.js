// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Function to handle QR code scanning and redirection
function scanQRCode() {
    const codeReader = new ZXing.BrowserQRCodeReader();

    // Access the camera and start scanning
    codeReader.decodeFromVideoDevice(undefined, 'cameraFeed', (result, error) => {
        if (result) {
            // Redirect the user to the scanned QR code URL
            window.location.href = result.text;
        } else {
            console.error('Error scanning QR code:', error);
        }
    });
}
// fiquei aqui, a camara abre falta scanear o qr code, talvez com botao de scan