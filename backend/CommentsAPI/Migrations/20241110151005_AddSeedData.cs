using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommentsAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "Email", "FileURL", "Homepage", "ParentId", "Username" },
                values: new object[,]
                {
                    { 11101, "This is the first comment!", new DateTime(2024, 11, 10, 17, 0, 4, 699, DateTimeKind.Local).AddTicks(2317), "user1@example.com", "gt86.png", "https://user1.com", null, "user1" },
                    { 11103, "This is a great post. Thank you for sharing!", new DateTime(2024, 11, 10, 11, 10, 4, 699, DateTimeKind.Local).AddTicks(2374), "user3@example.com", "code.txt", null, null, "user3" },
                    { 11105, "<strong>This is a bold comment.</strong>", new DateTime(2024, 11, 10, 17, 6, 4, 699, DateTimeKind.Local).AddTicks(2382), "user5@example.com", "tiger.jpg", null, null, "user5" },
                    { 11107, "<i>This comment is in italics.</i>", new DateTime(2024, 11, 8, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2389), "user7@example.com", null, null, null, "user7" },
                    { 11109, "Great job on the tutorial!", new DateTime(2024, 11, 10, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2396), "user9@example.com", "gt86.png", null, null, "user9" },
                    { 11111, "I have a suggestion. Maybe you could add more examples.", new DateTime(2024, 11, 6, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2401), "user11@example.com", "code.txt", null, null, "user11" },
                    { 11113, "This is a helpful post. I learned a lot.", new DateTime(2024, 11, 10, 17, 3, 4, 699, DateTimeKind.Local).AddTicks(2408), "user13@example.com", "tiger.jpg", null, null, "user13" },
                    { 11115, "Nice! I will try this approach.", new DateTime(2024, 11, 10, 7, 10, 4, 699, DateTimeKind.Local).AddTicks(2414), "user15@example.com", "gt86.png", null, null, "user15" },
                    { 11117, "I would love to see a video tutorial on this.", new DateTime(2024, 11, 10, 16, 56, 4, 699, DateTimeKind.Local).AddTicks(2420), "user17@example.com", "code.txt", null, null, "user17" },
                    { 11119, "<strong>Very informative!</strong> I will be using this soon.", new DateTime(2024, 11, 10, 16, 52, 4, 699, DateTimeKind.Local).AddTicks(2427), "user19@example.com", "test.gif", null, null, "user19" },
                    { 11121, "I think there's an error in this example.", new DateTime(2024, 11, 10, 16, 48, 4, 699, DateTimeKind.Local).AddTicks(2433), "user21@example.com", "code.txt", null, null, "user21" },
                    { 11123, "I'm having trouble understanding this part.", new DateTime(2024, 11, 10, 16, 44, 4, 699, DateTimeKind.Local).AddTicks(2439), "user23@example.com", "hello.txt", null, null, "user23" },
                    { 11125, "I love how simple this approach is.", new DateTime(2024, 11, 10, 16, 40, 4, 699, DateTimeKind.Local).AddTicks(2445), "user25@example.com", "code.txt", null, null, "user25" },
                    { 11126, "This helped me a lot. Thanks!", new DateTime(2024, 11, 10, 16, 38, 4, 699, DateTimeKind.Local).AddTicks(2448), "user26@example.com", null, null, 11126, "user26" },
                    { 11127, "Is it possible to use this with a different framework?", new DateTime(2024, 11, 10, 16, 36, 4, 699, DateTimeKind.Local).AddTicks(2451), "user27@example.com", "test.gif", null, null, "user27" },
                    { 11129, "<code>const y = 20;</code> Another code example.", new DateTime(2024, 11, 10, 17, 48, 4, 699, DateTimeKind.Local).AddTicks(2458), "user29@example.com", "tiger.jpg", null, null, "user29" },
                    { 11102, "I agree with the above comment.", new DateTime(2024, 11, 10, 17, 2, 4, 699, DateTimeKind.Local).AddTicks(2368), "user2@example.com", "hello.txt", null, 11107, "user2" },
                    { 11104, "I have a question about the code in the previous post.", new DateTime(2024, 11, 10, 17, 5, 4, 699, DateTimeKind.Local).AddTicks(2378), "user4@example.com", null, null, 11107, "user4" },
                    { 11106, "Can you provide more details on this?", new DateTime(2024, 11, 10, 17, 7, 4, 699, DateTimeKind.Local).AddTicks(2385), "user6@example.com", "test.gif", null, 11111, "user6" },
                    { 11108, "I found the issue. It was a small typo.", new DateTime(2024, 11, 10, 17, 9, 4, 699, DateTimeKind.Local).AddTicks(2393), "user8@example.com", "hello.txt", null, 11109, "user8" },
                    { 11112, "Could you explain how to use this in production?", new DateTime(2024, 11, 10, 17, 5, 4, 699, DateTimeKind.Local).AddTicks(2405), "user12@example.com", "test.gif", null, 11101, "user12" },
                    { 11118, "I'm stuck. Can someone help me with this part?", new DateTime(2024, 11, 10, 16, 55, 4, 699, DateTimeKind.Local).AddTicks(2423), "user18@example.com", null, null, 11111, "user18" },
                    { 11122, "Can you explain the error you're facing?", new DateTime(2024, 11, 10, 16, 46, 4, 699, DateTimeKind.Local).AddTicks(2436), "user22@example.com", null, null, 11101, "user22" },
                    { 11124, "Can someone clarify how to use the function?", new DateTime(2024, 11, 10, 16, 42, 4, 699, DateTimeKind.Local).AddTicks(2442), "user24@example.com", "gt86.png", null, 11125, "user24" },
                    { 11128, "Thanks for the clarification.", new DateTime(2024, 11, 10, 16, 34, 4, 699, DateTimeKind.Local).AddTicks(2454), "user28@example.com", "hello.txt", null, 11125, "user28" },
                    { 11130, "This is great! I'm going to try it.", new DateTime(2024, 11, 10, 17, 50, 4, 699, DateTimeKind.Local).AddTicks(2461), "user30@example.com", "code.txt", null, 11126, "user30" },
                    { 11110, "Thank you for your feedback!", new DateTime(2024, 11, 8, 17, 10, 4, 699, DateTimeKind.Local).AddTicks(2399), "user10@example.com", null, null, 11130, "user10" },
                    { 11114, "<code>let x = 10;</code> Simple code example.", new DateTime(2024, 11, 10, 17, 2, 4, 699, DateTimeKind.Local).AddTicks(2411), "user14@example.com", null, null, 11112, "user14" },
                    { 11116, "Is there any way to optimize this further?", new DateTime(2024, 11, 10, 5, 10, 4, 699, DateTimeKind.Local).AddTicks(2417), "user16@example.com", "hello.txt", null, 11112, "user16" },
                    { 11120, "Can you provide the source code for this?", new DateTime(2024, 11, 10, 16, 50, 4, 699, DateTimeKind.Local).AddTicks(2430), "user20@example.com", "tiger.jpg", null, 11110, "user20" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11102);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11103);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11104);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11105);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11106);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11108);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11113);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11114);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11115);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11116);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11117);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11118);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11119);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11120);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11121);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11122);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11123);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11124);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11127);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11128);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11129);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11107);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11109);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11110);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11111);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11112);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11125);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11101);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11130);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 11126);
        }
    }
}
