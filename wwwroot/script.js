const qrImg = document.getElementById("qr-code");

function fetchQRCode() {
    qrImg.src = `/api/qrcode?${Date.now()}`; 
    qrImg.alt = "QR Code Loading..."; 
}

fetchQRCode();

setInterval(() => {
    fetchQRCode();
}, 10000);
