using CleanCode.ExtractMethods.Models;
using CleanCode.ExtractMethods.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanCode.ExtractMethods
{
	public static class UserRatingService
	{
		public static async Task<int> GetUserRating(User user, bool useCache = true)
		{
			if (user.Type == UserType.SuperUser)
            {
                return GetSuperUserRating(user);
            }
            else
			{
				if (useCache)
                {
                    return GetRatingFromCache(user);
                }
                else
                {
					if (user.RegistrationDate > new DateTime(2015, 7, 20) 
						|| user.LastActivityDate > new DateTime(2015, 7, 20))
                    {
                        return GetRatingFromDb(user);
                    }
                    else
                    {
                        return await GetRatingFromLegacyApi(user);
                    }
                }
            }
		}

        private static int GetSuperUserRating(User user)
        {
            if (user.Id == "3aa857aa-77bf-4f8e-8545-3fffca0aa20d")
                return 9999;
            else if (user.Id == "bd1bf14d-aa05-4410-a0a1-6cbc604b1954")
                return 1000;
            else
                // error, unknown user type
                return -1;
        }

        private static async Task<int> GetRatingFromLegacyApi(User user)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {LegacyApiCredsProvider.GetBasicAuth()}");
            var response = await client.GetAsync("http://legacyapi.com/users/" + user.Id);
            if (response.IsSuccessStatusCode)
            {
                var data = JsonConvert
                    .DeserializeObject<UserActivityData>(await response.Content.ReadAsStringAsync());
                return ((data.PostsWritten / 5) + (data.CommentsWritten / 30)) / 2;
            }
            else
            {
                user.Type = UserType.Corrupted;
                // error
                return -1;
            }
        }

        private static int GetRatingFromCache(User user)
        {
            var cacheSingleton = UserRatingCache.GetInstance();
            var entry = cacheSingleton.FindById(user.Id);
            return entry.Rating;
        }

        private static int GetRatingFromDb(User user)
        {
            var sqlDbRepository = new SqlDbRepository();
            var data = sqlDbRepository.GetUserActivityDataAsync(user.Id);
            return data.Rating;
        }
    }
}