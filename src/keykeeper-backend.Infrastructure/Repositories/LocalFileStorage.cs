using keykeeper_backend.Application.DTOs;
using keykeeper_backend.Application.Interfaces;
using keykeeper_backend.domain.Entities;
using keykeeper_backend.Infrastructure.KeykepperDbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace keykeeper_backend.Infrastructure.Repositories
{
    public class LocalFileStorage : IFileStorage
    {
        private readonly string _root;
        private readonly AppDbContext _db;

        public LocalFileStorage(IWebHostEnvironment env, AppDbContext db)
        {
            _root = Path.Combine(env.WebRootPath, "uploads");
            _db = db; 
        }

        public async Task<string> SaveAsync(Stream file, string ext, string subfolder, CancellationToken ct)
        {
            var targetDir = Path.Combine(_root, subfolder);
            Directory.CreateDirectory(targetDir);

            var name = $"{Guid.NewGuid()}{ext}";
            var fullPath = Path.Combine(targetDir, name);

            await using var fs = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fs, ct);

            // Возвращаем путь с подпапкой
            return Path.Combine("uploads", subfolder, name).Replace('\\', '/');
        }

        public async Task AddPhotoAsync(ListingPhoto photo, CancellationToken ct)
        {
            await _db.ListingsPhotos.AddAsync(photo, ct);
        }
    }
}
