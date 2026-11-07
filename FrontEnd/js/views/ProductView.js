export default class ProductView {
  constructor() {
    this.table = document.querySelector("#productsTable");
    this.searchInput = document.querySelector("#searchInput");
    this.form = document.querySelector("#productForm");
    this.modal = new bootstrap.Modal(document.querySelector("#productModal"));
    this.deleteModal = new bootstrap.Modal(document.querySelector("#deleteModal"));
  }

  onSearch(callback) { this.searchInput.addEventListener("input", e => callback(e.target.value)); }
  onNew(callback) { document.querySelector("#newProductBtn").addEventListener("click", callback); }
  onRefresh(callback) { document.querySelector("#refreshBtn").addEventListener("click", callback); }
  onSubmit(callback) { this.form.addEventListener("submit", e => { e.preventDefault(); callback(this.readForm()); }); }
  onTableAction(callback) {
    this.table.addEventListener("click", e => {
      const button = e.target.closest("[data-action]");
      if (button) callback(button.dataset.action, Number(button.dataset.id));
    });
  }
  onConfirmDelete(callback) { document.querySelector("#confirmDeleteBtn").addEventListener("click", callback); }

  readForm() {
    return {
      id: document.querySelector("#productId").value ? Number(document.querySelector("#productId").value) : null,
      name: document.querySelector("#name").value,
      description: document.querySelector("#description").value,
      price: document.querySelector("#price").value,
      stock: document.querySelector("#stock").value,
      productTypeId: document.querySelector("#productTypeId").value
    };
  }

  setTypes(types) {
    const select = document.querySelector("#productTypeId");
    select.innerHTML = '<option value="">Selecciona un tipo</option>';
    types.forEach(type => {
      const id = type.id ?? type.ID ?? type.productTypeId;
      const name = type.name ?? type.Name ?? type.description ?? `Tipo ${id}`;
      if (id == null) return;
      select.insertAdjacentHTML("beforeend", `<option value="${this.escape(id)}">${this.escape(name)}</option>`);
    });
  }

  showProducts(products) {
    if (!products.length) {
      this.table.innerHTML = '<tr><td colspan="6" class="empty-state">No hay productos para mostrar.</td></tr>';
      return;
    }
    this.table.innerHTML = products.map(p => {
      const id = p.id ?? p.ID;
      const name = p.name ?? p.Name ?? "Sin nombre";
      const description = p.description ?? p.Description ?? "";
      const price = Number(p.price ?? p.Price ?? 0);
      const stock = Number(p.stock ?? p.Stock ?? 0);
      const type = p.productType?.name ?? p.productType?.Name ?? p.productTypeName ?? p.ProductTypeName ?? `Tipo ${p.productTypeId ?? p.ProductTypeId ?? p.productType_ID ?? "—"}`;
      return `<tr>
        <td class="text-secondary">#${this.escape(id)}</td>
        <td><div class="product-name">${this.escape(name)}</div><div class="product-description">${this.escape(description)}</div></td>
        <td><span class="category-pill">${this.escape(type)}</span></td>
        <td class="price">${price.toLocaleString("es-CO",{style:"currency",currency:"COP",maximumFractionDigits:0})}</td>
        <td><span class="stock-pill ${stock <= 5 ? "low" : ""}">${stock} unidades</span></td>
        <td class="text-end"><button class="action-btn" title="Editar" data-action="edit" data-id="${this.escape(id)}"><i class="bi bi-pencil"></i></button><button class="action-btn delete" title="Eliminar" data-action="delete" data-id="${this.escape(id)}"><i class="bi bi-trash3"></i></button></td>
      </tr>`;
    }).join("");
  }

  showStats(products) {
    document.querySelector("#totalProducts").textContent = products.length;
    document.querySelector("#availableProducts").textContent = products.filter(p => Number(p.stock ?? p.Stock ?? 0) > 0).length;
    document.querySelector("#lowStockProducts").textContent = products.filter(p => Number(p.stock ?? p.Stock ?? 0) <= 5).length;
    document.querySelector("#resultsLabel").textContent = `${products.length} producto(s)`;
  }

  openCreate() {
    this.form.reset();
    document.querySelector("#productId").value = "";
    document.querySelector("#modalTitle").textContent = "Nuevo producto";
    document.querySelector("#saveBtn").textContent = "Guardar producto";
    this.modal.show();
  }

  openEdit(product) {
    document.querySelector("#productId").value = product.id ?? product.ID;
    document.querySelector("#name").value = product.name ?? product.Name ?? "";
    document.querySelector("#description").value = product.description ?? product.Description ?? "";
    document.querySelector("#price").value = product.price ?? product.Price ?? 0;
    document.querySelector("#stock").value = product.stock ?? product.Stock ?? 0;
    document.querySelector("#productTypeId").value = product.productTypeId ?? product.ProductTypeId ?? product.productType_ID ?? "";
    document.querySelector("#modalTitle").textContent = "Editar producto";
    document.querySelector("#saveBtn").textContent = "Guardar cambios";
    this.modal.show();
  }

  closeForm() { this.modal.hide(); }
  askDelete(product) {
    const name = product.name ?? product.Name ?? "este producto";
    document.querySelector("#deleteMessage").textContent = `Se eliminará "${name}". Esta acción no se puede deshacer.`;
    this.deleteModal.show();
  }
  closeDelete() { this.deleteModal.hide(); }
  setSaving(saving) {
    const button = document.querySelector("#saveBtn");
    button.disabled = saving;
    button.textContent = saving ? "Guardando..." : (document.querySelector("#productId").value ? "Guardar cambios" : "Guardar producto");
  }
  alert(message, type = "danger") {
    document.querySelector("#alertArea").innerHTML = `<div class="alert alert-${type} alert-dismissible fade show" role="alert">${this.escape(message)}<button type="button" class="btn-close" data-bs-dismiss="alert"></button></div>`;
  }
  escape(value) {
    return String(value ?? "").replace(/[&<>"']/g, c => ({"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#039;"}[c]));
  }
}
