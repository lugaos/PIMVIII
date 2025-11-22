using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PIMVIII.Migrations
{
    /// <inheritdoc />
    public partial class FixManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlaylistID",
                table: "Conteudo",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Conteudo_PlaylistID",
                table: "Conteudo",
                column: "PlaylistID");

            migrationBuilder.AddForeignKey(
                name: "FK_Conteudo_Playlist_PlaylistID",
                table: "Conteudo",
                column: "PlaylistID",
                principalTable: "Playlist",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Conteudo_Playlist_PlaylistID",
                table: "Conteudo");

            migrationBuilder.DropIndex(
                name: "IX_Conteudo_PlaylistID",
                table: "Conteudo");

            migrationBuilder.DropColumn(
                name: "PlaylistID",
                table: "Conteudo");
        }
    }
}
