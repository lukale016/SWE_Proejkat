using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Projekat.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel;
using System.Net;
using System.Net.Http;
using System.IO;
using Microsoft.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Cors;
using Hangfire;

namespace Projekat.Controllers
{
    [Authorize]
    [EnableCors("LocalhostAllow")]
    [ApiController]
    [Route("[controller]")]
    public class VinylController : ControllerBase
    {
        private VinylContext vinylContext;

        public VinylController(VinylContext v)
        {
            vinylContext=v;
            RecurringJob.AddOrUpdate("EventControl", () => EventControl(), Cron.Weekly);
        }
///////////////////////////////////////////////////GET
        [HttpGet]
        [Route("LogedUser")]
        public ActionResult<User> GetLogedUser()
        {
            string id;
            if((id = User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value) == null)
                return NotFound();
            return vinylContext.Users.Find(id); 
        }

        [HttpGet]
        [Route("Users")]
        public ActionResult<List<User>> GetUsers()
        {
            return vinylContext.Users.ToList();
        }

        [HttpGet]
        [Route("User/{UserId}")]
        public ActionResult<User> GetUser(string UserId)
        {
            return vinylContext.Users.Find(UserId);
        }

        [HttpGet]
        [Route("UserPrivateEvents/{UserId}")]
        public ActionResult<List<Event>> GetUserPrivateEvents(string UserId)
        {
            string id = User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value;
            User u = vinylContext.Users.Find(UserId);
            bool friends = false;
            foreach (Friend f in u.Friends)
            {
                if(f.User2Ref == id)
                    friends = true;
            }
            if(!friends)
                return Unauthorized();
            List<Event> le = vinylContext.Users.Find(UserId).EventsOwned.ToList();
            List<Event> returnList = new List<Event>();
            foreach (Event e in le)
            {
                if(e.Modifier=="h")
                    returnList.Add(e);
            }
            return returnList;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("RecordsFrom/{category}")]
        public ActionResult<List<Record>> GetRecordsFrom(string category)
        {
            List<Record> ls = vinylContext.Records.ToList();
            List<Record> returnVal = new List<Record>();
            foreach (Record e in ls)
            {
                if(e.Category == category)
                    returnVal.Add(e);
            }
            return returnVal;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Caffes")]
        public ActionResult<List<Caffe>> GetCaffes()
        {
            return vinylContext.Caffes.ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Caffe/{CaffeId}")]
        public ActionResult<Caffe> GetCaffe(int CaffeId)
        {
            return vinylContext.Caffes.Find(CaffeId);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("OwnerCaffes/{UserId}")]
        public ActionResult<List<Caffe>> GetOwnerCaffes(string UserId)
        {
            return vinylContext.Users.Find(UserId).OwnedCaffes.ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Records")]
        public ActionResult<List<Record>> GetRecords()
        {
            return vinylContext.Records.ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Record/{RecordId}")]
        public ActionResult<Record> GetRecord(int RecordId)
        {
            return vinylContext.Records.Find(RecordId);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("PublicEvents")]
        public ActionResult<List<Event>> GetPublicEvents()
        {
            List<Event> ev = vinylContext.Events.ToList();
            List<Event> pev = new List<Event>();
            foreach (Event e in ev)
            {
                if(e.Modifier=="p")
                    pev.Add(e);
            }
            return pev;
        }

        [HttpGet]
        [Route("PrivateEvents")]
        public ActionResult<List<Event>> GetPrivateEvents()
        {
            List<Event> ev = vinylContext.Events.ToList();
            List<Event> pev = new List<Event>();
            foreach (Event e in ev)
            {
                if(e.Modifier=="h")
                    pev.Add(e);
            }
            return pev;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("Event/{EventId}")]
        public ActionResult<Event> GetEvent(int EventId)
        {
            Event e = vinylContext.Events.Find(EventId);
            if(e.Modifier == "h" && !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            if(e.Modifier == "h")
            {
                string id = User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value;
                User u = vinylContext.Users.Find(e.OwnerRef);
                bool friends = false;
                foreach (Friend f in u.Friends)
                {
                    if(f.User2Ref == id)
                        friends = true;
                }
                if(!friends)
                    return Unauthorized();
            }
            return e;
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("UserFriends/{UserId}")]
        public ActionResult<List<User>> GetUserFriends(string UserId)
        {
            List<Friend> lf = vinylContext.Users.Find(UserId).Friends.ToList();
            List<User> lu = new List<User>();
            foreach (Friend f in lf)
            {
                lu.Add(vinylContext.Users.Find(f.User2Ref));
            }
            return lu;
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("UserImg/{UserId}")]
        public IActionResult GetUserImg(string UserId)
        {
            User user;
            if ((user = vinylContext.Users.Find(UserId)) == null || user.ProfileImg == null)
                return NotFound();
            string path = Constants._rootPathProfiles + UserId + "\\" + user.ProfileImg;
            string ex = Path.GetExtension(path);
            string type = "image/" + ex;
            
            IFileProvider provider = new PhysicalFileProvider(Constants._rootPathProfiles + UserId);
            IFileInfo fileInfo = provider.GetFileInfo(user.ProfileImg);
            var readStr = fileInfo.CreateReadStream();
            
            return File(readStr, type, UserId + "_" + user.ProfileImg);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("NewsImgSmall/{NewsName}")]
        public IActionResult GetNewsImgSmall(string NewsName)
        {
            News news;
            if((news = vinylContext.News.SingleOrDefault(n => n.TitleSmall == NewsName)) == null || news.ImageSmall == null)
                return NotFound();
            string path = Constants._rootPathNews + NewsName + "\\" + "Small" + "//" + news.ImageSmall;
            string ex = Path.GetExtension(path);
            string type = "image/" + ex;

            IFileProvider provider = new PhysicalFileProvider(Constants._rootPathNews + NewsName + "//" + "Small");
            IFileInfo fileInfo = provider.GetFileInfo(news.ImageSmall);
            var readStr = fileInfo.CreateReadStream();

            return File(readStr, type, news.Id + "_" + news.ImageSmall);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("NewsImgLarge/{NewsName}")]
        public IActionResult GetNewsImgLarge(string NewsName)
        {
            News news;
            if((news = vinylContext.News.SingleOrDefault(n => n.TitleSmall == NewsName)) == null || news.ImageLarge == null)
                return NotFound();
            string path = Constants._rootPathNews + NewsName + "\\" + "Large" + "//" + news.ImageLarge;
            string ex = Path.GetExtension(path);
            string type = "image/" + ex;

            IFileProvider provider = new PhysicalFileProvider(Constants._rootPathNews + NewsName + "//" + "Large");
            IFileInfo fileInfo = provider.GetFileInfo(news.ImageLarge);
            var readStr = fileInfo.CreateReadStream();

            return File(readStr, type, news.Id + "_" + news.ImageLarge);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("CaffeImg/{CaffeName}")]
        public IActionResult GetCaffeImg(string CaffeName)
        {
            Caffe caffe;
            if((caffe = vinylContext.Caffes.SingleOrDefault(c => c.Name == CaffeName)) == null || caffe.BackgroundImg == null )
                return NotFound();
            string path = Constants._rootPathCaffes + CaffeName + "\\" + caffe.BackgroundImg;
            string ex = Path.GetExtension(path);
            string type = "image/" + ex;

            IFileProvider provider = new PhysicalFileProvider(Constants._rootPathCaffes + CaffeName);
            IFileInfo fileInfo = provider.GetFileInfo(caffe.BackgroundImg);
            var readStr = fileInfo.CreateReadStream();

            return File(readStr, type, CaffeName + "_" + caffe.BackgroundImg);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("RecordImg/{RecordName}")]
        public ActionResult GetRecordImg(string RecordName)
        {
            Record record;
            if((record = vinylContext.Records.SingleOrDefault(r => r.Name == RecordName)) == null || record.DisplayImg == null)
                return NotFound();
            string path = Constants._rootPathRecords + RecordName + "\\" + record.DisplayImg;
            string ex = Path.GetExtension(path);
            string type =  "image/" + ex;

            IFileProvider provider = new PhysicalFileProvider(Constants._rootPathRecords + RecordName);
            IFileInfo fileInfo = provider.GetFileInfo(record.DisplayImg);
            var readStr = fileInfo.CreateReadStream();

            return File(readStr, type, RecordName + "_" + record.DisplayImg);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("InterestedIn/{EventId}")]
        public ActionResult<List<Interested>> GetInterestedIn(int EventId)
        {
            Event e = vinylContext.Events.Find(EventId);
            if(e.Modifier == "h" && !User.Identity.IsAuthenticated)
                return Unauthorized();
            if(e.Modifier == "h")
            {
                string id = User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value;
                User u = vinylContext.Users.Find(e.OwnerRef);
                bool friends = false;
                foreach (Friend f in u.Friends)
                {
                    if(f.User2Ref == id)
                        friends = true;
                }
                if(!friends)
                    return Unauthorized();
            }
            return vinylContext.Events.Find(EventId).Interested.ToList();
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("News")]
        public ActionResult<List<News>> GetNews()
        {
            return vinylContext.News.ToList();
        }

        [HttpGet]
        [Route("UserFavoriteRecords/{UserId}")]
        public ActionResult<List<FavoriteRecord>> GetUserFavoriteRecords(string UserId)
        {
            return vinylContext.Users.Find(UserId).FavoriteRecords.ToList();
        }
///////////////////////////////////////////////JWTRelated
        private static string ClaimsIdentifier = ClaimTypes.NameIdentifier;
        private static string ClaimsRole = ClaimTypes.Role;

        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult CreateToken([FromBody] LoginModel login)
        {
            IActionResult response = NotFound();
            var user= Authenticate(login);
            if (user!=null)
            {
                var tokenStr = BuildToken(user);
                response=Ok(new {token = tokenStr});
            }

            return response;
        }

        private string BuildToken(User u)
        {
            List<Claim> claims;
            if(u.IsAdmin == 1)
            {
                claims = new List<Claim>{
                new Claim(ClaimsIdentifier, u.Username),
                new Claim(ClaimsRole, "Admin")
                };
            }
            else if(u.IsOwner == 1)
            {
                claims = new List<Claim>{
                new Claim(ClaimsIdentifier, u.Username),
                new Claim(ClaimsRole, "Owner")
                };
            }
            else if(u.IsAdmin == 1 && u.IsOwner == 1)
            {
                claims = new List<Claim>{
                new Claim(ClaimsIdentifier, u.Username),
                new Claim(ClaimsRole, "AdminOwner")
                };
            }
            else
            {
                claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, u.Username),
                new Claim(ClaimTypes.Role, "User")
                };
            }
            var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes("VinylAPISecretKey"));
            var creds= new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token=new JwtSecurityToken("https://localhost:5001/", "http://localhost:8080/", 
            claims ,expires: DateTime.Now.AddHours(2), signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginModel lm)
        {
            User u;
            if((u = vinylContext.Users.Find(lm.Username)) == null)
                return null;
            if(u.Password == lm.Password)
                return u;
            return null;
        }

        // private bool ValidateToken(string tokStr)
        // {
        //     var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("VinylAPISecretKey"));

        //     var myIssuer = "http://mysite.com";
        //     var myAudience = "http://myaudience.com";

        //     var tokenHandler = new JwtSecurityTokenHandler();
        //     try
        //     {
        //         tokenHandler.ValidateToken(tokStr, new TokenValidationParameters
        //         {
        //             ValidateIssuerSigningKey = true,
        //             ValidateIssuer = true,
        //             ValidateAudience = true,
        //             ValidIssuer = myIssuer,
        //             ValidAudience = myAudience,
        //             IssuerSigningKey = mySecurityKey
        //         }, out SecurityToken validatedToken);
        //     }
        //     catch
        //     {
        //         return false;
        //     }
        //     return true;
        // }
///////////////////////////////////////////////////POST
        [AllowAnonymous]
        [HttpPost]
        [Route("Add/User")]
        public IActionResult PostAddUser([FromBody]User u)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            if(vinylContext.Users.Find(u.Username)!=null)
                return Conflict("Alredy exists.");
            vinylContext.Users.Add(u);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/Record")]
        public IActionResult PostAddRecord([FromBody]Record r)
        {
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value != Constants._RoleAdmin && User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value != Constants._RoleAdminOwner)
                return Unauthorized();
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            vinylContext.Records.Add(r);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/News")]
        public IActionResult PostAddNews([FromBody]News n)
        {
            if(vinylContext.News.Count() == 3)
                return Forbid("Max news count reached.");
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value != Constants._RoleAdmin)
                return Unauthorized();
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            vinylContext.News.Add(n);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/Event")]
        public IActionResult PostAddEvent([FromBody]Event e)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            vinylContext.Events.Add(e);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/Comment")]
        public IActionResult PostAddComment([FromBody]Comment c)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            vinylContext.Comments.Add(c);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/Caffe")]
        public IActionResult PostAddCaffe([FromBody]Caffe c)
        {
            string role  = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(role != Constants._RoleOwner && role != Constants._RoleAdminOwner)
                return Unauthorized();
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            vinylContext.Caffes.Add(c);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/FavoriteRecord")]
        public IActionResult PostAddFavoriteRecord([FromBody]FavoriteRecord fr)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            if(vinylContext.FavoriteRecords.SingleOrDefault(e => (e.UserRef == fr.UserRef) && (e.RecordRef == fr.RecordRef)) != null)
                return Conflict("Alredy exists.");
            vinylContext.FavoriteRecords.Add(fr);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/Friend")]
        public IActionResult PostAddFriend([FromBody]Friend f)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            if(vinylContext.Friends.SingleOrDefault(e => (e.User1Ref == f.User1Ref) && (e.User2Ref == f.User2Ref)) != null)
                return Conflict("Alredy exists.");
            vinylContext.Friends.Add(f);
            Friend reverse = new Friend{
                User1Ref = f.User2Ref,
                User2Ref = f.User1Ref
            };
            PendingRequest pr = new PendingRequest
            {
                UserSentRef = f.User1Ref,
                UserReceavedRef = f.User2Ref
            };
            vinylContext.PendingRequests.Remove(pr);
            vinylContext.Friends.Add(reverse);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/Interested")]
        public IActionResult PostAddInteresterd([FromBody]Interested i)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            if(vinylContext.Interested.SingleOrDefault(e => (e.EventRef == i.EventRef) && (e.AttenderRef == i.AttenderRef)) != null)
                return Conflict("Alredy exists.");
            vinylContext.Interested.Add(i);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Add/PendingRequest")]
        public IActionResult PostAddPendingRequest([FromBody]PendingRequest pr)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid.");
            if(vinylContext.PendingRequests.SingleOrDefault(e => (e.UserSentRef == pr.UserSentRef) && (e.UserReceavedRef == pr.UserReceavedRef)) != null)
                return Conflict("Alredy exists.");
            vinylContext.PendingRequests.Add(pr);
            vinylContext.SaveChanges();
            return Ok();
        }
///////////////////////////////////////////////////////////POST_IMG
        [HttpPost]
        [Route("Add/UserImg/{UserId}")]
        public async Task<IActionResult> PostAddUserImg(string UserId, [FromForm]ImageUploadModel image)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            if(image == null)
                return BadRequest();
            User user;
            if((user = vinylContext.Users.Find(UserId)) == null)
                return NoContent();
            if(user.Username != User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value)
                return Unauthorized();
            if(!Directory.Exists(Constants._rootPathProfiles + "//" + UserId + "//"))
                Directory.CreateDirectory(Constants._rootPathProfiles + "//" + UserId + "//");
            if(user.ProfileImg != null)
                System.IO.File.Delete(Constants._rootPathProfiles + "//" + UserId + "//" + user.ProfileImg);
            if(image.Image.Length > 0)
            {
                using(FileStream fs = System.IO.File.Create(Constants._rootPathProfiles + "//" + UserId + "//" + image.Image.FileName))
                {
                    await image.Image.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                user.ProfileImg = image.Image.FileName;
                vinylContext.Users.Update(user);
                vinylContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Add/RecordImg/{RecordName}")]
        public async Task<IActionResult> POSTAddRecordImg(string RecordName, [FromForm]ImageUploadModel image)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            if(image == null)
                return BadRequest();
            Record record;
            if((record = vinylContext.Records.SingleOrDefault(r => r.Name == RecordName)) == null)
                return NoContent();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            if(!Directory.Exists(Constants._rootPathRecords + "//" + RecordName + "//"))
                Directory.CreateDirectory(Constants._rootPathRecords + "//" + RecordName + "//");
            if(record.DisplayImg != null)
                System.IO.File.Delete(Constants._rootPathRecords + "//" + RecordName + "//" + record.DisplayImg);
            if(image.Image.Length > 0)
            {
                using(FileStream fs = System.IO.File.Create(Constants._rootPathRecords + "//" + RecordName + "//" + image.Image.FileName))
                {
                    await image.Image.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                record.DisplayImg = image.Image.FileName;
                vinylContext.Records.Update(record);
                vinylContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Add/CaffeImg/{CaffeName}")]
        public async Task<IActionResult> POSTAddCaffeImg(string CaffeName, [FromForm]ImageUploadModel image)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            if(image == null)
                return BadRequest();
            Caffe caffe;
            if((caffe = vinylContext.Caffes.SingleOrDefault(c => c.Name == CaffeName)) == null)
                return NoContent();
            if(caffe.OwnerRef != User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value)
                return Unauthorized();
            if(!Directory.Exists(Constants._rootPathCaffes + "//" + CaffeName + "//"))
                Directory.CreateDirectory(Constants._rootPathCaffes + "//" + CaffeName + "//");
            if(caffe.BackgroundImg != null)
                System.IO.File.Delete(Constants._rootPathCaffes + "//" + CaffeName + "//" + caffe.BackgroundImg);
            if(image.Image.Length > 0)
            {
                using(FileStream fs = System.IO.File.Create(Constants._rootPathCaffes + "//" + CaffeName + "//" + image.Image.FileName))
                {
                    await image.Image.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                caffe.BackgroundImg = image.Image.FileName;
                vinylContext.Caffes.Update(caffe);
                vinylContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        
        [HttpPost]
        [Route("Add/NewsImgSmall/{NewsName}")]
        public async Task<IActionResult> POSTAddNewsImgSmall(string NewsName, [FromForm]ImageUploadModel image)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            if(image == null)
                return BadRequest();
            News news;
            if((news = vinylContext.News.SingleOrDefault(n => n.TitleSmall == NewsName)) == null)
                return NoContent();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            if(!Directory.Exists(Constants._rootPathNews + "//" + NewsName + "//"))
            {
                Directory.CreateDirectory(Constants._rootPathNews + "//" + NewsName + "//");
                Directory.CreateDirectory(Constants._rootPathNews + "//" + NewsName + "//" + "Small" + "//");
                Directory.CreateDirectory(Constants._rootPathNews + "//" + NewsName + "//" + "Large" + "//");
            }
            if(news.ImageSmall != null)
                System.IO.File.Delete(Constants._rootPathNews + "//" + NewsName + "//" + "Small" + "//" + news.ImageSmall);
            if(image.Image.Length > 0)
            {
                using(FileStream fs = System.IO.File.Create(Constants._rootPathNews + "//" + NewsName + "//" + "Small" + "//" + image.Image.FileName))
                {
                    await image.Image.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                news.ImageSmall = image.Image.FileName;
                vinylContext.News.Update(news);
                vinylContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("Add/NewsImgLarge/{NewsName}")]
        public async Task<IActionResult> POSTAddNewsImgLarge(string NewsName, [FromForm]ImageUploadModel image)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            if(image == null)
                return BadRequest();
            News news;
            if((news = vinylContext.News.SingleOrDefault(n => n.TitleSmall == NewsName)) == null)
                return NoContent();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            if(!Directory.Exists(Constants._rootPathNews + "//" + NewsName + "//"))
            {
                Directory.CreateDirectory(Constants._rootPathNews + "//" + NewsName + "//");
                Directory.CreateDirectory(Constants._rootPathNews + "//" + NewsName + "//" + "Small" + "//");
                Directory.CreateDirectory(Constants._rootPathNews + "//" + NewsName + "//" + "Large" + "//");
            }
            if(news.ImageLarge != null)
                System.IO.File.Delete(Constants._rootPathNews + "//" + NewsName + "//" + "Large" + "//" + news.ImageLarge);
            if(image.Image.Length > 0)
            {
                using(FileStream fs = System.IO.File.Create(Constants._rootPathNews + "//" + NewsName + "//" + "Large" + "//" + image.Image.FileName))
                {
                    await image.Image.CopyToAsync(fs);
                    await fs.FlushAsync();
                }
                news.ImageLarge = image.Image.FileName;
                vinylContext.News.Update(news);
                vinylContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
///////////////////////////////////////////////////////////PUT
        [HttpPut]
        [Route("Update/Caffe")]
        public IActionResult PutUpdateCaffe([FromBody]Caffe c)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid");
            Caffe caffe;
            if((caffe = vinylContext.Caffes.Find(c.Id)) == null)
                return NotFound("Element not found");
            if(caffe.OwnerRef != User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value)
                return Unauthorized();
            caffe.Name = c.Name;
            caffe.City = c.City;
            caffe.Address = c.Address;
            caffe.Info = c.Info;
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("Update/Event")]
        public IActionResult PutUpdateEvent([FromBody]Event e)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid");
            Event ev;
            if((ev = vinylContext.Events.Find(e.Id)) == null)
                return NotFound("Element not found");
            if(ev.OwnerRef != User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value)
                return Unauthorized();
            ev.Date = e.Date;
            ev.Info = e.Info;
            ev.Title = e.Title;
            ev.CaffeRef = e.CaffeRef;
            ev.Time = e.Time;
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("Update/News")]
        public IActionResult PutUpdateNews([FromBody]News n)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid");
            News news;
            if((news = vinylContext.News.Find(n.Id)) == null)
                return NotFound("Element not found");
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            news.TextLarge = n.TextLarge;
            news.TextSmall = n.TextSmall;
            news.TitleLarge = n.TitleLarge;
            news.TitleSmall = n.TitleSmall;
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("Update/Record")]
        public IActionResult PutUpdateRecord([FromBody]Record r)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid");
            Record record;
            if((record = vinylContext.Records.Find(r.Id)) == null)
                return NotFound("Element not found");
            record.Name = r.Name;
            record.Info = r.Info;
            record.Category = r.Category;
            record.Author = r.Author;
            record.Demo = r.Demo;
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("Update/User")]
        public IActionResult PutUpdateUser([FromBody]User u)
        {
            if(!ModelState.IsValid)
                return BadRequest("Model is not valid");
            User user;
            if((user = vinylContext.Users.Find(u.Username)) == null)
                return NotFound("Element not found");
            if(user.Username != User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value)
                return Unauthorized();
            user.Password = u.Password;
            user.Name = u.Name;
            user.Surname = u.Surname;
            user.City = u.City;
            user.Bio = u.Bio;
            user.IsOwner = u.IsOwner;
            user.IsAdmin = u.IsAdmin;
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("Update/Comment")]
        public ActionResult PutUpdateComment([FromBody]Comment c)
        {
            if(!ModelState.IsValid)
                return BadRequest();
            Comment comment;
            if((comment = vinylContext.Comments.Find(c.Id)) == null)
                return NotFound();
            if(comment.OwnerRef != User.Claims.SingleOrDefault(e => e.Type == ClaimsIdentifier).Value)
                return Unauthorized();
            comment.Content = c.Content;
            vinylContext.SaveChanges();
            return Ok();
        }
////////////////////////////////////////////////////DELETE
        [HttpDelete]
        [Route("Delete/Caffe/{CaffeId}")]
        public IActionResult DeleteCaffe(int CaffeId)
        {
            Caffe caffe;
            if((caffe = vinylContext.Caffes.Find(CaffeId)) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value != caffe.OwnerRef && role != Constants._RoleOwner && role != Constants._RoleAdminOwner)
                return Unauthorized();
            helpDeleteCaffe(caffe);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/Comment/{CommentId}")]
        public IActionResult DeleteComment(int CommentId)
        {
            Comment comment;
            if((comment = vinylContext.Comments.Find(CommentId)) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value != comment.OwnerRef && role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            vinylContext.Comments.Remove(comment);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/Event/{EventId}")]
        public IActionResult DeleteEvent(int EventId)
        {
            Event e;
            if((e = vinylContext.Events.Find(EventId)) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value != e.OwnerRef && role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            helpDeleteEvent(e);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/FavoriteRecord/{UserId}/{RecordId}")]
        public IActionResult DeleteFavoriteRecord(string UserId, int RecordId)
        {
            FavoriteRecord favoriteRecord;
            if((favoriteRecord = vinylContext.FavoriteRecords.SingleOrDefault(fr => (fr.UserRef == UserId) && (fr.RecordRef == RecordId))) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(c => c.Type == ClaimsIdentifier).Value;
            if(role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            vinylContext.FavoriteRecords.Remove(favoriteRecord);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/Friend/{User1Id}/{User2Id}")]
        public IActionResult DeleteFriend(string User1Id, string User2Id)
        {
            Friend friend;
            if((friend = vinylContext.Friends.SingleOrDefault(f => (f.User1Ref == User1Id) && (f.User2Ref == User2Id))) == null)
                return NotFound();
            if(friend.User1Ref != User.Claims.SingleOrDefault(c => c.Type == ClaimsIdentifier).Value)
                return Unauthorized();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value != friend.User1Ref && role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            vinylContext.Friends.Remove(friend);
            Friend friendReverse = new Friend{
                User1Ref = friend.User2Ref,
                User2Ref = friend.User1Ref
            };
            vinylContext.Friends.Remove(friendReverse);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/Interested/{EventId}/{UserId}")]
        public IActionResult DeleteInterested(int EventId, string UserId)
        {
            Interested interested;
            if((interested = vinylContext.Interested.SingleOrDefault(i => (i.EventRef == EventId) && (i.AttenderRef == UserId))) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value != interested.AttenderRef && role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            vinylContext.Interested.Remove(interested);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/News/{NewsId}")]
        public IActionResult DeleteNews(int NewsId)
        {
            News news;
            if((news = vinylContext.News.Find(NewsId)) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            vinylContext.News.Remove(news);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/PendingRequest/{UserSentId}/{UserReceavedId}")]
        public IActionResult DeletePendingRequest(string UserSentId, string UserReceavedId)
        {
            PendingRequest pending;
            if((pending = vinylContext.PendingRequests.SingleOrDefault(pr => (pr.UserSentRef == UserSentId) && (pr.UserReceavedRef == UserReceavedId))) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value != pending.UserSentRef && User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value == pending.UserReceavedRef && role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            vinylContext.PendingRequests.Remove(pending);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/Record/{RecordId}")]
        public IActionResult DeleteRecord(int RecordId)
        {
            Record record;
            if((record = vinylContext.Records.Find(RecordId)) == null)
                return  NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            helpDeleteRecord(record);
            vinylContext.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        [Route("Delete/User/{UserId}")]
        public IActionResult DeleteUser(string UserId)
        {
            User user;
            if((user = vinylContext.Users.Find(UserId)) == null)
                return NotFound();
            string role = User.Claims.SingleOrDefault(u => u.Type == ClaimsRole).Value;
            if(User.Claims.SingleOrDefault(u => u.Type == ClaimsIdentifier).Value != user.Username && role != Constants._RoleAdmin && role != Constants._RoleAdminOwner)
                return Unauthorized();
            helpDeleteUser(user);
            vinylContext.SaveChanges();
            return Ok();
        }

/////////////////////////////////////////////DeleteHelpers

        private void helpDeleteUser(User u)
        {
            List<Friend> userFriends = u.Friends.ToList();
            foreach (Friend e in userFriends)
            {
                vinylContext.Friends.Remove(e);
            }
            List<Event> userEvents = u.EventsOwned.ToList();
            foreach (Event e in userEvents)
            {
                helpDeleteEvent(e);
            }
            List<PendingRequest> userPendingRequests = u.PendingRequests.ToList();
            foreach (PendingRequest e in userPendingRequests)
            {
                vinylContext.PendingRequests.Remove(e);
            }
            List<PendingRequest> userSentRequests = u.SentRequests.ToList();
            foreach (PendingRequest e in userSentRequests)
            {
                vinylContext.PendingRequests.Remove(e);
            }
            List<FavoriteRecord> userFavRecords = u.FavoriteRecords.ToList();
            foreach (FavoriteRecord e in userFavRecords)
            {
                vinylContext.FavoriteRecords.Remove(e);
            }
            List<Comment> userComments = u.PostedComments.ToList();
            foreach (Comment e in userComments)
            {
                vinylContext.Comments.Remove(e);
            }
            if(u.IsOwner==1)
            {
                List<Caffe> userCaffes = u.OwnedCaffes.ToList();
                foreach (Caffe e in userCaffes)
                {
                    helpDeleteCaffe(e);
                }
            }
            vinylContext.Users.Remove(u);
        }
        
        private void helpDeleteRecord(Record r)
        {
            List<Comment> recordComments = r.Comments.ToList();
            foreach (Comment e in recordComments)
            {
                vinylContext.Comments.Remove(e);
            }
            foreach(Song s in r.Songs.ToList())
            {
                vinylContext.Songs.Remove(s);
            }
            vinylContext.Records.Remove(r);
        }

        private void helpDeleteEvent(Event e)
        {
            List<Interested> eventInterested = e.Interested.ToList();
            foreach (Interested el in eventInterested)
            {
                vinylContext.Interested.Remove(el);
            }
            vinylContext.Events.Remove(e);
        }

        private void helpDeleteCaffe(Caffe c)
        {
            List<Event> caffeEvents = c.OrganizedEvents.ToList();
            foreach (Event e in caffeEvents)
            {
                helpDeleteEvent(e);
            }
            vinylContext.Caffes.Remove(c);
        }
//////////////////////////////////////////////////TimeOriantedFunctions
        public void EventControl()
        {
            foreach(Event e in vinylContext.Events.ToList())
            {
                string s = e.Date;
                int year = Int32.Parse(e.Date.Substring(0, 4));
                int month = Int32.Parse(e.Date.Substring(5, 2));
                int day = Int32.Parse(e.Date.Substring(8, 2));

                if(year != DateTime.Today.Year)
                {
                    helpDeleteEvent(e);
                    vinylContext.SaveChanges();
                }
                else if(month != DateTime.Today.Month)
                {
                    helpDeleteEvent(e);
                    vinylContext.SaveChanges();
                }
                else if(day != DateTime.Today.Day)
                {
                    helpDeleteEvent(e);
                    vinylContext.SaveChanges();
                }
            }
        }
    }
}