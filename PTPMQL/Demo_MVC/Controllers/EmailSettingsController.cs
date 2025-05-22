using Demo_MVC.Models.Process;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Demo_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmailSettingsController : Controller
    {
        private readonly IOptions<MailSettings> _mailSettings;
        private readonly string _mailSaveDir = "mailsSave";

        public EmailSettingsController(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings;
        }

        public IActionResult Index()
        {
            var model = _mailSettings.Value;
            
            // Kiểm tra số lượng email chưa gửi
            int unsentEmailCount = 0;
            if (Directory.Exists(_mailSaveDir))
            {
                unsentEmailCount = Directory.GetFiles(_mailSaveDir, "*.eml").Length;
            }
            
            ViewBag.UnsentEmailCount = unsentEmailCount;
            return View(model);
        }

        public IActionResult FailedEmails()
        {
            var emailFiles = new List<EmailFileViewModel>();
            
            if (Directory.Exists(_mailSaveDir))
            {
                var files = Directory.GetFiles(_mailSaveDir, "*.eml")
                    .Select(f => new FileInfo(f))
                    .OrderByDescending(f => f.CreationTime);
                
                foreach (var file in files)
                {
                    emailFiles.Add(new EmailFileViewModel
                    {
                        FileName = file.Name,
                        CreatedDate = file.CreationTime,
                        FilePath = file.FullName
                    });
                }
            }
            
            return View(emailFiles);
        }

        public async Task<IActionResult> ViewEmail(string id)
        {
            var filePath = Path.Combine(_mailSaveDir, id);
            if (!System.IO.File.Exists(filePath) || !filePath.EndsWith(".eml"))
            {
                return NotFound();
            }
            
            string emailContent = await System.IO.File.ReadAllTextAsync(filePath);
            ViewBag.FileName = id;
            return View((object)emailContent);
        }

        public IActionResult DeleteAll()
        {
            if (Directory.Exists(_mailSaveDir))
            {
                foreach (var file in Directory.GetFiles(_mailSaveDir, "*.eml"))
                {
                    try
                    {
                        System.IO.File.Delete(file);
                    }
                    catch
                    {
                        // Ignore errors
                    }
                }
            }
            
            return RedirectToAction(nameof(FailedEmails));
        }
    }

    public class EmailFileViewModel
    {
        public string FileName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FilePath { get; set; }
    }
}
