namespace _18Iterator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== 迭代器模式 (Iterator Pattern) ===\n");

            // 示例1：图书馆书籍遍历
            Console.WriteLine("示例1：图书馆书籍遍历");
            Console.WriteLine("------------------------");
            RunLibraryExample();

            Console.WriteLine("\n示例2：文件系统遍历");
            Console.WriteLine("------------------------");
            RunFileSystemExample();

            Console.WriteLine("\n示例3：社交网络好友遍历");
            Console.WriteLine("------------------------");
            RunSocialNetworkExample();
        }

        static void RunLibraryExample()
        {
            var library = new Library();
            library.AddBook(new Book("设计模式", "Gang of Four", 1994));
            library.AddBook(new Book("代码整洁之道", "Robert C. Martin", 2008));
            library.AddBook(new Book("重构", "Martin Fowler", 1999));
            library.AddBook(new Book("领域驱动设计", "Eric Evans", 2003));

            Console.WriteLine("正向遍历图书:");
            var iterator = library.CreateIterator();
            while (iterator.HasNext())
            {
                var book = iterator.Next();
                Console.WriteLine($"  {book}");
            }

            Console.WriteLine("\n反向遍历图书:");
            var reverseIterator = library.CreateReverseIterator();
            while (reverseIterator.HasNext())
            {
                var book = reverseIterator.Next();
                Console.WriteLine($"  {book}");
            }

            Console.WriteLine("\n按年份筛选遍历 (2000年后):");
            var filteredIterator = library.CreateFilteredIterator(b => b.Year >= 2000);
            while (filteredIterator.HasNext())
            {
                var book = filteredIterator.Next();
                Console.WriteLine($"  {book}");
            }
        }

        static void RunFileSystemExample()
        {
            // 构建文件系统树
            var root = new FileNode("root", true);
            var src = new FileNode("src", true);
            var docs = new FileNode("docs", true);
            
            root.Add(src);
            root.Add(docs);
            root.Add(new FileNode("README.md", false));
            
            src.Add(new FileNode("main.cs", false));
            src.Add(new FileNode("utils.cs", false));
            var models = new FileNode("models", true);
            src.Add(models);
            models.Add(new FileNode("user.cs", false));
            models.Add(new FileNode("product.cs", false));
            
            docs.Add(new FileNode("guide.pdf", false));
            docs.Add(new FileNode("api.html", false));

            Console.WriteLine("深度优先遍历:");
            var dfsIterator = new DepthFirstIterator(root);
            while (dfsIterator.HasNext())
            {
                var node = dfsIterator.Next();
                var indent = new string(' ', node.Level * 2);
                Console.WriteLine($"{indent}{(node.IsDirectory ? "[D]" : "[F]")} {node.Name}");
            }

            Console.WriteLine("\n广度优先遍历:");
            var bfsIterator = new BreadthFirstIterator(root);
            while (bfsIterator.HasNext())
            {
                var node = bfsIterator.Next();
                Console.WriteLine($"  Level {node.Level}: {node.Name}");
            }
        }

        static void RunSocialNetworkExample()
        {
            var network = new SocialNetwork();
            
            // 添加用户
            var alice = new User("Alice", "alice@email.com");
            var bob = new User("Bob", "bob@email.com");
            var charlie = new User("Charlie", "charlie@email.com");
            var diana = new User("Diana", "diana@email.com");
            var eve = new User("Eve", "eve@email.com");
            
            network.AddUser(alice);
            network.AddUser(bob);
            network.AddUser(charlie);
            network.AddUser(diana);
            network.AddUser(eve);
            
            // 建立好友关系
            alice.AddFriend(bob);
            alice.AddFriend(charlie);
            bob.AddFriend(diana);
            charlie.AddFriend(diana);
            charlie.AddFriend(eve);

            Console.WriteLine("Alice的好友:");
            var friendIterator = network.GetFriendsIterator("Alice");
            while (friendIterator.HasNext())
            {
                var friend = friendIterator.Next();
                Console.WriteLine($"  {friend.Name} ({friend.Email})");
            }

            Console.WriteLine("\nAlice的二度好友 (朋友的朋友):");
            var secondDegreeIterator = network.GetSecondDegreeFriendsIterator("Alice");
            while (secondDegreeIterator.HasNext())
            {
                var friend = secondDegreeIterator.Next();
                Console.WriteLine($"  {friend.Name} ({friend.Email})");
            }

            Console.WriteLine("\n所有用户:");
            var allUsersIterator = network.GetAllUsersIterator();
            while (allUsersIterator.HasNext())
            {
                var user = allUsersIterator.Next();
                Console.WriteLine($"  {user.Name} - 好友数: {user.GetFriendsCount()}");
            }
        }
    }
}