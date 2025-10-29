# 🚀 Inicio Rápido - SistePayTiendaNube

## Paso 1: Registrar App en Tienda Nube

1. Ve a https://partners.tiendanube.com/
2. Crea una nueva aplicación
3. Configura:
   - **Redirect URI:** `http://localhost:4200/auth/callback`
4. Copia el **Client ID** y **Client Secret**

## Paso 2: Configurar Credenciales

### Backend
Edita `SistePay.TiendaNube.API/appsettings.json`:

```json
{
  "TiendaNube": {
    "ClientId": "TU_CLIENT_ID_AQUI",
    "ClientSecret": "TU_CLIENT_SECRET_AQUI",
    "StoreId": "TU_STORE_ID_AQUI"
  }
}
```

### Frontend
Edita `sistepay-app/src/environments/environment.ts`:

```typescript
export const environment = {
  apiUrl: 'http://localhost:5000/api',
  clientId: 'TU_CLIENT_ID_AQUI',
  redirectUri: 'http://localhost:4200/auth/callback'
};
```

## Paso 3: Ejecutar el Proyecto

### Opción A: Con Script (Recomendado)
```bash
./start.sh
```

### Opción B: Con Docker
```bash
docker-compose up --build
```

### Opción C: Manual

**Terminal 1 - Backend:**
```bash
cd SistePay.TiendaNube.API
dotnet run
```

**Terminal 2 - Frontend:**
```bash
cd sistepay-app
npm install
ng serve
```

## Paso 4: Probar la Aplicación

1. Abre http://localhost:4200
2. Inicia el flujo OAuth visitando:
   ```
   https://www.tiendanube.com/apps/authorize?client_id=TU_CLIENT_ID&redirect_uri=http://localhost:4200/auth/callback
   ```
3. Autoriza la aplicación
4. Serás redirigido al dashboard

## 🌐 URLs

- **Frontend:** http://localhost:4200
- **Backend:** http://localhost:5000
- **Swagger:** http://localhost:5000/swagger

## 📋 Rutas Disponibles

- `/dashboard` - Información de la tienda
- `/payments` - Crear pagos
- `/auth/callback` - Callback OAuth (automático)

## 🔧 Troubleshooting

### Error de CORS
Verifica que el backend esté corriendo en el puerto 5000.

### Token no válido
Asegúrate de haber completado el flujo OAuth correctamente.

### Puerto ocupado
Cambia los puertos en:
- Backend: `Properties/launchSettings.json`
- Frontend: `ng serve --port NUEVO_PUERTO`

## 📚 Documentación Completa

Lee `README.md` para información detallada sobre:
- Arquitectura del proyecto
- Endpoints disponibles
- Configuración de webhooks
- Mejores prácticas de seguridad

---

**¿Necesitas ayuda?** Revisa la documentación oficial de Tienda Nube:
https://tiendanube.github.io/api-documentation/
