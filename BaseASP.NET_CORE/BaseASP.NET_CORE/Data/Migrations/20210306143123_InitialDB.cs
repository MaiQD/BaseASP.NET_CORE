using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace BaseASP.NET_CORE.Data.Migrations
{
	public partial class InitialDB : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "NhatKys",
				columns: table => new
				{
					ID = table.Column<int>(nullable: false)
						.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
					TEN_DANG_NHAP = table.Column<string>(nullable: true),
					MO_TA = table.Column<string>(nullable: true),
					NGAY_TAO = table.Column<DateTime>(nullable: false),
					TEN_DAY_DU = table.Column<string>(nullable: true),
					DU_LIEU = table.Column<string>(nullable: true),
					PHAN_LOAI = table.Column<string>(nullable: true),
					IP_ADDRESS = table.Column<string>(nullable: true),
					GUID = table.Column<Guid>(nullable: false),
					UNG_DUNG = table.Column<string>(nullable: true),
					PAGE_URL = table.Column<string>(nullable: true),
					CAP_DO_ID = table.Column<int>(nullable: false),
					NOI_DUNG = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_NhatKys", x => x.ID);
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "NhatKys");
		}
	}
}