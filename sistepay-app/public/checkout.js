(function() {
  console.log('SistePay Checkout loaded');
  
  // Inicializar el checkout
  if (window.LoadCheckoutPaymentContext) {
    LoadCheckoutPaymentContext(function(Checkout) {
      console.log('Checkout context loaded', Checkout);
      
      // Indicar que el checkout está listo
      Checkout.addPaymentOption({
        id: 'credit_card',
        name: 'Tarjeta de Crédito',
        fields: [
          {
            id: 'card_number',
            label: 'Número de tarjeta',
            type: 'text',
            required: true
          },
          {
            id: 'card_holder',
            label: 'Titular',
            type: 'text',
            required: true
          },
          {
            id: 'expiry',
            label: 'Vencimiento (MM/AA)',
            type: 'text',
            required: true
          },
          {
            id: 'cvv',
            label: 'CVV',
            type: 'text',
            required: true
          }
        ],
        onSubmit: function(paymentData) {
          console.log('Payment submitted', paymentData);
          
          // Simular procesamiento exitoso
          setTimeout(function() {
            Checkout.setPaymentSuccess({
              transaction_id: 'test_' + Date.now(),
              status: 'approved'
            });
          }, 1000);
        }
      });
    });
  }
})();
