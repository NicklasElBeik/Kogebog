using Kogebog.API.Repository.Database.Entities;
using Kogebog.API.Repository.Repositories.Interfaces;
using Kogebog.API.Service.DTO.ProfileDTO;
using Kogebog.API.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kogebog.API.Service.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public ProfileResponse MapProfileToProfileResponse(Profile profile)
        {
            return new ProfileResponse
            {
                Id = profile.Id,
                Name = profile.Name,
                Recipes = profile.Recipes.Select(recipe => new ProfileRecipeResponse
                {
                    Id = recipe.Id,
                    Name = recipe.Name,
                    AmountOfServings = recipe.AmountOfServings,
                    Image = recipe.Image,
                })
            };
        }


        public Profile MapProfileRequestToProfile(ProfileRequest profileRequest)
        {
            return new Profile
            {
                Name = profileRequest.Name,
            };
        }

        public async Task<IEnumerable<ProfileResponse>> GetAllAsync()
        {
            IEnumerable<Profile> profiles = await _profileRepository.GetAllAsync();
            return profiles.Select(MapProfileToProfileResponse).ToList();
        }

        public async Task<ProfileResponse?> GetByIdAsync(Guid id)
        {
            var profile = await _profileRepository.GetByIdAsync(id);
            return profile is null ? null : MapProfileToProfileResponse(profile);
        }

        public async Task<ProfileResponse> AddAsync(ProfileRequest newProfileRequest)
        {
            var profile = MapProfileRequestToProfile(newProfileRequest);
            var insertedProfile = await _profileRepository.AddAsync(profile);
            return MapProfileToProfileResponse(insertedProfile);
        }

        public async Task<ProfileResponse> UpdateByIdAsync(Guid id, ProfileRequest updatedProfileRequest)
        {
            var profile = MapProfileRequestToProfile(updatedProfileRequest);
            var updatedProfile = await _profileRepository.UpdateByIdAsync(id, profile);
            return MapProfileToProfileResponse(updatedProfile);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            await _profileRepository.DeleteByIdAsync(id);
        }
    }
}
