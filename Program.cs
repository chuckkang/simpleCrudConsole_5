using System;
using DbConnection;
using System.Collections.Generic;
namespace simplecrud_5sdf
{
    class Program
    {
        static void Main(string[] args)
        { // since this is a static method you can only call static methods within....
			User inputUser = new User();
            Console.WriteLine("List all users >>>>>>>>>>>>");
			PrintUsers(ReturnUsers());

			Console.WriteLine("Add User >>>>>>>>>>>>");
			inputUser = GetUserData(inputUser);
			CreateUser(inputUser);

			Console.WriteLine("Update user >>>>>>>>>>>>");
			int userid = GetUserId();
			inputUser = new User();
			inputUser = GetUserData(inputUser);
			UpdateUser(userid, inputUser);
			Console.WriteLine("User has been updated");
			PrintUsers(ReturnUsers());
			Console.WriteLine("");

			Console.WriteLine("Delete User:");
			int delUserId = GetUserId();
			DeleteUser(delUserId);
			Console.WriteLine("User has been deletedd");
			PrintUsers(ReturnUsers());


			// PrintUsers(ReturnUsers());
			// Console.WriteLine("Enter FirstName");
			// inputUser.FirstName = Console.ReadLine();
			// Console.Write(inputUser.FirstName + ": This is the first name");			
			// PrintUsers(ReturnUsers());
        }

		public void test(){
			Console.WriteLine("test");
		}

		public static void CreateUser(User newUser)
		{
			DbConnector.Execute($"INSERT INTO users (first_name, last_name, email) VALUES ('{newUser.FirstName}', '{newUser.LastName}', '{newUser.Email}')");
		}
		
		public static int GetUserId()
		{
			Console.WriteLine("Please Enter a User Id to Update:");
			int id;
			int.TryParse(Console.ReadLine(), out id);
			return id;
		}
		public static User GetUserData(User inputUser )
		{

			Console.WriteLine("Please Enter a First Name:");
			inputUser.FirstName = Console.ReadLine();

			Console.WriteLine("Please Enter a Last Name:");
			inputUser.LastName = Console.ReadLine();

			Console.WriteLine("Please Enter an Email:");
			inputUser.Email = Console.ReadLine();

			return inputUser;
		}

		public static void DeleteUser(int userid)
		{
			DbConnector.Execute($"Delete from Users WHERE userid = {userid}");
		}
		public static void UpdateUser(int userid, User updateUser)
		{
			String sql = $"UPDATE Users SET first_name = '{updateUser.FirstName}', last_name = '{updateUser.LastName}', email = '{updateUser.Email}' WHERE userid = {userid}";
			Console.WriteLine(sql + " sql stateent");
			DbConnector.Execute(sql);
		}
		public static List<User> ReturnUsers()
		{
			List<Dictionary<string, object>> users = new List<Dictionary<string, object>>();
			List<User> combined = new List<User>();
			users = DbConnector.Query($"Select * from Users");
			foreach (var item in users)
			{
				combined.Add(new User((string)item["first_name"], (string)item["last_name"], (string)item["email"]));
			}
			return combined;
		}

		public static void PrintUsers(List<User> users){
			for (int x=0; x < users.Count; x++) 
			{
				Console.WriteLine($"User: {users[x].FirstName} {users[x].LastName}");
			}
		}
    }


	class User
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }

		public User(){
			FirstName = string.Empty;
			LastName = string.Empty;
			Email = string.Empty;
		}
		public User(string first, string last, string email)
		{
			FirstName = first;
			LastName = last;
			Email = email;
		}

		public string UserData(){
			return $"FirstName: {FirstName}, LastName: {LastName}, Email: {Email}";
		}
	}
}
