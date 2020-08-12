import { secret } from './secret';
var CryptoJS = require("crypto-js");

function encryptText(input) {
    return CryptoJS.HmacSHA256(input, secret).toString()
}

export{
    encryptText,
}
