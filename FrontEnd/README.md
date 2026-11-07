# PoliRestaurante - Cliente web MVC

Frontend inicial para consumir la API de PoliRestaurante desarrollada en ASP.NET Core.

## Tecnologías
- HTML5 y CSS3
- Bootstrap 5
- JavaScript ES Modules (`import` / `export`)
- Fetch API
- Patrón MVC en el cliente

## 1. Configurar la URL y rutas de la API

Abre `js/config.js` y modifica:

```js
export const API_BASE_URL = "https://localhost:7042";
export const API_ROUTES = {
  products: "/api/Products",
  productTypes: "/api/ProductTypes"
};
```

La URL del ejemplo es orientativa. Usa la dirección HTTPS que muestra tu proyecto .NET al ejecutarlo. Confirma también los nombres reales de las rutas en tus Controllers o Swagger.

El cliente espera estas operaciones:
- `GET /api/Products` -> lista de productos.
- `POST /api/Products` -> crea producto.
- `PUT /api/Products/{id}` -> actualiza producto.
- `DELETE /api/Products/{id}` -> elimina producto.
- `GET /api/ProductTypes` -> lista de tipos/categorías.

El JSON de creación/actualización esperado es:
```json
{
  "name": "Hamburguesa",
  "description": "Hamburguesa clásica",
  "stock": 10,
  "price": 18000,
  "productTypeId": 1
}
```

Si tus DTO usan otros nombres, ajusta `Product.js` y/o `ProductModel.js`. El ID no se envía al crear, porque SQL Server lo genera mediante IDENTITY.

## 2. Habilitar CORS en ASP.NET Core

En `Program.cs`, registra una política antes de `builder.Build()`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendLocal", policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
```

Después de construir la aplicación y antes de mapear los controladores:

```csharp
app.UseCors("FrontendLocal");
```

Si tienes autenticación, normalmente coloca `UseCors` después de `UseRouting` (si está explícito) y antes de `UseAuthorization`. No agregues una segunda política si ya tienes CORS configurado: adapta la existente.

## 3. Ejecutar

1. Abre PoliRestaurante en Visual Studio y ejecuta la API.
2. Confirma la URL HTTPS y que Swagger funcione.
3. Abre esta carpeta en VS Code.
4. Instala la extensión **Live Server**.
5. Clic derecho en `index.html` -> **Open with Live Server**.
6. El frontend normalmente abre en `http://127.0.0.1:5500`.

## 4. Estructura MVC

```text
PoliRestaurante-Frontend/
├── index.html
├── css/styles.css
└── js/
    ├── app.js
    ├── config.js
    ├── models/
    │   ├── Product.js
    │   └── ProductModel.js
    ├── views/
    │   └── ProductView.js
    └── controllers/
        └── ProductController.js
```

- **View:** renderiza la tabla y captura eventos.
- **Controller:** coordina el flujo de listar, crear, editar y eliminar.
- **Model:** representa y valida el producto; `ProductModel` se comunica con la API.
- **app.js:** instancia y conecta las tres piezas.

## Importante
Esta es una primera versión funcional del frontend, pero las rutas, puerto, nombres de DTO y forma de respuesta deben coincidir con tu API real. Si aparece 404, revisa las rutas; si aparece error CORS, revisa `Program.cs`; si aparece error de conexión, revisa `API_BASE_URL`.
