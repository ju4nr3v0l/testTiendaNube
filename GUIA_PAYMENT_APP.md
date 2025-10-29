# 🏦 Guía Completa: Payment App para Tienda Nube

## ✅ Estado Actual de tu Aplicación

**Ya tienes implementado:**
- ✅ OAuth2 funcionando
- ✅ Backend .NET 8 con API
- ✅ Frontend Angular 17
- ✅ Client ID: 22716
- ✅ Access Token obtenido

**Falta implementar:**
- ❌ Solicitar permisos `read_payments` y `write_payments`
- ❌ Crear Payment Provider automáticamente al instalar
- ❌ Implementar checkout.js (JavaScript del checkout)
- ❌ API de Transactions
- ❌ Webhooks de pagos

---

## 📋 Pasos Completos para Payment App

### 1️⃣ Solicitar Habilitación de APIs de Pagos

**IMPORTANTE:** Los permisos de pagos NO se pueden activar desde el portal.

**Acción requerida:**
```
Enviar email a: partners@tiendanube.com

Asunto: Solicitud de habilitación de APIs de pagos - App ID 22716

Mensaje:
Hola equipo de Tiendanube,

Solicito la habilitación de los siguientes permisos para mi aplicación:
- App ID: 22716
- App Name: SistePay
- Permisos solicitados: read_payments, write_payments

La aplicación es un medio de pago que permitirá a las tiendas 
procesar pagos a través de SistePay.

Gracias,
[Tu nombre]
```

**Tiempo de respuesta:** 2-5 días hábiles

---

### 2️⃣ Actualizar Scopes en el Portal de Socios

Una vez aprobado por soporte, actualiza tu app en:
https://partners.tiendanube.com/

**Scopes necesarios:**
- `read_products` (ya lo tienes)
- `read_orders`
- `write_orders`
- `read_payments` ⭐ (requiere aprobación)
- `write_payments` ⭐ (requiere aprobación)

---

### 3️⃣ Crear el Archivo checkout.js

Este archivo debe estar alojado en un CDN con HTTPS.

**Opciones:**
- AWS S3 + CloudFront
- Netlify
- Vercel
- GitHub Pages

**Contenido mínimo de checkout.js:**
```javascript
// checkout.js - Debe estar en HTTPS
(function() {
  window.SistePayCheckout = {
    init: function(options) {
      console.log('SistePay Checkout iniciado', options);
      
      // Escuchar eventos del checkout
      window.addEventListener('message', function(event) {
        if (event.data.type === 'checkout_ready') {
          // Checkout listo para recibir pagos
        }
      });
    },
    
    // Procesar el pago
    processPayment: function(paymentData) {
      // Enviar datos al backend
      fetch('http://localhost:5001/api/transactions', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(paymentData)
      })
      .then(response => response.json())
      .then(data => {
        // Notificar a Tienda Nube
        window.parent.postMessage({
          type: 'payment_success',
          transaction_id: data.id
        }, '*');
      });
    }
  };
})();
```

---

### 4️⃣ Implementar API de Transactions

Tu backend debe exponer estos endpoints:

**POST /api/transactions** - Crear transacción
```json
{
  "order_id": "12345",
  "amount": 100.50,
  "currency": "COP",
  "payment_method": "credit_card"
}
```

**GET /api/transactions/{id}** - Consultar transacción

**POST /api/transactions/{id}/confirm** - Confirmar pago

---

### 5️⃣ Registrar Payment Provider al Instalar

Cuando una tienda instala tu app, debes:

1. Obtener el access_token (ya lo haces)
2. Crear el Payment Provider automáticamente:

```bash
POST https://api.tiendanube.com/v1/{store_id}/payment_providers
Authorization: bearer {access_token}

{
  "name": "SistePay",
  "public_name": "Pagar con SistePay",
  "description": "Medio de pago seguro SistePay",
  "logo_urls": {
    "400x120": "https://tu-cdn.com/logo-400x120.png"
  },
  "supported_currencies": ["COP", "USD"],
  "supported_payment_methods": ["credit_card", "debit_card"],
  "checkout_js_url": "https://tu-cdn.com/checkout.js",
  "checkout_payment_options": ["credit_card"],
  "configuration_url": "https://tu-app.com/config",
  "rates_definition": ["percentage"]
}
```

---

### 6️⃣ Tipos de Integración

**Opción A: Integración Transparente**
- El usuario paga sin salir del checkout
- Requiere checkout.js completo
- Mejor experiencia de usuario

**Opción B: Integración Externa (Más fácil)**
- Redirige a tu sitio para pagar
- Más simple de implementar
- Retorna al checkout después del pago

**Opción C: Modal**
- Abre un iframe/modal
- Intermedio entre A y B

---

### 7️⃣ Flujo Completo de Pago

```
1. Cliente llega al checkout de Tienda Nube
2. Selecciona "SistePay" como medio de pago
3. Tu checkout.js se carga
4. Cliente ingresa datos de pago
5. checkout.js envía datos a tu backend
6. Tu backend procesa el pago
7. Creas una Transaction en Tienda Nube
8. Notificas el resultado al checkout
9. Tienda Nube completa la orden
```

---

## 🚀 Implementación Rápida (Integración Externa)

Para empezar rápido, usa **integración externa**:

1. **No necesitas checkout.js complejo**
2. **Redirige a tu página de pago**
3. **Procesa el pago**
4. **Redirige de vuelta con el resultado**

```json
{
  "checkout_payment_options": ["external"],
  "checkout_js_url": "https://tu-cdn.com/redirect.js"
}
```

**redirect.js simple:**
```javascript
window.location.href = 'https://tu-app.com/pay?order=' + orderId;
```

---

## 📝 Checklist de Implementación

### Antes de solicitar aprobación:
- [ ] App registrada en Portal de Socios
- [ ] Permisos de pagos aprobados por soporte
- [ ] checkout.js alojado en HTTPS
- [ ] Payment Provider creado automáticamente
- [ ] API de Transactions implementada
- [ ] Webhooks de pagos configurados
- [ ] Pruebas en tienda demo
- [ ] Logos en alta calidad (400x120px)
- [ ] Documentación para comerciantes
- [ ] Política de privacidad y términos

### Para producción:
- [ ] SSL/HTTPS en todos los endpoints
- [ ] Base de datos para transacciones
- [ ] Logs y monitoreo
- [ ] Manejo de errores robusto
- [ ] Pruebas de carga
- [ ] Cumplimiento PCI-DSS (si procesas tarjetas)

---

## 🔗 URLs Útiles

- Portal de Socios: https://partners.tiendanube.com/
- Documentación API: https://tiendanube.github.io/api-documentation/
- Soporte Partners: partners@tiendanube.com
- Comunidad: https://comunidad.tiendanube.com/

---

## ⏱️ Tiempo Estimado

- Solicitud de permisos: **2-5 días**
- Implementación básica: **1-2 semanas**
- Pruebas y ajustes: **1 semana**
- Auditoría de Tiendanube: **1-2 semanas**
- **Total: 4-6 semanas**

---

## 💡 Recomendación

**Para MVP rápido:**
1. Solicita permisos HOY a partners@tiendanube.com
2. Mientras esperas, implementa integración externa (más simple)
3. Aloja checkout.js en GitHub Pages o Netlify
4. Prueba con tienda demo
5. Después optimiza a integración transparente

**Tu app actual ya tiene el 40% del trabajo hecho (OAuth + Backend).**
