export default class Product {
  constructor({ id = null, name = "", description = "", stock = 0, price = 0, productTypeId = "" } = {}) {
    this.id = id;
    this.name = String(name).trim();
    this.description = String(description).trim();
    this.stock = Number(stock);
    this.price = Number(price);
    this.productTypeId = Number(productTypeId);
  }

  validate() {
    if (!this.name) return "El nombre es obligatorio.";
    if (!this.description) return "La descripción es obligatoria.";
    if (!Number.isFinite(this.price) || this.price < 0) return "El precio debe ser un número igual o mayor que cero.";
    if (!Number.isInteger(this.stock) || this.stock < 0) return "El stock debe ser un entero igual o mayor que cero.";
    if (!Number.isInteger(this.productTypeId) || this.productTypeId <= 0) return "Selecciona un tipo de producto válido.";
    return null;
  }

  toJSON() {
    return {
      name: this.name,
      description: this.description,
      stock: this.stock,
      price: this.price,
      productTypeId: this.productTypeId
    };
  }
}
