#!/bin/bash

echo "🔍 Verificando medio de pago en Tienda Nube..."
echo ""

echo "📋 PASOS PARA REGISTRAR TU MEDIO DE PAGO:"
echo ""
echo "1️⃣ Tu app necesita permisos adicionales:"
echo "   • Ve a: https://partners.tiendanube.com/"
echo "   • Edita tu app (ID: 22716)"
echo "   • En 'Scopes', agrega: write_payment_providers"
echo "   • Guarda los cambios"
echo ""

echo "2️⃣ Vuelve a autorizar la app con los nuevos permisos:"
echo "   👉 https://www.tiendanube.com/apps/22716/authorize"
echo ""

echo "3️⃣ Después de autorizar, registra el payment provider:"
echo "   curl -X POST http://localhost:5001/api/payment-provider/register"
echo ""

echo "4️⃣ Verifica que esté registrado:"
echo "   curl http://localhost:5001/api/payment-provider/list"
echo ""

echo "5️⃣ Para ver tu medio de pago en el checkout:"
echo "   • Ve a tu tienda en Tienda Nube"
echo "   • Panel Admin > Configuración > Medios de pago"
echo "   • Deberías ver 'SistePay' en la lista"
echo "   • Actívalo"
echo ""

echo "📝 NOTA: El scope actual es 'write_products'"
echo "   Necesitas agregar 'write_payment_providers' en el portal de socios"
echo ""
