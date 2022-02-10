using CleanCode.Srp.Models;
using CleanCode.Srp.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanCode.Srp
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
                        return await CalculateBasedOnDataFromLegacyApi(user);
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

        private static async Task<int> CalculateBasedOnDataFromLegacyApi(User user)
        {
            var data = await GetDataFromLegacyApi(user);
            return CalculateRating(data.PostsWritten, data.CommentsWritten);
        }

        private static async Task<UserActivityData> GetDataFromLegacyApi(User user)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {LegacyApiCredsProvider.GetBasicAuth()}");
            var response = await client.GetAsync("http://legacyapi.com/users/" + user.Id);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<UserActivityData>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                user.Type = UserType.Corrupted;
                throw new Exception("API call failed");
            }
        }

        private static int CalculateRating(int postsWritten, int commentsWritten)
        {
            return ((postsWritten / 5) + (commentsWritten / 30)) / 2;
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