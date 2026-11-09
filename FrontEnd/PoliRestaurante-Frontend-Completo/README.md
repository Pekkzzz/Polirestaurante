# PoliRestaurante — Frontend MVC completo

Cliente web en HTML, CSS, Bootstrap 5 y JavaScript ES Modules para consumir la API ASP.NET Core de PoliRestaurante.

## Configuración
Edita `js/config.js`:
```js
export const API_BASE_URL = "http://localhost:5016";
```
La dirección corresponde al `dotnet run` que el usuario mostró. Si cambia el puerto, actualízala.

## Ejecución
1. Ejecuta SQL Server y luego la API con `dotnet run`.
2. Comprueba Swagger en `http://localhost:5016/swagger`.
3. Abre esta carpeta en VS Code.
4. Inicia `index.html` con Live Server (normalmente `http://127.0.0.1:5500`).
5. El backend debe tener CORS habilitado para `http://127.0.0.1:5500` y `http://localhost:5500`.

## Módulos incluidos
Productos, tipos de producto, pedidos, reservas, mesas, recogidas, usuarios, roles, estados de reserva y estados de recogida. Incluye login en el módulo JS (modal disponible desde la lógica) y dashboard.

## Endpoints cotejados con Controllers del ZIP
- `ProductController`: GET/POST `/api/Product`, PATCH `/api/Product`, DELETE `/api/Product/{id}`; tipos bajo `/api/Product/ProductType`.
- `OrderController`: GET/POST `/api/Order`, PATCH `/api/Order`, PATCH `/api/Order/{id}/status`, DELETE `/api/Order/{id}`.
- `ReservationController`: GET/POST `/api/Reservation`, PATCH `/api/Reservation`, DELETE `/api/Reservation/{id}`.
- `TableController`: GET por ID, POST, PATCH, DELETE por ID. No expone GET de colección.
- `ToPickUpController`: GET por ID, POST, PATCH, DELETE por ID. No expone GET de colección.
- `UsersController`: GET por ID, POST, PUT, DELETE por ID, POST `/api/Users/Register`, POST `/api/Users/Login`. No expone GET de colección.
- `UserRoleController`: GET por ID, POST, PATCH, DELETE por ID. No expone GET de colección.
- `ReservationStatusController`: GET por ID, POST, PATCH, DELETE por ID. No expone GET de colección.
- `PickUpStatusController`: GET por ID, POST, PATCH, DELETE por ID. No expone GET de colección.

## Limitaciones que dependen del backend
Los Controllers de mesas, recogidas, usuarios, roles y estados no tienen GET sin ID. Por eso la interfaz ofrece consulta por ID y no inventa un listado. Para que esos módulos puedan mostrar todos los registros, añade endpoints `GET` de colección a los Controllers correspondientes (y métodos GetAll a los servicios si todavía no existen).

El controller de roles/estados solo expone GET por ID. La interfaz no puede poblar automáticamente selectores de esos catálogos hasta que exista un GET de colección.

Los DTOs se han usado como contrato de entrada. Por ejemplo `Product_CreateDto` no incluye `Stock`, mientras `Product_UpdateDto` sí lo incluye. El backend puede ignorar propiedades extra, pero si la validación rechaza el payload, revisa el mensaje de la API.

## MVC
- `js/config.js`: URLs y definición de recursos.
- `js/api.js`: cliente HTTP y errores.
- `js/app.js`: controlador de navegación, formularios y CRUD, renderizado.
- `index.html`: estructura de vistas.
- `css/styles.css`: estilos.

## Seguridad
Este es un frontend de desarrollo. No se debe exponer públicamente sin autenticación y autorización adecuadas. El token de login se guarda en sessionStorage como demostración; el backend compartido no aplica automáticamente ese token a cada petición en esta versión.
