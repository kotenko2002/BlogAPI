﻿namespace BlogAPI.DAL.Entities.Hashtags
{
    public class Hashtag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PostHashtag> PostHashtags { get; set; }
    }
}
