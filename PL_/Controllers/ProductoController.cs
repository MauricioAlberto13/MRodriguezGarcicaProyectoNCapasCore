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

         //  ML.Result result = _producto.GetAllV(producto);
            ML.Result result = _producto.GetAll();
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

    

           // ML.Result result = _producto.GetAllV(producto);
           ML.Result result = _producto.GetAll();
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




    }
}
