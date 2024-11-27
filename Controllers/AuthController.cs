using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProjectBuoi7.Models;

namespace ProjectBuoi7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        // Danh sách user giả lập
        private static List<User> users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin123", Role = "Admin" },
            new User { Id = 2, Username = "user", Password = "user123", Role = "User" }
        };

        // Bộ nhớ tạm để lưu token đăng nhập
        private static Dictionary<int, string> loggedInUsers = new Dictionary<int, string>();

        // Đăng nhập
        [HttpPost("login")]
        public IActionResult Login([FromBody] User loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("Invalid input.");
            }

            // Kiểm tra user
            var user = users.FirstOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // Tạo token (đơn giản là một chuỗi giả lập)
            var token = $"token_{user.Id}_{System.Guid.NewGuid()}";

            // Lưu token vào danh sách user đã đăng nhập
            if (!loggedInUsers.ContainsKey(user.Id))
            {
                loggedInUsers.Add(user.Id, token);
            }
            else
            {
                loggedInUsers[user.Id] = token; // Cập nhật token mới
            }

            return Ok(new
            {
                message = "Login successful.",
                userId = user.Id,
                username = user.Username,
                token = token
            });
        }

        // Đăng xuất
        [HttpPost("logout")]
        public IActionResult Logout([FromBody] LogoutRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Token))
            {
                return BadRequest("Token is required.");
            }

            // Tìm user dựa trên token
            var userId = loggedInUsers.FirstOrDefault(kvp => kvp.Value == request.Token).Key;
            if (userId == 0)
            {
                return Unauthorized("Invalid token.");
            }

            // Xóa user khỏi danh sách đăng nhập
            loggedInUsers.Remove(userId);

            return Ok(new { message = "Logout successful." });
        }
    }

    // Class cho yêu cầu Logout
    public class LogoutRequest
    {
        public string Token { get; set; }
    }
}
