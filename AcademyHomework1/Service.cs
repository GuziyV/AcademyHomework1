using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AcademyHomework1
{
    class Service
    {
        private HttpClient _client = new HttpClient();


        public List<User> _users
        {
            get
            {
                string json = _client.GetStringAsync("https://5b128555d50a5c0014ef1204.mockapi.io/users").Result;
                JArray _JArrayUsers = JArray.Parse(json);
                var postJoinComments = _JArrayPosts.GroupJoin(
                                                _JArrayComments,
                                                post => (int)post["id"],
                                                comment => (int)comment["postId"],
                                                (post, comments) => new Post
                                                {
                                                    Id = (int)post["id"],
                                                    CreatedAt = (DateTime)post["createdAt"],
                                                    Title = (string)post["title"],
                                                    Body = (string)post["body"],
                                                    Likes = (int)post["likes"],
                                                    UserId = (int)post["userId"],
                                                    Comments = new JArray(comments).ToObject<List<Comment>>().ToList()
                                                });


                var usersJoinPosts = _JArrayUsers.GroupJoin(
                                            postJoinComments,
                                            user => (int)user["id"],
                                            post => post.UserId,
                                            (user, posts) => new User
                                            {
                                                Id = (int)user["id"],
                                                CreatedAt = (DateTime)user["createdAt"],
                                                Name = (string)user["name"],
                                                Avatar = (string)user["avatar"],
                                                Email = (string)user["email"],
                                                Posts = posts.ToList()
                                            });


                return usersJoinPosts.ToList();

            }
        }

        private JArray _JArrayPosts
        {
            get
            {
                string json = _client.GetStringAsync("https://5b128555d50a5c0014ef1204.mockapi.io/posts").Result;
                return JArray.Parse(json);
            }
        }

        private JArray _JArrayTodos
        {
            get
            {
                string json = _client.GetStringAsync("https://5b128555d50a5c0014ef1204.mockapi.io/todos").Result;
                return JArray.Parse(json);;
            }
        }

        private JArray _JArrayComments
        {
            get
            {
                string json = _client.GetStringAsync("https://5b128555d50a5c0014ef1204.mockapi.io/comments").Result;
                return JArray.Parse(json); ;
            }
        }

        private List<Address> _addresses
        {
            get
            {
                string json = _client.GetStringAsync("https://5b128555d50a5c0014ef1204.mockapi.io/address").Result;
                return JsonConvert.DeserializeObject<List<Address>>(json);
            }
        }

        public Dictionary<Post, int> GetNumberOfCommentsById(int id)
        {
            var posts = (from u in _users
                         where u.Id == id
                         select u.Posts).First();

            var postsWithNumber = from p in posts
                                  select new
                                  {
                                      Post = p,
                                      Number = p.Comments.Count()
                                  };
            Dictionary<Post, int> res = new Dictionary<Post, int>();
            foreach(var p in postsWithNumber)
            {
                res[p.Post] = p.Number;
            }
            return res;

        }
    }
}
