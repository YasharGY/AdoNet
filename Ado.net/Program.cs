

using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;

var conectionStr = "Server=DESKTOP-L7G1IEN\\SQLEXPRESS; Database=LIBRARY; Trusted_Connection=true";
SqlConnection conn = new SqlConnection(conectionStr);
//CreateUser("Nurlan", "Nuruzade", "+994555456687", "Genclik", "n@gmail.com");
//CreateBook("The Two Towers", "54321", 300);
//SearchBookByISBN("12345");
//SearchUserByPhone("+994558788861");
//SearchBookByTitle("gatsby");
CreateBorrowing(1, 1);



////var query = "SELECT * FROM Users";
////SqlCommand cmd = new SqlCommand(query,conn);
////conn.Open();
////SqlDataReader reader=cmd.ExecuteReader();
////Console.WriteLine(reader.HasRows);
////conn.Close();

void CreateUser(string name, string surname,string phone, string address, string email)
{
	string query = $"INSERT INTO Users (Name, Surname, Phone, Address, Mail)" +
	$"VALUES ('{name}','{surname}', '{phone}', '{address}', '{email}')";	;
    using(SqlConnection conn= new SqlConnection(conectionStr))
    {
		try
		{
			SqlCommand command = new SqlCommand(query,conn);
			conn.Open();
			int result = command.ExecuteNonQuery();
			if(result > 0) 
			{
				Console.WriteLine("User created");
			}
		}
		catch (Exception ex)
		{

			Console.WriteLine(ex.Message);
		}
		conn.Close();
		
    }
}

void CreateBook(string title, string isbn, int pageCount)
{
    string query = $"INSERT INTO Books (Title, ISBN, PageCount)" +
    $"VALUES ('{title}','{isbn}', '{pageCount}')"; ;
    using (SqlConnection conn = new SqlConnection(conectionStr))
    {
        try
        {
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            int result = command.ExecuteNonQuery();
            if (result > 0)
            {
                Console.WriteLine("Book created");
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        conn.Close();

    }
}

void SearchBookByISBN(string isbn)
{
    string query = $"SELECT * FROM Books WHERE ISBN = '{isbn}';";
    using (SqlConnection conn = new SqlConnection(conectionStr))
    {
        try
        {
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Book ID:'{reader["Id"]}',Title:'{reader["title"]},ISBN:{reader["isbn"]}");
            }
      
        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        conn.Close();

    }
}

void SearchBookByTitle(string title)
{
    if (title.Length < 2)
    {
        Console.WriteLine("Please enter at least 2 characters for book name");
        return;
    }
    string query = $"SELECT * FROM BOOKS WHERE TITLE LIKE '%{title}%'";
    using (SqlConnection conn = new SqlConnection(conectionStr))
    {
        try
        {
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"Book ID:'{reader["Id"]}',Title:'{reader["title"]},ISBN:{reader["isbn"]}");
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        conn.Close();

    }
}

void SearchUserByPhone(string phone)
{
    string query = $"SELECT * FROM Users WHERE PHONE = '{phone}';";
    using (SqlConnection conn = new SqlConnection(conectionStr))
    {
        try
        {
            SqlCommand command = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"User ID:'{reader["Id"]}',Name:'{reader["name"]},Surname:{reader["surname"]},Phone:{reader["phone"]},Mail:{reader["mail"]}");
            }

        }
        catch (Exception ex)
        {

            Console.WriteLine(ex.Message);
        }
        conn.Close();

    }
}

void CreateBorrowing(int userid, int bookid)
{
    string checkQuery = $"SELECT * FROM BORROWINGS WHERE BookId ={bookid} AND RETURNDATE IS NULL";

    using (SqlConnection conn= new SqlConnection(conectionStr))
    {
        try
        {
            SqlCommand checkCommand = new SqlCommand(checkQuery, conn);
            conn.Open();
            SqlDataReader reader = checkCommand.ExecuteReader();
            if (reader.HasRows)
            {
                Console.WriteLine("This book is already borrowed");
                return;  
            }
            reader.Close();
            string createQuery = $"INSERT INTO BORROWINGS (UserId,BookId,BorrowingDate)"+
                $"VALUES ({userid}, {bookid}, GETDATE())";
            SqlCommand createCommand = new SqlCommand(createQuery, conn);
            int result = createCommand.ExecuteNonQuery();
            if (result > 0) 
            {
                Console.WriteLine("Borrowing Created");            
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
          
        }
    }
}