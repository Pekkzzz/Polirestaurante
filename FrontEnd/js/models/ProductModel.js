import { API_BASE_URL, API_ROUTES } from "../config.js";

export default class ProductModel {
  async request(path, options = {}) {
    let response;
    try {
      response = await fetch(`${API_BASE_URL}${path}`, {
        ...options,
        headers: { "Content-Type": "application/json", Accept: "application/json", ...(options.headers || {}) }
      });
    } catch (error) {
      throw new Error(`No fue posible conectar con la API (${API_BASE_URL}). Verifica que .NET esté ejecutándose y que la URL sea correcta.`);
    }

    const text = await response.text();
    let body = null;
    if (text) {
      try { body = JSON.parse(text); } catch { body = text; }
    }
    if (!response.ok) {
      const message = body?.message || body?.description || body?.title || (typeof body === "string" ? body : null);
      throw new Error(message || `La API respondió HTTP ${response.status}.`);
    }
    if (body && typeof body === "object" && (body.err === true || body.response === false)) {
      throw new Error(body.message || body.description || "La operación no pudo completarse.");
    }
    return body;
  }

  unwrap(value, keys) {
    let current = value;
    for (let i = 0; i < 3; i++) {
      if (Array.isArray(current) || !current || typeof current !== "object") break;
      const key = keys.find(k => current[k] !== undefined && current[k] !== null);
      if (!key) break;
      current = current[key];
    }
    return current;
  }

  async getAll() {
    const result = await this.request(API_ROUTES.products);
    const data = this.unwrap(result, ["products", "data", "items", "result"]);
    if (!Array.isArray(data)) throw new Error("La ruta de productos no devolvió una lista. Revisa el formato de respuesta en ProductModel.js.");
    return data;
  }

  async getTypes() {
    const result = await this.request(API_ROUTES.productTypes);
    const data = this.unwrap(result, ["productTypes", "types", "data", "items", "result"]);
    if (!Array.isArray(data)) throw new Error("La ruta de tipos de producto no devolvió una lista.");
    return data;
  }

  create(product) {
    return this.request(API_ROUTES.products, { method: "POST", body: JSON.stringify(product.toJSON()) });
  }

  update(product) {
    return this.request(`${API_ROUTES.products}/${product.id}`, { method: "PUT", body: JSON.stringify(product.toJSON()) });
  }

  delete(id) {
    return this.request(`${API_ROUTES.products}/${id}`, { method: "DELETE" });
  }
}
