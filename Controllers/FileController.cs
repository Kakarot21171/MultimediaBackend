using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultimediaManagementBackend.Data;
using MultimediaManagementBackend.Models;

[ApiController]
[Route("api/[controller]")]

public class FileController : ControllerBase
{
    private readonly AppDbContext _context;

    public FileController(AppDbContext context)
    {
        _context = context;
    }


    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file, [FromForm] string category, [FromForm] string tags)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("File is empty");
        }

        // Save the file to the uploads folder
        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var filePath = Path.Combine(uploadsFolder, file.FileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        // Save metadata to the database
        var metadata = new FileMetadata
        {
            FileName = file.FileName,
            FileType = file.ContentType,
            Category = category,
            UploadDate = DateTime.Now,
            FilePath = filePath,
            Tags = tags
        };

        _context.Files.Add(metadata);
        await _context.SaveChangesAsync();

        return Ok(metadata);
    }

    [HttpGet("files")]
    public async Task<IActionResult> GetFiles()
    {
        var files = await _context.Files.ToListAsync();
        return Ok(files);
    }
}



