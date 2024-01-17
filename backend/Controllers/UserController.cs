using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega.Classes;
using Vega.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly MyDBContext _context;

        public UserController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/User/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/User/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/User
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
      
        // POST: api/User/Login
        [HttpPost("Login")]
        public async Task<ActionResult<User>> LogInUser(LogInUser user)
        {
            var authenticatedUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

            if (authenticatedUser == null)
            {
                return NotFound(new { message = "Invalid email or password" });
            }

            var issuer = "https://joydipkanjilal.com/";
            var audience = "https://joydipkanjilal.com/";
            var key = Encoding.ASCII.GetBytes("ddsadhasbd asdadsad sdas dasd asdasdasd as dasd sad sadas dadssndn asdnasjdnas jd asdas dasjdnas jn dsjan dasjn djasn djasndasjndjasndajsn djnasjnd");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("Id", authenticatedUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, authenticatedUser.Username),
            new Claim(JwtRegisteredClaimNames.Email, authenticatedUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            var userWithToken = new LoggedInUser
            {
                Id = authenticatedUser.Id,
                Username = authenticatedUser.Username,
                Email = authenticatedUser.Email,
                AccountType = authenticatedUser.AccountType,
                Token = jwtToken
            };

            return Ok(new { message = "User logged in successfully", user = userWithToken });
        }


        // POST: api/User/Register
        [HttpPost("Register")]
        public async Task<ActionResult<User>> RegisterUser(User userRegistration)
        {
            if (await _context.Users.AnyAsync(u => u.Email == userRegistration.Email))
            {
                return Conflict(new { message = "Email is already registered" });
            }

            var newUser = new User
            {
                Username = userRegistration.Username,
                Email = userRegistration.Email,
                AccountType = userRegistration.AccountType,
                Password = userRegistration.Password,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Generate JWT token
            var issuer = "https://joydipkanjilal.com/";
            var audience = "https://joydipkanjilal.com/";
            var key = Encoding.ASCII.GetBytes("ddsadhasbd asdasndn asdnasjdnas jd asdas dasjdnas jn dsjan dasjn djasn djasndasjndjasndajsn djnasjnd");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim("Id", newUser.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, newUser.Username),
            new Claim(JwtRegisteredClaimNames.Email, newUser.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);

            return CreatedAtAction("GetUser", new { id = newUser.Id }, new { message = "User registered successfully", user = newUser, token = jwtToken });
        }

       [HttpGet("CurrentUser")]
public ActionResult<LoggedInUser> GetCurrentUser()
{
    foreach (var claim in User.Claims)
    {
        Console.WriteLine($"Claim Type: {claim.Type}, Claim Value: {claim.Value}");
    }

    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "Id");
    if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
    {
        return BadRequest(new { message = "Invalid user claims" });
    }

    var user = _context.Users.Find(userId);

    if (user == null)
    {
        return NotFound(new { message = "User not found" });
    }

    return Ok(user);
}

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
