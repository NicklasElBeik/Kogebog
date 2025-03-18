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
    public class UnitRepository : IUnitRepository
    {
        private readonly ApplicationDbContext _context;

        public UnitRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _context.Units.ToListAsync();
        }

        public async Task<Unit?> GetByIdAsync(Guid id)
        {
            return await _context.Units.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Unit> AddAsync(Unit newUnit)
        {
            await _context.Units.AddAsync(newUnit);
            await _context.SaveChangesAsync();
            var unit = await GetByIdAsync(newUnit.Id);

            if (unit is null)
                throw new Exception("Unit wasn't added");

            return unit;
        }

        public async Task<Unit> UpdateByIdAsync(Guid id, Unit updatedUnit)
        {
            var existingUnit = await GetByIdAsync(id);

            if (existingUnit is null)
                throw new Exception("Unit not found");

            existingUnit.Name = updatedUnit.Name;
            existingUnit.PluralName = updatedUnit.PluralName;
            existingUnit.Abbreviation = updatedUnit.Abbreviation;

            await _context.SaveChangesAsync();

            return existingUnit;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var existingUnit = await _context.Units.FindAsync(id);
            if (existingUnit != null)
            {
                _context.Units.Remove(existingUnit);
                await _context.SaveChangesAsync();
            }
        }
    }
}
