# AIB-payment-sample

Two sample AIB payment forms for Authipay (.Net Core MVC)

Please, don't use client form for production, as it exposes the shared secret.

Minimal settings in aib.json:
```javascript
{
  "url": "https://www.ipg-online.com/connect/gateway/processing",  // the endpoint for real payment, not the test
  "responseSuccessUrl": "",                 // Url to redirect user after the payment
  "responseFailUrl": "",                    // Url to redirect user after the payment
  "txntype": "sale",                        // Type of transaction
  "timezone": "Europe/Dublin",              // Timezone
  "txndatetime": "2021:02:09-22:09:00",     // Formatted datetime, shouldn't be more than 15 minutes before payment
  "hashalgorithm": "SHA256",                // Hash algorithm
  "hash": "",                               // Hash of some fields, including secret
  "storename": "13013639271",               // Storename, actually the ID of your store
  "mode": "payonly",                        // Payment mode
  "oid": "abcd123",                         // OrderId, optional
  "chargetotal": "13.00",                   // Amount
  "currency": "978",                        // Currency code
  "paymentMethod": "M",                     // Payment method, optional
  "sharedSecret": ""                        // Your shared secret
}
```
