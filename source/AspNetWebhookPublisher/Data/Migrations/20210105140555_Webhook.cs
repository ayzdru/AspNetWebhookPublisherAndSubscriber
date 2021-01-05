using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetWebhookPublisher.Data.Migrations
{
    public partial class Webhook : Migration
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
                    { new Guid("3c358c09-d38b-4e8c-86b5-8597fee1a7e0"), new DateTime(2021, 1, 5, 17, 5, 54, 640, DateTimeKind.Local).AddTicks(2480), "Triggered when personel is created.", "Person Created", "person.created" },
                    { new Guid("70720dea-b4c1-4d49-bd40-fe9016b18d03"), new DateTime(2021, 1, 5, 17, 5, 54, 641, DateTimeKind.Local).AddTicks(9292), "Triggered when personel is updated.", "Person Updated", "person.updated" },
                    { new Guid("62a76f04-146a-419f-a785-71fc3e36ca8e"), new DateTime(2021, 1, 5, 17, 5, 54, 641, DateTimeKind.Local).AddTicks(9355), "Triggered when personel is deleted.", "Person Deleted", "person.deleted" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptionContentTypes",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { new Guid("4ad91b1a-4009-4620-a4b7-a17746e7bbf2"), new DateTime(2021, 1, 5, 17, 5, 54, 644, DateTimeKind.Local).AddTicks(444), "application/json" },
                    { new Guid("845fe809-bacc-4aab-bdeb-6cb5e5b7292b"), new DateTime(2021, 1, 5, 17, 5, 54, 644, DateTimeKind.Local).AddTicks(470), "application/x-www-form-urlencoded" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptionTypes",
                columns: new[] { "Id", "Created", "Name" },
                values: new object[,]
                {
                    { new Guid("47d861e3-9042-4b65-b029-a5d07672abab"), new DateTime(2021, 1, 5, 17, 5, 54, 644, DateTimeKind.Local).AddTicks(2762), "All" },
                    { new Guid("0c26be7d-8053-49e2-a590-ea991c1ae24a"), new DateTime(2021, 1, 5, 17, 5, 54, 644, DateTimeKind.Local).AddTicks(2781), "Specific" }
                });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptions",
                columns: new[] { "Id", "Created", "IsActive", "PayloadUrl", "Secret", "WebhookSubscriptionContentTypeId", "WebhookSubscriptionTypeId" },
                values: new object[] { new Guid("468caa56-538f-4317-a5c7-cadcda765212"), new DateTime(2021, 1, 5, 17, 5, 54, 644, DateTimeKind.Local).AddTicks(8398), true, "http://localhost:5040/webhook-json-data-test", "secret", new Guid("4ad91b1a-4009-4620-a4b7-a17746e7bbf2"), new Guid("47d861e3-9042-4b65-b029-a5d07672abab") });

            migrationBuilder.InsertData(
                table: "WebhookSubscriptions",
                columns: new[] { "Id", "Created", "IsActive", "PayloadUrl", "Secret", "WebhookSubscriptionContentTypeId", "WebhookSubscriptionTypeId" },
                values: new object[] { new Guid("3e93f47a-a422-4084-870c-667dc7196520"), new DateTime(2021, 1, 5, 17, 5, 54, 644, DateTimeKind.Local).AddTicks(9157), true, "http://localhost:5040/webhook-form-data-test", "secret", new Guid("845fe809-bacc-4aab-bdeb-6cb5e5b7292b"), new Guid("47d861e3-9042-4b65-b029-a5d07672abab") });

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
