// Ajusta esta configuración a las rutas reales de tu ASP.NET Core API.
// Si tu API usa otro puerto, cambia API_BASE_URL (por ejemplo https://localhost:7042).
export const API_BASE_URL = "https://localhost:7042";

export const API_ROUTES = {
  products: "/api/Products",
  productTypes: "/api/ProductTypes"
};

// Si tu API devuelve un objeto envoltorio, PersonaModel intenta reconocer
// propiedades comunes: data, products, product, items o result.
