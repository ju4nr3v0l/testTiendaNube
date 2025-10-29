# SistePayTiendaNube

Proyecto full-stack para integración con Tienda Nube usando OAuth2.

## Estructura del Proyecto

```
sistepay-app/
├── sistepay-app/              # Frontend Angular 17
├── SistePay.TiendaNube.API/   # Backend .NET 8
├── docker-compose.yml         # Configuración Docker
└── README.md
```

## Requisitos Previos

- Node.js 18+ y npm
- .NET 8 SDK
- Docker y Docker Compose (opcional)
- Cuenta en el Portal de Socios de Tienda Nube

## Configuración Inicial

### 1. Registrar la App en Tienda Nube

1. Accede al [Portal de Socios de Tienda Nube](https://partners.tiendanube.com/)
2. Crea una nueva aplicación
3. Configura la URL de redirección: `http://localhost:4200/auth/callback`
4. Anota el **Client ID** y **Client Secret**

### 2. Configurar el Backend

Edita el archivo `SistePay.TiendaNube.API/appsettings.json`:

```json
{
  "TiendaNube": {
    "ClientId": "TU_CLIENT_ID_AQUI",
    "ClientSecret": "TU_CLIENT_SECRET_AQUI",
    "StoreId": "TU_STORE_ID_AQUI"
  }
}
```

### 3. Configurar el Frontend

Edita el archivo `sistepay-app/src/environments/environment.ts`:

```typescript
export const environment = {
  apiUrl: 'http://localhost:5000/api',
  clientId: 'TU_CLIENT_ID_AQUI',
  redirectUri: 'http://localhost:4200/auth/callback'
};
```

## Ejecución Local

### Opción 1: Sin Docker

#### Backend (.NET 8)

```bash
cd SistePay.TiendaNube.API
dotnet restore
dotnet run
```

El backend estará disponible en: `http://localhost:5000`

#### Frontend (Angular)

```bash
cd sistepay-app
npm install
ng serve
```

El frontend estará disponible en: `http://localhost:4200`

### Opción 2: Con Docker

```bash
docker-compose up --build
```

Esto levantará ambos servicios:
- Frontend: `http://localhost:4200`
- Backend: `http://localhost:5000`

## Flujo OAuth2 con Tienda Nube

### 1. Iniciar Autorización

Redirige al usuario a:

```
https://www.tiendanube.com/apps/authorize?client_id=TU_CLIENT_ID&redirect_uri=http://localhost:4200/auth/callback
```

### 2. Callback

Tienda Nube redirigirá a tu aplicación con un código:

```
http://localhost:4200/auth/callback?code=CODIGO_TEMPORAL
```

### 3. Intercambio de Token

El frontend automáticamente enviará el código al backend para obtener el `access_token`.

### 4. Usar el Token

El token se guarda en `localStorage` y se usa para todas las peticiones subsecuentes.

## Endpoints del Backend

### Autenticación

- `POST /api/auth/token` - Intercambia el código por un access_token
  ```json
  {
    "code": "codigo_de_autorizacion"
  }
  ```

### Tienda

- `GET /api/store/info` - Obtiene información de la tienda autenticada

### Órdenes

- `GET /api/orders` - Lista todas las órdenes de la tienda

### Pagos

- `POST /api/payments` - Crea una orden de pago simulada
  ```json
  {
    "amount": 100.50,
    "description": "Descripción del pago"
  }
  ```

### Webhooks

- `POST /api/webhooks/orders` - Recibe notificaciones de órdenes creadas

## Configurar Webhooks en Tienda Nube

1. En el portal de socios, configura el webhook:
   - URL: `http://tu-dominio.com/api/webhooks/orders`
   - Evento: `orders/created`

2. Para desarrollo local, usa [ngrok](https://ngrok.com/):
   ```bash
   ngrok http 5000
   ```

## Probar con una Tienda Demo

1. Crea una tienda de prueba en Tienda Nube
2. Instala tu aplicación en la tienda demo
3. Completa el flujo OAuth2
4. Prueba las funcionalidades desde el dashboard

## Estructura del Frontend

- `auth-callback`: Maneja el callback de OAuth y obtiene el token
- `dashboard`: Muestra información de la tienda y cantidad de pedidos
- `payments`: Permite crear órdenes de pago

## Estructura del Backend

- `Controllers/`: Controladores de la API
- `Services/`: Lógica de negocio y comunicación con Tienda Nube
- `Models/`: Modelos de datos
- `Dtos/`: Data Transfer Objects

## Características Implementadas

✅ Flujo OAuth2 completo con Tienda Nube  
✅ Obtención de información de la tienda  
✅ Listado de órdenes  
✅ Creación de pagos simulados  
✅ Webhook para órdenes creadas  
✅ User-Agent personalizado en todas las peticiones  
✅ CORS configurado  
✅ Logging básico  
✅ Docker Compose para despliegue  

## Notas de Seguridad

- **NO** subas el archivo `appsettings.json` con credenciales reales a repositorios públicos
- Usa variables de entorno en producción
- El `access_token` se guarda en memoria en el backend (considera usar Redis para producción)
- Implementa autenticación adicional para proteger tus endpoints

## Troubleshooting

### Error de CORS

Verifica que el backend esté configurado para permitir peticiones desde `http://localhost:4200`.

### Token no válido

Asegúrate de que el `access_token` esté siendo enviado correctamente en las peticiones.

### Webhook no recibe datos

Para desarrollo local, usa ngrok para exponer tu servidor local a internet.

## Próximos Pasos

- Implementar persistencia de tokens (Redis/Base de datos)
- Agregar autenticación JWT para el frontend
- Implementar manejo de refresh tokens
- Agregar más endpoints de la API de Tienda Nube
- Implementar tests unitarios e integración

## Soporte

Para más información sobre la API de Tienda Nube:
- [Documentación oficial](https://tiendanube.github.io/api-documentation/)
- [Portal de Socios](https://partners.tiendanube.com/)

## Licencia

MIT
