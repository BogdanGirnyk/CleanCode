using CleanCode.Initial.Models;
using CleanCode.Initial.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CleanCode.Initial
{
	public static class UserRatingService
	{
		public static async Task<int> GetUserRating(User user, bool useCache = true)
		{
			if (user.Type == UserType.SuperUser)
			{
				// admin or moderator
				if (user.Id == "3aa857aa-77bf-4f8e-8545-3fffca0aa20d")
					return 9999;
				else if (user.Id == "bd1bf14d-aa05-4410-a0a1-6cbc604b1954")
					return 1000;
				else
					// error, unknown user type
					return -1;
			}
			else
			{
				// First, try get from cache if flag is true
				if (useCache)
				{
					var cacheSingleton = UserRatingCache.GetInstance();
					var entry = cacheSingleton.FindById(user.Id);
					return entry.Rating;
				}
				else
				{
					if (user.RegistrationDate > new DateTime(2015, 7, 20) 
						|| user.LastActivityDate > new DateTime(2015, 7, 20))
					{
						// For users active after 07.20.2015 calculate based on data from db 
						var sqlDbRepository = new SqlDbRepository();
						var data = sqlDbRepository.GetUserDataAsync(user.Id);
						return data.Rating;
					}
					else
					{
						// For old users get rating from legacy API
						var client = new HttpClient();
						client.DefaultRequestHeaders.Add("Authorization", $"Basic {LegacyApiCredsProvider.GetBasicAuth()}");
						var response = await client.GetAsync("http://legacyapi.com/users/" + user.Id);
						if (response.IsSuccessStatusCode)
						{
							var data = JsonConvert.DeserializeObject<UserActivityData>(await response.Content.ReadAsStringAsync());
							return ((data.PostsWritten / 5) + (data.CommentsWritten / 30)) / 2;
						}
						else
						{
							user.Type = UserType.Corrupted;
							// error
							return -1;
						}
					}
				}
			}
		}
	}
}