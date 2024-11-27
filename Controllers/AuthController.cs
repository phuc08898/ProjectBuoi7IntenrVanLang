using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using ProjectBuoi7.Data;
using ProjectBuoi7.Models;

namespace ProjectBuoi7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly SQLDbcontext _context;

        // Inject DbContext vào controller
        public AuthController(SQLDbcontext context)
        {
            _context = context;
        }

        // Bộ nhớ tạm để lưu token đăng nhập
        private static Dictionary<int, string> loggedInUsers = new Dictionary<int, string>();

        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            if (loginUser == null)
            {
                return BadRequest("Invalid input.");
            }

            // Kiểm tra user trong cơ sở dữ liệu
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User newUser)
        {
            if (newUser == null || string.IsNullOrEmpty(newUser.Username) || string.IsNullOrEmpty(newUser.Password))
            {
                return BadRequest("Username and password are required.");
            }

            // Kiểm tra xem username đã tồn tại chưa
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Username == newUser.Username);
            if (existingUser != null)
            {
                return Conflict("Username is already taken.");
            }

            // Thêm user mới vào cơ sở dữ liệu
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "User registered successfully.",
                userId = newUser.Id,
                username = newUser.Username
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
