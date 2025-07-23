using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController(AppDbContext context) : ControllerBase
    {
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                var categoriasProdutos = context.Categorias?.Include(p => p.Produtos).Where(c => c.CategoriaID <= 5).Take(100).AsNoTracking().ToList();
                if (categoriasProdutos is null || categoriasProdutos.Count == 0)
                {
                    return NotFound("Categoria produto não encontrado...");
                }
                return categoriasProdutos;
            } catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try {
                var categorias = context.Categorias?.Take(100).AsNoTracking().ToList();
                if (categorias is null || categorias.Count == 0)
                {
                    return NotFound("Categorias não encontradas");
                }
                return categorias;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<Categoria> Get(int id)
        {

            try
            {
                var categoria = context.Categorias?.FirstOrDefault(c => c.CategoriaID == id);
                if (categoria is null)
                {
                    return NotFound("Categoria não contrada");
                }
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }

        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                {
                    return BadRequest("Dados invalidos");
                }
                context.Categorias?.Add(categoria);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = categoria.CategoriaID }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaID)
                {
                    return BadRequest("Dados invalidos");
                }
                context.Entry(categoria).State = EntityState.Modified;
                context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = context.Categorias?.Find(id);
                if (categoria is null)
                {
                    return NotFound($"Categoria id {id} não encontrada");
                }
                context.Categorias?.Remove(categoria);
                context.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }
    }
}
