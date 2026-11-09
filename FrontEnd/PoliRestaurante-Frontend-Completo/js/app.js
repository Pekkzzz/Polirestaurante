import {RESOURCES,ROUTES} from "./config.js";
import {api} from "./api.js";
const $=s=>document.querySelector(s);
const modal=new bootstrap.Modal($("#recordModal"));
let current="products",records=[],editing=null,searchTerm="";
const titles={dashboard:["Resumen","Vista general de la operación del restaurante."]};
const pages={
 dashboard:["Resumen","Vista general de la operación del restaurante."],
 products:["Productos","Administra el catálogo y las existencias."],
 productTypes:["Tipos de producto","Organiza las categorías del menú."],
 orders:["Pedidos","Consulta pedidos y actualiza su estado."],
 reservations:["Reservas","Administra las reservas de tus clientes."],
 tables:["Mesas","Registra y consulta las mesas del restaurante."],
 pickups:["Recogidas","Gestiona pedidos para recoger."],
 users:["Usuarios","Administra las cuentas registradas."],
 roles:["Roles","Configura los permisos de los usuarios."],
 reservationStatuses:["Estados de reserva","Configura los estados disponibles."],
 pickupStatuses:["Estados de recogida","Configura los estados de recogida."]
};
function val(o,k){if(!o)return "";return o[k]??o[k[0].toUpperCase()+k.slice(1)]??""}
function esc(v){return String(v??"").replace(/[&<>"']/g,c=>({"&":"&amp;","<":"&lt;",">":"&gt;",'"':"&quot;","'":"&#39;"}[c]))}
function alertMsg(msg,type="danger"){$("#alerts").innerHTML=`<div class="alert alert-${type} alert-dismissible fade show">${esc(msg)}<button class="btn-close" data-bs-dismiss="alert"></button></div>`;window.scrollTo({top:0,behavior:"smooth"})}
function showPage(page){
 current=page;document.querySelectorAll(".nav-item").forEach(b=>b.classList.toggle("active",b.dataset.page===page));
 $("#dashboardPage").classList.toggle("d-none",page!=="dashboard");$("#modulePage").classList.toggle("d-none",page==="dashboard");
 $("#pageTitle").textContent=pages[page][0];$("#pageSubtitle").textContent=pages[page][1];$("#crumb").textContent=pages[page][0].toUpperCase();
 if(page==="dashboard")loadDashboard();else setupModule();
}
document.querySelectorAll(".nav-item").forEach(b=>b.addEventListener("click",()=>showPage(b.dataset.page)));
$("#quickGrid").innerHTML=Object.entries(RESOURCES).map(([k,r])=>`<button class="quick" data-quick="${k}"><i class="bi ${r.icon}"></i><b>${r.label}</b><small>Administrar módulo</small></button>`).join("");
$("#quickGrid").addEventListener("click",e=>{let b=e.target.closest("[data-quick]");if(b)showPage(b.dataset.quick)});
async function loadDashboard(){
 $("#dashboardStats").innerHTML=`<div class="stat"><div class="stat-icon"><i class="bi bi-box-seam"></i></div><div><span>Módulos disponibles</span><b>10</b></div></div><div class="stat"><div class="stat-icon"><i class="bi bi-diagram-3"></i></div><div><span>API</span><b>ASP.NET</b></div></div><div class="stat"><div class="stat-icon"><i class="bi bi-database-check"></i></div><div><span>Base de datos</span><b>SQL Server</b></div></div><div class="stat"><div class="stat-icon"><i class="bi bi-code-slash"></i></div><div><span>Arquitectura</span><b>MVC</b></div></div>`;
}
function setupModule(){
 const r=RESOURCES[current];$("#moduleTitle").textContent=r.label;$("#moduleDescription").textContent=r.list===null?"Este Controller no expone un GET general. Puedes consultar registros por ID y realizar las operaciones disponibles.":"Consulta, crea, edita y elimina registros.";
 $("#createBtn").textContent="Nuevo "+r.singular;$("#createBtn").onclick=()=>openForm();$("#reloadBtn").onclick=loadRecords;$("#search").value="";$("#search").oninput=e=>{searchTerm=e.target.value.toLowerCase();render()};
 $("#tableHead").innerHTML=`<tr><th>REGISTRO</th><th>DETALLES</th><th class="text-end">ACCIONES</th></tr>`;
 if(r.list===null){$("#footNote").textContent="Consulta por ID disponible mediante el botón de búsqueda.";$("#tableBody").innerHTML=`<tr><td colspan="3" class="empty">Este controlador no tiene endpoint GET de listado.</td></tr>`;renderLookup();return}
 loadRecords();
}
function renderLookup(){
 let old=$("#lookupBox");if(old)old.remove();
 let div=document.createElement("div");div.id="lookupBox";div.className="mb-3 d-flex gap-2";
 div.innerHTML='<input id="lookupId" type="number" min="1" class="form-control" style="max-width:180px" placeholder="ID del registro"><button id="lookupBtn" class="btn btn-outline-primary">Consultar por ID</button>';
 $("#modulePage").insertBefore(div,$("#modulePage").querySelector(".toolbar"));
 $("#lookupBtn").onclick=async()=>{let id=$("#lookupId").value;if(!id)return alertMsg("Escribe un ID válido.","warning");try{let item=await api(RESOURCES[current].base+"/"+id);records=[item];render()}catch(e){alertMsg(e.message)}};
}
async function loadRecords(){
 const r=RESOURCES[current];if(r.list===null)return;
 try{records=await api(r.base+(r.list||""));if(!Array.isArray(records))throw new Error("El endpoint no devolvió una lista.");render();$("#footNote").textContent=`${records.length} registro(s) cargados desde ${r.base}`;}
 catch(e){records=[];render();alertMsg(e.message)}
}
function render(){
 const r=RESOURCES[current];let filtered=records.filter(o=>JSON.stringify(o).toLowerCase().includes(searchTerm));
 $("#count").textContent=`${filtered.length} registro(s)`;
 if(!filtered.length){$("#tableBody").innerHTML='<tr><td colspan="3" class="empty">No hay registros para mostrar.</td></tr>';return}
 $("#tableBody").innerHTML=filtered.map(o=>{
  let id=val(o,"id");let pairs=Object.keys(o).filter(k=>!["id","password"].includes(k.toLowerCase())).slice(0,5);
  let detail=pairs.map(k=>`<span class="me-3"><b>${esc(k)}:</b> ${esc(typeof o[k]==="object"?JSON.stringify(o[k]):o[k])}</span>`).join("");
  return `<tr><td><b>#${esc(id||"—")}</b><div class="text-secondary">${esc(r.singular)}</div></td><td>${detail}</td><td class="text-end"><button class="action" data-edit="${esc(id)}" title="Editar"><i class="bi bi-pencil"></i></button><button class="action del" data-delete="${esc(id)}" title="Eliminar"><i class="bi bi-trash"></i></button></td></tr>`;
 }).join("");
}
$("#tableBody").addEventListener("click",async e=>{
 let edit=e.target.closest("[data-edit]"),del=e.target.closest("[data-delete]");let id=Number(edit?.dataset.edit??del?.dataset.delete);if(!id)return;
 let item=records.find(x=>Number(val(x,"id"))===id);
 if(edit){if(!item){try{item=await api(RESOURCES[current].base+"/"+id)}catch(err){return alertMsg(err.message)}}openForm(item)}
 if(del&&confirm(`¿Eliminar ${RESOURCES[current].singular} #${id}?`)){try{await api(RESOURCES[current].base+RESOURCES[current].remove.replace("{id}",id),{method:"DELETE"});alertMsg("Registro eliminado.","success");await loadRecords()}catch(err){alertMsg(err.message)}}
});
function fieldsFor(r,isEdit){return isEdit?(r.updateFields||[["id","ID","number",true],...r.fields]):r.fields}
function openForm(item=null){
 const r=RESOURCES[current];editing=item;$("#formTitle").textContent=item?"Editar "+r.singular:"Nuevo "+r.singular;$("#formError").classList.add("d-none");
 $("#formFields").innerHTML=fieldsFor(r,!!item).map(([key,label,type,required])=>{
  let value=val(item,key);if(type==="datetime-local"&&value)value=String(value).slice(0,16);
  if(type==="checkbox")return `<div class="col-md-6"><div class="form-check mt-4"><input class="form-check-input" id="f_${key}" name="${key}" type="checkbox" ${value?"checked":""}><label class="form-check-label" for="f_${key}">${label}</label></div></div>`;
  return `<div class="col-md-6"><label class="form-label" for="f_${key}">${label}</label><input class="form-control" id="f_${key}" name="${key}" type="${type}" value="${esc(value)}" ${required?"required":""} ${type==="number"?"step=any":""}></div>`;
 }).join("");
 modal.show();
}
$("#recordForm").addEventListener("submit",async e=>{
 e.preventDefault();const r=RESOURCES[current],form=new FormData(e.currentTarget),payload={};
 for(const [key,label,type] of fieldsFor(r,!!editing)){let el=e.currentTarget.elements.namedItem(key);if(!el)continue;let v=type==="checkbox"?el.checked:el.value;if(v===""&&type!=="checkbox")v=null;else if(type==="number")v=Number(v);if(type==="datetime-local"&&v)v=new Date(v).toISOString();payload[key]=v}
 const isEdit=!!editing;if(isEdit&&payload.id===undefined)payload.id=Number(val(editing,"id"));
 try{
  const method=isEdit?(current==="users"?"PUT":"PATCH"):"POST";
  await api(r.base+(isEdit?"":r.create||""),{method,body:JSON.stringify(payload)});
  modal.hide();alertMsg(`${r.singular} ${isEdit?"actualizado":"creado"} correctamente.`,"success");
  if(r.list!==null)await loadRecords();else $("#tableBody").innerHTML='<tr><td colspan="3" class="empty">Operación completada. Consulta por ID para verificar.</td></tr>';
 }catch(err){$("#formError").textContent=err.message;$("#formError").classList.remove("d-none")}
});
$("#loginForm").addEventListener("submit",async e=>{e.preventDefault();let f=new FormData(e.currentTarget);try{let token=await api(ROUTES.login,{method:"POST",body:JSON.stringify({username:f.get("username"),password:f.get("password")})});sessionStorage.setItem("poliToken",typeof token==="string"?token:JSON.stringify(token));bootstrap.Modal.getInstance($("#loginModal")).hide();alertMsg("Inicio de sesión correcto.","success")}catch(err){alertMsg(err.message)}});
showPage("dashboard");
