using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Kogebog.API.Service.DTO.UnitDTO;
using Kogebog.API.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kogebog.API.Service.Services
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public UnitResponse MapUnitToUnitResponse(Unit unit)
        {
            return new UnitResponse
            {
                Id = unit.Id,
                Name = unit.Name,
                PluralName = unit.PluralName,
                Abbreviation = unit.Abbreviation,
            };
        }


        public Unit MapUnitRequestToUnit(UnitRequest unitRequest)
        {
            return new Unit
            {
                Name = unitRequest.Name,
                PluralName = unitRequest.PluralName,
                Abbreviation = unitRequest.Abbreviation,
            };
        }

        public async Task<IEnumerable<UnitResponse>> GetAllAsync()
        {
            IEnumerable<Unit> units = await _unitRepository.GetAllAsync();
            return units.Select(MapUnitToUnitResponse).ToList();
        }

        public async Task<UnitResponse?> GetByIdAsync(Guid id)
        {
            var unit = await _unitRepository.GetByIdAsync(id);
            return unit is null ? null : MapUnitToUnitResponse(unit);
        }

        public async Task<UnitResponse> AddAsync(UnitRequest newUnitRequest)
        {
            var unit = MapUnitRequestToUnit(newUnitRequest);
            var insertedUnit = await _unitRepository.AddAsync(unit);
            return MapUnitToUnitResponse(insertedUnit);
        }

        public async Task<UnitResponse> UpdateByIdAsync(Guid id, UnitRequest updatedUnitRequest)
        {
            var unit = MapUnitRequestToUnit(updatedUnitRequest);
            var updatedUnit = await _unitRepository.UpdateByIdAsync(id, unit);
            return MapUnitToUnitResponse(updatedUnit);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _unitRepository.DeleteByIdAsync(id);
        }
    }
}
