using Kogebog.API.Repository.Database;
using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kogebog.API.Repository.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public ProfileRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Profile>> GetAllAsync()
        {
            return await _context.Profiles
                .Include(p => p.Recipes)
                .ToListAsync();
        }

        public async Task<Profile?> GetByIdAsync(Guid id)
        {
            return await _context.Profiles
                .Include(p => p.Recipes)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Profile> AddAsync(Profile newProfile)
        {
            await _context.Profiles.AddAsync(newProfile);
            await _context.SaveChangesAsync();
            var profile = await GetByIdAsync(newProfile.Id);

            if (profile is null)
                throw new Exception("Profile wasn't added");

            return profile;
        }

        public async Task<Profile> UpdateByIdAsync(Guid id, Profile updatedProfile)
        {
            var existingProfile = await GetByIdAsync(id);

            if (existingProfile is null)
                throw new Exception("Profile not found");

            existingProfile.Name = updatedProfile.Name;

            await _context.SaveChangesAsync();

            return existingProfile;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existingProfile = await _context.Profiles.FindAsync(id);
            if (existingProfile != null)
            {
                _context.Profiles.Remove(existingProfile);
                await _context.SaveChangesAsync();
            }
        }
    }
}
