using Jobinja.Api.Data;
using Jobinja.Api.DTOs.JobApplications;
using Jobinja.Api.Entities;
using Jobinja.Api.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jobinja.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobApplicationsController(ApplicationDbContext context,
    IWebHostEnvironment environment)
    : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Create([FromForm] CreateJobApplicationRequestDto request)
    {
        var agahi = await context.Agahis.FirstOrDefaultAsync(x => x.Id == request.AgahiId);
        if (agahi is null || !agahi.IsActive)
        {
            return BadRequest("آگهی مورد نظر یافت نشد یا غیرفعال است");
        }

        var karjo = await context.Karjos.FirstOrDefaultAsync(x => x.Id == request.KarjoId);
        if (karjo is null)
        {
            return BadRequest("شناسه کارجو صحیح نمی باشد");
        }

        var alreadyApplied = await context.JobApplications
            .AnyAsync(x => x.AgahiId == request.AgahiId
                        && x.KarjoId == request.KarjoId);


        if (alreadyApplied)
        {
            return BadRequest("شما قبلاً برای این آگهی رزومه ارسال کرده اید");
        }

        if (request.ResumeFile is null || request.ResumeFile.Length == 0)
        {
            return BadRequest("فایل رزومه الزامی است");
        }

        #region FileUpload

        var uploadFolder = Path.Combine(environment.WebRootPath, "Resumes");

        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ResumeFile.FileName)}";
        var filePath = Path.Combine(uploadFolder, fileName);
        await using var stream = new FileStream(filePath, FileMode.Create);
        await request.ResumeFile.CopyToAsync(stream);

        var resumeUrl = $"Resumes/{fileName}";

        karjo.ResumePath = resumeUrl;

        #endregion FileUpload

        var application = new JobApplication
        {
            AgahiId = request.AgahiId,
            KarjoId = request.KarjoId,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            ResumePath = resumeUrl,
        };

        context.JobApplications.Add(application);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var applications = await context.JobApplications
            .Select(a => new GetJobApplicationResponseDto
            {
                Id = a.Id,
                AgahiId = a.AgahiId,
                AgahiTitle = a.Agahi.Title,
                FullName = a.FullName,
                PhoneNumber = a.PhoneNumber,
                ResumePath = a.ResumePath,
                CreatedAt = a.CreatedAt
            })
            .ToListAsync();

        return Ok(applications);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(int id)
    {
        var application = await context.JobApplications
            .Where(x => x.Id == id)
            .Select(a => new GetJobApplicationByIdResponseDto
            {
                Id = a.Id,
                AgahiId = a.AgahiId,
                AgahiTitle = a.Agahi.Title,
                CompanyName = a.Agahi.Karfarma.CompanyName,
                KarjoId = a.KarjoId,
                FullName = a.FullName,
                PhoneNumber = a.PhoneNumber,
                KarjoJobTitle = a.Karjo.JobTitle,
                ResumePath = a.ResumePath,
                CreatedAt = a.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (application is null)
        {
            return NotFound("درخواست مورد نظر یافت نشد");
        }

        return Ok(application);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update([FromRoute] int id, [FromForm] UpdateJobApplicationRequestDto request)
    {
        if (id != request.Id)
        {
            return BadRequest("شناسه بدنه درخواست با شناسه آدرس مطابقت ندارد");
        }

        var application = await context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);
        if (application is null)
        {
            return NotFound("درخواست مورد نظر یافت نشد");
        }



        application.FullName = request.FullName;
        application.PhoneNumber = request.PhoneNumber;

        if (request.ResumeFile is not null && request.ResumeFile.Length > 0)
        {
            var uploadFolder = Path.Combine(environment.WebRootPath, "Resumes");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ResumeFile.FileName)}";
            var filePath = Path.Combine(uploadFolder, fileName);
            await using var stream = new FileStream(filePath, FileMode.Create);
            await request.ResumeFile.CopyToAsync(stream);

            application.ResumePath = $"Resumes/{fileName}";
        }

        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id}/cancel")]
    public async Task<ActionResult> Cancel(int id)
    {
        var application = await context.JobApplications.FirstOrDefaultAsync(x => x.Id == id);
        if (application is null)
        {
            return NotFound("درخواست مورد نظر یافت نشد");
        }

        await context.SaveChangesAsync();

        return Ok();
    }
}