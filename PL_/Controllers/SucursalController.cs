using Microsoft.AspNetCore.Mvc;

namespace PL_.Controllers
{
    public class SucursalController : Controller
    {




        private readonly BL.ProductoSucursal _productoSucursal;
        private readonly BL.Sucursal _sucursal;
        



        public SucursalController(BL.ProductoSucursal productoSucursal, BL.Sucursal sucursal)
        {
            _productoSucursal= productoSucursal;
            _sucursal= sucursal;
            

        }


        [HttpGet]
        public IActionResult GetAll()
        {
            ML.ProductoSucursal productoSucursal= new ML.ProductoSucursal();
            productoSucursal.Sucursal = new ML.Sucursal();
            ML.Result resultSu = _sucursal.GetAll();
            ML.Result result = _productoSucursal.GetAll();
              
            if (result.Correct.HasValue)
            {
                productoSucursal.ProductoSucursales = result.Objects;
                productoSucursal.Sucursal.Sucursales = resultSu.Objects;
               
            }

            return View(productoSucursal);
        }


        [HttpPost]
        public IActionResult GetAll(ML.ProductoSucursal productoSucursal)
        {
            // ML.Producto producto = new ML.Producto();
            //ML.Result result = _restaurante.GetAll();
            //producto.SubCategoria = new ML.SubCategoria();
            //producto.SubCategoria.Categoria = new ML.Categoria();



            ML.Result result = _productoSucursal.GetAllV(productoSucursal);
            // ML.Result result = _producto.GetAll();
            if (result.Correct.HasValue)
            {
                productoSucursal.ProductoSucursales = result.Objects;
            }
            ML.Result resultCategoria = _sucursal.GetAll();
            if (resultCategoria.Correct.HasValue)
            {
               productoSucursal.Sucursal.Sucursales= resultCategoria.Objects;
            }
            return View(productoSucursal);
        }

        public IActionResult? Delete(int IdProducto)

        {

            ML.Result result = _productoSucursal.Delete2(IdProducto);
            if (result.Correct.HasValue)
            {
                return RedirectToAction("GetAll");
            }
            return null;
        }
        //public IActionResult GetAll()
        //{
        //    return View();
        //}



        public IActionResult? UpdateStock(ML.ProductoSucursal productoSucursal)

        {

            ML.Result result = _productoSucursal.Update(productoSucursal);
            if (result.Correct.HasValue)
            {
                return RedirectToAction("GetAll");
            }
            return null;
        }

    }
}
