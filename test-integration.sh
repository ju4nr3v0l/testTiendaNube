#!/bin/bash

echo "🔍 Verificando integración con Tienda Nube..."
echo ""

# Verificar servicios
echo "1️⃣ Verificando servicios..."
curl -s http://localhost:4200 > /dev/null && echo "   ✅ Frontend: http://localhost:4200" || echo "   ❌ Frontend no responde"
curl -s http://localhost:5001/swagger/index.html > /dev/null && echo "   ✅ Backend: http://localhost:5001" || echo "   ❌ Backend no responde"
echo ""

# Verificar configuración
echo "2️⃣ Verificando configuración..."
if grep -q "REEMPLAZAR" /Users/juanmarulanda/WebstormProjects/sistepay-app/SistePay.TiendaNube.API/appsettings.json; then
    echo "   ❌ Credenciales no configuradas en appsettings.json"
else
    echo "   ✅ Credenciales configuradas en backend"
fi

if grep -q "REEMPLAZAR" /Users/juanmarulanda/WebstormProjects/sistepay-app/sistepay-app/src/environments/environment.ts; then
    echo "   ❌ Credenciales no configuradas en environment.ts"
else
    echo "   ✅ Credenciales configuradas en frontend"
fi
echo ""

# Probar endpoint de health
echo "3️⃣ Probando endpoints del backend..."
echo "   Testing /api/store/info..."
RESPONSE=$(curl -s -o /dev/null -w "%{http_code}" http://localhost:5001/api/store/info)
if [ "$RESPONSE" = "200" ]; then
    echo "   ✅ Store Info: OK (200)"
elif [ "$RESPONSE" = "404" ]; then
    echo "   ⚠️  Store Info: No autorizado (404) - Necesitas completar OAuth"
else
    echo "   ⚠️  Store Info: HTTP $RESPONSE"
fi
echo ""

# Instrucciones OAuth
echo "4️⃣ Para completar la integración OAuth:"
echo ""
CLIENT_ID=$(grep -o '"ClientId": "[^"]*' /Users/juanmarulanda/WebstormProjects/sistepay-app/SistePay.TiendaNube.API/appsettings.json | cut -d'"' -f4)
echo "   Visita esta URL en tu navegador:"
echo "   👉 https://www.tiendanube.com/apps/authorize/authorize?client_id=$CLIENT_ID&redirect_uri=http://localhost:4200/auth/callback"
echo ""
echo "   Después de autorizar, serás redirigido a:"
echo "   http://localhost:4200/auth/callback?code=CODIGO_TEMPORAL"
echo ""
echo "   El frontend automáticamente:"
echo "   • Enviará el código al backend"
echo "   • Obtendrá el access_token"
echo "   • Te redirigirá al dashboard"
echo ""

echo "5️⃣ URLs útiles:"
echo "   • Frontend: http://localhost:4200"
echo "   • Backend Swagger: http://localhost:5001/swagger"
echo "   • Dashboard: http://localhost:4200/dashboard"
echo "   • Pagos: http://localhost:4200/payments"
echo ""
