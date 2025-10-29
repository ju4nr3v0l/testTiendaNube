#!/bin/bash

echo "🚀 Iniciando SistePayTiendaNube..."
echo ""

# Verificar si existen las credenciales
if grep -q "REEMPLAZAR_CLIENT_ID" SistePay.TiendaNube.API/appsettings.json; then
    echo "⚠️  ADVERTENCIA: Debes configurar las credenciales en:"
    echo "   - SistePay.TiendaNube.API/appsettings.json"
    echo "   - sistepay-app/src/environments/environment.ts"
    echo ""
fi

echo "Selecciona una opción:"
echo "1) Ejecutar con Docker"
echo "2) Ejecutar sin Docker (requiere .NET 8 y Node.js)"
echo "3) Solo Backend"
echo "4) Solo Frontend"
read -p "Opción: " option

case $option in
    1)
        echo "🐳 Iniciando con Docker..."
        docker-compose up --build
        ;;
    2)
        echo "📦 Iniciando Backend y Frontend..."
        cd SistePay.TiendaNube.API
        dotnet run &
        BACKEND_PID=$!
        cd ../sistepay-app
        npm install
        ng serve &
        FRONTEND_PID=$!
        echo ""
        echo "✅ Servicios iniciados:"
        echo "   Backend: http://localhost:5000"
        echo "   Frontend: http://localhost:4200"
        echo ""
        echo "Presiona Ctrl+C para detener..."
        wait
        ;;
    3)
        echo "🔧 Iniciando solo Backend..."
        cd SistePay.TiendaNube.API
        dotnet run
        ;;
    4)
        echo "🎨 Iniciando solo Frontend..."
        cd sistepay-app
        npm install
        ng serve
        ;;
    *)
        echo "❌ Opción inválida"
        exit 1
        ;;
esac
