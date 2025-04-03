using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPerformanceCalculator.DTOs;
using ProductPerformanceCalculator.Entities;
using ProductPerformanceCalculator.Services;

namespace ProductPerformanceCalculator.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "producto";

        public ProductsController(ApplicationDbContext context, IMapper mapper, 
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoDTO>>> Get()
        {
            var productos = await context.Productos.ToListAsync();
            var productoDTO = mapper.Map<List<ProductoDTO>>(productos);

            return productoDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerProductoPorId")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)
        {
            var producto = await context.Productos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto is null)
            {
                return NotFound("Producto no encontrado");
            }

            var productoDTO = mapper.Map<ProductoDTO>(producto);

            return productoDTO;
        }

       
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductoCreationDTO productoCreationDTO)
        {
            var producto = mapper.Map<Producto>(productoCreationDTO);

            if(producto.Foto is not null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, productoCreationDTO.Foto!);
                producto.Foto = url;
            }

            context.Add(producto);
            await context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerProductoPorId", new { id = producto.Id }, producto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] ProductoCreationDTO productoCreationDTO)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == productoCreationDTO.Id);

            if(producto is null)
            {
                return NotFound("producto no encontrado");
            }

            producto = mapper.Map(productoCreationDTO, producto);
            context.Update(producto);
            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productosBorrados = await context.Productos.Where(p => p.Id == id).ExecuteDeleteAsync();

            if(productosBorrados == 0)
            {
                return NotFound("Producto no encontrado");
            }

            return NoContent();

        }



    }
}
