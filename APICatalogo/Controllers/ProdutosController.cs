using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            try
            {
                var produtos = await context.Produtos.Take(100).AsNoTracking().ToListAsync();
                if (produtos is null)
                {
                    return NotFound("Produtos não contrados");
                }
                return produtos;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            try
            {
                var produto = await context.Produtos.Take(100).AsNoTracking().FirstOrDefaultAsync(p => p.ProdutoID == id);
                if (produto is null)
                {
                    return NotFound("Produtos não contrados");
                }
                return produto;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            try
            {
                if (produto is null)
                {
                    return BadRequest("Dados invalidos");
                }
                context.Produtos.Add(produto);
                context.SaveChanges();
                return CreatedAtAction(nameof(Get), new { id = produto.ProdutoID }, produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            try
            {
                if (id != produto.ProdutoID)
                {
                    return BadRequest("Dados invalidos");
                }

                context.Entry(produto).State = EntityState.Modified;
                context.SaveChanges();

                return Ok(produto);
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
                var produto = context.Produtos.FirstOrDefault(p => p.ProdutoID == id);
                if (produto is null)
                {
                    return NotFound($"Produto id {id} não econtrado");
                }
                context.Produtos.Remove(produto);
                context.SaveChanges();
                return Ok(produto);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema ao tratar sua solicitação");
            }
        }
    }
}
