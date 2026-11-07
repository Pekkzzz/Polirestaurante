import ProductModel from "./models/ProductModel.js";
import ProductView from "./views/ProductView.js";
import ProductController from "./controllers/ProductController.js";

const model = new ProductModel();
const view = new ProductView();
const controller = new ProductController(model, view);

controller.init();
