using keykeeper_backend.Application.DTOs;
using keykeeper_backend.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace keykeeper_backend.Application.Interfaces
{
    public interface IFileStorage
    {
        Task<string> SaveAsync(Stream file, string ext, string subfolder, CancellationToken ct);
        Task AddPhotoAsync(ListingPhoto photo, CancellationToken ct);
    }
}
