using Microsoft.EntityFrameworkCore.Migrations;

namespace PlusGG.Data.Migrations
{
    public partial class DataModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RuneCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuneCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SummonerSpells",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SummonerSpells", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainRunes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RuneCategoryId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    Sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainRunes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainRunes_RuneCategories_RuneCategoryId",
                        column: x => x.RuneCategoryId,
                        principalTable: "RuneCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Runes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    LevelRune = table.Column<int>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Logo = table.Column<string>(nullable: true),
                    RuneCategoryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Runes_RuneCategories_RuneCategoryId",
                        column: x => x.RuneCategoryId,
                        principalTable: "RuneCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Champions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    SummonerSpellDId = table.Column<int>(nullable: true),
                    SummonerSpellFId = table.Column<int>(nullable: true),
                    Logo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Champions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Champions_SummonerSpells_SummonerSpellDId",
                        column: x => x.SummonerSpellDId,
                        principalTable: "SummonerSpells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Champions_SummonerSpells_SummonerSpellFId",
                        column: x => x.SummonerSpellFId,
                        principalTable: "SummonerSpells",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChampionRunes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimaryMainRuneId = table.Column<int>(nullable: true),
                    PrimaryRune1Id = table.Column<int>(nullable: true),
                    PrimaryRune2Id = table.Column<int>(nullable: true),
                    PrimaryRune3Id = table.Column<int>(nullable: true),
                    SecondaryRune1Id = table.Column<int>(nullable: true),
                    SecondaryRune2Id = table.Column<int>(nullable: true),
                    SecondaryRune3Id = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChampionRunes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChampionRunes_MainRunes_PrimaryMainRuneId",
                        column: x => x.PrimaryMainRuneId,
                        principalTable: "MainRunes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChampionRunes_Runes_PrimaryRune1Id",
                        column: x => x.PrimaryRune1Id,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChampionRunes_Runes_PrimaryRune2Id",
                        column: x => x.PrimaryRune2Id,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChampionRunes_Runes_PrimaryRune3Id",
                        column: x => x.PrimaryRune3Id,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChampionRunes_Runes_SecondaryRune1Id",
                        column: x => x.SecondaryRune1Id,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChampionRunes_Runes_SecondaryRune2Id",
                        column: x => x.SecondaryRune2Id,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChampionRunes_Runes_SecondaryRune3Id",
                        column: x => x.SecondaryRune3Id,
                        principalTable: "Runes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Spells",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Logo = table.Column<string>(nullable: true),
                    SpellType = table.Column<string>(nullable: true),
                    ChampionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spells", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spells_Champions_ChampionId",
                        column: x => x.ChampionId,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchUps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    MainChampionId = table.Column<int>(nullable: true),
                    VsChampionId = table.Column<int>(nullable: true),
                    WinRate = table.Column<double>(nullable: false),
                    StrongerEarly = table.Column<bool>(nullable: false),
                    StrongerMid = table.Column<bool>(nullable: false),
                    StrongerLate = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MatchUps_ChampionRunes_Id",
                        column: x => x.Id,
                        principalTable: "ChampionRunes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchUps_Champions_MainChampionId",
                        column: x => x.MainChampionId,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchUps_Champions_VsChampionId",
                        column: x => x.VsChampionId,
                        principalTable: "Champions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemSets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MatchUpId = table.Column<int>(nullable: true),
                    ItemId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemSets_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSets_MatchUps_MatchUpId",
                        column: x => x.MatchUpId,
                        principalTable: "MatchUps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChampionRunes_PrimaryMainRuneId",
                table: "ChampionRunes",
                column: "PrimaryMainRuneId");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionRunes_PrimaryRune1Id",
                table: "ChampionRunes",
                column: "PrimaryRune1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionRunes_PrimaryRune2Id",
                table: "ChampionRunes",
                column: "PrimaryRune2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionRunes_PrimaryRune3Id",
                table: "ChampionRunes",
                column: "PrimaryRune3Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionRunes_SecondaryRune1Id",
                table: "ChampionRunes",
                column: "SecondaryRune1Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionRunes_SecondaryRune2Id",
                table: "ChampionRunes",
                column: "SecondaryRune2Id");

            migrationBuilder.CreateIndex(
                name: "IX_ChampionRunes_SecondaryRune3Id",
                table: "ChampionRunes",
                column: "SecondaryRune3Id");

            migrationBuilder.CreateIndex(
                name: "IX_Champions_SummonerSpellDId",
                table: "Champions",
                column: "SummonerSpellDId");

            migrationBuilder.CreateIndex(
                name: "IX_Champions_SummonerSpellFId",
                table: "Champions",
                column: "SummonerSpellFId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSets_ItemId",
                table: "ItemSets",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSets_MatchUpId",
                table: "ItemSets",
                column: "MatchUpId");

            migrationBuilder.CreateIndex(
                name: "IX_MainRunes_RuneCategoryId",
                table: "MainRunes",
                column: "RuneCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchUps_MainChampionId",
                table: "MatchUps",
                column: "MainChampionId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchUps_VsChampionId",
                table: "MatchUps",
                column: "VsChampionId");

            migrationBuilder.CreateIndex(
                name: "IX_Runes_RuneCategoryId",
                table: "Runes",
                column: "RuneCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Spells_ChampionId",
                table: "Spells",
                column: "ChampionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemSets");

            migrationBuilder.DropTable(
                name: "Spells");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "MatchUps");

            migrationBuilder.DropTable(
                name: "ChampionRunes");

            migrationBuilder.DropTable(
                name: "Champions");

            migrationBuilder.DropTable(
                name: "MainRunes");

            migrationBuilder.DropTable(
                name: "Runes");

            migrationBuilder.DropTable(
                name: "SummonerSpells");

            migrationBuilder.DropTable(
                name: "RuneCategories");
        }
    }
}
