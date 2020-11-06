using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BPCPrototypeRest.Model;
using BPCPrototypeRest.Persistency;
using BPCPrototypeRest.Utilities;

namespace BPCPrototypeRest.Managers
{
    public class CustomerManager
    {
        #region Connection string
        private const string connString = "Server=tcp:bpcserver.database.windows.net,1433;Initial Catalog=bpcdb;Persist Security Info=False;User ID=bpcadm;Password=Philipersej123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
		#endregion

		#region Instance fields
		private RestWorker restWorker = new RestWorker();
        private string _errorMessage;
        private bool _loginSuccess = false;
        private SharedUser _shared;
        private RelayCommand _tryLogin;

		#endregion

		#region Constructors
		//public HomePageLogin()
  //      {
  //          _shared = SharedUser.Instance;
  //          _tryLogin = new RelayCommand(CheckUserInfo, null);
  //      }


		#endregion

        #region Properties

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                //OnPropertyChanged();
            }
        }

        public SharedUser Shared
        {
            get { return _shared; }
        }
        #endregion

		#region GetAllCustomers
		public IList<Customer> GetAllCustomers()
		{
			List<Customer> customerList = new List<Customer>();

			using (SqlConnection conn = new SqlConnection(connString))
			{
				conn.Open();

				using (SqlCommand command = new SqlCommand("Select * from Customer", conn))
				{
					SqlDataReader reader = command.ExecuteReader();
					while (reader.Read())
					{
						customerList.Add(ReadNextCustomer(reader));
					}
				}
			}
			return customerList;
		}
		#endregion

		#region GetCustomerFromName
		public Customer GetCustomerFromId(int id)
		{
			Customer customer = new Customer();

			using (SqlConnection conn = new SqlConnection(connString))
			{
				conn.Open();

				using (SqlCommand command = new SqlCommand("Select * from Customer where CvrNo = @id", conn))
				{
					command.Parameters.AddWithValue("@id", id);
					SqlDataReader reader = command.ExecuteReader();
					if (reader.Read())
					{
						customer = ReadNextCustomer(reader);
					}
				}
			}
			return customer;
		}
		#endregion

		#region CreateCustomer
		public bool CreateCustomer(Customer customer)
		{
			bool created = false;

			using (SqlConnection conn = new SqlConnection(connString))
			{
				conn.Open();

				using (SqlCommand command = new SqlCommand("Insert into Customer (CompanyName, CvrNo, EMail, TelephoneNo, MobileNo, Address, PostalCode, City, Country, Password, TruckdriverId) " +
																		"values(@CompanyName, @CvrNo, @EMail, @TelephoneNo, @MobileNo, @Address, @PostalCode, @City, @Country, @Password, @Truckdriver)", conn))
				{
					command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
					command.Parameters.AddWithValue("@CvrNo", customer.CvrNo);
					command.Parameters.AddWithValue("@EMail", customer.EMail);
					command.Parameters.AddWithValue("@TelephoneNo", customer.TelephoneNo);
					command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
					command.Parameters.AddWithValue("@Address", customer.Address);
					command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
					command.Parameters.AddWithValue("@City", customer.City);
					command.Parameters.AddWithValue("@Country", customer.Country);
					command.Parameters.AddWithValue("@Password", customer.Password);
					command.Parameters.AddWithValue("@Truckdriver", customer.TruckdriverId);

					int rows = command.ExecuteNonQuery();
					created = rows == 1;
				}
			}
			return created;
		}
		#endregion

		#region UpdateCustomer
		public bool UpdateCustomer(Customer customer, int id)
		{
			bool updated = false;

			using (SqlConnection conn = new SqlConnection(connString))
			{
				conn.Open();

				using (SqlCommand command = new SqlCommand("Update Customer set City = @City, EMail = @EMail, TelephoneNo = @TelephoneNo, " +
					"									MobileNo = @MobileNo, Address = @Address, PostalCode = @PostalCode, Country = @Country, " +
					"									Password = @Password, TruckdriverId = @Truckdriver, CompanyName = @CompanyName where CvrNo = @Id", conn))
				{
					command.Parameters.AddWithValue("@CompanyName", customer.CompanyName);
					command.Parameters.AddWithValue("@EMail", customer.EMail);
					command.Parameters.AddWithValue("@TelephoneNo", customer.TelephoneNo);
					command.Parameters.AddWithValue("@MobileNo", customer.MobileNo);
					command.Parameters.AddWithValue("@Address", customer.Address);
					command.Parameters.AddWithValue("@PostalCode", customer.PostalCode);
					command.Parameters.AddWithValue("@City", customer.City);
					command.Parameters.AddWithValue("@Country", customer.Country);
					command.Parameters.AddWithValue("@Password", customer.Password);
					command.Parameters.AddWithValue("@Truckdriver", customer.TruckdriverId);
					command.Parameters.AddWithValue("@Id", id);

					int rows = command.ExecuteNonQuery();
					updated = rows == 1;
				}
			}
			return updated;
		}
		#endregion

		#region DeleteCustomer
		public Customer DeleteCustomer(int id)
		{
			Customer customer = GetCustomerFromId(id);

			using (SqlConnection conn = new SqlConnection(connString))
			{
				conn.Open();

				using (SqlCommand command = new SqlCommand("Delete from Customer where CvrNo = @Id", conn))
				{
					command.Parameters.AddWithValue("@Id", id);
					command.ExecuteNonQuery();
				}
			}
			return customer;
		}
		#endregion

        #region ReadNextCustomer
        private Customer ReadNextCustomer(SqlDataReader reader)
		{
			Customer customer = new Customer();

			customer.CompanyName = reader.GetString(0);
			customer.CvrNo = reader.GetInt32(1);
			customer.EMail = reader.GetString(2);
			customer.TelephoneNo = reader.GetString(3);
			customer.MobileNo = reader.GetString(4);
			customer.Address = reader.GetString(5);
			customer.PostalCode = reader.GetString(6);
			customer.City = reader.GetString(7);
			customer.Country = reader.GetString(8);
			customer.Password = reader.GetString(9);
			customer.TruckdriverId = reader.GetInt32(10);

			return customer;
		}
		#endregion

        #region Login Logic

        // Reads through a car list from the DB, through REST worker and checks if information is similar to input
        //public async void CheckUserInfoCar()
        //{
        //    List<Car> carList = (List<Car>)await restWorker.GetAllObjectsAsync<Car>(tableName: Datastructures.TableName.Car);
        //    foreach (var car in carList)
        //    {
        //        if (_shared.UserUser == car.CvrNo)
        //        {
        //            if (_shared.UserPass.Equals(car.Password))
        //            {
        //                _loginSuccess = true;
        //                _navigation.Navigate(typeof(View.DisplayBookingCar));
        //            }
        //        }
        //    }
        //}

        private async void CheckUserInfoCustomer()
        {
            Customer customer = await restWorker.GetObjectFromIdAsync<Customer>(Shared.UserUser, Datastructures.TableName.Customer);
            if (Shared.UserPass.Equals(customer.Password))
            {
                _loginSuccess = true;
            }
        }

        //uskønt admin hack
        private void AdminLogin(int name, string psw)
        {
            if (Shared.UserUser == name && Shared.UserPass.Equals(psw));

        }


        // Method runs both aforementioned methods and is stored in a RelayCommand which is bound to the login button
        public void CheckUserInfo()
        {
            AdminLogin(2020, "Admin");

            //CheckUserInfoCar();
            CheckUserInfoCustomer();
            if (!_loginSuccess)
                ErrorMessage = "Fejl i login.";
        }
        #endregion


		
	}
}

