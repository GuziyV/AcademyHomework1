using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AcademyHomework1
{
    class Service
    {
        private HttpClient _client = new HttpClient();

        static public List<User> Users;

        public Service()
        {
            string json = _client.GetStringAsync("https://5b128555d50a5c0014ef1204.mockapi.io/users").Result;
            JArray _JArrayUsers = JArray.Parse(json);
            var postsWithComments = GetAllPosts();

            var usersJoinPosts = _JArrayUsers.GroupJoin(
                postsWithComments,
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
            Users = usersJoinTodo.ToList();
        }

        private IEnumerable<Post> GetAllPosts()
        {
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
            return postJoinComments;
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

        private IEnumerable<Post> GetPostsById(int id)
        {
            var posts = (from u in Users
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

            var smallComments = posts.SelectMany(post => post.Comments).Where(comment => comment.Body.Length < 50);

            return smallComments;
        }
        public IEnumerable<(int, string)> GetCompletedTasksById(int id)
        {
            var todos = (from u in Users
                         where u.Id == id
                         select u.Todos).First();


            var completed = from todo in todos
                            where todo.IsComplete == true
                            select (Id: todo.Id, Name: todo.Name);

            return completed;
        }

        public IEnumerable<User> GetSortedUsers()
        {
            var sortedUsers = Users.GroupJoin(Users.SelectMany(u => u.Todos)
                .OrderByDescending(todo => todo.Name.Length),
                user => user.Id, todo => todo.UserId,
                (user, todos) => new User
                {
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    Avatar = user.Avatar,
                    Email = user.Email,
                    Name = user.Name,
                    Posts = user.Posts,
                    Todos = todos.ToList(),

                }).OrderBy(user => user.Name);
            return sortedUsers;
        }

        public (User, Post, int?, int, Post, Post) GetFirstStructure(int id)
        {
            var user = Users.Where(u => u.Id == id).First();

            var lastPost = user.Posts.OrderByDescending(p => p.CreatedAt).FirstOrDefault();

            int? numberOfComments = lastPost?.Comments.Count();

            int numberOfNotDone = user.Todos.Where(todo => todo.IsComplete == false).Count();

            if (lastPost == null)
            {
                Post mostPopularComments = null;
                Post mostPopularLikes = null;

                return (user, lastPost, numberOfComments, numberOfNotDone, mostPopularComments, mostPopularLikes);
            }
            else
            {
                var mostPopularComments = user.Posts.
                    OrderByDescending(post => post.Comments.Where(comment => comment.Body.Length > 80).Count()).First();

                var mostPopularLikes = user.Posts.OrderByDescending(post => post.Likes).First();
                return (user, lastPost, numberOfComments, numberOfNotDone, mostPopularComments, mostPopularLikes);
            }
        }

        public (Post, Comment, Comment, int?) GetSecondStructure(int id)
        {
            var post = Users.SelectMany(u => u.Posts).Where(p => p.Id == id).First();

            var theLongestComment = post.Comments.OrderByDescending(comment => comment.Body.Length).First();

            var theLikestComment = post.Comments.OrderByDescending(comment => comment.Likes).First();

            int? numberOfComments = post?.Comments.Where(comment => (comment.Likes == 0 || comment.Body.Length > 80))
                .Count();

            return (post, theLongestComment, theLikestComment, numberOfComments);

        }
    }
}
