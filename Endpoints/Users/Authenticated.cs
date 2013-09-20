using System;

using InstaSharp.Model.Responses;

namespace InstaSharp.Endpoints.Users {
    public class Authenticated : InstagramAPI {

        readonly Unauthenticated _unauthenticated;

        public Authenticated(InstagramConfig config, AuthInfo authInfo)
            : base(config, authInfo, "/users/") {
                _unauthenticated = new Unauthenticated(config);
        }

        public UserResponse Get() {
            return (UserResponse)Mapper.Map<UserResponse>(GetJson(AuthInfo.User.Id));
        }

        public UserResponse Get(int userId) {
            return (UserResponse)Mapper.Map<UserResponse>(GetJson(userId));
        }

        private string GetJson(int userId) {
            return _unauthenticated.GetJson(userId);
        }

        public MediasResponse Feed(string user) {
            return (MediasResponse)Mapper.Map<MediasResponse>(FeedJson(String.Empty, 0));
        }

        public MediasResponse Feed(string user, string maxId) {
            return (MediasResponse)Mapper.Map<MediasResponse>(FeedJson(maxId, 0)); 
        }

        public MediasResponse Feed(string user, int count) {
            return (MediasResponse)Mapper.Map<MediasResponse>(FeedJson(String.Empty, count));
        }

        public MediasResponse Feed(string user, string maxId, int count) {
            return (MediasResponse)Mapper.Map<MediasResponse>(FeedJson(maxId, count));
        }

        private string FeedJson(string maxId, int count) {
            string uri = String.Concat(base.Uri, "self/feed?access_token=", AuthInfo.Access_Token);

            if (!String.IsNullOrEmpty(maxId)) uri += "&max_id=" + maxId;
            if (count > 0) uri += "&count=" + count;

            return HttpClient.GET(uri);
        }

        public MediasResponse Recent() {
            return (MediasResponse)Mapper.Map<MediasResponse>(RecentJson("", "", 0, null, null, null));
        } 

        public MediasResponse Recent(string maxId) {
            return (MediasResponse)Mapper.Map<MediasResponse>(RecentJson(maxId, "", 0, null, null, null));
        }

		public MediasResponse Recent(string maxId, int count)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, "", count, null, null, null));
		}

		public MediasResponse Recent(string maxId, int count, int? userID)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, "", count, userID, null, null));
		}

		public MediasResponse Recent(string maxId, string minId, int count)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, minId, count, null, null, null));
		}

		public MediasResponse Recent(string maxId, string minId, int count, int? userID)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, minId, count, userID, null, null));
		}

		public MediasResponse Recent(string maxId, string minId, int count, DateTime minTimestamp, DateTime maxTimestamp)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, minId, count, null, minTimestamp, maxTimestamp));
		}

		public MediasResponse Recent(string maxId, string minId, int count, DateTime? minTimestamp, DateTime? maxTimestamp)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, minId, count, null, minTimestamp, maxTimestamp));
		}

		public MediasResponse Recent(string maxId, string minId, int count, int? userID, DateTime minTimestamp, DateTime maxTimestamp)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, minId, count, userID, minTimestamp, maxTimestamp));
		}

		public MediasResponse Recent(string maxId, string minId, int count, int? userID, DateTime? minTimestamp, DateTime? maxTimestamp)
		{
			return (MediasResponse) Mapper.Map<MediasResponse>(RecentJson(maxId, minId, count, userID, minTimestamp, maxTimestamp));
		}

		private string RecentJson(string maxId, string minId, int count, int? userID, DateTime? minTimestamp, DateTime? maxTimestamp)
		{
			string uri = String.Concat(base.Uri, userID ?? AuthInfo.User.Id, "/media/recent?access_token=", AuthInfo.Access_Token);

            if (!String.IsNullOrEmpty(maxId)) uri += "&max_id=" + maxId;
            if (!String.IsNullOrEmpty(minId)) uri += "&min_id=" + minId;
            if (count > 0) uri += "&count=" + count;
            if (minTimestamp.HasValue) uri += "&min_timestamp=" + minTimestamp;
            if (maxTimestamp.HasValue) uri += "&max_timestamp" + maxTimestamp;

            return HttpClient.GET(uri);
        }

        public MediasResponse Liked() {
            return (MediasResponse)Mapper.Map<MediasResponse>(LikedJson("", 0));
        }

        public MediasResponse Liked(string max_like_id) {
            return (MediasResponse)Mapper.Map<MediasResponse>(LikedJson(max_like_id, 0));
        }

        public MediasResponse Liked(string max_like_id, int count) {
            return (MediasResponse)Mapper.Map<MediasResponse>(LikedJson(max_like_id, count));
        }

        private string LikedJson(string max_like_id, int count) {
            string uri = String.Concat(base.Uri, "self/media/liked?access_token=", AuthInfo.Access_Token);

            if (String.IsNullOrEmpty(max_like_id)) uri += "&max_like_id=" + max_like_id;
            if (count > 0) uri += "&count=" + count;

            return HttpClient.GET(uri);
        }

        public UsersResponse Search(string searchTerm) {
            return (UsersResponse)Mapper.Map<UsersResponse>(SearchJson(searchTerm, 0));
        }

        public UsersResponse Search(string searchTerm, int count) {
            return (UsersResponse)Mapper.Map<UsersResponse>(SearchJson(searchTerm, count));
        }

        private string SearchJson(string searchTerm, int count) {
            return _unauthenticated.SearchJson(searchTerm, count);
        }
    }
}
