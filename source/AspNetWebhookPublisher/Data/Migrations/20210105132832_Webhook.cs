using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetWebhookPublisher.Data.Migrations
{
    public partial class Webhook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebhookEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSubscriptionContentTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSubscriptionContentTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSubscriptionTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSubscriptionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WebhookPayloads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Attempt = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookPayloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebhookPayloads_WebhookEvents_WebhookEventId",
                        column: x => x.WebhookEventId,
                        principalTable: "WebhookEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSubscriptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookSubscriptionContentTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookSubscriptionTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayloadUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Secret = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSubscriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebhookSubscriptions_WebhookSubscriptionContentTypes_WebhookSubscriptionContentTypeId",
                        column: x => x.WebhookSubscriptionContentTypeId,
                        principalTable: "WebhookSubscriptionContentTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebhookSubscriptions_WebhookSubscriptionTypes_WebhookSubscriptionTypeId",
                        column: x => x.WebhookSubscriptionTypeId,
                        principalTable: "WebhookSubscriptionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebhookResponses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookPayloadId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HttpStatusCode = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebhookResponses_WebhookPayloads_WebhookPayloadId",
                        column: x => x.WebhookPayloadId,
                        principalTable: "WebhookPayloads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebhookSubscriptionAllowedEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookSubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebhookEventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebhookSubscriptionAllowedEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebhookSubscriptionAllowedEvents_WebhookEvents_WebhookEventId",
                        column: x => x.WebhookEventId,
                        principalTable: "WebhookEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebhookSubscriptionAllowedEvents_WebhookSubscriptions_WebhookSubscriptionId",
                        column: x => x.WebhookSubscriptionId,
                        principalTable: "WebhookSubscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "WebhookEvents",
                columns: new[] { "Id", "Created", "Description", "DisplayName", "Name" },
                values: new object[,]
                {
                    { new Guid("d2207623-81c1-4964-ab7a-0d47d1f0b1c6"), new DateTime(2021, 1, 5, 16, 28, 32, 41, DateTimeKind.Local).AddTicks(6621), "Triggered when personel is created.", "Person Created", "person.created" },
                    { new Guid("6fdc0abb-847d-4ebd-b4b1-7ec6cbf07fe5"), new DateTime(2021, 1, 5, 16, 28, 32, 42, DateTimeKind.Local).AddTicks(2797), "Triggered when personel is updated.", "Person Updated", "person.updated" },
                    { new Guid("e95c84d1-bde4-455e-934c-23203a9085db"), new DateTime(2021, 1, 5, 16, 28, 32, 42, DateTimeKind.Local).AddTicks(2810), "Triggered when personel is deleted.", "Person Deleted", "person.deleted" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptionContentTypes",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { new Guid("bc71a96a-50fe-4da8-9b6c-4c3d648ba65f"), new DateTime(2021, 1, 5, 16, 28, 32, 43, DateTimeKind.Local).AddTicks(927), "application/json" },
                    { new Guid("4f8698fa-fd8f-487f-82d2-2887754fa9fd"), new DateTime(2021, 1, 5, 16, 28, 32, 43, DateTimeKind.Local).AddTicks(937), "application/x-www-form-urlencoded" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptionTypes",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { new Guid("d8d11b47-a78f-40aa-adf9-8f0b7475e4e1"), new DateTime(2021, 1, 5, 16, 28, 32, 43, DateTimeKind.Local).AddTicks(1800), "All" },
                    { new Guid("8a684a84-fd72-47b2-ac97-20713b8d1c64"), new DateTime(2021, 1, 5, 16, 28, 32, 43, DateTimeKind.Local).AddTicks(1816), "Specific" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptions",
                columns: new[] { "Id", "Created", "IsActive", "PayloadUrl", "Secret", "WebhookSubscriptionContentTypeId", "WebhookSubscriptionTypeId" },
                values: new object[] { new Guid("495b762c-d088-47ab-a8f2-aa43c783fc39"), new DateTime(2021, 1, 5, 16, 28, 32, 43, DateTimeKind.Local).AddTicks(3963), true, "http://localhost:5040/webhook-form-data-test", "secret", new Guid("bc71a96a-50fe-4da8-9b6c-4c3d648ba65f"), new Guid("d8d11b47-a78f-40aa-adf9-8f0b7475e4e1") });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptions",
                columns: new[] { "Id", "Created", "IsActive", "PayloadUrl", "Secret", "WebhookSubscriptionContentTypeId", "WebhookSubscriptionTypeId" },
                values: new object[] { new Guid("c49cd2ef-89d0-4f31-b0c1-8d8d7f9a5fbb"), new DateTime(2021, 1, 5, 16, 28, 32, 43, DateTimeKind.Local).AddTicks(4257), true, "http://localhost:5040/webhook-json-data-test", "secret", new Guid("4f8698fa-fd8f-487f-82d2-2887754fa9fd"), new Guid("d8d11b47-a78f-40aa-adf9-8f0b7475e4e1") });

            migrationBuilder.CreateIndex(
                name: "IX_WebhookPayloads_WebhookEventId",
                table: "WebhookPayloads",
                column: "WebhookEventId");

            migrationBuilder.CreateIndex(
                name: "IX_WebhookResponses_WebhookPayloadId",
                table: "WebhookResponses",
                column: "WebhookPayloadId");

            migrationBuilder.CreateIndex(
                name: "IX_WebhookSubscriptionAllowedEvents_WebhookEventId",
                table: "WebhookSubscriptionAllowedEvents",
                column: "WebhookEventId");

            migrationBuilder.CreateIndex(
                name: "IX_WebhookSubscriptionAllowedEvents_WebhookSubscriptionId",
                table: "WebhookSubscriptionAllowedEvents",
                column: "WebhookSubscriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_WebhookSubscriptions_WebhookSubscriptionContentTypeId",
                table: "WebhookSubscriptions",
                column: "WebhookSubscriptionContentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WebhookSubscriptions_WebhookSubscriptionTypeId",
                table: "WebhookSubscriptions",
                column: "WebhookSubscriptionTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebhookResponses");

            migrationBuilder.DropTable(
                name: "WebhookSubscriptionAllowedEvents");

            migrationBuilder.DropTable(
                name: "WebhookPayloads");

            migrationBuilder.DropTable(
                name: "WebhookSubscriptions");

            migrationBuilder.DropTable(
                name: "WebhookEvents");

            migrationBuilder.DropTable(
                name: "WebhookSubscriptionContentTypes");

            migrationBuilder.DropTable(
                name: "WebhookSubscriptionTypes");
        }
    }
}
