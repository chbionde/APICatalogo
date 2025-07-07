using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCriacao, CategoriaId) values ('Coca-Cola1', 'Refrigerante de cola', 5.00, 'coca-cola1.jpg', 99, now(), 1)");
            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCriacao, CategoriaId) values ('Coca-Cola2', 'Refrigerante de cola', 6.00, 'coca-cola2.jpg', 56, now(), 1)");
            mb.Sql("Insert into Produtos (Nome, Descricao, Preco, ImagemUrl, Estoque, DataCriacao, CategoriaId) values ('Coca-Cola3', 'Refrigerante de cola', 7.00, 'coca-cola3.jpg', 15, now(), 1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("Delete from Produtos");
        }
    }
}
