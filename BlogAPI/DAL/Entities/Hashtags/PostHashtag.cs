﻿using BlogAPI.DAL.Entities.Posts;

namespace BlogAPI.DAL.Entities.Hashtags
{
    public class PostHashtag
    {
        public int PostId { get; set; }
        public Post Post { get; set; }

        public int HashtagId { get; set; }
        public Hashtag Hashtag { get; set; }
    }
}
