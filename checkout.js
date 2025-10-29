// SistePay Checkout usando Nube SDK
(function() {
  'use strict';
  
  console.log('SistePay Checkout v2.0 - Nube SDK');
  
  // Esperar a que Nube SDK esté disponible
  function waitForNube() {
    if (typeof Nube !== 'undefined') {
      initCheckout();
    } else {
      setTimeout(waitForNube, 100);
    }
  }
  
  function initCheckout() {
    console.log('Nube SDK loaded');
    
    // Inicializar Nube SDK
    Nube.init({
      onReady: function() {
        console.log('Nube SDK ready');
        setupPaymentOption();
      }
    });
  }
  
  function setupPaymentOption() {
    // Registrar el método de pago
    Nube.Checkout.addPaymentOption({
      id: 'sistepay',
      name: 'SistePay',
      
      // Cuando se carga la opción de pago
      onLoad: function(context) {
        console.log('Payment option loaded', context);
        
        // Obtener datos del checkout
        var checkoutData = context.checkout;
        var orderId = checkoutData.order_id || Date.now();
        var amount = checkoutData.total || 0;
        
        // Construir URL de redirección
        var paymentUrl = 'http://localhost:4200/payments' +
          '?order_id=' + orderId +
          '&amount=' + amount +
          '&checkout_id=' + (checkoutData.id || '') +
          '&return_url=' + encodeURIComponent(window.location.href);
        
        // Mostrar botón de pago
        return {
          html: '<div style="padding: 20px; text-align: center;">' +
            '<p style="margin-bottom: 15px;">Serás redirigido a SistePay para completar tu pago de forma segura.</p>' +
            '<button id="sistepay-btn" style="padding: 15px 30px; background: #4CAF50; color: white; border: none; border-radius: 4px; cursor: pointer; font-size: 16px;">' +
            'Continuar al Pago' +
            '</button>' +
          '</div>',
          
          onRender: function() {
            // Agregar evento al botón
            document.getElementById('sistepay-btn').addEventListener('click', function() {
              window.location.href = paymentUrl;
            });
          }
        };
      },
      
      // Cuando se envía el formulario
      onSubmit: function(context) {
        console.log('Payment submitted', context);
        
        var checkoutData = context.checkout;
        var orderId = checkoutData.order_id || Date.now();
        var amount = checkoutData.total || 0;
        
        // Redirigir a la página de pago
        var paymentUrl = 'http://localhost:4200/payments' +
          '?order_id=' + orderId +
          '&amount=' + amount +
          '&checkout_id=' + (checkoutData.id || '') +
          '&return_url=' + encodeURIComponent(window.location.href);
        
        window.location.href = paymentUrl;
        
        // Prevenir el envío del formulario
        return false;
      }
    });
  }
  
  // Iniciar
  waitForNube();
})();
