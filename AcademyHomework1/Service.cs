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
                                            (user, posts) => new
                                            {
                                                Id = (int)user["id"],
                                                CreatedAt = (DateTime)user["createdAt"],
                                                Name = (string)user["name"],
                                                Avatar = (string)user["avatar"],
                                                Email = (string)user["email"],
                                                Posts = posts.ToList()
                                            });

                var usersJoinTodo = usersJoinPosts.GroupJoin(
                                            _JArrayTodos,
                                            user => user.Id,
                                            todo => (int)todo["userId"],
                                            (user, todos) => new User
                                            {
                                                Id = user.Id,
                                                CreatedAt = user.CreatedAt,
                                                Name = user.Name,
                                                Avatar = user.Avatar,
                                                Email = user.Email,
                                                Posts = user.Posts,
                                                Todos = new JArray(todos).ToObject<List<Todo>>().ToList()
                                            });


                return usersJoinTodo.ToList();

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
                return JArray.Parse(json);
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

        private IEnumerable<Post> GetPostsById(int id)
        {
            var posts = (from u in _users
                         where u.Id == id
                         select u.Posts).First();
            return posts;
        }

        public IEnumerable<(Post, int)> GetNumberOfCommentsById(int id)
        {
            var posts = GetPostsById(id);

            var postsWithNumber = from p in posts
                                  select (Post: p, Number: p.Comments.Count());

            return postsWithNumber;
        }

        public IEnumerable<Comment> GetCommentsWithSmallBodyById(int id)
        {
            var posts = GetPostsById(id);

            var allComments = new List<Comment>();
            
            foreach(var post in posts)
            {
                allComments.AddRange(post.Comments);
            }

            var smallComments = allComments.Where(c => c.Body.Length < 50);
            return smallComments;
        }
        public IEnumerable<(int, string)> GetCompletedTasksById(int id)
        {
            var todos = (from u in _users
                        where u.Id == id
                         select u.Todos).First();


            var completed = from todo in todos
                            where todo.IsComplete == true
                            select (Id: todo.Id, Name: todo.Name);

            return completed;
        }
    }
}
