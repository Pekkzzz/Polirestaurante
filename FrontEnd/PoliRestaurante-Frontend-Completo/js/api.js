import {API_BASE_URL} from "./config.js";
export async function api(path,options={}){
 let response;
 try{response=await fetch(API_BASE_URL+path,{...options,headers:{"Accept":"application/json","Content-Type":"application/json",...(options.headers||{})}})}
 catch(e){throw new Error(`No se pudo conectar con ${API_BASE_URL}. Comprueba que dotnet run siga activo.`)}
 const raw=await response.text();let data=null;try{data=raw?JSON.parse(raw):null}catch{data=raw}
 if(!response.ok){let msg=typeof data==="string"?data:data?.message||data?.title||data?.description||data?.detail;
  if(data?.errors)msg=Object.values(data.errors).flat().join(" ");
  throw new Error(msg||`Error HTTP ${response.status}`)}
 return data;
}
