﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaSharp.Models.Responses;

namespace InstaSharp.Endpoints
{
    public class Tags : InstaSharp.Endpoints.InstagramAPI {
        
        /// <summary>
        /// Tag Endpoints
        /// </summary>
        /// <param name="config">An instance of the InstagramConfig class</param>
        public Tags(InstagramConfig config)
            : base("/tags/", config) { }

        /// <summary>
        /// Get information about a tag object.
        /// </summary>
        /// <para>
        /// <c>Requires Authentication: False</c>
        /// </para>
        /// <param name="tagName">Return information about this tag.</param>
        /// <returns>TagResponse</returns>
        public TagResponse Get(string tagName) {
            return (TagResponse)Mapper.Map<TagResponse>(GetJson(tagName));
        }

        /// <summary>
        /// Get information about a tag object.
        /// </summary>
        /// <para>
        /// <c>Requires Authentication: False</c>
        /// </para>
        /// <param name="tagName">Return information about this tag.</param>
        /// <returns>String</returns>
        public string GetJson(string tagName) {
            string uri = string.Format(base.Uri + "{0}?client_id={1}", tagName, InstagramConfig.ClientId);
            return HttpClient.GET(uri);
        }

        /// <summary>
        /// Get a list of recently tagged media. Note that this media is ordered by when the media was tagged with this tag, rather than the order it was posted. Use the max_tag_id and min_tag_id parameters in the pagination response to paginate through these objects.
        /// </summary>
        /// <para>
        /// <c>Requires Authentication: False</c>
        /// </para>
        /// <param name="tagName">Return information about this tag.</param>
        /// <param name="min_id">Return media before this min_id. If you don't want to use this parameter, use null.</param>
        /// <param name="max_id">Return media after this max_id. If you don't want to use this parameter, use null.</param>
        /// <returns>MediasResponse</returns>
        public MediasResponse Recent(string tagName, string min_id = "", string max_id = "") {
            return (MediasResponse)Mapper.Map<MediasResponse>(RecentJson(tagName, min_id, max_id));
        }

        /// <summary>
        /// Get a list of recently tagged media. Note that this media is ordered by when the media was tagged with this tag, rather than the order it was posted. Use the max_tag_id and min_tag_id parameters in the pagination response to paginate through these objects.
        /// </summary>
        /// <para>
        /// <c>Requires Authentication: False</c>
        /// </para>
        /// <param name="tagName">Return information about this tag.</param>
        /// <param name="min_id">Return media before this min_id. If you don't want to use this parameter, use null.</param>
        /// <param name="max_id">Return media after this max_id. If you don't want to use this parameter, use null.</param>
        /// <returns>String</returns>
        private string RecentJson(string tagName, string min_id = "", string max_id = "") {
            var uri = base.FormatUri(string.Format("{0}/media/recent", tagName));
            if (!string.IsNullOrEmpty(min_id)) uri.AppendFormat("&min_tag_id={0}", min_id);
            if (!string.IsNullOrEmpty(max_id)) uri.AppendFormat("&max_tag_id={0}", max_id);

            return HttpClient.GET(uri.ToString());
        }

        /// <summary>
        /// Search for tags by name. Results are ordered first as an exact match, then by popularity. Short tags will be treated as exact matches.
        /// </summary>
        /// <para>
        /// <c>Requires Authentication: False</c>
        /// </para>
        /// <param name="searchTerm">A valid tag name without a leading #. (eg. snowy, nofilter)</param>
        /// <returns>TagsResponse</returns>
        public TagsResponse Search(string searchTerm) {
            return (TagsResponse)Mapper.Map<TagsResponse>(SearchJson(searchTerm));
        }

        /// <summary>
        /// Search for tags by name. Results are ordered first as an exact match, then by popularity. Short tags will be treated as exact matches.
        /// </summary>
        /// <para>
        /// <c>Requires Authentication: False</c>
        /// </para>
        /// <param name="searchTerm">A valid tag name without a leading #. (eg. snowy, nofilter)</param>
        /// <returns>String</returns>
        public string SearchJson(string searchTerm) {
            var uri = base.FormatUri(string.Format("/search/?q={0}", searchTerm));
            return HttpClient.GET(uri.ToString());
        }
    }
}