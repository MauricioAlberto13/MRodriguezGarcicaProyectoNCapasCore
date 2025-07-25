using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class ProductoController : Controller
    {

        private readonly BL.Producto _producto;
        private readonly BL.SubCategoria _subCategoria;
        private readonly BL.Categoria _categoria;



        public ProductoController(BL.Producto producto, BL.SubCategoria subCategoria, BL.Categoria categoria)
        {
            _producto = producto;
            _subCategoria = subCategoria;
            _categoria = categoria;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAllProductos()
        {
            ML.Producto producto = new ML.Producto();
            //ML.Result result = _restaurante.GetAll();

            ML.Result resultCate = _categoria.GetAll();

           ML.Result result = _producto.GetAllV(producto);
         //0   ML.Result result = _producto.GetAll();
            if (result.Correct.HasValue)
            {
                producto.Productos= result.Objects;
                producto.SubCategoria = new ML.SubCategoria();
                producto.SubCategoria.Categoria = new ML.Categoria();
                producto.SubCategoria.Categoria.Categorias = resultCate.Objects;
            }

            return View(producto);
        }


        [HttpPost]
        public IActionResult GetAllProductos(ML.Producto producto, IFormFile archivo)
        {
            // ML.Producto producto = new ML.Producto();
            //ML.Result result = _restaurante.GetAll();
            //producto.SubCategoria = new ML.SubCategoria();
            //producto.SubCategoria.Categoria = new ML.Categoria();

    

            ML.Result result = _producto.GetAllV(producto);
          // ML.Result result = _producto.GetAll();
            if (result.Correct.HasValue)
            {
                producto.Productos = result.Objects;
            }
            ML.Result resultCategoria = _categoria.GetAll();
            if (resultCategoria.Correct.HasValue)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
            }
            return View(producto);
        }


        [HttpGet]
        public IActionResult Form(ML.Producto producto)
        {


          // ML.Producto producto= new ML.Producto();
            producto.SubCategoria= new ML.SubCategoria();
            producto.SubCategoria.Categoria= new ML.Categoria();




            producto.SubCategoria.Categoria = new ML.Categoria();
            ML.Result resultCategoria = _categoria.GetAll();
            if (resultCategoria.Correct.HasValue)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
            }


            if (producto.IdProducto > 0)
            {
                //Esto es haciendo uso del web service con SOAP

                //UsuarioReference.UsuarioClient usuarioSOAP = new UsuarioReference.UsuarioClient();
                //var respuesta = usuarioSOAP.GetById(IdUsuario.Value);

                //Con soap
                // usuario = GetBySoapUsuario(IdUsuario.Value);


                // ML.Result result = GetByIdWebAPI(IdUsuario.Value);
                ML.Result result = _producto.GetById(producto.IdProducto);

                if (result.Correct.HasValue)
                {
                    producto = (ML.Producto)result.Object;


           
                    if (resultCategoria.Correct.HasValue)
                    {
                        producto.SubCategoria.Categoria.Categorias= resultCategoria.Objects;
                    }
                    ML.Result subCategoriaResult = _subCategoria.GetSubCategoriaByIdCategoria(producto.SubCategoria.Categoria.IdCategoria);
                    if (subCategoriaResult.Correct.HasValue)
                    {
                        producto.SubCategoria.SubCategorias = subCategoriaResult.Objects;
                    }



                }

            }


            if (resultCategoria.Correct.HasValue)
            {
                producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
            }

            return View(producto);
        }

        [HttpPost]
        public IActionResult Form(ML.Producto producto, IFormFile? imagenUser)
        {
            if (ModelState.IsValid)
            {
                if (imagenUser != null && imagenUser.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        imagenUser.CopyTo(memoryStream);
                        producto.Imagen = memoryStream.ToArray();
                    }
                }
                if (producto.IdProducto > 0)
                {

                    ML.Result result = _producto.Update(producto);
                    
                    if (!result.Correct.HasValue)
                    {
                        return View(producto);
                    }
                }
                else
                {
                    ML.Result result = _producto.Add(producto);

                    if (!result.Correct.HasValue)
                    {
                        return View(producto);
                    }
                }

                producto.SubCategoria.Categoria = new ML.Categoria();
                ML.Result resultCategoria = _categoria.GetAll();
                if (resultCategoria.Correct.HasValue)
                {
                    producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
                }


            }
            else
            {

                producto.SubCategoria.Categoria = new ML.Categoria();
                ML.Result resultCategoria = _categoria.GetAll();
                if (resultCategoria.Correct.HasValue)
                {
                    producto.SubCategoria.Categoria.Categorias = resultCategoria.Objects;
                }
   

                ML.Result subcategoria = _subCategoria.GetSubCategoriaByIdCategoria((int)(producto.SubCategoria.IdSubCategoria));
                if (subcategoria.Correct.HasValue)
                {
                    producto.SubCategoria.SubCategorias = subcategoria.Objects;
                }

                return View(producto);
            }

            return RedirectToAction("GetAllProductos");
        }



        public IActionResult? Delete(int IdProducto)

        {

            ML.Result result = _producto.Delete(IdProducto);
            if (result.Correct.HasValue)
            {
                return RedirectToAction("GetAllProductos");
            }
            return null;
        }

        [HttpGet]

        public JsonResult GetCategoriaByIdSub(int IdCategoria)
        {
            var resultCategoria = _subCategoria.GetSubCategoriaByIdCategoria(IdCategoria);
            return new JsonResult(resultCategoria);
        }


        public IActionResult GetAllJS()
        {
            ML.Producto producto = new ML.Producto();
            //ML.Result result = _restaurante.GetAll();

            ML.Result resultCate = _categoria.GetAll();


            //0   ML.Result result = _producto.GetAll();
            if (resultCate.Correct.HasValue)
            {
                producto.SubCategoria = new ML.SubCategoria();
                producto.SubCategoria.Categoria = new ML.Categoria();
                producto.SubCategoria.Categoria.Categorias = resultCate.Objects;
            }

            return View(producto);
        }



        [HttpGet]
        public JsonResult GProductos()
        {
            var resultPro = _producto.GetAll();
            return new JsonResult(resultPro);
        }
        [HttpGet]
        public JsonResult GByIdProductos(int IdProducto)
        {
            ML.Result result = new ML.Result();

            result = _producto.GetById(IdProducto);


            return new JsonResult(result);
        }



        [HttpPost]
        public JsonResult AProducto([FromBody] ML.Producto producto)
        {
   

            if (producto.Imagen != null)
            {
                producto.Imagen = Convert.FromBase64String(producto.ImagenBase64);
                producto.ImagenBase64 = "";
            }
            else
            {
                producto.Imagen = null;
                producto.ImagenBase64 = "";
            }
            ML.Result result = _producto.Add(producto);
            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult UProducto([FromBody] ML.Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.ImagenBase64) && producto.ImagenBase64 != "null")
            {
                producto.Imagen = Convert.FromBase64String(producto.ImagenBase64);
            }

            producto.ImagenBase64 = "";

            ML.Result result = _producto.Update(producto);
            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult DProducto([FromBody] ML.Producto producto)
        {
            ML.Result result = _producto.Delete(producto.IdProducto);
            return new JsonResult(result);
        }
        [HttpGet]
        public JsonResult BusquedaAbierta(int? IdCategoria, int? IdSubCategoria)
        {
            ML.Producto producto = new ML.Producto();
            producto.SubCategoria = new ML.SubCategoria();
            producto.SubCategoria.Categoria = new ML.Categoria();

            if (IdCategoria.HasValue)
            {
                producto.SubCategoria.Categoria.IdCategoria = IdCategoria.Value;
            }

            if (IdSubCategoria.HasValue)
            {
                producto.SubCategoria.IdSubCategoria = IdSubCategoria.Value;
            }

            ML.Result result = _producto.GetAllV(producto);

            return new JsonResult(result);
        }


    }
}
