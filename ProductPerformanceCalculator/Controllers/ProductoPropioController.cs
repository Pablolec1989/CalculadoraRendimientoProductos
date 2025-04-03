using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPerformanceCalculator.DTOs;
using ProductPerformanceCalculator.Entities;
using ProductPerformanceCalculator.Services;

namespace ProductPerformanceCalculator.Controllers
{
    [ApiController]
    [Route("api/productoPropio")]
    public class ProductoPropioController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IAlmacenadorArchivos almacenadorArchivos;
        private readonly string contenedor = "productoPropio";

        public ProductoPropioController(ApplicationDbContext context, IMapper mapper,
            IAlmacenadorArchivos almacenadorArchivos)
        {
            this.context = context;
            this.mapper = mapper;
            this.almacenadorArchivos = almacenadorArchivos;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductoPropioDTO>>> Get()
        {
            var productos = await context.Productos.ToListAsync();
            var productoPropioDTO = mapper.Map<List<ProductoPropioDTO>>(productos);

            return productoPropioDTO;
        }

        [HttpGet("{id:int}", Name = "ObtenerProductoPropioPorId")]
        public async Task<ActionResult<ProductoPropioDTO>> Get(int id)
        {
            var producto = await context.Productos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto is null)
            {
                return NotFound("Producto no encontrado");
            }

            var productoPropioDTO = mapper.Map<ProductoPropioDTO>(producto);

            return productoPropioDTO;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductoPropioCreationDTO productoPropioCreationDTO)
        {
            var producto = mapper.Map<Producto>(productoPropioCreationDTO);

            if (producto.Foto is not null)
            {
                var url = await almacenadorArchivos.Almacenar(contenedor, productoPropioCreationDTO.Foto!);
                producto.Foto = url;
            }

            context.Add(producto);
            await context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerProductoPropioPorId", new { id = producto.Id }, producto);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] ProductoPropioCreationDTO productoPropioCreationDTO)
        {
            var producto = await context.Productos.FirstOrDefaultAsync(p => p.Id == productoPropioCreationDTO.Id);

            if (producto is null)
            {
                return NotFound("producto no encontrado");
            }

            producto = mapper.Map(productoPropioCreationDTO, producto);
            context.Update(producto);
            await context.SaveChangesAsync();

            return NoContent();

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var productosBorrados = await context.Productos.Where(p => p.Id == id).ExecuteDeleteAsync();

            if (productosBorrados == 0)
            {
                return NotFound("Producto no encontrado");
            }

            return NoContent();

        }



    }
}
