import Product from "../models/Product.js";

export default class ProductController {
  constructor(model, view) {
    this.model = model;
    this.view = view;
    this.products = [];
    this.filtered = [];
    this.deleteId = null;
  }

  init() {
    this.view.onSearch(term => this.search(term));
    this.view.onNew(() => this.view.openCreate());
    this.view.onRefresh(() => this.load());
    this.view.onSubmit(data => this.save(data));
    this.view.onTableAction((action, id) => this.handleAction(action, id));
    this.view.onConfirmDelete(() => this.remove());
    this.load();
    this.loadTypes();
  }

  async load() {
    try {
      this.products = await this.model.getAll();
      this.search(this.view.searchInput.value);
    } catch (error) {
      this.view.table.innerHTML = `<tr><td colspan="6" class="empty-state">${this.view.escape(error.message)}</td></tr>`;
      this.view.showStats([]);
      this.view.alert(error.message);
    }
  }

  async loadTypes() {
    try { this.view.setTypes(await this.model.getTypes()); }
    catch (error) { this.view.alert(`No se pudieron cargar las categorías: ${error.message}`); }
  }

  search(term) {
    const q = term.toLocaleLowerCase("es");
    this.filtered = this.products.filter(p =>
      String(p.name ?? p.Name ?? "").toLocaleLowerCase("es").includes(q) ||
      String(p.description ?? p.Description ?? "").toLocaleLowerCase("es").includes(q)
    );
    this.view.showProducts(this.filtered);
    this.view.showStats(this.products);
  }

  async save(data) {
    const product = new Product(data);
    const error = product.validate();
    if (error) { this.view.alert(error, "warning"); return; }
    const editing = product.id !== null;
    this.view.setSaving(true);
    try {
      if (editing) await this.model.update(product);
      else await this.model.create(product);
      this.view.closeForm();
      this.view.alert(editing ? "Producto actualizado correctamente." : "Producto creado correctamente.", "success");
      await this.load();
    } catch (err) { this.view.alert(err.message); }
    finally { this.view.setSaving(false); }
  }

  handleAction(action, id) {
    const product = this.products.find(p => Number(p.id ?? p.ID) === id);
    if (!product) return;
    if (action === "edit") this.view.openEdit(product);
    if (action === "delete") {
      this.deleteId = id;
      this.view.askDelete(product);
    }
  }

  async remove() {
    if (!this.deleteId) return;
    const button = document.querySelector("#confirmDeleteBtn");
    button.disabled = true;
    try {
      await this.model.delete(this.deleteId);
      this.view.closeDelete();
      this.view.alert("Producto eliminado correctamente.", "success");
      this.deleteId = null;
      await this.load();
    } catch (error) { this.view.alert(error.message); }
    finally { button.disabled = false; }
  }
}
