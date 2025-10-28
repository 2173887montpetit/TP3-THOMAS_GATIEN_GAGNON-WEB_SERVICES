using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServerFlappybirb.Models;
using ServerFlappybirb.DTOs;
using ServerFlappybirb.Data;

namespace ServerFlappybirb.Controllers
{
    [Route("api/[controller]/[action]")] // Utilisez cette règle de routage globale !
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<Users> _userManager;
        private readonly ServerFlappybirbContext _context;

        public UsersController(UserManager<Users> userManager, ServerFlappybirbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO login)
        {
            // Tenter de trouver l'utilisateur dans la BD à partir de son pseudo
            Users? user = await _userManager.FindByNameAsync(login.Username);

            // Si l'utilisateur existe ET que son mot de passe est exact
            if (user != null && await _userManager.CheckPasswordAsync(user, login.Password))
            {
                // Récupérer les rôles de l'utilisateur (Cours 22+)
                IList<string> roles = await _userManager.GetRolesAsync(user);
                List<Claim> authClaims = new List<Claim>();
                foreach (string role in roles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, role));
                }
                authClaims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

                // Générer et chiffrer le token 
                SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes("LooOOongue Phrase SiNoN Ça ne Marchera PaAaAAAaAas !")); // Phrase identique dans Program.cs
                JwtSecurityToken token = new JwtSecurityToken(
                    issuer: "https://localhost:7278", // ⛔ Vérifiez le PORT de votre serveur dans launchSettings.json !
                    audience: "http://localhost:4200",
                    claims: authClaims,
                    expires: DateTime.Now.AddMinutes(30), // Durée de validité du token
                    signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                    );

                // Envoyer le token à l'application cliente sous forme d'objet JSON
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    validTo = token.ValidTo
                });
            }
            // Utilisateur inexistant ou mot de passe incorrecte
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "Le nom d'utilisateur ou le mot de passe est invalide." });
            }
        }



        [HttpPost]
        public async Task<ActionResult> Register(RegisterDTO register)
        {
            // Vérifier si l'utilisateur existe déjà
            Users? userExists = await _userManager.FindByNameAsync(register.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest,
                    new { Message = "L'utilisateur existe déjà !" });
            Users user = new Users()
            {
                UserName = register.Username,
                Email = register.Email,
                SecurityStamp = Guid.NewGuid().ToString()
            };
            // Créer l'utilisateur dans la BD
            IdentityResult result = await _userManager.CreateAsync(user, register.Password);
            
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "La création de l'utilisateur a échoué ! Veuillez vérifier le mot de passe et réessayer." });
            return Ok(new { Message = "Utilisateur créé avec succès !" });
        }
    }
}
