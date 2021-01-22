using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetWebhookPublisher.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

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
                    { new Guid("ea39a6c2-0c77-4526-b023-28b3535c76b7"), new DateTime(2021, 1, 22, 13, 10, 5, 550, DateTimeKind.Local).AddTicks(9620), "Triggered when personel is created.", "Person Created", "person.created" },
                    { new Guid("e16f5835-c547-46bb-a377-13be94d9de06"), new DateTime(2021, 1, 22, 13, 10, 5, 551, DateTimeKind.Local).AddTicks(7126), "Triggered when personel is updated.", "Person Updated", "person.updated" },
                    { new Guid("e8ea10ca-da56-4cb6-83d0-8656e567b4ec"), new DateTime(2021, 1, 22, 13, 10, 5, 551, DateTimeKind.Local).AddTicks(7138), "Triggered when personel is deleted.", "Person Deleted", "person.deleted" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptionContentTypes",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { new Guid("778b03a7-afe0-494e-a340-0be192300d57"), new DateTime(2021, 1, 22, 13, 10, 5, 552, DateTimeKind.Local).AddTicks(5348), "application/json" },
                    { new Guid("25d51904-b42d-49c4-894f-8b002a9a6d59"), new DateTime(2021, 1, 22, 13, 10, 5, 552, DateTimeKind.Local).AddTicks(5359), "application/x-www-form-urlencoded" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptionTypes",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { new Guid("0bb61f8f-e79b-4585-9286-c626bd44e841"), new DateTime(2021, 1, 22, 13, 10, 5, 552, DateTimeKind.Local).AddTicks(6244), "All" },
                    { new Guid("3a28d233-9c3c-4f42-a9ad-b002b4d968a8"), new DateTime(2021, 1, 22, 13, 10, 5, 552, DateTimeKind.Local).AddTicks(6251), "Specific" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptions",
                columns: new[] { "Id", "Created", "IsActive", "PayloadUrl", "Secret", "WebhookSubscriptionContentTypeId", "WebhookSubscriptionTypeId" },
                values: new object[] { new Guid("94922244-aa47-4a73-9887-a7ec023d6785"), new DateTime(2021, 1, 22, 13, 10, 5, 552, DateTimeKind.Local).AddTicks(8334), true, "http://localhost:5045/webhook-json-data-test", "secret", new Guid("778b03a7-afe0-494e-a340-0be192300d57"), new Guid("0bb61f8f-e79b-4585-9286-c626bd44e841") });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptions",
                columns: new[] { "Id", "Created", "IsActive", "PayloadUrl", "Secret", "WebhookSubscriptionContentTypeId", "WebhookSubscriptionTypeId" },
                values: new object[] { new Guid("3035877d-199d-4b45-a877-01119c14431f"), new DateTime(2021, 1, 22, 13, 10, 5, 552, DateTimeKind.Local).AddTicks(8629), true, "http://localhost:5045/webhook-form-data-test", "secret", new Guid("25d51904-b42d-49c4-894f-8b002a9a6d59"), new Guid("0bb61f8f-e79b-4585-9286-c626bd44e841") });

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
                name: "Persons");

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
