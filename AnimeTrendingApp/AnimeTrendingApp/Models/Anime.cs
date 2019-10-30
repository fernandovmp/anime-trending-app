using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeTrendingApp.Models
{
    public class Anime
    {
        public string CanonicalTitle { get; set; }
        public string Synopsis { get; set; }
        public int PopularityRank { get; set; }
        public int RatingRank { get; set; }
        public string Status { get; set; }
        public int EpisodeCount { get; set; }
        public PosterImage PosterImage { get; set; }
        public PosterImage CoverImage { get; set; }
    }
}
