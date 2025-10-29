(function() {
  'use strict';
  
  console.log('SistePay Checkout v1.0 loaded');
  
  // Esperar a que LoadCheckoutPaymentContext esté disponible
  var checkInterval = setInterval(function() {
    if (typeof LoadCheckoutPaymentContext !== 'undefined') {
      clearInterval(checkInterval);
      initCheckout();
    }
  }, 100);
  
  function initCheckout() {
    LoadCheckoutPaymentContext(function(Checkout, PaymentOptions) {
      console.log('Checkout context initialized');
      
      // Registrar el método de pago
      Checkout.addPaymentOption({
        id: 'sistepay_credit_card',
        name: 'Tarjeta de Crédito',
        
        // Renderizar el formulario
        onLoad: function() {
          console.log('Payment option loaded');
          
          var html = '<div style="padding: 20px;">' +
            '<div style="margin-bottom: 15px;">' +
              '<label style="display: block; margin-bottom: 5px;">Número de tarjeta</label>' +
              '<input type="text" id="card_number" placeholder="1234 5678 9012 3456" style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 4px;" />' +
            '</div>' +
            '<div style="margin-bottom: 15px;">' +
              '<label style="display: block; margin-bottom: 5px;">Nombre del titular</label>' +
              '<input type="text" id="card_holder" placeholder="JUAN PEREZ" style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 4px;" />' +
            '</div>' +
            '<div style="display: flex; gap: 10px; margin-bottom: 15px;">' +
              '<div style="flex: 1;">' +
                '<label style="display: block; margin-bottom: 5px;">Vencimiento</label>' +
                '<input type="text" id="expiry" placeholder="MM/AA" style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 4px;" />' +
              '</div>' +
              '<div style="flex: 1;">' +
                '<label style="display: block; margin-bottom: 5px;">CVV</label>' +
                '<input type="text" id="cvv" placeholder="123" style="width: 100%; padding: 10px; border: 1px solid #ddd; border-radius: 4px;" />' +
              '</div>' +
            '</div>' +
          '</div>';
          
          return html;
        },
        
        // Procesar el pago
        onSubmit: function() {
          console.log('Processing payment...');
          
          var cardNumber = document.getElementById('card_number').value;
          var cardHolder = document.getElementById('card_holder').value;
          var expiry = document.getElementById('expiry').value;
          var cvv = document.getElementById('cvv').value;
          
          // Validación básica
          if (!cardNumber || !cardHolder || !expiry || !cvv) {
            Checkout.setPaymentError('Por favor complete todos los campos');
            return;
          }
          
          // Simular procesamiento (aquí iría la llamada a tu backend)
          setTimeout(function() {
            // Pago exitoso
            Checkout.setPaymentSuccess({
              transaction_id: 'SISTEPAY_' + Date.now(),
              payment_method: 'credit_card',
              card_last_digits: cardNumber.slice(-4)
            });
          }, 1500);
        }
      });
    });
  }
})();
